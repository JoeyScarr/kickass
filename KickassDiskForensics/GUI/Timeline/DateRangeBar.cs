using System;
using System.Drawing;
using System.Windows.Forms;

namespace KFA.GUI.Timeline {
    public partial class DateRangeBar : UserControl {
        // delegate to handle range changed
        public delegate void RangeChangedEventHandler(object sender, EventArgs e);

        // delegate to handle range is changing
        public delegate void RangeChangingEventHandler(object sender, EventArgs e);

        public DateRangeBar() {
            InitializeComponent();

            // use double buffering
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
        }


        public enum ActiveMarkType { None, Left, Right };
        public enum RangeBarOrientation { Horizontal, Vertical };
        public enum TopBottomOrientation { Top, Bottom, Both };

        private Font m_ToolTipFont = new Font("Arial", 8);
        private Color m_ColorInner = Color.LightGreen;
        private Color m_ColorRange = Color.FromKnownColor(KnownColor.Control);
        private Color m_ColorShadowLight = Color.FromKnownColor(KnownColor.ControlLightLight);
        private Color m_ColorShadowDark = Color.FromKnownColor(KnownColor.ControlDarkDark);
        private int m_ShadowSize = 1;
        private DateTime m_Minimum = DateTime.Now.AddDays(-1);
        private DateTime m_Maximum = DateTime.Now;
        private DateTime m_RangeMin = DateTime.Now.AddHours(-2);
        private DateTime m_RangeMax = DateTime.Now.AddHours(-1);
        private ActiveMarkType m_ActiveMark = ActiveMarkType.None;


        private RangeBarOrientation m_OrientationBar = RangeBarOrientation.Horizontal; // orientation of range bar
        private TopBottomOrientation m_OrientationScale = TopBottomOrientation.Bottom;
        private int m_BarHeight = 8;		// Height of Bar
        private int m_MarkWidth = 8;		// Width of mark knobs
        private int m_MarkHeight = 24;	// total height of mark knobs
        private int m_TickHeight = 6;	// Height of axis tick
        private int m_NumAxisDivisions = 10;
        
        private int m_LeftPos = 0, m_RightPos = 0;	// Pixel-Position of mark buttons
        private int m_XPosMin, m_XPosMax;

        private Point[] m_LMarkPnt = new Point[5];
        private Point[] m_RMarkPnt = new Point[5];

        private bool m_MoveLMark = false;
        private bool m_MoveRMark = false;

        //------------------------------------
        // Properties
        //------------------------------------

        /// <summary>
        /// set or get tick height
        /// </summary>
        public int HeightOfTick {
            set {
                m_TickHeight = Math.Min(Math.Max(1, value), m_BarHeight);
                Invalidate();
                Update();
            }
            get {
                return m_TickHeight;
            }
        }

        /// <summary>
        /// set or get mark knob height
        /// </summary>
        public int HeightOfMark {
            set {
                m_MarkHeight = Math.Max(m_BarHeight + 2, value);
                Invalidate();
                Update();
            }
            get {
                return m_MarkHeight;
            }
        }


        /// <summary>
        /// set/get height of mark
        /// </summary>
        public int HeightOfBar {
            set {
                m_BarHeight = Math.Min(value, m_MarkHeight - 2);
                Invalidate();
                Update();
            }
            get {
                return m_BarHeight;
            }

        }

        /// <summary>
        /// set or get range bar orientation
        /// </summary>
        public RangeBarOrientation Orientation {
            set {
                m_OrientationBar = value;
                Invalidate();
                Update();
            }
            get {
                return m_OrientationBar;
            }
        }

        /// <summary>
        /// set or get scale orientation
        /// </summary>
        public TopBottomOrientation ScaleOrientation {
            set {
                m_OrientationScale = value;
                Invalidate();
                Update();
            }
            get {
                return m_OrientationScale;
            }
        }

        /// <summary>
        ///  set or get right side of range
        /// </summary>
        public DateTime RangeMaximum {
            set {
                m_RangeMax = value;
                if (m_RangeMax < m_Minimum)
                    m_RangeMax = m_Minimum;
                else if (m_RangeMax > m_Maximum)
                    m_RangeMax = m_Maximum;
                if (m_RangeMax < m_RangeMin)
                    m_RangeMax = m_RangeMin;
                Range2Pos();
                Invalidate(true);
            }
            get { return m_RangeMax; }
        }


