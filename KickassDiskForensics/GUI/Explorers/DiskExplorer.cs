using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using KFA.DataStream;
using KFA.Disks;
using Image = KFA.Disks.Image;

namespace KFA.GUI.Explorers {
    public partial class DiskExplorer : UserControl, IExplorer {
        private IDataStream m_CurrentStream = null;
        public DiskExplorer() {
            InitializeComponent();
        }

        #region IExplorer Members

        public bool CanView(IDataStream stream) {
            return stream is PhysicalDisk || stream is PhysicalDiskSection
                || stream is Disks.Image || stream is LogicalDisk;
        }

        public void View(IDataStream stream) {
            if (stream != m_CurrentStream) {
                m_CurrentStream = stream;
                if (stream is IDescribable) {
                    tbDescription.Text = ((IDescribable)stream).TextDescription;
                }

                if (stream is PhysicalDisk) {
                    partitionDiagram1.Disk = stream as PhysicalDisk;
                } else if (stream is PhysicalDiskSection) {
                    partitionDiagram1.ActiveSection = stream as PhysicalDiskSection;
                } else {
                    partitionDiagram1.ActiveSection = null;
                }
            }
        }

        #endregion
    }
}
