using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.TailChaser.Editor.Model
{
    public delegate void OnLayerChangeDelegate(Layer layer);

    public class Layer
    {
        public Layer(Scheme scheme)
        {
            m_Scheme = scheme;
            m_Name = "";
            m_Bitmap = new Bitmap(scheme.Palette);
        }

        public event OnLayerChangeDelegate OnChange;

        public Scheme Scheme
        {
            get
            {
                return m_Scheme;
            }
        }

        public int Index
        {
            get
            {
                return m_Scheme.IndexOf(this);
            }
        }

        public string Name
        {
            get
            {
                return m_Name;
            }

            set
            {
                if (!m_Name.Equals(value))
                {
                    m_Name = value;

                    if (OnChange != null)
                        OnChange(this);
                }
            }
        }

        public SignalSet SignalSet
        {
            get
            {
                return m_SignalSet;
            }

            set
            {
                if (!m_SignalSet.Equals(value))
                {
                    m_SignalSet = value;

                    if (OnChange != null)
                        OnChange(this);
                }
            }
        }

        public Bitmap Bitmap
        {
            get
            {
                return m_Bitmap;
            }
        }

        private Scheme m_Scheme;
        private string m_Name;
        private SignalSet m_SignalSet;
        private Bitmap m_Bitmap;
    }
}
