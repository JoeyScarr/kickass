using System;
using System.Windows.Forms;
using KFA.Evidence;

namespace KFA.GUI {
    public partial class CaseInfo : Form {
        private Case m_Case;

        public CaseInfo(Case c) {
            InitializeComponent();
            m_Case = c;

            LoadCase();
        }

        private void LoadCase() {
            txtCaseName.Text = m_Case.Name;
            txtCaseID.Text = m_Case.CaseID;
            txtSuspectName.Text = m_Case.Suspect;
            txtSeizureNotes.Text = m_Case.SeizureNotes;
            foreach (String investigator in m_Case.Investigators) {
                listInvestigators.Items.Add(investigator);
            }
            txtNotes.Text = m_Case.Notes;
        }

        private void SaveCase() {
            m_Case.Name = txtCaseName.Text;
            m_Case.CaseID = txtCaseID.Text;
            m_Case.Suspect = txtSuspectName.Text;
            m_Case.SeizureNotes = txtSeizureNotes.Text;
            m_Case.Investigators.Clear();
            foreach (var investigator in listInvestigators.Items) {
                m_Case.Investigators.Add(investigator.ToString());
            }
            m_Case.Notes = txtNotes.Text;
            m_Case.Save();
        }

        private void button4_Click(object sender, EventArgs e) {
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e) {
            SaveCase();
            this.Dispose();
        }

        private void btnGenerate_Click(object sender, EventArgs e) {
            txtCaseID.Text = "CASE" + Guid.NewGuid().ToString().Substring(0, 4);
        }

        private void btnLog_Click(object sender, EventArgs e) {
            Form form = new LogForm(m_Case.Actions);
            form.Show();
        }

        private void btnAddInvestigator_Click(object sender, EventArgs e) {
            String n = InputBox.Show("Investigator Name");
            if (!String.IsNullOrEmpty(n)) {
                listInvestigators.Items.Add(n);
            }
        }

        private void btnRemoveInvestigator_Click(object sender, EventArgs e) {
            if (listInvestigators.SelectedItem != null) {
                listInvestigators.Items.Remove(listInvestigators.SelectedItem);
            }
        }
    }
}
