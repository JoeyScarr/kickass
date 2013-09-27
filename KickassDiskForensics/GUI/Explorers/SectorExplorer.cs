using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KFA.GUI.Viewers;
using KFA.DataStream;
using KFA.Disks;

namespace KFA.GUI.Explorers {
    public partial class SectorExplorer : UserControl, IExplorer {
        private Dictionary<SectorStatus, Color> m_Legend = new Dictionary<SectorStatus, Color>(){
            {SectorStatus.MasterBootRecord, Color.Maroon},
            {SectorStatus.SlackSpace, Color.Gray},
            {SectorStatus.UnknownFilesystem, Color.Lavender},
            {SectorStatus.NTFSUsed, Color.LightBlue},
            {SectorStatus.NTFSFree, Color.LightGreen},
            {SectorStatus.NTFSBad, Color.Black},
            {SectorStatus.FATUsed, Color.Blue},
            {SectorStatus.FATFree, Color.Green},
            {SectorStatus.FATBad, Color.Black},
            {SectorStatus.FATReserved, Color.Salmon},
            {SectorStatus.FATFAT, Color.Orange},
            {SectorStatus.Unknown, Color.LightYellow}};
        private Dictionary<SectorStatus, string> m_Labels = new Dictionary<SectorStatus, string>(){
            {SectorStatus.MasterBootRecord, "Master Boot Record"},
            {SectorStatus.SlackSpace, "Slack Space"},
            {SectorStatus.UnknownFilesystem, "Unknown Filesystem"},
            {SectorStatus.NTFSUsed, "Used (NTFS)"},
            {SectorStatus.NTFSFree, "Free (NTFS)"},
            {SectorStatus.NTFSBad, "Bad (NTFS)"},
            {SectorStatus.FATUsed, "Used (FAT)"},
            {SectorStatus.FATFree, "Free (FAT)"},
            {SectorStatus.FATBad, "Bad (FAT)"},
            {SectorStatus.FATReserved, "Reserved sectors (FAT)"},
            {SectorStatus.FATFAT, "File Allocation Table (FAT)"},
            {SectorStatus.Unknown, "Unknown"}};
        private ulong m_sector;
        private IHasSectors m_image;
        private IDataStream m_CurrentStream = null;

        public SectorExplorer() {
            InitializeComponent();

            Dictionary<string, Color> leg = new Dictionary<string, Color>();
            foreach (SectorStatus status in m_Legend.Keys) {
                leg.Add(m_Labels[status], m_Legend[status]);
            }

            legend.SetData(leg);
        }

        private void viewSector(ulong sectorNum) {
            m_sector = sectorNum;

            ulong totalSectors = m_image.StreamLength / m_image.GetSectorSize();
            ulong startByte = sectorNum * m_image.GetSectorSize();
            m_image.Open();
            ulong endByte = Math.Min((sectorNum + 1) * m_image.GetSectorSize(), m_image.StreamLength);
            CaseForm.Instance.ViewableObject = new SectorStream(m_image, startByte, endByte - startByte, m_sector);
            m_image.Close();
            lbSectorNum.Text = String.Format("Sector {0} of {1} (bytes {2}-{3})", m_sector, totalSectors, startByte, endByte - 1);

            btnFirst.Enabled = btnPrevious.Enabled = m_sector != 0;
            btnLast.Enabled = btnNext.Enabled = m_sector != totalSectors - 1;
            txtSectorNum.Text = m_sector.ToString();
        }

        private void btnNext_Click(object sender, EventArgs e) {
            viewSector(m_sector + 1);
            FixScrollbar();
            UpdateDiagram(m_sector);
        }

        private void btnPrevious_Click(object sender, EventArgs e) {
            viewSector(m_sector - 1);
            FixScrollbar();
            UpdateDiagram(m_sector);
        }

        private void btnFirst_Click(object sender, EventArgs e) {
            viewSector(0);
            FixScrollbar();
            UpdateDiagram(m_sector);
        }

        private void btnLast_Click(object sender, EventArgs e) {
            viewSector(m_image.StreamLength / m_image.GetSectorSize() - 1);
            FixScrollbar();
            UpdateDiagram(m_sector);
        }

        private void btnJump_Click(object sender, EventArgs e) {
            viewSector(UInt64.Parse(txtSectorNum.Text));
            FixScrollbar();
            UpdateDiagram(m_sector);
        }

