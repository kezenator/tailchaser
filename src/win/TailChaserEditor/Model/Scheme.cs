using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.TailChaser.Editor.Model
{
    public delegate void OnSchemeChangedDelegate(Scheme s);
    public delegate void OnSchemeLayerListChangedDelegate(Scheme s, int index, Layer layer, bool added);

    public class Scheme
    {
        public Scheme(Palette palette)
        {
            m_Palette = palette;
            m_Name = "";
            m_Description = "";
            m_Layers = new List<Layer>();
        }

        public event OnSchemeChangedDelegate OnNameChanged;
        public event OnSchemeChangedDelegate OnDescriptionChanged;
        public event OnSchemeLayerListChangedDelegate OnLayerListChanged;

        public Palette Palette
        {
            get
            {
                return m_Palette;
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

                    if (OnNameChanged != null)
                        OnNameChanged(this);
                }
            }
        }

        public string Description
        {
            get
            {
                return m_Description;
            }

            set
            {
                if (!m_Description.Equals(value))
                {
                    m_Description = value;

                    if (OnDescriptionChanged != null)
                        OnDescriptionChanged(this);
                }
            }
        }

        public int IndexOf(Layer l)
        {
            int result = -1;

            if (l.Scheme == this)
            {
                result = m_Layers.IndexOf(l);
            }

            return result;
        }

        public Layer AddLayer()
        {
            Layer layer = new Layer(this);

            m_Layers.Add(layer);

            if (OnLayerListChanged != null)
                OnLayerListChanged(this, IndexOf(layer), layer, true);

            return layer;
        }

        public int NumLayers
        {
            get
            {
                return m_Layers.Count;
            }
        }

        public IEnumerable<Layer> Layers
        {
            get
            {
                return new List<Layer>(m_Layers);
            }
        }

        private Palette m_Palette;
        private string m_Name;
        private string m_Description;
        private List<Layer> m_Layers;
    }
}
