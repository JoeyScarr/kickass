using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KFS.DataStream;
using KFS.FileSystems;

namespace KFA.GUI.Viewers {
    public partial class WebBrowserControl : UserControl, IDataViewer {
        public WebBrowserControl() {
            InitializeComponent();
        }

        #region IDataViewer Members

        public bool CanView(IDataStream stream) {
            stream.Open();
            bool res = stream.StreamLength < 100 * 1024 // Don't bother with files over 100K
                && IsHtml(stream);
            stream.Close();
            return res;
        }

        private bool IsHtml(IDataStream stream) {
            File file = stream as File;
            if (file != null) {
                string name = file.Name.ToLower();
                return name.EndsWith(".html")
                    || name.EndsWith(".htm")
                    || name.EndsWith(".php")
                    || name.EndsWith(".asp")
                    || name.EndsWith(".aspx");
            } else {
                return false;
            }
        }

        public void View(IDataStream stream) {
            string path = Util.CreateTemporaryFile(stream);
            webBrowser1.Url = new Uri(path);
        }

        #endregion
    }
}
