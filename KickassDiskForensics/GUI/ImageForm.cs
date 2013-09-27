using System;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using KFA.Disks;
using KFA.Evidence;
using Image = KFA.Disks.Image;

namespace KFA.GUI {
    public partial class ImageForm : Form {
        private Thread imager;
        private IImageable stream;
        private String filePath;

        public ImageForm() {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        public void CreateImage(IImageable stream, String filePath) {
            this.filePath = filePath;
            this.stream = stream;

            CreateImage();
        }

        private void CreateImage() {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;

            CaseForm.Instance.ActiveCase.LogAction(String.Format("Started imaging... (saving to {0})", filePath), ActionType.DiskImaged);
            Disks.Image image = null;
            imager = new Thread(delegate() {
                try {
                    btnCancel.Enabled = true;
                    image = Image.CreateImage(stream, filePath, delegate(ulong l, ulong total) {
                        progressBar1.Value = (int)((float)l * 100.0 / (float)total);
                        lblProgress.Text = string.Format("Read/Write {0} of {1} bytes", l, total);
                    });
                    CaseForm.Instance.ActiveCase.LogAction("Imaging complete - saving to " + filePath, ActionType.DiskImaged);
                    var result = MessageBox.Show("Imaging complete - Add to case?", "Yes or No", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes) {
                        CaseForm.Instance.ActiveCase.LogAction("Image added casee", ActionType.DiskImaged);
                        CaseForm.Instance.ActiveCase.AddImage(image);
                        this.Invoke(new Action(delegate() {
                            CaseForm.Instance.RefreshCaseTree();
                        }));
                    }
                } catch (ThreadAbortException) { }
                this.Close();
            });
            imager.Start();
        }

        private void btnStart_Click(object sender, EventArgs e) {
            CreateImage();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            CaseForm.Instance.ActiveCase.LogAction("Imaging aborted", ActionType.DiskImaged);
            imager.Abort();
            if (File.Exists(filePath)) {
                File.Delete(filePath);
            }
            this.Dispose();
        }
    }
}
