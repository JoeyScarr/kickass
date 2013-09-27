using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using KFA.DataStream;

namespace KFA.GUI.Viewers {
    public partial class HexControl : UserControl, IDataViewer {

        private int numRows = 32;

        private IDataStream m_dataStream;
        private bool m_selecting = false;
        private ulong m_selectionStart, m_selectionEnd;
        private ulong m_start, m_end;
        private ulong m_startRow;

        public HexControl() {
            InitializeComponent();
        }

        public bool CanView(IDataStream stream) {
            return true;
        }

        public void View(IDataStream stream) {
            DataStream = stream;
        }

        public void View(IDataStream stream, ulong start, ulong end) {
            DataStream = stream;
            SetRange(start, end);
        }

        public IDataStream DataStream {
            set {
                if (value != null) {
                    m_dataStream = value;

                    bigScrollbar1.SmallChange = 1;
                    m_dataStream.Open();
                    bigScrollbar1.Maximum = Math.Max(32, (long)(m_dataStream.StreamLength / 16)) - 1;
                    bigScrollbar1.LargeChange = numRows;
                    m_dataStream.Close();
                    bigScrollbar1.Minimum = 0;

                    Update(0);

                    m_dataStream.Open();
                    SetRange(0, m_dataStream.StreamLength);
                    m_dataStream.Close();
                }
            }
            get {
                return this.m_dataStream;
            }
        }

        public void SetRange(ulong start, ulong end) {
            m_start = start;
            m_end = end;

            ulong len = end - start;
            bigScrollbar1.Value = 0;
            bigScrollbar1.Maximum = Math.Max(32, (long)(len / 16)) - 1;
            Update(0);
        }


        public void Select(ulong start, ulong end) {
            if (start > end) Debugger.Break();
            m_selectionStart = start;
            m_selectionEnd = end;

            Update((m_selectionStart / 512) * 32);
            bigScrollbar1.Value = Math.Max(0, (long)(m_selectionStart / 16));

            ulong startByte = m_startRow * 16;
            DoSelectRelative(m_selectionStart - startByte, m_selectionEnd - startByte);

            this.Refresh();
        }

        private void DoSelectRelative(ulong s, ulong e) {
            ulong t1, t2;
            t1 = s * 1 +(s / 16);
            t2 = e * 1 +(e / 16);
            txtBoxAscii.Select((int)t1, (int)(t2 - t1));

            t1 = s * 3 +(s / 16);
            t2 = e * 3 +(e / 16);
            txtBoxHex.Select((int)t1, (int)(t2 - t1));

            ParseData(m_startRow * 16 + s);
        }

        public ulong SelectionStart {
            get {
                return m_selectionStart;
            }
        }

        public ulong SelectionEnd {
            get {
                return m_selectionEnd;
            }
        }

        public Byte[] GetSelectedBytes() {
            Byte[] result = new Byte[m_selectionEnd - m_selectionStart];
            m_dataStream.Open();
            for (ulong l = m_selectionStart; l < m_selectionEnd; l++) {
                result[l - m_selectionStart] = m_dataStream.GetByte(l);
            }
            m_dataStream.Close();
            return result;
        }

        private void Update(ulong row) {
            if (m_dataStream != null) {
                numRows = txtBoxHex.Height / 15;

                m_startRow = row;
                ulong endRow = m_startRow + (ulong)numRows;

                ulong startOffset = m_startRow * 16 + m_start;
                m_dataStream.Open();
                ulong endOffset = Math.Min(m_dataStream.StreamLength, endRow * 16 + m_start);
                m_dataStream.Close();

                String hex = "", ascii = "";
                listBox1.Items.Clear();

                m_dataStream.Open();
                for (ulong rowOffset = startOffset; rowOffset < endOffset; rowOffset += 16) {
                    String hexLine = "", asciiLine = "";
                    for (int j = 0; j < 16 && rowOffset + (ulong)j < endOffset; j += 1) {
                        ulong bO = rowOffset + (ulong)j;
                        Byte b = m_dataStream.GetByte(bO);
                        hexLine += b.ToString("X2") + " ";
                        asciiLine += ShowChar((char)b);
                    }
                    hex += hexLine + "\n";
                    ascii += asciiLine + "\n";

                    String rowLabel = rowOffset.ToString("X16");
                    listBox1.Items.Add(rowLabel);
                }
                m_dataStream.Close();

                txtBoxHex.Text = hex;
                txtBoxAscii.Text = ascii;
            }
        }

