using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.TailChaser.Editor.UI.Controls
{
    public delegate void UndoRedoAvailableChangedDelegate(UndoRedoBuffer source);

    public partial class UndoRedoBuffer : Component
    {
        public UndoRedoBuffer()
        {
            m_UndoEntries = new Stack<Entry>();
            m_RedoEntries = new Stack<Entry>();
            m_UndoAvailable = false;
            m_RedoAvailable = false;

            InitializeComponent();
        }

        public UndoRedoBuffer(IContainer container)
        {
            m_UndoEntries = new Stack<Entry>();
            m_RedoEntries = new Stack<Entry>();
            m_UndoAvailable = false;
            m_RedoAvailable = false;

            container.Add(this);

            InitializeComponent();
        }

        public abstract class Entry
        {
            protected Entry(UndoRedoBuffer parent)
            {
                m_Parent = parent;
                m_CanBeModified = true;
            }

            public void PerformAndPushUndo()
            {
                Redo();

                if (m_Parent != null)
                {
                    m_Parent.PushUndo(this);
                }
            }

            protected bool CanBeModified()
            {
                return m_CanBeModified;
            }

            public abstract void Undo();
            public abstract void Redo();

            private UndoRedoBuffer m_Parent;
            private bool m_CanBeModified;
        }

        public event UndoRedoAvailableChangedDelegate OnUndoAvailableChanged;
        public event UndoRedoAvailableChangedDelegate OnRedoAvailableChanged;

        public bool UndoAvailable
        {
            get
            {
                return m_UndoAvailable;
            }
        }

        public bool RedoAvailable
        {
            get
            {
                return m_RedoAvailable;
            }
        }

        public void Undo()
        {
            if (m_UndoEntries.Count > 0)
            {
                Entry e = m_UndoEntries.Pop();
                m_RedoEntries.Push(e);

                e.Undo();

                UpdateAvailability();
            }
        }

        public void Redo()
        {
            if (m_RedoEntries.Count > 0)
            {
                Entry e = m_RedoEntries.Pop();
                m_UndoEntries.Push(e);

                e.Redo();

                UpdateAvailability();
            }
        }

        private void PushUndo(Entry entry)
        {
            m_UndoEntries.Push(entry);
            m_RedoEntries.Clear();

            UpdateAvailability();
        }

        private void UpdateAvailability()
        {
            bool new_undo_available = m_UndoEntries.Count != 0;
            bool new_redo_available = m_RedoEntries.Count != 0;

            if (m_UndoAvailable != new_undo_available)
            {
                m_UndoAvailable = new_undo_available;

                if (OnUndoAvailableChanged != null)
                {
                    OnUndoAvailableChanged(this);
                }
            }

            if (m_RedoAvailable != new_redo_available)
            {
                m_RedoAvailable = new_redo_available;

                if (OnRedoAvailableChanged != null)
                {
                    OnRedoAvailableChanged(this);
                }
            }
        }

        private Stack<Entry> m_UndoEntries;
        private Stack<Entry> m_RedoEntries;
        private bool m_UndoAvailable;
        private bool m_RedoAvailable;
    }
}
