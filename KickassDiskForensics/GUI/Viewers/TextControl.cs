using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using KFS.DataStream;

namespace KFA.GUI.Viewers {
	public partial class TextControl : UserControl, IDataViewer {

		private static int numRows = 32;
		private static int numColumns = 10;

		private IDataStream m_dataStream;
		private ulong m_selectionStart, m_selectionEnd, m_row;

		public TextControl() {
			InitializeComponent();
		}

		public bool CanView(IDataStream stream) {
			return true;
		}

		public void View(IDataStream stream) {
			if (stream != null) {
				m_dataStream = stream;

				updateScroll();
				update(0);
			}
		}

		private void updateScroll() {
			m_dataStream.Open();
			bigScrollbar1.Minimum = 0;
			bigScrollbar1.SmallChange = 1;
			bigScrollbar1.LargeChange = numRows;
			bigScrollbar1.Maximum = Math.Max(32, (long)(m_dataStream.StreamLength / (ulong)numColumns))
									+ bigScrollbar1.LargeChange - numRows - 1;
			m_dataStream.Close();
		}

		public ulong SelectionStart {
			get {
				return m_selectionStart;
			}
			set {
				m_selectionStart = value;
			}
		}

		public ulong SelectionEnd {
			get {
				return m_selectionEnd;
			}
			set {
				m_selectionEnd = value;
			}
		}

		private void update(ulong row) {
			m_row = row;
			numColumns = txtBoxAscii.Width / 8;
			numRows = (this.Height - 25) / 15;
			updateScroll();

			ulong startRow = row;
			ulong endRow = startRow + (ulong)numRows;

			ulong startOffset = startRow * (ulong)numColumns;
			ulong endOffset = endRow * (ulong)numColumns;

			String ascii = "";
			listBox1.Items.Clear();

			m_dataStream.Open();
			for (ulong rowOffset = startOffset; rowOffset < Math.Min(m_dataStream.StreamLength, endOffset); rowOffset += (ulong)numColumns) {
				byte[] line = m_dataStream.GetBytes(rowOffset, Math.Min((ulong)numColumns, m_dataStream.StreamLength - rowOffset));
				String asciiLine = ConvertToString(line) + "\n";
				String rowLabel = rowOffset.ToString("X16");
				listBox1.Items.Add(rowLabel);
			}
			m_dataStream.Close();

			txtBoxAscii.Text = ascii;
		}

		private string ConvertToString(byte[] data) {
			StringBuilder sb = new StringBuilder();
			foreach (byte b in data) {
				sb.Append(ShowChar(b));
			}
			return sb.ToString();
		}

		private char ShowChar(byte b) {
			return IsDisplayableChar(b) ? (char)b : '.';
		}

		private bool IsDisplayableChar(byte b) {
			return b >= ' ' /*32*/ && b <= '~' /*126*/;
		}

		private ulong fromRowCol(ulong row, ulong col, int charsPerByte) {
			ulong result = row * (ulong)(16 * charsPerByte + 1) + (col * (ulong)charsPerByte);
			return result;
		}

		private void txtBoxAscii_Resize(object sender, EventArgs e) {
			if (m_dataStream != null) {
				update(m_row);
			}
		}

		private void bigScrollbar1_BigScroll(object sender, BigScrollEventArgs e) {
			if (e.OldValue != e.NewValue) {
				update((ulong)e.NewValue);
			}
		}
	}
}
