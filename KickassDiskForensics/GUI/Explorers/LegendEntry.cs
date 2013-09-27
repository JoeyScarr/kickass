using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KFA.GUI.Explorers {
    public partial class LegendEntry : UserControl {
        public LegendEntry() {
            InitializeComponent();
        }
        public LegendEntry(Color color, string label)
            : this() {
            pbColor.BackColor = color;
            label1.Text = label;
        }
    }
}
