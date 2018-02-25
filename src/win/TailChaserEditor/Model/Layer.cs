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
            m_Pattern = Pattern.Solid;
            m_Field1 = 0;
            m_Field2 = 0;
            m_Field3 = 0;
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
        
        public Pattern Pattern
        {
            get
            {
                return m_Pattern;
            }

            set
            {
                if (!m_Pattern.Equals(value))
                {
                    m_Pattern = value;

                    if (OnChange != null)
                        OnChange(this);
                }
            }
        }

        public UInt16 Field1
        {
            get
            {
                return m_Field1;
            }

            set
            {
                if (!m_Field1.Equals(value))
                {
                    m_Field1 = value;

                    if (OnChange != null)
                        OnChange(this);
                }
            }
        }

        public UInt16 Field2
        {
            get
            {
                return m_Field2;
            }

            set
            {
                if (!m_Field2.Equals(value))
                {
                    m_Field2 = value;

                    if (OnChange != null)
                        OnChange(this);
                }
            }
        }

        public UInt16 Field3
        {
            get
            {
                return m_Field3;
            }

            set
            {
                if (!m_Field3.Equals(value))
                {
                    m_Field3 = value;

                    if (OnChange != null)
                        OnChange(this);
                }
            }
        }

        private Scheme m_Scheme;
        private string m_Name;
        private SignalSet m_SignalSet;
        private Bitmap m_Bitmap;
        private Pattern m_Pattern;
        private UInt16 m_Field1;
        private UInt16 m_Field2;
        private UInt16 m_Field3;
    }
}
