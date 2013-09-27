using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KFA.DataStream;
using KFA.Search;
using FileSystems.FileSystem;

namespace KFA.GUI.Viewers {
    public partial class Gallery : UserControl, IDataViewer {
        public Gallery() {
            InitializeComponent();
        }

        #region IDataViewer Members

        public bool CanView(IDataStream stream) {
            return stream is Folder;
        }

        Folder m_Folder;
        public void View(IDataStream stream) {
            foreach (PictureControl old in flowLayoutPanel1.Controls) {
                old.Dispose();
            }
            flowLayoutPanel1.Controls.Clear();
            m_Folder = stream as Folder;
            if (m_Folder != null) {
                PictureControl previous = null;
                PictureControl first = null;
                foreach (FileSystemNode f in m_Folder.GetChildren()) {
                    if (f is File && FileTypes.IsPicture(f)) {
                        PictureControl control = new PictureControl();
                        flowLayoutPanel1.Controls.Add(control);
                        control.SetDataStream(f);
                        if (previous != null) {
                            previous.SetNextPictureControl(control);
                        }
                        previous = control;
                        if (first == null) {
                            first = control;
                        }
                    }
                }
                if (first != null) {
                    first.ViewDataStream();
                }
            }
        }

        #endregion
    }
}
