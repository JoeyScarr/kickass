using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace KFA.GUI {
    public partial class SplashScreen : Form {
        List<KeyValuePair<String, Action>> m_actions;
        Thread worker;

        public SplashScreen(List<KeyValuePair<String, Action>> actions) {
            InitializeComponent();
            m_actions = actions;
        }

        private void SplashScreen_Load(object sender, EventArgs e) {
            this.CenterToScreen();
            this.BringToFront();
            lblTitle.Text = String.Format("{0} {1}", Application.ProductName, Application.ProductVersion);
            lblUser.Text = String.Format("Copyright © {0}", Application.CompanyName);
            
            progressBar1.Maximum = m_actions.Count;

            worker = new Thread(delegate() {
                for (int i = 0; i < m_actions.Count; i++) {
                    var pair = m_actions[i];
                    this.Invoke(new Action(delegate() {
                        lblLoading.Text = pair.Key;
                        progressBar1.Value = i;
                    }));
                    pair.Value();
                }
                this.Invoke(new Action(delegate() {
                    this.Dispose();
                }));
            });
            worker.Start();
        }
    }
}