        /// <summary>
        /// set or get left side of range
        /// </summary>
        public DateTime RangeMinimum {
            set {
                m_RangeMin = value;
                if (m_RangeMin < m_Minimum)
                    m_RangeMin = m_Minimum;
                else if (m_RangeMin > m_Maximum)
                    m_RangeMin = m_Maximum;
                if (m_RangeMin > m_RangeMax)
                    m_RangeMin = m_RangeMax;
                Range2Pos();
                Invalidate(true);
            }
            get {
                return m_RangeMin;
            }
        }


        /// <summary>
        /// set or get right side of total range
        /// </summary>
        public DateTime TotalMaximum {
            set {
                m_Maximum = value;
                if (m_RangeMax > m_Maximum)
                    m_RangeMax = m_Maximum;
                Range2Pos();
                Invalidate(true);
            }
            get { return m_Maximum; }
        }


        /// <summary>
        /// set or get left side of total range
        /// </summary>
        public DateTime TotalMinimum {
            set {
                m_Minimum = value;
                if (m_RangeMin < m_Minimum)
                    m_RangeMin = m_Minimum;
                Range2Pos();
                Invalidate(true);
            }
            get { return m_Minimum; }
        }


        /// <summary>
        /// set or get number of divisions
        /// </summary>
        public int DivisionNum {
            set {
                m_NumAxisDivisions = value;
                Refresh();
            }
            get { return m_NumAxisDivisions; }
        }


        /// <summary>
        /// set or get color of inner range
        /// </summary>
        public Color InnerColor {
            set {
                m_ColorInner = value;
                Refresh();
            }
            get { return m_ColorInner; }
        }


        /// <summary>
        /// set selected range
        /// </summary>
        /// <param name="left">left side of range</param>
        /// <param name="right">right side of range</param>
        public void SelectRange(DateTime left, DateTime right) {
            RangeMinimum = left;
            RangeMaximum = right;
            Range2Pos();
            Invalidate(true);
        }


        /// <summary>
        /// set range limits
        /// </summary>
        /// <param name="left">left side of range limit</param>
        /// <param name="right">right side of range limit</param>
        public void SetRangeLimit(DateTime left, DateTime right) {
            m_Minimum = left;
            m_Maximum = right;
            Range2Pos();
            Invalidate(true);
        }

        private void DrawTooltip(Graphics g, DateTime value, float x, float y) {
            SolidBrush brushMark = new SolidBrush(m_ColorShadowDark);
            StringFormat strformat = new StringFormat();
            strformat.Alignment = StringAlignment.Center;
            strformat.LineAlignment = StringAlignment.Near;
            g.DrawString(value.ToString("yyyy-MM-dd"), m_ToolTipFont, brushMark, x, y, strformat);
        }

