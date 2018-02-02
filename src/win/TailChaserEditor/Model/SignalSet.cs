using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.TailChaser.Editor.Model
{
    public struct SignalSet
    {
        public byte GetMaskAsByte()
        {
            return (byte)m_Mask;
        }

        public byte GetValueAsByte()
        {
            return (byte)m_Value;
        }

        public bool IsSet(SignalMask mask)
        {
            return ((m_Mask & mask) == mask)
                && ((m_Value & mask) == mask);
        }

        public bool IsReset(SignalMask mask)
        {
            return ((m_Mask & mask) == mask)
                && ((m_Value & mask) == 0);
        }

        public bool IsExcluded(SignalMask mask)
        {
            return (m_Mask & mask) == 0;
        }

        public void SetSet(SignalMask mask)
        {
            m_Mask |= mask;
            m_Value |= mask;
        }

        public void SetReset(SignalMask mask)
        {
            m_Mask |= mask;
            m_Value &= (SignalMask)~mask;
        }

        public void SetExcluded(SignalMask mask)
        {
            m_Mask &= (SignalMask)~mask;
            m_Value &= (SignalMask)~mask;
        }

        public bool ConditionIsMatchedByState(SignalSet other)
        {
            return (((m_Mask & other.m_Mask) == m_Mask)
                && ((m_Mask & other.m_Value) == m_Value));
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is SignalSet))
                return false;

            SignalSet other = (SignalSet)obj;

            return (m_Mask == other.m_Mask)
                && (m_Value == other.m_Value);
        }

        public override int GetHashCode()
        {
            return m_Mask.GetHashCode() | (m_Value.GetHashCode() << 8)
                | (m_Mask.GetHashCode() << 16) | (m_Value.GetHashCode() << 24);
        }

        public override string ToString()
        {
            if (m_Mask == 0)
                return "Always";

            StringBuilder sb = new StringBuilder();
            bool first = true;

            foreach (KeyValuePair<SignalMask, string> entry in mask_names)
            {
                if (IsExcluded(entry.Key))
                {
                    // Ignore
                }
                else
                {
                    if (first)
                    {
                        first = false;
                        sb.Append("if ");
                    }
                    else
                    {
                        sb.Append(", ");
                    }

                    if (IsReset(entry.Key))
                        sb.Append("not ");

                    sb.Append(entry.Value);
                }
            }

            return sb.ToString();
        }

        private SignalMask m_Mask;
        private SignalMask m_Value;

        private static Dictionary<SignalMask, string> mask_names = new Dictionary<SignalMask, string>
        {
            { SignalMask.Tail,              "Tail" },
            { SignalMask.Brake,             "Brake" },
            { SignalMask.Reverse,           "Reverse" },
            { SignalMask.IndicatorFlash,    "Indicator (Flash)" },
            { SignalMask.IndicatorSolid,    "Indicator (Solid)" },
        };
    }
}
