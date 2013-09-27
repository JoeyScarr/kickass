using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using KFA.Evidence;

namespace KFA.GUI {
    public partial class LogForm : Form {
        public LogForm(List<CaseAction> actions) {
            InitializeComponent();

            DataTable dt = new DataTable();
            dt.Columns.Add("Date/Time");
            dt.Columns.Add("Type");
            dt.Columns.Add("Message");
            foreach (CaseAction action in actions) {
                DataRow dr = dt.NewRow();
                dr[0] = action.Time.ToShortDateString() + " " + action.Time.ToShortTimeString();
                dr[1] = action.Type;
                dr[2] = action.Message;
                dt.Rows.Add(dr);
            }

            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Width = 130;
            dataGridView1.Columns[2].Width = 400;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            foreach (DataGridViewRow row in dataGridView1.Rows) {
                if (row.Cells[1].Value.ToString().Contains("Stream")) {
                    row.Visible = !checkBox1.Checked;
                }
            }
        }
    }
}