        // paint event reaction
        protected override void OnPaint(PaintEventArgs e) {
            int h = this.Height;
            int w = this.Width;
            int baryoff, markyoff, tickyoff1, tickyoff2;
            double dtick;
            int tickpos;
            Pen penRange = new Pen(m_ColorRange);
            Pen penShadowLight = new Pen(m_ColorShadowLight);
            Pen penShadowDark = new Pen(m_ColorShadowDark);
            SolidBrush brushShadowLight = new SolidBrush(m_ColorShadowLight);
            SolidBrush brushShadowDark = new SolidBrush(m_ColorShadowDark);
            SolidBrush brushInner;
            SolidBrush brushRange = new SolidBrush(m_ColorRange);

            if (this.Enabled == true)
                brushInner = new SolidBrush(m_ColorInner);
            else
                brushInner = new SolidBrush(Color.FromKnownColor(KnownColor.InactiveCaption));

            // range
            m_XPosMin = m_MarkWidth + 1;
            if (this.m_OrientationBar == RangeBarOrientation.Horizontal)
                m_XPosMax = w - m_MarkWidth - 1;
            else
                m_XPosMax = h - m_MarkWidth - 1;

            // range check
            if (m_LeftPos < m_XPosMin) m_LeftPos = m_XPosMin;
            if (m_LeftPos > m_XPosMax) m_LeftPos = m_XPosMax;
            if (m_RightPos > m_XPosMax) m_RightPos = m_XPosMax;
            if (m_RightPos < m_XPosMin) m_RightPos = m_XPosMin;

            Range2Pos();


            if (this.m_OrientationBar == RangeBarOrientation.Horizontal) {

                baryoff = (h - m_BarHeight) / 2;
                markyoff = baryoff + (m_BarHeight - m_MarkHeight) / 2 - 1;

                // total range bar frame			
                e.Graphics.FillRectangle(brushShadowDark, 0, baryoff, w - 1, m_ShadowSize);	// top
                e.Graphics.FillRectangle(brushShadowDark, 0, baryoff, m_ShadowSize, m_BarHeight - 1);	// left
                e.Graphics.FillRectangle(brushShadowLight, 0, baryoff + m_BarHeight - 1 - m_ShadowSize, w - 1, m_ShadowSize);	// bottom
                e.Graphics.FillRectangle(brushShadowLight, w - 1 - m_ShadowSize, baryoff, m_ShadowSize, m_BarHeight - 1);	// right


                // inner region
                e.Graphics.FillRectangle(brushInner, m_LeftPos, baryoff + m_ShadowSize, m_RightPos - m_LeftPos, m_BarHeight - 1 - 2 * m_ShadowSize);

                // Skala
                if (m_OrientationScale == TopBottomOrientation.Bottom) {
                    tickyoff1 = tickyoff2 = baryoff + m_BarHeight + 2;
                } else if (m_OrientationScale == TopBottomOrientation.Top) {
                    tickyoff1 = tickyoff2 = baryoff - m_TickHeight - 4;
                } else {
                    tickyoff1 = baryoff + m_BarHeight + 2;
                    tickyoff2 = baryoff - m_TickHeight - 4;
                }

                if (m_NumAxisDivisions > 1) {
                    dtick = (double)(m_XPosMax - m_XPosMin) / (double)m_NumAxisDivisions;
                    for (int i = 0; i < m_NumAxisDivisions + 1; i++) {
                        tickpos = (int)Math.Round((double)i * dtick);
                        if (m_OrientationScale == TopBottomOrientation.Bottom
                            || m_OrientationScale == TopBottomOrientation.Both) {
                            e.Graphics.DrawLine(penShadowDark, m_MarkWidth + 1 + tickpos,
                                tickyoff1,
                                m_MarkWidth + 1 + tickpos,
                                tickyoff1 + m_TickHeight);
                        }
                        if (m_OrientationScale == TopBottomOrientation.Top
                            || m_OrientationScale == TopBottomOrientation.Both) {
                            e.Graphics.DrawLine(penShadowDark, m_MarkWidth + 1 + tickpos,
                                tickyoff2,
                                m_MarkWidth + 1 + tickpos,
                                tickyoff2 + m_TickHeight);
                        }
                    }
                }

                // left mark knob				
                m_LMarkPnt[0].X = m_LeftPos - m_MarkWidth; m_LMarkPnt[0].Y = markyoff + m_MarkHeight / 3;
                m_LMarkPnt[1].X = m_LeftPos; m_LMarkPnt[1].Y = markyoff;
                m_LMarkPnt[2].X = m_LeftPos; m_LMarkPnt[2].Y = markyoff + m_MarkHeight;
                m_LMarkPnt[3].X = m_LeftPos - m_MarkWidth; m_LMarkPnt[3].Y = markyoff + 2 * m_MarkHeight / 3;
                m_LMarkPnt[4].X = m_LeftPos - m_MarkWidth; m_LMarkPnt[4].Y = markyoff;
                e.Graphics.FillPolygon(brushRange, m_LMarkPnt);
                e.Graphics.DrawLine(penShadowDark, m_LMarkPnt[3].X - 1, m_LMarkPnt[3].Y, m_LMarkPnt[1].X - 1, m_LMarkPnt[2].Y); // lower left shadow
                e.Graphics.DrawLine(penShadowLight, m_LMarkPnt[0].X - 1, m_LMarkPnt[0].Y, m_LMarkPnt[0].X - 1, m_LMarkPnt[3].Y); // left shadow				
                e.Graphics.DrawLine(penShadowLight, m_LMarkPnt[0].X - 1, m_LMarkPnt[0].Y, m_LMarkPnt[1].X - 1, m_LMarkPnt[1].Y); // upper shadow
                if (m_LeftPos < m_RightPos)
                    e.Graphics.DrawLine(penShadowDark, m_LMarkPnt[1].X, m_LMarkPnt[1].Y + 1, m_LMarkPnt[1].X, m_LMarkPnt[2].Y); // right shadow
                if (m_ActiveMark == ActiveMarkType.Left) {
                    e.Graphics.DrawLine(penShadowLight, m_LeftPos - m_MarkWidth / 2 - 1, markyoff + m_MarkHeight / 3, m_LeftPos - m_MarkWidth / 2 - 1, markyoff + 2 * m_MarkHeight / 3); // active mark
                    e.Graphics.DrawLine(penShadowDark, m_LeftPos - m_MarkWidth / 2, markyoff + m_MarkHeight / 3, m_LeftPos - m_MarkWidth / 2, markyoff + 2 * m_MarkHeight / 3); // active mark			
                }



                // right mark knob
                m_RMarkPnt[0].X = m_RightPos + m_MarkWidth; m_RMarkPnt[0].Y = markyoff + m_MarkHeight / 3;
                m_RMarkPnt[1].X = m_RightPos; m_RMarkPnt[1].Y = markyoff;
                m_RMarkPnt[2].X = m_RightPos; m_RMarkPnt[2].Y = markyoff + m_MarkHeight;
                m_RMarkPnt[3].X = m_RightPos + m_MarkWidth; m_RMarkPnt[3].Y = markyoff + 2 * m_MarkHeight / 3;
                m_RMarkPnt[4].X = m_RightPos + m_MarkWidth; m_RMarkPnt[4].Y = markyoff;
                e.Graphics.FillPolygon(brushRange, m_RMarkPnt);
                if (m_LeftPos < m_RightPos)
                    e.Graphics.DrawLine(penShadowLight, m_RMarkPnt[1].X - 1, m_RMarkPnt[1].Y + 1, m_RMarkPnt[2].X - 1, m_RMarkPnt[2].Y); // left shadow
                e.Graphics.DrawLine(penShadowDark, m_RMarkPnt[2].X, m_RMarkPnt[2].Y, m_RMarkPnt[3].X, m_RMarkPnt[3].Y); // lower right shadow
                e.Graphics.DrawLine(penShadowDark, m_RMarkPnt[0].X, m_RMarkPnt[0].Y, m_RMarkPnt[1].X, m_RMarkPnt[1].Y); // upper shadow
                e.Graphics.DrawLine(penShadowDark, m_RMarkPnt[0].X, m_RMarkPnt[0].Y + 1, m_RMarkPnt[3].X, m_RMarkPnt[3].Y); // right shadow
                if (m_ActiveMark == ActiveMarkType.Right) {
                    e.Graphics.DrawLine(penShadowLight, m_RightPos + m_MarkWidth / 2 - 1, markyoff + m_MarkHeight / 3, m_RightPos + m_MarkWidth / 2 - 1, markyoff + 2 * m_MarkHeight / 3); // active mark
                    e.Graphics.DrawLine(penShadowDark, m_RightPos + m_MarkWidth / 2, markyoff + m_MarkHeight / 3, m_RightPos + m_MarkWidth / 2, markyoff + 2 * m_MarkHeight / 3); // active mark				
                }

                if (m_MoveLMark) {
                    DrawTooltip(e.Graphics, m_RangeMin, m_LeftPos, tickyoff1 + m_TickHeight);
                }

                if (m_MoveRMark) {
                    DrawTooltip(e.Graphics, m_RangeMax, m_RightPos, tickyoff1 + m_TickHeight);
                }

            } else // vertical bar
			{
                baryoff = (w + m_BarHeight) / 2;
                markyoff = baryoff - m_BarHeight / 2 - m_MarkHeight / 2;
                if (m_OrientationScale == TopBottomOrientation.Bottom) {
                    tickyoff1 = tickyoff2 = baryoff + 2;
                } else if (m_OrientationScale == TopBottomOrientation.Top) {
                    tickyoff1 = tickyoff2 = baryoff - m_BarHeight - 2 - m_TickHeight;
                } else {
                    tickyoff1 = baryoff + 2;
                    tickyoff2 = baryoff - m_BarHeight - 2 - m_TickHeight;
                }

                // total range bar frame			
                e.Graphics.FillRectangle(brushShadowDark, baryoff - m_BarHeight, 0, m_BarHeight, m_ShadowSize);	// top
                e.Graphics.FillRectangle(brushShadowDark, baryoff - m_BarHeight, 0, m_ShadowSize, h - 1);	// left				
                e.Graphics.FillRectangle(brushShadowLight, baryoff, 0, m_ShadowSize, h - 1);	// right
                e.Graphics.FillRectangle(brushShadowLight, baryoff - m_BarHeight, h - m_ShadowSize, m_BarHeight, m_ShadowSize);	// bottom

                // inner region
                e.Graphics.FillRectangle(brushInner, baryoff - m_BarHeight + m_ShadowSize, m_LeftPos, m_BarHeight - 2 * m_ShadowSize, m_RightPos - m_LeftPos);

                // Skala
                if (m_NumAxisDivisions > 1) {
                    dtick = (double)(m_XPosMax - m_XPosMin) / (double)m_NumAxisDivisions;
                    for (int i = 0; i < m_NumAxisDivisions + 1; i++) {
                        tickpos = (int)Math.Round((double)i * dtick);
                        if (m_OrientationScale == TopBottomOrientation.Bottom
                            || m_OrientationScale == TopBottomOrientation.Both)
                            e.Graphics.DrawLine(penShadowDark,
                                tickyoff1,
                                m_MarkWidth + 1 + tickpos,
                                tickyoff1 + m_TickHeight,
                                m_MarkWidth + 1 + tickpos
                                );
                        if (m_OrientationScale == TopBottomOrientation.Top
                            || m_OrientationScale == TopBottomOrientation.Both)
                            e.Graphics.DrawLine(penShadowDark,
                                tickyoff2,
                                m_MarkWidth + 1 + tickpos,
                                tickyoff2 + m_TickHeight,
                                m_MarkWidth + 1 + tickpos
                                );
                    }
                }

                // left(upper) mark knob				
                m_LMarkPnt[0].Y = m_LeftPos - m_MarkWidth; m_LMarkPnt[0].X = markyoff + m_MarkHeight / 3;
                m_LMarkPnt[1].Y = m_LeftPos; m_LMarkPnt[1].X = markyoff;
                m_LMarkPnt[2].Y = m_LeftPos; m_LMarkPnt[2].X = markyoff + m_MarkHeight;
                m_LMarkPnt[3].Y = m_LeftPos - m_MarkWidth; m_LMarkPnt[3].X = markyoff + 2 * m_MarkHeight / 3;
                m_LMarkPnt[4].Y = m_LeftPos - m_MarkWidth; m_LMarkPnt[4].X = markyoff;
                e.Graphics.FillPolygon(brushRange, m_LMarkPnt);
                e.Graphics.DrawLine(penShadowDark, m_LMarkPnt[3].X, m_LMarkPnt[3].Y, m_LMarkPnt[2].X, m_LMarkPnt[2].Y); // right shadow
                e.Graphics.DrawLine(penShadowLight, m_LMarkPnt[0].X - 1, m_LMarkPnt[0].Y, m_LMarkPnt[3].X - 1, m_LMarkPnt[3].Y); // top shadow				
                e.Graphics.DrawLine(penShadowLight, m_LMarkPnt[0].X - 1, m_LMarkPnt[0].Y, m_LMarkPnt[1].X - 1, m_LMarkPnt[1].Y); // left shadow
                if (m_LeftPos < m_RightPos)
                    e.Graphics.DrawLine(penShadowDark, m_LMarkPnt[1].X, m_LMarkPnt[1].Y, m_LMarkPnt[2].X, m_LMarkPnt[2].Y); // lower shadow
                if (m_ActiveMark == ActiveMarkType.Left) {
                    e.Graphics.DrawLine(penShadowLight, markyoff + m_MarkHeight / 3, m_LeftPos - m_MarkWidth / 2, markyoff + 2 * m_MarkHeight / 3, m_LeftPos - m_MarkWidth / 2); // active mark
                    e.Graphics.DrawLine(penShadowDark, markyoff + m_MarkHeight / 3, m_LeftPos - m_MarkWidth / 2 + 1, markyoff + 2 * m_MarkHeight / 3, m_LeftPos - m_MarkWidth / 2 + 1); // active mark			
                }

                // right mark knob
                m_RMarkPnt[0].Y = m_RightPos + m_MarkWidth; m_RMarkPnt[0].X = markyoff + m_MarkHeight / 3;
                m_RMarkPnt[1].Y = m_RightPos; m_RMarkPnt[1].X = markyoff;
                m_RMarkPnt[2].Y = m_RightPos; m_RMarkPnt[2].X = markyoff + m_MarkHeight;
                m_RMarkPnt[3].Y = m_RightPos + m_MarkWidth; m_RMarkPnt[3].X = markyoff + 2 * m_MarkHeight / 3;
                m_RMarkPnt[4].Y = m_RightPos + m_MarkWidth; m_RMarkPnt[4].X = markyoff;
                e.Graphics.FillPolygon(brushRange, m_RMarkPnt);
                e.Graphics.DrawLine(penShadowDark, m_RMarkPnt[2].X, m_RMarkPnt[2].Y, m_RMarkPnt[3].X, m_RMarkPnt[3].Y); // lower right shadow
                e.Graphics.DrawLine(penShadowDark, m_RMarkPnt[0].X, m_RMarkPnt[0].Y, m_RMarkPnt[1].X, m_RMarkPnt[1].Y); // upper shadow
                e.Graphics.DrawLine(penShadowDark, m_RMarkPnt[0].X, m_RMarkPnt[0].Y, m_RMarkPnt[3].X, m_RMarkPnt[3].Y); // right shadow
                if (m_LeftPos < m_RightPos)
                    e.Graphics.DrawLine(penShadowLight, m_RMarkPnt[1].X, m_RMarkPnt[1].Y, m_RMarkPnt[2].X, m_RMarkPnt[2].Y); // left shadow
                if (m_ActiveMark == ActiveMarkType.Right) {
                    e.Graphics.DrawLine(penShadowLight, markyoff + m_MarkHeight / 3, m_RightPos + m_MarkWidth / 2 - 1, markyoff + 2 * m_MarkHeight / 3, m_RightPos + m_MarkWidth / 2 - 1); // active mark
                    e.Graphics.DrawLine(penShadowDark, markyoff + m_MarkHeight / 3, m_RightPos + m_MarkWidth / 2, markyoff + 2 * m_MarkHeight / 3, m_RightPos + m_MarkWidth / 2); // active mark				
                }

                if (m_MoveLMark) {
                    DrawTooltip(e.Graphics, m_RangeMin, m_LeftPos, tickyoff1 + m_TickHeight);
                }

                if (m_MoveRMark) {
                    DrawTooltip(e.Graphics, m_RangeMax, m_RightPos, tickyoff1 + m_TickHeight);
                }
            }

        }


