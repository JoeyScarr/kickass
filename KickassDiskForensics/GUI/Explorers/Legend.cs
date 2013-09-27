using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KFA.GUI.Explorers {
    public partial class Legend : UserControl {
        public Legend() {
            InitializeComponent();
        }

        public void SetData(Dictionary<string, Color> entries) {
            flowLayoutPanel1.Controls.Clear();
            foreach (KeyValuePair<string, Color> pair in entries) {
                flowLayoutPanel1.Controls.Add(new LegendEntry(pair.Value, pair.Key));
            }
        }
    }
}
