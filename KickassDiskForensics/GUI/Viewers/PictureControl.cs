using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Drawing2D;
using KFS.DataStream;
using KFS.FileSystems;
using KFA.Search;

namespace KFA.GUI.Viewers {
    public partial class PictureControl : UserControl, IDataViewer {
        Picture p;

        public PictureControl() {
            InitializeComponent();
        }

        private void SetImage(System.Drawing.Image image) {
            int MaxHeight = pictureBox1.Height;
            int MaxWidth = pictureBox1.Width;
            int NewWidth = MaxWidth;
            if (image.Width <= NewWidth) {
                NewWidth = image.Width;
            }

            int NewHeight = image.Height * NewWidth / image.Width;
            if (NewHeight > MaxHeight) {
                // Resize with height instead
                NewWidth = image.Width * MaxHeight / image.Height;
                NewHeight = MaxHeight;
            }

            Bitmap bitmap = new Bitmap(NewWidth, NewHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)bitmap);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(image, 0, 0, NewWidth, NewHeight);
            g.Dispose();
            pictureBox1.Image = bitmap;
        }

        #region IDataViewer Members

        public bool CanView(IDataStream stream) {
            return FileTypes.IsPicture(stream);
        }

        public void View(IDataStream stream) {
            if (stream is FileSystemNode) {
                lCaption.Text = ((FileSystemNode)stream).Name;
            }
            p = new Picture(stream);
            Thread t = new Thread(delegate(object o) {
                Picture pic = (Picture)o;
                System.Drawing.Image i = pic.GetImage();
                if (i != null) {
                    this.Invoke(new Action(delegate() {
                        SetImage(i);
                        lLoading.Visible = false;
                    }));
                }
            });
            t.Start(p);
        }

        #endregion

        IDataStream m_Stream = null;
        PictureControl m_Next = null;
        public void SetDataStream(IDataStream stream) {
            m_Stream = stream;
            if (m_Stream is FileSystemNode) {
                lCaption.Text = ((FileSystemNode)m_Stream).Name;
            }
        }

        public void SetNextPictureControl(PictureControl next) {
            m_Next = next;
        }

        public void ViewDataStream() {
            if (!m_Disposed) {
                p = new Picture(m_Stream);
                Thread t = new Thread(delegate(object o) {
                    Picture pic = (Picture)o;
                    System.Drawing.Image i = pic.GetImage();
                    this.Invoke(new Action(delegate() {
                        if (i != null) {
                            SetImage(i);
                        }
                        lLoading.Visible = false;
                        if (m_Next != null) {
                            m_Next.ViewDataStream();
                        }
                    }));
                });
                t.Start(p);
            }
        }
        bool m_Disposed = false;
        public new void Dispose() {
            if (p != null) {
                p.Dispose();
                p = null;
                m_Disposed = true;
            }
        }
    }
}
