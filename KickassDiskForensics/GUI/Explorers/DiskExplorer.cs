using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using KFS.DataStream;
using KFS.Disks;
using Image = KFS.Disks.Image;

namespace KFA.GUI.Explorers {
    public partial class DiskExplorer : UserControl, IExplorer {
        private IDataStream m_CurrentStream = null;
        public DiskExplorer() {
            InitializeComponent();
        }

        #region IExplorer Members

        public bool CanView(IDataStream stream) {
            return stream is IPhysicalDisk || stream is PhysicalDiskSection
                || stream is Image || stream is ILogicalDisk;
        }

        public void View(IDataStream stream) {
            if (stream != m_CurrentStream) {
                m_CurrentStream = stream;
                if (stream is IDescribable) {
                    tbDescription.Text = ((IDescribable)stream).TextDescription;
                }

                if (stream is IPhysicalDisk) {
                    partitionDiagram1.Disk = stream as IPhysicalDisk;
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
