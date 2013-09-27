using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KFA.Disks;

namespace KFA.GUI.Explorers {
    public partial class PartitionDiagram : UserControl {
        public PartitionDiagram() {
            InitializeComponent();
            Resize += new EventHandler(PartitionDiagram_Resize);
        }

        void PartitionDiagram_Resize(object sender, EventArgs e) {
            UpdateDiagram();
        }

        private static int MIN_SIZE = 20;
        private PhysicalDisk m_Disk;
        private PhysicalDiskSection m_ActiveSection;

        public PhysicalDisk Disk {
            get { return m_Disk; }
            set {
                m_Disk = value;
                UpdateDiagram();
            }
        }

        public PhysicalDiskSection ActiveSection {
            get { return m_ActiveSection; }
            set {
                m_ActiveSection = value;
                m_Disk = value == null ? null : value.PhysicalDisk;
                UpdateDiagram();
            }
        }

        public void UpdateDiagram() {
            Bitmap diagram = new Bitmap(panelDiagram.Width, panelDiagram.Height);
            System.Drawing.Graphics g = Graphics.FromImage(diagram);

            if (Disk == null || Disk.Sections == null) {
                panelDiagram.BackgroundImage = diagram;
                return;
            }

            //Compute width
            double totalWidth = 0;
            foreach (var section in Disk.Sections) {
                double width = (((double)section.Length / Disk.StreamLength) * panelDiagram.Width);
                totalWidth += width > MIN_SIZE ? width : MIN_SIZE;
            }

            //Draw
            double start = 0;
            foreach (var section in Disk.Sections) {
                double width = (((double)section.Length / Disk.StreamLength) * panelDiagram.Width);
                width = width > MIN_SIZE ? width : MIN_SIZE;
                width = (width / totalWidth) * panelDiagram.Width;

                Rectangle r = new Rectangle((int)start, 0, (int)width, panelDiagram.Height);

                //Filled rectangle
                Brush b = null;
                if (section is MasterBootRecord) {
                    b = new SolidBrush(Color.LightSalmon);
                } else if (section is PhysicalDiskPartition) {
                    b = new SolidBrush(Color.CornflowerBlue);
                } else {
                    b = new SolidBrush(Color.LightSlateGray);
                }
                g.FillRectangle(b, r);

                //Outline + text
                Pen pen = new Pen(Color.Black);
                g.DrawRectangle(pen, r);
                g.DrawString(section.ToString(), new Font(FontFamily.GenericSerif, 8), new SolidBrush(Color.Black), new PointF(r.X + 3, r.Y + 3));

                //Selection outline
                if (section == ActiveSection) {
                    pen = new Pen(Color.White);
                    pen.Width = 2;
                    g.DrawRectangle(pen, r.X + 2, r.Y + 2, r.Width - 3, r.Height - 5);
                }
                start += width;
            }
            panelDiagram.BackgroundImage = diagram;
        }
    }
}