        private void txtSectorNum_TextChanged(object sender, EventArgs e) {
            try {
                ulong sector = UInt64.Parse(txtSectorNum.Text);
                if (sector < 0 || sector >= m_image.StreamLength / m_image.GetSectorSize()) {
                    throw new Exception("Invalid sector");
                }
                txtSectorNum.BackColor = Color.White;
                btnJump.Enabled = true;
            } catch {
                txtSectorNum.BackColor = Color.Red;
                btnJump.Enabled = false;
            }
        }

        private void FixScrollbar() {
            //Fix scrollbar to show selected sector
            int numColumns = panel1.Width / SQUARE_SIZE;
            int numRows = panel1.Height / SQUARE_SIZE;

            ulong sectorRow = m_sector / (ulong) numColumns;
            if ((int) sectorRow < vScrollBar.Value) {
                vScrollBar.Value = (int)sectorRow;
            }
            if ((int) sectorRow > vScrollBar.Value + numRows) {
                vScrollBar.Value = ((int)sectorRow - numRows) + 1;
            }
        }

        private void UpdateScrollbar() {
            m_image.Open();
            ulong totalSectors = m_image.StreamLength / m_image.GetSectorSize();
            int numColumns = panel1.Width / SQUARE_SIZE;
            int numRows = panel1.Height / SQUARE_SIZE;
            ulong totalNumRows = totalSectors / (ulong)numColumns + 1;
            m_image.Close();

            vScrollBar.SmallChange = 1;
            vScrollBar.LargeChange = 1;
            vScrollBar.Minimum = 0;
            vScrollBar.Maximum = Math.Max(numRows, ((int)totalNumRows - numRows));
        }

        private static int SQUARE_SIZE = 16;
        private static int SQUARE_SIZE_INNER = 12;

        private void UpdateDiagram(ulong sectorNum) {
            Bitmap diagram = new Bitmap(panel1.Width, panel1.Height);
            System.Drawing.Graphics g = Graphics.FromImage(diagram);
            ulong totalSectors = m_image.StreamLength / m_image.GetSectorSize();
            int numColumns = panel1.Width / SQUARE_SIZE;
            int numRows = panel1.Height / SQUARE_SIZE;

            m_image.Open();
            
            int firstRow = vScrollBar.Value;


            for (int j = 0; j < numRows; j++) {
                for (int i = 0; i < numColumns; i++) {
                    ulong sector = (ulong)((firstRow + j) * numColumns + i);
                    if (sector >= totalSectors) {
                        goto end;
                    }

                    Rectangle r = new Rectangle(i * SQUARE_SIZE, j * SQUARE_SIZE, SQUARE_SIZE_INNER, SQUARE_SIZE_INNER);

                    SectorStatus s = m_image.GetSectorStatus(sector);
                    Brush b = new SolidBrush(m_Legend[s]);
                    g.FillRectangle(b, r);

                    if (sector == sectorNum) {
                        r = new Rectangle(i * SQUARE_SIZE, j * SQUARE_SIZE, SQUARE_SIZE_INNER, SQUARE_SIZE_INNER);
                        g.DrawRectangle(new Pen(Color.Black, 3), r);
                    }
                }
            }
            m_image.Close();

            end:
            panel1.BackgroundImage = diagram;
        }

        private void panel1_Resize(object sender, EventArgs e) {
            if (m_image != null) {
                UpdateScrollbar();
                UpdateDiagram(m_sector);
            }
        }

        private void vScrollBar_Scroll(object sender, ScrollEventArgs e) {
            UpdateDiagram(m_sector);
        }

        #region IExplorer Members

        public bool CanView(IDataStream stream) {
            return stream is IHasSectors && stream.StreamLength > 0;
        }

        public void View(IDataStream stream) {
            if (stream != m_CurrentStream) {
                m_CurrentStream = stream;
                m_image = (IHasSectors)stream;

                vScrollBar.Value = 0;
                UpdateScrollbar();
                //viewSector(0);
                UpdateDiagram(0);
                this.Refresh();
            }
        }

        #endregion


        private void panel1_MouseClick(object sender, MouseEventArgs e) {
            int numColumns = panel1.Width / SQUARE_SIZE;
            int numRows = panel1.Height / SQUARE_SIZE;

            int firstRow = vScrollBar.Value;

            int i = e.X / SQUARE_SIZE;
            int j = e.Y / SQUARE_SIZE;

            viewSector((ulong)((firstRow + j) * numColumns + i));
            FixScrollbar();
            UpdateDiagram(m_sector);
        }

        private void txtSectorNum_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return) {
                e.Handled = true;
                btnJump.PerformClick();
            }
        }

    }
}