        private char ShowChar(char c) {
            return IsDisplayableChar(c) ? c : '.';
        }

        private bool IsDisplayableChar(char c) {
            return c >= ' ' /*32*/ && c <= '~' /*126*/;
        }

        private void txtBoxHex_SelectionChanged(object sender, EventArgs e) {
            if (m_selecting) {
                return;
            }
            m_selecting = true;

            ulong rowS = toRow((ulong) txtBoxHex.SelectionStart, 3);
            ulong colS = toCol((ulong) txtBoxHex.SelectionStart, 3);
            ulong rowE = toRow((ulong) (txtBoxHex.SelectionStart + txtBoxHex.SelectionLength), 3);
            ulong colE = toCol((ulong) (txtBoxHex.SelectionStart + txtBoxHex.SelectionLength), 3);

            DoSelectRelative(rowS * 16 + colS, rowE * 16 + colE);
            m_selecting = false;
        }

        private void txtBoxAscii_SelectionChanged(object sender, EventArgs e) {
            if (m_selecting) {
                return;
            }
            m_selecting = true;
            ulong rowS = toRow((ulong) txtBoxAscii.SelectionStart, 1);
            ulong colS = toCol((ulong) txtBoxAscii.SelectionStart, 1);
            ulong rowE = toRow((ulong) (txtBoxAscii.SelectionStart + txtBoxAscii.SelectionLength), 1);
            ulong colE = toCol((ulong) (txtBoxAscii.SelectionStart + txtBoxAscii.SelectionLength), 1);

            DoSelectRelative(rowS * 16 + colS, rowE * 16 + colE);
            m_selecting = false;
        }

        private ulong toRow(ulong selectionPoint, int charsPerByte) {
            return selectionPoint / (ulong) (16 * charsPerByte + 1);
        }

        private ulong toCol(ulong selectionPoint, int charsPerByte) {
            return (selectionPoint % (ulong)(16 * charsPerByte + 1)) / (ulong) charsPerByte;
        }

        private ulong fromRowCol(ulong row, ulong col, int charsPerByte) {
            ulong result = row * (ulong)(16 * charsPerByte + 1) + (col * (ulong) charsPerByte);
            return result;
        }

        private void bigScrollbar1_BigScroll(object sender, BigScrollEventArgs e) {
            if (e.OldValue != e.NewValue) {
                Update((ulong)e.NewValue);
            }
            txtBoxAscii_SelectionChanged(null, null);
        }

        private void HexControl_Resize(object sender, EventArgs e) {
            Update((ulong)bigScrollbar1.Value);
            txtBoxAscii_SelectionChanged(null, null);
        }

        private void ParseData(ulong offset) {
            if (m_dataStream != null) {
                m_dataStream.Open();
                if ((long)offset <= (long)m_dataStream.StreamLength - 1) {
                    tbByte.Text = m_dataStream.GetByte(offset).ToString();
                } else {
                    tbByte.Text = "";
                }
                if ((long)offset <= (long)m_dataStream.StreamLength - 2) {
                    tbInt16.Text = Util.GetInt16(m_dataStream, offset).ToString();
                    tbUint16.Text = Util.GetUInt16(m_dataStream, offset).ToString();
                } else {
                    tbInt16.Text = "";
                    tbUint16.Text = "";
                }
                if ((long)offset <= (long)m_dataStream.StreamLength - 4) {
                    tbInt32.Text = Util.GetInt32(m_dataStream, offset).ToString();
                    tbUint32.Text = Util.GetUInt32(m_dataStream, offset).ToString();
                } else {
                    tbInt32.Text = "";
                    tbUint32.Text = "";
                }
                if ((long)offset <= (long)m_dataStream.StreamLength - 8) {
                    tbInt64.Text = Util.GetInt64(m_dataStream, offset).ToString();
                    tbUint64.Text = Util.GetUInt64(m_dataStream, offset).ToString();
                } else {
                    tbInt64.Text = "";
                    tbUint64.Text = "";
                }
                tbAscii.Text = Util.GetASCIIString(m_dataStream, offset, Util.StrLen(m_dataStream, offset, 20));
                tbUnicode.Text = Util.GetUnicodeString(m_dataStream, offset, 20);
                m_dataStream.Close();
            }
        }
    }
}
