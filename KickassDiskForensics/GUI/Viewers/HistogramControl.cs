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
using FileSystems.FileSystem;
using KFA.DataStream;

namespace KFA.GUI.Viewers {
	public partial class HistogramControl : UserControl, IDataViewer {
		private IDataStream m_stream;
		private Bitmap diagram;
		private long[] bytecounts;
		private long max;
		private bool loaded = false;

		public HistogramControl() {
			InitializeComponent();
			diagram = new Bitmap(this.panel1.Width, panel1.Height);
			View(null);
		}

		private void LoadData() {
			bytecounts = new long[256];
			max = 1;

			if (m_stream != null) {
				m_stream.Open();
				byte[] data = m_stream.GetBytes(0, m_stream.StreamLength);
				for (int i = 0; i < data.Length; i++) {
					bytecounts[data[i]]++;
				}
				m_stream.Close();
			}

			for (int i = 0; i < bytecounts.Length; i++) {
				if (bytecounts[i] > max) {
					max = bytecounts[i];
				}
			}
			loaded = true;
			UpdateDiagram();
		}

		private void UpdateDiagram() {
			diagram = new Bitmap(this.panel1.Width, panel1.Height);
			Graphics g = Graphics.FromImage(diagram);

			g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, panel1.Width, panel1.Height));

			if (loaded) {
				int[] xvalues = new int[bytecounts.Length];
				double current = 0;
				for (int i = 0; i < xvalues.Length; i++) {
					current += (double)panel1.Width / (double)bytecounts.Length;
					xvalues[i] = (int)current;
				}

				for (int i = 0; i < bytecounts.Length; i++) {
					int x1 = i == 0 ? 0 : xvalues[i - 1];
					int width = Math.Max(1, i == 0 ? xvalues[0] : xvalues[i] - xvalues[i - 1]);
					double y = ((float)bytecounts[i] / (float)max) * panel1.Height;

					Rectangle r = new Rectangle(x1, (int)(panel1.Height - y), width, (int)y);
					Brush b = new SolidBrush(Color.Red);
					g.FillRectangle(b, r);
				}
				button1.Visible = false;
			} else {
				button1.Visible = m_stream != null;
			}
			Refresh();
		}

		private void panel1_Paint(object sender, PaintEventArgs e) {
			if (diagram != null) {
				e.Graphics.DrawImage(diagram, new Point(0, 0));
			}
		}

		public bool CanView(IDataStream stream) {
			return true;
		}

		public void View(IDataStream stream) {
			loaded = false;
			m_stream = stream;
			UpdateDiagram();
		}

		private void button1_Click(object sender, EventArgs e) {
			LoadData();
			button1.Visible = false;
			Refresh();
		}

		private void panel1_Resize(object sender, EventArgs e) {
			UpdateDiagram();
		}
	}
}
