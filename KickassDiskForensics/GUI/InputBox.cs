using System;
using System.Windows.Forms;

namespace KFA.GUI {
    public partial class InputBox : Form {
        static string val;

        public static String Show(String title) {
            val = "";
            InputBox ib = new InputBox();
            ib.Text = title;
            ib.ShowDialog();

            return val;
        }

        private InputBox() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Dispose();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e) {
            val = textBox1.Text;
            if (e.KeyCode == Keys.Enter) {
                this.Dispose();
            }
        }
    }
}