        // mouse down event
        protected override void OnMouseDown(MouseEventArgs e) {
            if (this.Enabled) {
                Rectangle LMarkRect = new Rectangle(
                    Math.Min(m_LMarkPnt[0].X, m_LMarkPnt[1].X),		// X
                    Math.Min(m_LMarkPnt[0].Y, m_LMarkPnt[3].Y),		// Y
                    Math.Abs(m_LMarkPnt[2].X - m_LMarkPnt[0].X),		// width
                    Math.Max(Math.Abs(m_LMarkPnt[0].Y - m_LMarkPnt[3].Y), Math.Abs(m_LMarkPnt[0].Y - m_LMarkPnt[1].Y)));	// height
                Rectangle RMarkRect = new Rectangle(
                    Math.Min(m_RMarkPnt[0].X, m_RMarkPnt[2].X),		// X
                    Math.Min(m_RMarkPnt[0].Y, m_RMarkPnt[1].Y),		// Y
                    Math.Abs(m_RMarkPnt[0].X - m_RMarkPnt[2].X),		// width
                    Math.Max(Math.Abs(m_RMarkPnt[2].Y - m_RMarkPnt[0].Y), Math.Abs(m_RMarkPnt[1].Y - m_RMarkPnt[0].Y)));		// height

                if (LMarkRect.Contains(e.X, e.Y)) {
                    this.Capture = true;
                    m_MoveLMark = true;
                    m_ActiveMark = ActiveMarkType.Left;
                    Invalidate(true);
                }

                if (RMarkRect.Contains(e.X, e.Y)) {
                    this.Capture = true;
                    m_MoveRMark = true;
                    m_ActiveMark = ActiveMarkType.Right;
                    Invalidate(true);
                }
            }
        }


