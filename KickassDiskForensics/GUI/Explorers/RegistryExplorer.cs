using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KFA.ApplicationLevel.Registry;
using KFS.FileSystems;
using KFS.DataStream;
using KFS.Disks;
using KFS.FileSystems.NTFS;

namespace KFA.GUI.Explorers {
    public partial class RegistryExplorer : UserControl, IExplorer {

        private IDataStream m_CurrentStream = null;
        private Registry m_registry;

        public RegistryExplorer() {
            InitializeComponent();
            treeKeys.ImageList = imageList1;
        }

        private void AppendChildren(TreeNode node, List<VirtualRegistryKey> children) {
            node.Nodes.Clear();
            foreach (VirtualRegistryKey child in children) {
                TreeNode treeNode = new TreeNode(child.ToString());
                treeNode.Tag = child;
                treeNode.ImageKey = treeNode.SelectedImageKey = "Directory";
                treeNode.Nodes.Add(new TreeNode("dummy"));
                node.Nodes.Add(treeNode);
            }
        }

        private void treeFiles_BeforeExpand(object sender, TreeViewCancelEventArgs e) {
            VirtualRegistryKey key = e.Node.Tag as VirtualRegistryKey;
            if (key != null) {
                AppendChildren(e.Node, key.GetChildren());
            }
        }

        private void treeFiles_AfterSelect(object sender, TreeViewEventArgs e) {
            VirtualRegistryKey key = e.Node.Tag as VirtualRegistryKey;
            listView.Items.Clear();
            if (key != null) {
                foreach (var kvp in key.GetValues()) {
                    listView.Items.Add(new ListViewItem(new String[] { kvp.Key, kvp.Value }));
                }
            }
        }

        public bool CanView(IDataStream stream) {
            return stream is IFileSystemStore && (stream as IFileSystemStore).FS as FileSystemNTFS != null;
        }

        public void View(IDataStream stream) {
            if (stream != m_CurrentStream) {
                m_CurrentStream = stream;
                m_registry = new Registry((stream as IFileSystemStore).FS);
                radioViewSimple.Checked = true;
                cmbUser.Items.Clear();
                foreach (String user in m_registry.GetUsers()) {
                    cmbUser.Items.Add(user);
                    cmbUser.SelectedIndex = 0;
                }
                UpdateTree();
            }
        }

        private void UpdateSimple() {
            treeKeys.Nodes.Clear();
            foreach (VirtualRegistryKey key in m_registry.GetRawHives()) {
                TreeNode node = new TreeNode(key.ToString());
                node.Nodes.Add(new TreeNode("dummy"));
                node.ImageKey = node.SelectedImageKey = "Hive";
                node.Tag = key;
                treeKeys.Nodes.Add(node);
            }
        }

        private void UpdateComplex() {
            treeKeys.Nodes.Clear();
            foreach (VirtualRegistryKey key in m_registry.GetForUser(cmbUser.Text)) {
                TreeNode node = new TreeNode(key.ToString());
                node.Nodes.Add(new TreeNode("dummy"));
                node.ImageKey = node.SelectedImageKey = "Hive";
                node.Tag = key;
                treeKeys.Nodes.Add(node);
            }
        }

        private void UpdateTree() {
            if (radioViewSimple.Checked) {
                UpdateSimple();
            } else {
                UpdateComplex();
            }
        }

        private void cmbUser_SelectedValueChanged(object sender, EventArgs e) {
            if (radioViewFull.Checked) {
                UpdateComplex();
            }
        }

        private void radioViewFull_CheckedChanged(object sender, EventArgs e) {
            UpdateTree();
            cmbUser.Enabled = radioViewFull.Checked;
        }
    }
}
