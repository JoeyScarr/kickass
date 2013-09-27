using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KFA.DataStream;
using KFA.Disks;
using FileSystems.FileSystem;
using KFA.Search;

namespace KFA.GUI.Explorers {
    public partial class SearchResultExplorer : UserControl, IExplorer {
        public SearchResultExplorer() {
            InitializeComponent();
            treeFiles.Nodes.Add("Search Results");
            treeFiles.Nodes[0].Expand();
        }

        public void AddResults(SearchResults results) {
            TreeNode node = new TreeNode(results.ToString());
            AppendChildren(node, results.GetChildren());
            node.Tag = results;
            node.Expand();
            treeFiles.Nodes[0].Nodes.Add(node);
        }

        private void AppendChildren(TreeNode node, IEnumerable<FileSystemNode> children) {
            node.Nodes.Clear();
            foreach (FileSystemNode child in children) {
                TreeNode treeNode = new TreeNode(child.ToString());
                treeNode.Tag = child;
                if (child.Deleted) {
                    treeNode.ForeColor = Color.Red;
                }
                if (child is Folder || (child is File && ((File)child).IsZip)) {
                    treeNode.Nodes.Add(new TreeNode("dummy"));
                    child.Loaded = false;
                }
                node.Nodes.Add(treeNode);
            }
        }

        private void treeFiles_BeforeExpand(object sender, TreeViewCancelEventArgs e) {
            FileSystemNode fsNode = e.Node.Tag as FileSystemNode;
            if (fsNode != null && !fsNode.Loaded) {
                AppendChildren(e.Node, fsNode.GetChildren());
                fsNode.Loaded = true;
            }
        }

        private void treeFiles_AfterSelect(object sender, TreeViewEventArgs e) {
            IDescribable fsNode = e.Node.Tag as IDescribable;
            if (fsNode != null) {
                tbDescription.Text = fsNode.TextDescription;
            }
            IDataStream stream = e.Node.Tag as IDataStream;
            if (stream != null) {
                CaseForm.Instance.ViewableObject = stream;
            }
            IClickable clickable = e.Node.Tag as IClickable;
            if (clickable != null) {
                clickable.Clicked();
            }
        }

        #region IExplorer Members

        public bool CanView(IDataStream stream) {
            return true;
        }

        public void View(IDataStream stream) { }

        #endregion
    }
}