        // mouse up event
        protected override void OnMouseUp(MouseEventArgs e) {
            if (this.Enabled) {
                this.Capture = false;

                m_MoveLMark = false;
                m_MoveRMark = false;

                Invalidate();

                OnRangeChanged(EventArgs.Empty);
            }
        }


        // mouse move event
        protected override void OnMouseMove(MouseEventArgs e) {
            if (this.Enabled) {
                int h = this.Height;
                int w = this.Width;
                double r1 = (double)m_RangeMin.Ticks * (double)w / (double)(m_Maximum.Ticks - m_Minimum.Ticks);
                double r2 = (double)m_RangeMax.Ticks * (double)w / (double)(m_Maximum.Ticks - m_Minimum.Ticks);
                Rectangle LMarkRect = new Rectangle(
                    Math.Min(m_LMarkPnt[0].X, m_LMarkPnt[1].X),		// X
                    Math.Min(m_LMarkPnt[0].Y, m_LMarkPnt[3].Y),		// Y
                    Math.Abs(m_LMarkPnt[2].X - m_LMarkPnt[0].X),		// width
                    Math.Max(Math.Abs(m_LMarkPnt[0].Y - m_LMarkPnt[3].Y), Math.Abs(m_LMarkPnt[0].Y - m_LMarkPnt[1].Y)));	// height
                Rectangle RMarkRect = new Rectangle(
                    Math.Min(m_RMarkPnt[0].X, m_RMarkPnt[2].X),		// X
                    Math.Min(m_RMarkPnt[0].Y, m_RMarkPnt[1].Y),		// Y
                    Math.Abs(m_RMarkPnt[0].X - m_RMarkPnt[2].X),		// width
                    Math.Max(Math.Abs(m_RMarkPnt[2].Y - m_RMarkPnt[0].Y), Math.Abs(m_RMarkPnt[1].Y - m_RMarkPnt[0].Y)));		// height

                if (LMarkRect.Contains(e.X, e.Y) || RMarkRect.Contains(e.X, e.Y)) {
                    if (this.m_OrientationBar == RangeBarOrientation.Horizontal)
                        this.Cursor = Cursors.SizeWE;
                    else
                        this.Cursor = Cursors.SizeNS;
                } else this.Cursor = Cursors.Arrow;

                if (m_MoveLMark) {
                    if (this.m_OrientationBar == RangeBarOrientation.Horizontal)
                        this.Cursor = Cursors.SizeWE;
                    else
                        this.Cursor = Cursors.SizeNS;
                    if (this.m_OrientationBar == RangeBarOrientation.Horizontal)
                        m_LeftPos = e.X;
                    else
                        m_LeftPos = e.Y;
                    if (m_LeftPos < m_XPosMin)
                        m_LeftPos = m_XPosMin;
                    if (m_LeftPos > m_XPosMax)
                        m_LeftPos = m_XPosMax;
                    if (m_RightPos < m_LeftPos)
                        m_RightPos = m_LeftPos;
                    Pos2Range();
                    m_ActiveMark = ActiveMarkType.Left;
                    Invalidate(true);

                    OnRangeChanging(EventArgs.Empty);
                } else if (m_MoveRMark) {
                    if (this.m_OrientationBar == RangeBarOrientation.Horizontal)
                        this.Cursor = Cursors.SizeWE;
                    else
                        this.Cursor = Cursors.SizeNS;
                    if (this.m_OrientationBar == RangeBarOrientation.Horizontal)
                        m_RightPos = e.X;
                    else
                        m_RightPos = e.Y;
                    if (m_RightPos > m_XPosMax)
                        m_RightPos = m_XPosMax;
                    if (m_RightPos < m_XPosMin)
                        m_RightPos = m_XPosMin;
                    if (m_LeftPos > m_RightPos)
                        m_LeftPos = m_RightPos;
                    Pos2Range();
                    m_ActiveMark = ActiveMarkType.Right;
                    Invalidate(true);

                    OnRangeChanging(EventArgs.Empty);
                }
            }
        }


