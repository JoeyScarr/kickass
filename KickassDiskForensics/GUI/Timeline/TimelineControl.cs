using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace KFA.GUI.Timeline {
    public partial class TimelineControl : UserControl {
        public enum DisplayMode {
            None,
            HourDayMonth,
            DayMonthYear,
            MonthYear
        }

        private static Dictionary<DisplayMode, Granularity> m_Granularities = new Dictionary<DisplayMode, Granularity>() {
                                { DisplayMode.HourDayMonth, Granularity.Hour },
                                { DisplayMode.DayMonthYear, Granularity.Day },
                                { DisplayMode.MonthYear, Granularity.Month } };

        public event EventHandler ActivityPeriodSelected;

        private const int MIN_DAY_COLUMN_WIDTH = 20;
        private const int MIN_MONTH_COLUMN_WIDTH = 40;
        private const int MIN_VERTICAL_UNIT_HEIGHT = 40;

        private int m_HeaderHeight = 18;
        private int m_LeftColumnWidth = 60;
        private Pen m_BorderPen = new Pen(Color.Gray);
        private Pen m_GridPen = new Pen(Color.Silver);
        private Font m_HeaderFont = new Font("Trebuchet MS", 8);
        
        private DisplayMode m_DisplayMode = DisplayMode.None;
        private DateTime m_RangeBegin, m_RangeEnd;
        private TimelineEventStore m_EventStore = null;
        private ActivityPeriod m_CurrentPeriod = null;

        public TimelineControl() {
            InitializeComponent();

            // use double buffering
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            SetRange(DateTime.Now.AddDays(-1), DateTime.Now, false);
        }

        private void OnActivityPeriodSelected(object sender, EventArgs args) {
            m_CurrentPeriod = sender as ActivityPeriod;
            if (ActivityPeriodSelected != null) {
                ActivityPeriodSelected(sender, args);
            }
            Invalidate();
        }

        public void SetRange(DateTime start, DateTime finish, bool reload) {
            if (start <= finish) {
                m_RangeBegin = start;
                m_RangeEnd = finish;
            } else {
                m_RangeBegin = finish;
                m_RangeEnd = start;
            }
            // round begin back to midnight
            m_RangeBegin = m_RangeBegin.Date;
            // round end forward to midnight
            if ((m_RangeEnd - m_RangeEnd.Date).Ticks > 0) {
                m_RangeEnd = m_RangeEnd.Date.AddDays(1);
            }

            UpdateDisplayMode();

            if (m_DisplayMode == DisplayMode.DayMonthYear) {
                // round begin back to nearest month
                m_RangeBegin = new DateTime(m_RangeBegin.Year, m_RangeBegin.Month, 1);
                // round end forward to nearest month
                DateTime endMonth = new DateTime(m_RangeEnd.Year, m_RangeEnd.Month, 1);
                if ((m_RangeEnd - endMonth).Ticks > 0) {
                    m_RangeEnd = endMonth.AddMonths(1);
                }
            } else if (m_DisplayMode == DisplayMode.MonthYear) {
                // round begin back to nearest year
                m_RangeBegin = new DateTime(m_RangeBegin.Year, 1, 1);
                // round end forward to nearest year
                DateTime endYear = new DateTime(m_RangeEnd.Year, 1, 1);
                if ((m_RangeEnd - endYear).Ticks > 0) {
                    m_RangeEnd = endYear.AddYears(1);
                }
            }

            if (reload && EventStore != null) {
                Controls.Clear();
                foreach (ActivityPeriod period in EventStore.GetActivity(m_RangeBegin, m_RangeEnd, m_Granularities[m_DisplayMode])) {
                    Controls.Add(period);
                    period.Click += OnActivityPeriodSelected;
                    switch (m_DisplayMode) {
                        case DisplayMode.HourDayMonth:
                            double hourHeight = (double)MainAreaHeight / 24.0;
                            double dayWidth = (double)MainAreaWidth / (double)Days;
                            period.Width = (int)dayWidth;
                            period.Height = (int)(period.Length.Hours * hourHeight);
                            int day = period.Start.DayOfYear - m_RangeBegin.AddYears(m_RangeBegin.Year - period.Start.Year).DayOfYear;
                            period.Left = LeftColumnWidth + (int)(day * dayWidth);
                            period.Top = HeaderHeight * 2 + (int)(period.Start.Hour * hourHeight);
                            break;
                        case DisplayMode.DayMonthYear:
                            double dayHeight = (double)MainAreaHeight / 31.0;
                            double monthWidth = (double)MainAreaWidth / (double)Months;
                            period.Width = (int)monthWidth;
                            period.Height = (int)(period.Length.Days * dayHeight);
                            int month = period.Start.Month - m_RangeBegin.AddYears(m_RangeBegin.Year - period.Start.Year).Month;
                            period.Left = LeftColumnWidth + (int)(month * monthWidth);
                            period.Top = HeaderHeight * 2 + (int)(period.Start.Day * dayHeight);
                            break;
                    }
                }
            }
            Invalidate();
        }

        private void UpdateDisplayMode() {
            if ((double)MainAreaWidth / (double)Days >= MIN_DAY_COLUMN_WIDTH) {
                m_DisplayMode = DisplayMode.HourDayMonth;
            } else {
                if ((double)MainAreaWidth / (double)Months >= MIN_MONTH_COLUMN_WIDTH) {
                    m_DisplayMode = DisplayMode.DayMonthYear;
                } else {
                    m_DisplayMode = DisplayMode.MonthYear;
                }
            }
        }

        protected override void OnSizeChanged(EventArgs e) {
            SetRange(m_RangeBegin, m_RangeEnd, true);
        }

        public Pen BorderPen {
            get { return m_BorderPen; }
            set { m_BorderPen = value; }
        }

        public int HeaderHeight {
            get { return m_HeaderHeight; }
            set { m_HeaderHeight = value; }
        }

        public int LeftColumnWidth {
            get { return m_LeftColumnWidth; }
            set { m_LeftColumnWidth = value; }
        }

        public int MainAreaWidth {
            get { return Width - LeftColumnWidth; }
        }

        public int TotalHeaderHeight {
            get {
                if (m_DisplayMode == DisplayMode.MonthYear) {
                    return HeaderHeight;
                } else {
                    return HeaderHeight * 2;
                }
            }
        }

        public int MainAreaHeight {
            get { return Height - TotalHeaderHeight; }
        }

        public int Days {
            get { return (int)Math.Ceiling((m_RangeEnd - m_RangeBegin).TotalDays); }
        }

        public int Months {
            get { return (int)Math.Ceiling((m_RangeEnd.Year - m_RangeBegin.Year) * 12
                + m_RangeEnd.Month - m_RangeBegin.Month
                + (double)(m_RangeEnd.Day - m_RangeBegin.Day) / 35.0); }
        }

        public TimelineEventStore EventStore {
            get { return m_EventStore; }
            set {
                m_EventStore = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            e.Graphics.Clear(Color.White);
            if (m_DisplayMode != DisplayMode.None) {
                DateTime begin = m_RangeBegin;
                DateTime end = m_RangeEnd;
                switch (m_DisplayMode) {
                    case DisplayMode.HourDayMonth:
                        double hourHeight = (double)MainAreaHeight / 24.0;
                        double dayWidth = (double)MainAreaWidth / (double)Days;
                        {
                            // draw a row for each hour
                            double prev = 0;
                            double current = hourHeight;
                            StringFormat f = new StringFormat();
                            f.Alignment = StringAlignment.Far;
                            int hour = 12;
                            string suff = "am";
                            for (int i = 0; i < 24; i++) {
                                e.Graphics.DrawRectangle(m_GridPen,
                                    new Rectangle(0, HeaderHeight * 2 + (int)prev,
                                        Width, (int)current - (int)prev));
                                e.Graphics.DrawString(hour + suff, m_HeaderFont, Brushes.Black,
                                    LeftColumnWidth - 2, HeaderHeight * 2 + (int)(i * hourHeight) - 2, f);
                                prev += hourHeight;
                                current += hourHeight;
                                hour++;
                                if (hour == 12) suff = "pm";
                                if (hour >= 13) hour -= 12;
                            }
                        }
                        e.Graphics.DrawLine(BorderPen, new Point(0, 0), new Point(Width, 0));
                        e.Graphics.DrawLine(BorderPen, new Point(0, HeaderHeight), new Point(Width, HeaderHeight));
                        e.Graphics.DrawLine(BorderPen, new Point(0, HeaderHeight * 2), new Point(Width, HeaderHeight * 2)); {
                            // draw a column for each day
                            double prev = 0;
                            double monthPrev = 0;
                            int monthInd = m_RangeBegin.Month;
                            string monthName = m_RangeBegin.ToString("MMMM");
                            double current = dayWidth;
                            StringFormat f = new StringFormat();
                            f.Alignment = StringAlignment.Center;
                            for (int i = 0; i < Days; i++) {
                                e.Graphics.DrawRectangle(m_BorderPen,
                                    new Rectangle(LeftColumnWidth + (int)prev, HeaderHeight,
                                        (int)current - (int)prev, Height - HeaderHeight));
                                DateTime cur = m_RangeBegin.AddDays(i);
                                e.Graphics.DrawString(cur.Day.ToString(), m_HeaderFont,
                                    Brushes.Black, LeftColumnWidth + (int)prev + (int)((float)(current - prev) / 2),
                                    HeaderHeight + 2, f);

                                if (cur.Month != monthInd) {
                                    e.Graphics.DrawString(monthName, m_HeaderFont,
                                        Brushes.Black, LeftColumnWidth + (int)monthPrev + (int)((float)(prev - monthPrev) / 2),
                                        2, f);
                                    e.Graphics.DrawLine(m_BorderPen, LeftColumnWidth + (int)prev, 0,
                                        LeftColumnWidth + (int)prev, HeaderHeight);
                                    monthInd = cur.Month;
                                    monthName = cur.ToString("MMMM");
                                    monthPrev = current;
                                }

                                prev += dayWidth;
                                current += dayWidth;
                            }
                            e.Graphics.DrawString(monthName, m_HeaderFont,
                                        Brushes.Black, LeftColumnWidth + (int)monthPrev + (int)((float)(prev - monthPrev) / 2),
                                        2, f);
                        }
                        if (EventStore != null) {
                            foreach (ActivityPeriod period in Controls) {
                                int day = period.Start.DayOfYear - m_RangeBegin.AddYears(m_RangeBegin.Year - period.Start.Year).DayOfYear;
                                if (day >= 0) {
                                    int width = (int)dayWidth;
                                    int height = (int)(period.Length.Hours * hourHeight);
                                    int left = LeftColumnWidth + (int)(day * dayWidth);
                                    int top = HeaderHeight * 2 + (int)(period.Start.Hour * hourHeight);
                                    Brush fill = Brushes.CornflowerBlue;
                                    if (period.Equals(m_CurrentPeriod)) {
                                        fill = Brushes.DarkBlue;
                                    }
                                    e.Graphics.FillRectangle(fill, left, top, width, height);
                                    e.Graphics.DrawRectangle(Pens.Black, left, top, width, height);
                                }
                            }
                        }
                        break;
                    case DisplayMode.DayMonthYear:
                        
                        double dayHeight = (double)MainAreaHeight / 31.0;
                        double monthWidth = (double)MainAreaWidth / (double)Months;
                        {
                            // draw a row for each day
                            double prev = 0;
                            double current = dayHeight;
                            StringFormat f = new StringFormat();
                            f.Alignment = StringAlignment.Far;
                            int day = 1;
                            for (int i = 0; i < 31; i++) {
                                e.Graphics.DrawRectangle(m_GridPen,
                                    new Rectangle(0, HeaderHeight * 2 + (int)prev,
                                        Width, (int)current - (int)prev));
                                e.Graphics.DrawString(day.ToString(), m_HeaderFont, Brushes.Black,
                                    LeftColumnWidth - 2, HeaderHeight * 2 + (int)(i * dayHeight) - 2, f);
                                prev += dayHeight;
                                current += dayHeight;
                                day++;
                            }
                        }

                        e.Graphics.DrawLine(BorderPen, new Point(0, 0), new Point(Width, 0));
                        e.Graphics.DrawLine(BorderPen, new Point(0, HeaderHeight), new Point(Width, HeaderHeight));
                        e.Graphics.DrawLine(BorderPen, new Point(0, HeaderHeight * 2), new Point(Width, HeaderHeight * 2));
                        {
                            // draw a column for each month
                            double prev = 0;
                            double current = monthWidth;
                            double yearPrev = 0;
                            int year = m_RangeBegin.Year;
                            StringFormat f = new StringFormat();
                            f.Alignment = StringAlignment.Center;
                            for (int i = 0; i < Months; i++) {
                                e.Graphics.DrawRectangle(m_BorderPen,
                                    new Rectangle(LeftColumnWidth + (int)prev, HeaderHeight,
                                        (int)current - (int)prev, Height - HeaderHeight));

                                DateTime cur = m_RangeBegin.AddMonths(i);
                                e.Graphics.DrawString(cur.ToString("MMMM"), m_HeaderFont,
                                    Brushes.Black, LeftColumnWidth + (int)prev + (int)((float)(current - prev) / 2),
                                    HeaderHeight + 2, f);

                                if (cur.Year != year) {
                                    e.Graphics.DrawString(year.ToString(), m_HeaderFont, Brushes.Black,
                                        LeftColumnWidth + (int)yearPrev + (int)((float)(prev - yearPrev) / 2),
                                        2, f);
                                    e.Graphics.DrawLine(m_BorderPen, LeftColumnWidth + (int)prev, 0,
                                        LeftColumnWidth + (int)prev, HeaderHeight);
                                    year = cur.Year;
                                    yearPrev = prev;
                                }

                                prev += monthWidth;
                                current += monthWidth;
                            }
                            e.Graphics.DrawString(year.ToString(), m_HeaderFont, Brushes.Black,
                                    LeftColumnWidth + (int)yearPrev + (int)((float)(prev - yearPrev) / 2),
                                    2, f);
                        }
                        if (EventStore != null) {
                            foreach (ActivityPeriod period in Controls) {
                                int month = period.Start.Month - m_RangeBegin.AddYears(m_RangeBegin.Year - period.Start.Year).Month;
                                if (month >= 0) {
                                    int width = (int)monthWidth;
                                    int height = (int)(period.Length.Days * dayHeight);
                                    int left = LeftColumnWidth + (int)(month * monthWidth);
                                    int top = HeaderHeight * 2 + (int)(period.Start.Day * dayHeight);
                                    Brush fill = Brushes.CornflowerBlue;
                                    if (period.Equals(m_CurrentPeriod)) {
                                        fill = Brushes.DarkBlue;
                                    }
                                    e.Graphics.FillRectangle(fill, left, top, width, height);
                                    e.Graphics.DrawRectangle(Pens.Black, left, top, width, height);
                                }
                            }
                        }

                        break;
                    case DisplayMode.MonthYear:

                        break;
                }
                e.Graphics.DrawRectangle(BorderPen, new Rectangle(0, 0, Width - 1, Height - 1));
                e.Graphics.DrawLine(BorderPen, new Point(LeftColumnWidth, 0), new Point(LeftColumnWidth, Height));
            }
        }

        protected override void OnResize(EventArgs e) {
            UpdateDisplayMode();
            base.OnResize(e);
        }
    }
}
