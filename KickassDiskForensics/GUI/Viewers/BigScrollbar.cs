using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace KFA.GUI.Viewers {
    public partial class BigScrollbar : UserControl {
        public delegate void BigScrollEventHandler(object sender, BigScrollEventArgs e);

        public event BigScrollEventHandler BigScroll;

        public BigScrollbar() {
            InitializeComponent();
            vScrollBar.Scroll += new ScrollEventHandler(vScrollBar_Scroll);
        }

        void vScrollBar_Scroll(object sender, ScrollEventArgs e) {
            long OldValue = Value;
            if (e.Type == ScrollEventType.EndScroll) {
                Value = m_Value;
            } else if (e.NewValue == vScrollBar.Minimum) {
                Value = Minimum;
            } else if (e.NewValue == vScrollBar.Maximum) {
                Value = Maximum;
            } else if (e.Type == ScrollEventType.SmallDecrement) {
                Value -= SmallChange;
            } else if (e.Type == ScrollEventType.SmallIncrement) {
                Value += SmallChange;
            } else if (e.Type == ScrollEventType.LargeDecrement) {
                Value -= LargeChange;
            } else if (e.Type == ScrollEventType.LargeIncrement) {
                Value += LargeChange;
            } else {
                Value = (long)e.NewValue * (Maximum - Minimum) / (long)(vScrollBar.Maximum - vScrollBar.Minimum);
            }
            long NewValue = Value;
            if (BigScroll != null) {
                BigScroll(this, new BigScrollEventArgs(OldValue, NewValue, e.Type));
            }
        }

        private long m_Max = 100, m_Min = 0, m_SmallChange = 1, m_LargeChange = 10, m_Value = 0;
        [DefaultValue(100)]
        public long Maximum {
            get { return m_Max; }
            set {
                m_Max = value;
                updateInternalScrollbar();
            }
        }
        [DefaultValue(0)]
        public long Minimum {
            get { return m_Min; }
            set {
                m_Min = 0;
                updateInternalScrollbar();
            }
        }
        [DefaultValue(1)]
        public long SmallChange {
            get { return m_SmallChange; }
            set {
                m_SmallChange = value;
                updateInternalScrollbar();
            }
        }
        [DefaultValue(10)]
        public long LargeChange {
            get { return m_LargeChange; }
            set {
                m_LargeChange = value;
                updateInternalScrollbar();
            }
        }
        [DefaultValue(0)]
        public long Value {
            get { return m_Value; }
            set {
                m_Value = Math.Min(m_Max, Math.Max(m_Min, value));
                updateInternalScrollbar();
            }
        }

        private void updateInternalScrollbar() {
            m_Value = Math.Max(Minimum, Math.Min(Maximum, Value));
            int min, max, small, large, value;
            min = (int)m_Min;
            max = (int)Math.Min((long)short.MaxValue/*vScrollBar.Height - 20*/, m_Max);
            small = Math.Max(1, (int)(m_SmallChange * (max - min) / (m_Max - m_Min)));
            large = Math.Max(1, (int)(m_LargeChange * (max - min) / (m_Max - m_Min)));
            value = (int)(m_Value * (max - min) / (m_Max - m_Min));

            vScrollBar.Minimum = min;
            vScrollBar.Maximum = max;
            vScrollBar.Value = value;
        }
    }
    public class BigScrollEventArgs : EventArgs {
        public long OldValue { get; private set; }
        public long NewValue { get; private set; }
        public ScrollEventType Type { get; private set; }
        public BigScrollEventArgs(long oldval, long newval, ScrollEventType type) {
            OldValue = oldval;
            NewValue = newval;
            Type = type;
        }
    }
}