        /// <summary>
        ///  transform pixel position to range position
        /// </summary>
        private void Pos2Range() {
            int w;
            int posw;

            if (this.m_OrientationBar == RangeBarOrientation.Horizontal)
                w = this.Width;
            else
                w = this.Height;
            posw = w - 2 * m_MarkWidth - 2;

            m_RangeMin = m_Minimum + new TimeSpan((long)Math.Round((double)(m_Maximum - m_Minimum).Ticks * (double)(m_LeftPos - m_XPosMin) / (double)posw));
            m_RangeMax = m_Minimum + new TimeSpan((long)Math.Round((double)(m_Maximum - m_Minimum).Ticks * (double)(m_RightPos - m_XPosMin) / (double)posw));
        }


        /// <summary>
        ///  transform range position to pixel position
        /// </summary>
        private void Range2Pos() {
            int w;
            int posw;

            if (this.m_OrientationBar == RangeBarOrientation.Horizontal)
                w = this.Width;
            else
                w = this.Height;
            posw = w - 2 * m_MarkWidth - 2;

            m_LeftPos = m_XPosMin + (int)Math.Round((double)posw * (double)(m_RangeMin - m_Minimum).Ticks / (double)(m_Maximum - m_Minimum).Ticks);
            m_RightPos = m_XPosMin + (int)Math.Round((double)posw * (double)(m_RangeMax - m_Minimum).Ticks / (double)(m_Maximum - m_Minimum).Ticks);
        }


