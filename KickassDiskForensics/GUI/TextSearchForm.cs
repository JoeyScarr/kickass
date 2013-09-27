using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using KFA.DataStream;
using KFA.Evidence;
using FileSystems.FileSystem;
using KFA.Search;

namespace KFA.GUI {
    public partial class TextSearchForm : Form {
        private ulong m_pos;
        private List<IDataStream> hierarchy;
        private IDataStream stream;
        private Thread finder;

        public TextSearchForm(IDataStream stream) {
            InitializeComponent();
            m_pos = 0;

            hierarchy = CaseForm.Instance.GetStreamHeirachy();
            foreach(IDataStream s in hierarchy) {
                comboBox1.Items.Add(s.StreamName);
            }
            comboBox1.Text = comboBox1.Items[0].ToString();
            this.Text = "Text Search";
        }

        private void Find(IDataStream stream, bool findAll) {
            CaseForm.Instance.ActiveCase.LogAction(String.Format("Finding text/hex string {0} in {1}", textBoxSearchTerm.Text, stream.StreamName), ActionType.StreamViewed);

            finder = new Thread(delegate() {
                try {
                    this.Invoke(new Action(delegate() {
                        btnCancel.Enabled = true;
                        btnFind.Enabled = false;
                        btnFindAll.Enabled = false;
                    }));
                    if (findAll) {
                        List<ulong> results = new List<ulong>();
                        ulong result = FindNextResult();
                        while (result != ulong.MaxValue) {
                            results.Add(result);
                            result = FindNextResult();
                        }
                        FoundAll(results);
                        this.Invoke(new Action(delegate() {
                            this.Dispose();
                        }));
                    } else {
                        ulong result = FindNextResult();
                        FoundOne(result);
                        this.Invoke(new Action(delegate() {
                            btnCancel.Enabled = false;
                            btnFind.Enabled = true;
                            btnFindAll.Enabled = true;
                        }));
                    }
                } catch (ThreadAbortException) { }
            });
            finder.Start();
        }

        private ulong FindNextResult() {
            ulong result = ulong.MaxValue, end = 0;
            SearchUtil.ProgressCallback callback = delegate(ulong l) {
                this.Invoke(new Action(delegate() {
                    progressBar1.Value = (int)((float)l * 100.0 / (float)stream.StreamLength);
                }));
            };

            if (radioButton1.Checked) {
                result = SearchUtil.FindTextAscii(stream, textBoxSearchTerm.Text, m_pos, callback);
            } else if (radioButton2.Checked) {
                result = SearchUtil.FindTextUnicode(stream, textBoxSearchTerm.Text, m_pos, callback);
            } else if (radioButton3.Checked) {
                result = SearchUtil.FindHexString(stream, textBoxSearchTerm.Text, m_pos, callback);
                
            }
            end = result + (ulong) GetSearchLengthBytes();
            m_pos = result + 1;
            return result;
        }

        private ulong GetSearchLengthBytes() {
            if (radioButton1.Checked) {
                return (ulong) textBoxSearchTerm.Text.Length;
            } else if (radioButton2.Checked) {
                return (ulong)textBoxSearchTerm.Text.Length * 2;
            } else if (radioButton3.Checked) {
                return (ulong)textBoxSearchTerm.Text.Length / 2;
            }
            return 0;
        }

        private void FoundAll(List<ulong> results) {
            CaseForm.Instance.ActiveCase.LogAction("Text Search complete, found " + results.Count + " results", ActionType.Search);

            MessageBox.Show("Seach complete - found " + results.Count + " results");
            String searchTitle = "";

            List<FileSystemNode> resultNodes = new List<FileSystemNode>();
            foreach (ulong result in results) {
                ulong contextStart = Math.Max(0, result - 4);
                ulong start = result;
                ulong end = result + GetSearchLengthBytes();
                ulong contextEnd = Math.Min(stream.StreamLength, end + 4);
                String context = "";
                if (radioButton1.Checked) {
                    searchTitle = "Ascii";
                    context = Util.GetASCIIString(stream, contextStart, start - contextStart)
                        + ">" + Util.GetASCIIString(stream, start, end - start)
                        + "<" + Util.GetASCIIString(stream, end, contextEnd - end);
                } else if (radioButton2.Checked) {
                    searchTitle = "Unicode";
                    context = Util.GetUnicodeString(stream, contextStart, start - contextStart)
                        + ">" + Util.GetUnicodeString(stream, start, end - start)
                        + "<" + Util.GetUnicodeString(stream, end, contextEnd - end);
                } else if (radioButton3.Checked) {
                    searchTitle = "Hex";
                    context = Util.GetHexString(stream, contextStart, start - contextStart)
                        + ">" + Util.GetHexString(stream, start, end - start)
                        + "<" + Util.GetHexString(stream, end, contextEnd - end);
                }
                resultNodes.Add(new TextSearchResult(stream, start, end - start, result + ": " + context));
            }
            SearchResults sr = new SearchResults(String.Format("{0} search '{1}'", searchTitle, textBoxSearchTerm.Text), resultNodes);

            this.Invoke(new Action(delegate() {
                CaseForm.Instance.AddSearchResult(sr);
            }));
        }

        private void FoundOne(ulong result) {
            if (result == ulong.MaxValue) {
                MessageBox.Show("No more results");
            } else {
                this.Invoke(new Action(delegate() {
                    CaseForm.Instance.HighlightBytes(stream, result, GetSearchLengthBytes());
                }));
            }
        }

        private void btnFind_Click(object sender, EventArgs e) {
            stream = hierarchy.First(x => x.StreamName.Equals(comboBox1.Text));
            Find(stream, false);
        }

        private void btnFindAll_Click(object sender, EventArgs e) {
            stream = hierarchy.First(x => x.StreamName.Equals(comboBox1.Text));
            Find(stream, true);
           
            //ulong result = new 
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            finder.Abort();
            this.Dispose();
        }
    }
}