        /// <summary>
        /// method to handle resize event
        /// </summary>
        /// <param name="sender">object that sends event to resize</param>
        /// <param name="e">event parameter</param>
        protected override void OnResize(EventArgs e) {
            Range2Pos();
            Invalidate(true);
        }


        /// <summary>
        /// method to handle key pressed event
        /// </summary>
        /// <param name="sender">object that sends key pressed event</param>
        /// <param name="e">event parameter</param>
        protected override void OnKeyPress(KeyPressEventArgs e) {
            /*if (this.Enabled) {
                if (m_ActiveMark == ActiveMarkType.Left) {
                    if (e.KeyChar == '+') {
                        throw new NotImplementedException();
                        //m_RangeMin++;
                        if (m_RangeMin > m_Maximum)
                            m_RangeMin = m_Maximum;
                        if (m_RangeMax < m_RangeMin)
                            m_RangeMax = m_RangeMin;
                        OnRangeChanged(EventArgs.Empty);
                    } else if (e.KeyChar == '-') {
                        throw new NotImplementedException();
                        //m_RangeMin--;
                        if (m_RangeMin < m_Minimum)
                            m_RangeMin = m_Minimum;
                        OnRangeChanged(EventArgs.Empty);
                    }
                } else if (m_ActiveMark == ActiveMarkType.Right) {
                    if (e.KeyChar == '+') {
                        throw new NotImplementedException();
                        //rangeMax++;
                        if (m_RangeMax > m_Maximum)
                            m_RangeMax = m_Maximum;
                        OnRangeChanged(EventArgs.Empty);
                    } else if (e.KeyChar == '-') {
                        throw new NotImplementedException();
                        //rangeMax--;
                        if (m_RangeMax < m_Minimum)
                            m_RangeMax = m_Minimum;
                        if (m_RangeMax < m_RangeMin)
                            m_RangeMin = m_RangeMax;
                        OnRangeChanged(EventArgs.Empty);
                    }
                }
                Invalidate(true);
            }*/
        }

        protected override void OnSizeChanged(EventArgs e) {
            Invalidate(true);
            Update();
        }

        protected override void OnLeave(EventArgs e) {
            m_ActiveMark = ActiveMarkType.None;
        }


        public event RangeChangedEventHandler RangeChanged; // event handler for range changed
        public event RangeChangedEventHandler RangeChanging; // event handler for range is changing

        public virtual void OnRangeChanged(EventArgs e) {
            if (RangeChanged != null)
                RangeChanged(this, e);
        }

        public virtual void OnRangeChanging(EventArgs e) {
            if (RangeChanging != null)
                RangeChanging(this, e);
        }


    }
}
