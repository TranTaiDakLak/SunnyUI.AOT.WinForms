using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("TextChanged")]
    [DefaultProperty("Text")]

    public partial class UITextBox : UIPanel, ISymbol, IToolTip
    {
        private readonly UIEdit edit = new UIEdit();
        private readonly UIScrollBar bar = new UIScrollBar();
        private readonly UISymbolButton btn = new UISymbolButton();

        public UITextBox()
        {
            try
            {
                InitializeComponent();
                InitializeComponentEnd = true;
                SetStyleFlags(true, true, true);

                ShowText = false;
                MinimumSize = new Size(1, 16);

                edit.AutoSize = false;
                edit.Top = (Height - edit.Height) / 2;
                edit.Left = 4;
                edit.Width = Width - 8;
                edit.Text = String.Empty;
                edit.BorderStyle = BorderStyle.None;

                // Gắn sự kiện, mỗi sự kiện đều bọc try-catch
                edit.TextChanged += (s, e) => SafeInvoke(() => Edit_TextChanged(s, e));
                edit.KeyDown += (s, e) => SafeInvoke(() => Edit_OnKeyDown(s, e));
                edit.KeyUp += (s, e) => SafeInvoke(() => Edit_OnKeyUp(s, e));
                edit.KeyPress += (s, e) => SafeInvoke(() => Edit_OnKeyPress(s, e));
                edit.MouseEnter += (s, e) => SafeInvoke(() => Edit_MouseEnter(s, e));
                edit.Click += (s, e) => SafeInvoke(() => Edit_Click(s, e));
                edit.DoubleClick += (s, e) => SafeInvoke(() => Edit_DoubleClick(s, e));
                edit.Leave += (s, e) => SafeInvoke(() => Edit_Leave(s, e));
                edit.Validated += (s, e) => SafeInvoke(() => Edit_Validated(s, e));
                edit.Validating += (s, e) => SafeInvoke(() => Edit_Validating(s, e));
                edit.GotFocus += (s, e) => SafeInvoke(() => Edit_GotFocus(s, e));
                edit.LostFocus += (s, e) => SafeInvoke(() => Edit_LostFocus(s, e));
                edit.MouseLeave += (s, e) => SafeInvoke(() => Edit_MouseLeave(s, e));
                edit.MouseWheel += (s, e) => SafeInvoke(() => Edit_MouseWheel(s, e));
                edit.MouseDown += (s, e) => SafeInvoke(() => Edit_MouseDown(s, e));
                edit.MouseUp += (s, e) => SafeInvoke(() => Edit_MouseUp(s, e));
                edit.MouseMove += (s, e) => SafeInvoke(() => Edit_MouseMove(s, e));
                edit.SelectionChanged += (s, e) => SafeInvoke(() => Edit_SelectionChanged(s, e));
                edit.MouseClick += (s, e) => SafeInvoke(() => Edit_MouseClick(s, e));
                edit.MouseDoubleClick += (s, e) => SafeInvoke(() => Edit_MouseDoubleClick(s, e));
                edit.SizeChanged += (s, e) => SafeInvoke(() => Edit_SizeChanged(s, e));
                edit.FontChanged += (s, e) => SafeInvoke(() => Edit_FontChanged(s, e));

                btn.Parent = this;
                btn.Visible = false;
                btn.Text = "";
                btn.Symbol = 361761;
                btn.Top = 1;
                btn.Height = 25;
                btn.Width = 29;
                btn.BackColor = Color.Transparent;
                btn.Click += (s, e) => SafeInvoke(() => Btn_Click(s, e));
                btn.Radius = 3;
                btn.SymbolOffset = new Point(-1, 1);

                edit.Invalidate();
                Controls.Add(edit);
                fillColor = Color.White;

                bar.Parent = this;
                bar.Dock = DockStyle.None;
                bar.Visible = false;
                bar.ValueChanged += (s, e) => SafeInvoke(() => Bar_ValueChanged(s, e));
                bar.MouseEnter += (s, e) => SafeInvoke(() => Bar_MouseEnter(s, e));
                TextAlignment = ContentAlignment.MiddleLeft;

                lastEditHeight = edit.Height;
                Width = 150;
                Height = 29;

                editCursor = Cursor;
                TextAlignmentChange += (s, align) => SafeInvoke(() => UITextBox_TextAlignmentChange(s, align));
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        #region SafeInvoke Helper
        private void SafeInvoke(Action action)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        private T SafeInvoke<T>(Func<T> func, T defaultValue = default)
        {
            try
            {
                return func != null ? func() : defaultValue;
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
                return defaultValue;
            }
        }
        #endregion

        #region IToolTip Implementation
        /// <summary>
        /// Thực hiện hàm của interface IToolTip
        /// </summary>
        /// <returns>Trả về Control để hiển thị tooltip</returns>
        public Control ExToolTipControl()
        {
            try
            {
                return edit;
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
                return null;
            }
        }
        #endregion

        #region Thuộc tính ShowButton
        private bool showButton = false;

        [DefaultValue(false), Category("SunnyUI"), Description("Hiển thị nút Button bên phải")]
        public bool ShowButton
        {
            get => showButton;
            set
            {
                try
                {
                    // Nếu đang ở chế độ multiline, không cho hiển thị button
                    showButton = !multiline && value;
                    if (btn.IsValid()) btn.Visible = showButton;
                    SizeChange();
                }
                catch (Exception ex)
                {
                    SunnyLog.Logs(ex);
                }
            }
        }
        #endregion
        [Browsable(false)]
        public override string[] FormTranslatorProperties { get; }

        #region Sự kiện chuột (chuẩn hoá)
        private void Edit_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                MouseUp?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        private void Edit_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                MouseDown?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        private void Edit_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                MouseMove?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        private void Edit_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                MouseLeave?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }
        #endregion

        private void Edit_FontChanged(object sender, EventArgs e)
        {
            try
            {
                if (!edit.Multiline)
                {
                    int height = edit.Font.Height;
                    edit.AutoSize = false;
                    edit.Height = height + 2;
                    SizeChange();
                }
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        int lastEditHeight = -1;
        private void Edit_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (lastEditHeight != edit.Height)
                {
                    lastEditHeight = edit.Height;
                    SizeChange();
                }
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        public override void SetDPIScale()
        {
            try
            {
                base.SetDPIScale();
                if (DesignMode) return;
                if (!UIDPIScale.NeedSetDPIFont()) return;
                edit.SetDPIScale();
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        [Description("开启后可响应某些触屏的点击事件"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool TouchPressClick
        {
            get => SafeInvoke(() => edit.TouchPressClick, false);
            set => SafeInvoke(() => edit.TouchPressClick = value);
        }

        private bool _autoSize = false;
        public new bool AutoSize
        {
            get => _autoSize;
            set
            {
                try
                {
                    _autoSize = value;
                    SizeChange();
                }
                catch (Exception ex)
                {
                    SunnyLog.Logs(ex);
                }
            }
        }

        private UIButton tipsBtn;
        public void SetTipsText(ToolTip toolTip, string text)
        {
            try
            {
                if (tipsBtn == null)
                {
                    tipsBtn = new UIButton();
                    tipsBtn.Cursor = System.Windows.Forms.Cursors.Hand;
                    tipsBtn.Size = new System.Drawing.Size(6, 6);
                    tipsBtn.Style = Sunny.UI.UIStyle.Red;
                    tipsBtn.StyleCustomMode = true;
                    tipsBtn.Text = "";
                    tipsBtn.Click += (s, e) => SafeInvoke(() => TipsBtn_Click(s, e));

                    Controls.Add(tipsBtn);
                    tipsBtn.Location = new System.Drawing.Point(Width - 8, 2);
                    tipsBtn.BringToFront();
                }

                toolTip.SetToolTip(tipsBtn, text);
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        public event EventHandler TipsClick;
        private void TipsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                TipsClick?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        public void CloseTips()
        {
            try
            {
                if (tipsBtn != null)
                {
                    tipsBtn.Click -= (s, e) => SafeInvoke(() => TipsBtn_Click(s, e));
                    tipsBtn.Dispose();
                    tipsBtn = null;
                }
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        public new event EventHandler MouseDoubleClick;
        public new event EventHandler MouseClick;

        private void Edit_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                MouseDoubleClick?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        private void Edit_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                MouseClick?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        private int scrollBarWidth = 0;

        [DefaultValue(0), Category("SunnyUI"), Description("垂直滚动条宽度，最小为原生滚动条宽度")]
        public int ScrollBarWidth
        {
            get => scrollBarWidth;
            set
            {
                try
                {
                    scrollBarWidth = value;
                    SetScrollInfo();
                }
                catch (Exception ex)
                {
                    SunnyLog.Logs(ex);
                }
            }
        }

        private int scrollBarHandleWidth = 6;

        [DefaultValue(6), Category("SunnyUI"), Description("垂直滚动条滑块宽度，最小为原生滚动条宽度")]
        public int ScrollBarHandleWidth
        {
            get => scrollBarHandleWidth;
            set
            {
                try
                {
                    scrollBarHandleWidth = value;
                    if (bar != null) bar.FillWidth = value;
                }
                catch (Exception ex)
                {
                    SunnyLog.Logs(ex);
                }
            }
        }

        private void Edit_SelectionChanged(object sender, UITextBoxSelectionArgs e)
        {
            try
            {
                SelectionChanged?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        public event OnSelectionChanged SelectionChanged;

        public void SetButtonToolTip(ToolTip toolTip, string tipText)
        {
            try
            {
                toolTip.SetToolTip(btn, tipText);
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        protected override void OnContextMenuStripChanged(EventArgs e)
        {
            try
            {
                base.OnContextMenuStripChanged(e);
                if (edit != null) edit.ContextMenuStrip = ContextMenuStrip;
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        private void Edit_LostFocus(object sender, EventArgs e)
        {
            try
            {
                LostFocus?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        private void Edit_GotFocus(object sender, EventArgs e)
        {
            try
            {
                GotFocus?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        private void Edit_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Validating?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        public new event MouseEventHandler MouseDown;
        public new event MouseEventHandler MouseUp;
        public new event MouseEventHandler MouseMove;
        public new event EventHandler GotFocus;
        public new event EventHandler LostFocus;
        public new event CancelEventHandler Validating;
        public new event EventHandler Validated;
        public new event EventHandler MouseLeave;
        public new event EventHandler DoubleClick;
        public new event EventHandler Click;
        [Browsable(true)]
        public new event EventHandler TextChanged;
        public new event KeyEventHandler KeyDown;
        public new event KeyEventHandler KeyUp;
        public new event KeyPressEventHandler KeyPress;
        public new event EventHandler Leave;

        private void Edit_Validated(object sender, EventArgs e)
        {
            try
            {
                Validated?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        public new void Focus()
        {
            try
            {
                base.Focus();
                edit.Focus();
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        [Browsable(false)]
        public UIEdit TextBox => edit;

        private void Edit_Leave(object sender, EventArgs e)
        {
            try
            {
                Leave?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            try
            {
                base.OnEnabledChanged(e);
                edit.BackColor = GetFillColor();

                edit.Visible = true;
                edit.Enabled = Enabled;
                if (!Enabled)
                {
                    if (NeedDrawDisabledText) edit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        private bool NeedDrawDisabledText => !Enabled && StyleCustomMode && (ForeDisableColor != Color.FromArgb(109, 109, 103) || FillDisableColor != Color.FromArgb(244, 244, 244));

        public override bool Focused => SafeInvoke(() => edit.Focused, false);

        [DefaultValue(false)]
        [Description("激活时选中全部文字"), Category("SunnyUI")]
        public bool FocusedSelectAll
        {
            get => SafeInvoke(() => edit.FocusedSelectAll, false);
            set => SafeInvoke(() => edit.FocusedSelectAll = value);
        }

        private void UITextBox_TextAlignmentChange(object sender, ContentAlignment alignment)
        {
            try
            {
                if (edit == null) return;
                if (alignment == ContentAlignment.TopLeft || alignment == ContentAlignment.MiddleLeft || alignment == ContentAlignment.BottomLeft)
                    edit.TextAlign = HorizontalAlignment.Left;

                if (alignment == ContentAlignment.TopCenter || alignment == ContentAlignment.MiddleCenter || alignment == ContentAlignment.BottomCenter)
                    edit.TextAlign = HorizontalAlignment.Center;

                if (alignment == ContentAlignment.TopRight || alignment == ContentAlignment.MiddleRight || alignment == ContentAlignment.BottomRight)
                    edit.TextAlign = HorizontalAlignment.Right;
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        private void Edit_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DoubleClick?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            try
            {
                Click?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        protected override void OnCursorChanged(EventArgs e)
        {
            try
            {
                base.OnCursorChanged(e);
                edit.Cursor = Cursor;
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        private Cursor editCursor;

        private void Bar_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                editCursor = Cursor;
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        private void Edit_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                Cursor = editCursor;
                if (FocusedSelectAll)
                {
                    SelectAll();
                }
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        private void Edit_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                OnMouseWheel(e);
                if (bar != null && bar.Visible && edit != null)
                {
                    var si = ScrollBarInfo.GetInfo(edit.Handle);
                    if (e.Delta > 10)
                    {
                        if (si.nPos > 0)
                        {
                            ScrollBarInfo.ScrollUp(edit.Handle);
                        }
                    }
                    else if (e.Delta < -10)
                    {
                        if (si.nPos < si.ScrollMax)
                        {
                            ScrollBarInfo.ScrollDown(edit.Handle);
                        }
                    }
                }
                SetScrollInfo();
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        private void Bar_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (edit != null)
                {
                    ScrollBarInfo.SetScrollValue(edit.Handle, bar.Value);
                }
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        private bool multiline = false;

        [DefaultValue(false)]
        public bool Multiline
        {
            get => multiline;
            set
            {
                try
                {
                    multiline = value;
                    edit.Multiline = value;
                    if (value && Type != UIEditType.String)
                    {
                        Type = UIEditType.String;
                    }
                    SizeChange();
                }
                catch (Exception ex)
                {
                    SunnyLog.Logs(ex);
                }
            }
        }

        private bool showScrollBar;

        [DefaultValue(false)]
        [Description("显示垂直滚动条"), Category("SunnyUI")]
        public bool ShowScrollBar
        {
            get => showScrollBar;
            set
            {
                try
                {
                    value = value && Multiline;
                    showScrollBar = value;
                    if (value)
                    {
                        edit.ScrollBars = ScrollBars.Vertical;
                        bar.Visible = true;
                    }
                    else
                    {
                        edit.ScrollBars = ScrollBars.None;
                        bar.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    SunnyLog.Logs(ex);
                }
            }
        }

        [DefaultValue(true)]
        public bool WordWarp
        {
            get => SafeInvoke(() => edit.WordWrap, true);
            set => SafeInvoke(() => edit.WordWrap = value);
        }

        public void Select(int start, int length)
        {
            try
            {
                edit.Focus();
                edit.Select(start, length);
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        public void ScrollToCaret()
        {
            try
            {
                edit.ScrollToCaret();
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        private void Edit_OnKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                KeyPress?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        private void Edit_OnKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DoEnter?.Invoke(this, e);
                }
                KeyDown?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        public event EventHandler DoEnter;

        private void Edit_OnKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                KeyUp?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        [DefaultValue(null)]
        [Description("水印文字"), Category("SunnyUI")]
        public string Watermark
        {
            get => SafeInvoke(() => edit.Watermark, "");
            set => SafeInvoke(() => edit.Watermark = value);
        }

        [DefaultValue(typeof(Color), "Gray")]
        [Description("水印文字颜色"), Category("SunnyUI")]
        public Color WatermarkColor
        {
            get => SafeInvoke(() => edit.WaterMarkColor, Color.Gray);
            set => SafeInvoke(() => edit.WaterMarkColor = value);
        }

        [DefaultValue(typeof(Color), "Gray")]
        [Description("水印文字激活颜色"), Category("SunnyUI")]
        public Color WatermarkActiveColor
        {
            get => SafeInvoke(() => edit.WaterMarkActiveForeColor, Color.Gray);
            set => SafeInvoke(() => edit.WaterMarkActiveForeColor = value);
        }

        public void SelectAll()
        {
            try
            {
                edit.Focus();
                edit.SelectAll();
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        internal void CheckMaxMin()
        {
            try
            {
                edit.CheckMaxMin();
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        private void Edit_TextChanged(object s, EventArgs e)
        {
            try
            {
                if (IsDisposed) return;
                TextChanged?.Invoke(this, e);
                SetScrollInfo();
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            try
            {
                base.OnFontChanged(e);
                if (DefaultFontSize < 0 && edit != null)
                {
                    edit.Font = this.Font;
                }
                Invalidate();
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            try
            {
                base.OnSizeChanged(e);
                SizeChange();
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        public void SetScrollInfo()
        {
            try
            {
                if (bar == null) return;
                var si = ScrollBarInfo.GetInfo(edit.Handle);
                if (si.ScrollMax > 0)
                {
                    bar.Maximum = si.ScrollMax;
                    bar.Value = si.nPos;
                }
                else
                {
                    bar.Maximum = si.ScrollMax;
                }
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        protected override void OnRadiusChanged(int value)
        {
            try
            {
                base.OnRadiusChanged(value);
                SizeChange();
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        private void SizeChange()
        {
            try
            {
                if (!InitializeComponentEnd) return;
                if (edit == null) return;
                if (btn == null) return;

                if (!multiline)
                {
                    // Đơn dòng
                    if (Dock == DockStyle.None && AutoSize)
                    {
                        if (Height != edit.Height + 5)
                            Height = edit.Height + 5;
                    }

                    if (edit.Top != (Height - edit.Height) / 2 + 1)
                    {
                        edit.Top = (Height - edit.Height) / 2 + 1;
                    }

                    int added = Radius <= 5 ? 0 : (Radius - 5) / 2;

                    if (icon == null && Symbol == 0)
                    {
                        edit.Left = 4;
                        edit.Width = Width - 8;
                        edit.Left = edit.Left + added;
                        edit.Width = edit.Width - added * 2;
                    }
                    else
                    {
                        if (icon != null)
                        {
                            edit.Left = 4 + iconSize;
                            edit.Width = Width - 8 - iconSize - added;
                        }
                        else if (Symbol > 0)
                        {
                            edit.Left = 4 + SymbolSize;
                            edit.Width = Width - 8 - SymbolSize - added;
                        }
                    }

                    btn.Left = Width - 2 - ButtonWidth - added;
                    btn.Top = 2;
                    btn.Height = Height - 4;

                    if (ShowButton)
                    {
                        edit.Width = edit.Width - btn.Width - 3 - added;
                    }

                    if (tipsBtn != null)
                    {
                        if (ShowButton)
                            tipsBtn.Location = new System.Drawing.Point(Width - btn.Width - 10 - added, 2);
                        else
                            tipsBtn.Location = new System.Drawing.Point(Width - 8 - added, 2);
                    }
                }
                else
                {
                    btn.Visible = false;
                    edit.Top = 3;
                    edit.Height = Height - 6;
                    edit.Left = 4;
                    edit.Width = Width - 8;

                    int barWidth = Math.Max(ScrollBarInfo.VerticalScrollBarWidth() + 2, ScrollBarWidth);
                    bar.Top = 2;
                    bar.Width = barWidth + 1;
                    bar.Left = Width - barWidth - 3;
                    bar.Height = Height - 4;
                    bar.BringToFront();

                    SetScrollInfo();
                }
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            try
            {
                base.OnGotFocus(e);
                edit.Focus();
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        public void Clear()
        {
            try
            {
                edit.Clear();
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        [DefaultValue('\0')]
        [Description("密码掩码"), Category("SunnyUI")]
        public char PasswordChar
        {
            get => SafeInvoke(() => edit.PasswordChar, '\0');
            set => SafeInvoke(() => edit.PasswordChar = value);
        }

        private bool isReadOnly = false;
        [DefaultValue(false)]
        [Description("是否只读"), Category("SunnyUI")]
        public bool ReadOnly
        {
            get => isReadOnly;
            set
            {
                try
                {
                    isReadOnly = value;
                    edit.ReadOnly = value;
                    edit.BackColor = GetFillColor();
                    Invalidate();
                }
                catch (Exception ex)
                {
                    SunnyLog.Logs(ex);
                }
            }
        }

        [Description("输入类型"), Category("SunnyUI")]
        [DefaultValue(UIEditType.String)]
        public UIEditType Type
        {
            get => SafeInvoke(() => edit.Type, UIEditType.String);
            set => SafeInvoke(() => edit.Type = value);
        }

        [Description("当InputType为数字类型时，能输入的最大值。"), Category("SunnyUI")]
        [DefaultValue(2147483647D)]
        public double Maximum
        {
            get => SafeInvoke(() => edit.MaxValue, 2147483647D);
            set => SafeInvoke(() => edit.MaxValue = value);
        }

        [Description("当InputType为数字类型时，能输入的最小值。"), Category("SunnyUI")]
        [DefaultValue(-2147483648D)]
        public double Minimum
        {
            get => SafeInvoke(() => edit.MinValue, -2147483648D);
            set => SafeInvoke(() => edit.MinValue = value);
        }

        [DefaultValue(0.00)]
        [Description("浮点返回值"), Category("SunnyUI")]
        public double DoubleValue
        {
            get => SafeInvoke(() => edit.DoubleValue, 0.00);
            set => SafeInvoke(() => edit.DoubleValue = value);
        }

        [DefaultValue(0)]
        [Description("整型返回值"), Category("SunnyUI")]
        public int IntValue
        {
            get => SafeInvoke(() => edit.IntValue, 0);
            set => SafeInvoke(() => edit.IntValue = value);
        }

        [Description("文本返回值"), Category("SunnyUI")]
        [Browsable(true)]
        [DefaultValue("")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public override string Text
        {
            get => SafeInvoke(() => edit.Text, "");
            set => SafeInvoke(() => edit.Text = value);
        }

        [Description("浮点数，显示文字小数位数"), Category("SunnyUI")]
        [DefaultValue(2)]
        public int DecimalPlaces
        {
            get => SafeInvoke(() => edit.DecLength, 2);
            set => SafeInvoke(() => edit.DecLength = Math.Max(value, 0));
        }

        [DefaultValue(false)]
        [Description("整型或浮点输入时，是否可空显示"), Category("SunnyUI")]
        public bool CanEmpty
        {
            get => SafeInvoke(() => edit.CanEmpty, false);
            set => SafeInvoke(() => edit.CanEmpty = value);
        }

        public void Empty()
        {
            try
            {
                if (edit.CanEmpty)
                    edit.Text = "";
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        public bool IsEmpty => SafeInvoke(() => edit.Text == "", true);

        protected override void OnMouseDown(MouseEventArgs e)
        {
            try
            {
                ActiveControl = edit;
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        [DefaultValue(32767)]
        public int MaxLength
        {
            get => SafeInvoke(() => edit.MaxLength, 32767);
            set => SafeInvoke(() => edit.MaxLength = Math.Max(value, 1));
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            try
            {
                base.SetStyleColor(uiColor);

                fillColor = uiColor.EditorBackColor;
                foreColor = UIFontColor.Primary;
                edit.BackColor = GetFillColor();
                edit.ForeColor = GetForeColor();
                edit.ForeDisableColor = uiColor.ForeDisableColor;

                if (bar != null && bar.Style == UIStyle.Inherited)
                {
                    bar.ForeColor = uiColor.PrimaryColor;
                    bar.HoverColor = uiColor.ButtonFillHoverColor;
                    bar.PressColor = uiColor.ButtonFillPressColor;
                    bar.FillColor = fillColor;
                    scrollBarColor = uiColor.PrimaryColor;
                    scrollBarBackColor = fillColor;
                }

                if (btn != null && btn.Style == UIStyle.Inherited)
                {
                    btn.ForeColor = uiColor.ButtonForeColor;
                    btn.FillColor = uiColor.ButtonFillColor;
                    btn.RectColor = uiColor.RectColor;

                    btn.FillHoverColor = uiColor.ButtonFillHoverColor;
                    btn.RectHoverColor = uiColor.ButtonRectHoverColor;
                    btn.ForeHoverColor = uiColor.ButtonForeHoverColor;

                    btn.FillPressColor = uiColor.ButtonFillPressColor;
                    btn.RectPressColor = uiColor.ButtonRectPressColor;
                    btn.ForePressColor = uiColor.ButtonForePressColor;
                }
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        [DefaultValue(true), Description("滚动条主题样式"), Category("SunnyUI")]
        public bool ScrollBarStyleInherited
        {
            get => SafeInvoke(() => bar != null && bar.Style == UIStyle.Inherited, true);
            set
            {
                try
                {
                    if (value)
                    {
                        if (bar != null) bar.Style = UIStyle.Inherited;
                        scrollBarColor = UIStyles.Blue.PrimaryColor;
                        scrollBarBackColor = UIStyles.Blue.EditorBackColor;
                    }
                }
                catch (Exception ex)
                {
                    SunnyLog.Logs(ex);
                }
            }
        }

        protected override void SetForeDisableColor(Color color)
        {
            try
            {
                base.SetForeDisableColor(color);
                edit.ForeDisableColor = color;
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        private Color scrollBarColor = Color.FromArgb(80, 160, 255);

        [Description("滚动条填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color ScrollBarColor
        {
            get => scrollBarColor;
            set
            {
                try
                {
                    scrollBarColor = value;
                    bar.HoverColor = bar.PressColor = bar.ForeColor = value;
                    bar.Style = UIStyle.Custom;
                    Invalidate();
                }
                catch (Exception ex)
                {
                    SunnyLog.Logs(ex);
                }
            }
        }

        private Color scrollBarBackColor = Color.White;

        [Description("滚动条背景颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "White")]
        public Color ScrollBarBackColor
        {
            get => scrollBarBackColor;
            set
            {
                try
                {
                    scrollBarBackColor = value;
                    bar.FillColor = value;
                    bar.Style = UIStyle.Custom;
                    Invalidate();
                }
                catch (Exception ex)
                {
                    SunnyLog.Logs(ex);
                }
            }
        }

        protected override void AfterSetForeColor(Color color)
        {
            try
            {
                base.AfterSetForeColor(color);
                edit.ForeColor = GetForeColor();
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        protected override void AfterSetFillColor(Color color)
        {
            try
            {
                base.AfterSetFillColor(color);
                edit.BackColor = GetFillColor();
                bar.FillColor = color;
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        protected override void AfterSetFillReadOnlyColor(Color color)
        {
            try
            {
                base.AfterSetFillReadOnlyColor(color);
                edit.BackColor = GetFillColor();
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        protected override void AfterSetForeReadOnlyColor(Color color)
        {
            try
            {
                base.AfterSetForeReadOnlyColor(color);
                edit.ForeColor = GetForeColor();
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        public enum UIEditType
        {
            String,
            Integer,
            Double
        }

        [DefaultValue(false)]
        public bool AcceptsReturn
        {
            get => SafeInvoke(() => edit.AcceptsReturn, false);
            set => SafeInvoke(() => edit.AcceptsReturn = value);
        }

        [DefaultValue(AutoCompleteMode.None)]
        public AutoCompleteMode AutoCompleteMode
        {
            get => SafeInvoke(() => edit.AutoCompleteMode, AutoCompleteMode.None);
            set => SafeInvoke(() => edit.AutoCompleteMode = value);
        }

        [DefaultValue(AutoCompleteSource.None)]
        public AutoCompleteSource AutoCompleteSource
        {
            get => SafeInvoke(() => edit.AutoCompleteSource, AutoCompleteSource.None);
            set => SafeInvoke(() => edit.AutoCompleteSource = value);
        }

        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get => SafeInvoke(() => edit.AutoCompleteCustomSource, null) ?? new AutoCompleteStringCollection();
            set => SafeInvoke(() => edit.AutoCompleteCustomSource = value);
        }

        [DefaultValue(CharacterCasing.Normal)]
        public CharacterCasing CharacterCasing
        {
            get => SafeInvoke(() => edit.CharacterCasing, CharacterCasing.Normal);
            set => SafeInvoke(() => edit.CharacterCasing = value);
        }

        public void Paste(string text)
        {
            try
            {
                edit.Paste(text);
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        internal class TextBoxAutoCompleteSourceConverter : EnumConverter
        {
            public TextBoxAutoCompleteSourceConverter(Type type) : base(type) { }

            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                try
                {
                    StandardValuesCollection values = base.GetStandardValues(context);
                    ArrayList list = new ArrayList();
                    int count = values.Count;
                    for (int i = 0; i < count; i++)
                    {
                        string currentItemText = values[i].ToString();
                        if (currentItemText != null && !currentItemText.Equals("ListItems"))
                        {
                            list.Add(values[i]);
                        }
                    }
                    return new StandardValuesCollection(list);
                }
                catch (Exception ex)
                {
                    SunnyLog.Logs(ex);
                    return base.GetStandardValues(context);
                }
            }
        }

        [DefaultValue(false)]
        public bool AcceptsTab
        {
            get => SafeInvoke(() => edit.AcceptsTab, false);
            set => SafeInvoke(() => edit.AcceptsTab = value);
        }

        [DefaultValue(false)]
        public bool EnterAsTab
        {
            get => SafeInvoke(() => edit.EnterAsTab, false);
            set => SafeInvoke(() => edit.EnterAsTab = value);
        }

        [DefaultValue(true)]
        public bool ShortcutsEnabled
        {
            get => SafeInvoke(() => edit.ShortcutsEnabled, true);
            set => SafeInvoke(() => edit.ShortcutsEnabled = value);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanUndo
        {
            get => SafeInvoke(() => edit.CanUndo, false);
        }

        [DefaultValue(true)]
        public bool HideSelection
        {
            get => SafeInvoke(() => edit.HideSelection, true);
            set => SafeInvoke(() => edit.HideSelection = value);
        }

        public string[] Lines
        {
            get => SafeInvoke(() => edit.Lines, null) ?? Array.Empty<string>();
            set => SafeInvoke(() => edit.Lines = value);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Modified
        {
            get => SafeInvoke(() => edit.Modified, false);
            set => SafeInvoke(() => edit.Modified = value);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PreferredHeight
        {
            get => SafeInvoke(() => edit.PreferredHeight, edit.Height);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedText
        {
            get => SafeInvoke(() => edit.SelectedText, "");
            set => SafeInvoke(() => edit.SelectedText = value);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionLength
        {
            get => SafeInvoke(() => edit.SelectionLength, 0);
            set => SafeInvoke(() => edit.SelectionLength = value);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart
        {
            get => SafeInvoke(() => edit.SelectionStart, 0);
            set => SafeInvoke(() => edit.SelectionStart = value);
        }

        [Browsable(false)]
        public int TextLength => SafeInvoke(() => edit.TextLength, 0);

        public void AppendText(string text)
        {
            try
            {
                edit.AppendText(text);
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        public void ClearUndo()
        {
            try
            {
                edit.ClearUndo();
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        public void Copy()
        {
            try
            {
                edit.Copy();
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        public void Cut()
        {
            try
            {
                edit.Cut();
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        public void Paste()
        {
            try
            {
                edit.Paste();
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        public char GetCharFromPosition(Point pt)
        {
            return SafeInvoke(() => edit.GetCharFromPosition(pt), '\0');
        }

        public int GetCharIndexFromPosition(Point pt)
        {
            return SafeInvoke(() => edit.GetCharIndexFromPosition(pt), 0);
        }

        public int GetLineFromCharIndex(int index)
        {
            return SafeInvoke(() => edit.GetLineFromCharIndex(index), 0);
        }

        public Point GetPositionFromCharIndex(int index)
        {
            return SafeInvoke(() => edit.GetPositionFromCharIndex(index), Point.Empty);
        }

        public int GetFirstCharIndexFromLine(int lineNumber)
        {
            return SafeInvoke(() => edit.GetFirstCharIndexFromLine(lineNumber), 0);
        }

        public int GetFirstCharIndexOfCurrentLine()
        {
            return SafeInvoke(() => edit.GetFirstCharIndexOfCurrentLine(), 0);
        }

        public void DeselectAll()
        {
            try
            {
                edit.DeselectAll();
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        public void Undo()
        {
            try
            {
                edit.Undo();
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        private Image icon;
        [Description("图标"), Category("SunnyUI")]
        [DefaultValue(null)]
        public Image Icon
        {
            get => icon;
            set
            {
                try
                {
                    icon = value;
                    SizeChange();
                    Invalidate();
                }
                catch (Exception ex)
                {
                    SunnyLog.Logs(ex);
                }
            }
        }

        private int iconSize = 24;
        [Description("图标大小(方形)"), Category("SunnyUI"), DefaultValue(24)]
        public int IconSize
        {
            get => iconSize;
            set
            {
                try
                {
                    iconSize = Math.Min(UIGlobal.EditorMinHeight, value);
                    SizeChange();
                    Invalidate();
                }
                catch (Exception ex)
                {
                    SunnyLog.Logs(ex);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                base.OnPaint(e);
                if (multiline) return;

                if (icon != null)
                {
                    e.Graphics.DrawImage(icon, new Rectangle(4, (Height - iconSize) / 2, iconSize, iconSize),
                        new Rectangle(0, 0, icon.Width, icon.Height), GraphicsUnit.Pixel);
                }
                else if (Symbol != 0)
                {
                    e.Graphics.DrawFontImage(Symbol, SymbolSize, SymbolColor,
                        new Rectangle(4 + symbolOffset.X, (Height - SymbolSize) / 2 + 1 + symbolOffset.Y, SymbolSize, SymbolSize),
                        SymbolOffset.X, SymbolOffset.Y, SymbolRotate);
                }

                if (Text.IsValid() && NeedDrawDisabledText)
                {
                    string text = Text;
                    if (PasswordChar > 0)
                    {
                        text = PasswordChar.ToString().Repeat(text.Length);
                    }

                    ContentAlignment textAlign = ContentAlignment.MiddleLeft;
                    if (TextAlignment == ContentAlignment.TopCenter || TextAlignment == ContentAlignment.MiddleCenter || TextAlignment == ContentAlignment.BottomCenter)
                        textAlign = ContentAlignment.MiddleCenter;
                    if (TextAlignment == ContentAlignment.TopRight || TextAlignment == ContentAlignment.MiddleRight || TextAlignment == ContentAlignment.BottomRight)
                        textAlign = ContentAlignment.MiddleRight;

                    e.Graphics.DrawString(text, edit.Font, ForeDisableColor, edit.Bounds, textAlign);
                }
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        public Color _symbolColor = UIFontColor.Primary;

        [DefaultValue(typeof(Color), "48, 48, 48")]
        [Description("字体图标颜色"), Category("SunnyUI")]
        public Color SymbolColor
        {
            get => _symbolColor;
            set
            {
                try
                {
                    _symbolColor = value;
                    Invalidate();
                }
                catch (Exception ex)
                {
                    SunnyLog.Logs(ex);
                }
            }
        }

        private int _symbol;

        [DefaultValue(0)]
        [Description("字体图标"), Category("SunnyUI")]
        public int Symbol
        {
            get => _symbol;
            set
            {
                try
                {
                    _symbol = value;
                    SizeChange();
                    Invalidate();
                }
                catch (Exception ex)
                {
                    SunnyLog.Logs(ex);
                }
            }
        }

        private int _symbolSize = 24;

        [DefaultValue(24)]
        [Description("字体图标大小"), Category("SunnyUI")]
        public int SymbolSize
        {
            get => _symbolSize;
            set
            {
                try
                {
                    _symbolSize = Math.Max(value, 16);
                    _symbolSize = Math.Min(value, UIGlobal.EditorMaxHeight);
                    SizeChange();
                    Invalidate();
                }
                catch (Exception ex)
                {
                    SunnyLog.Logs(ex);
                }
            }
        }

        private Point symbolOffset = new Point(0, 0);

        [DefaultValue(typeof(Point), "0, 0")]
        [Description("字体图标的偏移位置"), Category("SunnyUI")]
        public Point SymbolOffset
        {
            get => symbolOffset;
            set
            {
                try
                {
                    symbolOffset = value;
                    Invalidate();
                }
                catch (Exception ex)
                {
                    SunnyLog.Logs(ex);
                }
            }
        }

        private int _symbolRotate = 0;

        [DefaultValue(0)]
        [Description("字体图标旋转角度"), Category("SunnyUI")]
        public int SymbolRotate
        {
            get => _symbolRotate;
            set
            {
                try
                {
                    if (_symbolRotate != value)
                    {
                        _symbolRotate = value;
                        Invalidate();
                    }
                }
                catch (Exception ex)
                {
                    SunnyLog.Logs(ex);
                }
            }
        }

        [DefaultValue(361761)]
        [Description("按钮字体图标"), Category("SunnyUI")]
        public int ButtonSymbol
        {
            get => SafeInvoke(() => btn.Symbol, 361761);
            set => SafeInvoke(() => btn.Symbol = value);
        }

        [DefaultValue(24)]
        [Description("按钮字体图标大小"), Category("SunnyUI")]
        public int ButtonSymbolSize
        {
            get => SafeInvoke(() => btn.SymbolSize, 24);
            set => SafeInvoke(() => btn.SymbolSize = value);
        }

        [DefaultValue(typeof(Point), "-1, 1")]
        [Description("按钮字体图标的偏移位置"), Category("SunnyUI")]
        public Point ButtonSymbolOffset
        {
            get => SafeInvoke(() => btn.SymbolOffset, new Point(-1, 1));
            set => SafeInvoke(() => btn.SymbolOffset = value);
        }

        [DefaultValue(0)]
        [Description("按钮字体图标旋转角度"), Category("SunnyUI")]
        public int ButtonSymbolRotate
        {
            get => SafeInvoke(() => btn.SymbolRotate, 0);
            set => SafeInvoke(() => btn.SymbolRotate = value);
        }

        [DefaultValue(typeof(Color), "80, 160, 255")]
        [Description("按钮填充颜色"), Category("SunnyUI")]
        public Color ButtonFillColor
        {
            get => SafeInvoke(() => btn.FillColor, Color.FromArgb(80, 160, 255));
            set
            {
                SafeInvoke(() =>
                {
                    btn.FillColor = value;
                    btn.Style = UIStyle.Custom;
                });
            }
        }

        [DefaultValue(typeof(Color), "White")]
        [Description("按钮字体颜色"), Category("SunnyUI")]
        public Color ButtonForeColor
        {
            get => SafeInvoke(() => btn.ForeColor, Color.White);
            set
            {
                SafeInvoke(() =>
                {
                    btn.SymbolColor = btn.ForeColor = value;
                    btn.Style = UIStyle.Custom;
                });
            }
        }

        [DefaultValue(typeof(Color), "80, 160, 255")]
        [Description("按钮边框颜色"), Category("SunnyUI")]
        public Color ButtonRectColor
        {
            get => SafeInvoke(() => btn.RectColor, Color.FromArgb(80, 160, 255));
            set
            {
                SafeInvoke(() =>
                {
                    btn.RectColor = value;
                    btn.Style = UIStyle.Custom;
                });
            }
        }

        [DefaultValue(typeof(Color), "115, 179, 255")]
        [Description("按钮鼠标移上时填充颜色"), Category("SunnyUI")]
        public Color ButtonFillHoverColor
        {
            get => SafeInvoke(() => btn.FillHoverColor, Color.FromArgb(115, 179, 255));
            set
            {
                SafeInvoke(() =>
                {
                    btn.FillHoverColor = value;
                    btn.Style = UIStyle.Custom;
                });
            }
        }

        [DefaultValue(typeof(Color), "White")]
        [Description("按钮鼠标移上时字体颜色"), Category("SunnyUI")]
        public Color ButtonForeHoverColor
        {
            get => SafeInvoke(() => btn.ForeHoverColor, Color.White);
            set
            {
                SafeInvoke(() =>
                {
                    btn.SymbolHoverColor = btn.ForeHoverColor = value;
                    btn.Style = UIStyle.Custom;
                });
            }
        }

        [DefaultValue(typeof(Color), "115, 179, 255")]
        [Description("鼠标移上时边框颜色"), Category("SunnyUI")]
        public Color ButtonRectHoverColor
        {
            get => SafeInvoke(() => btn.RectHoverColor, Color.FromArgb(115, 179, 255));
            set
            {
                SafeInvoke(() =>
                {
                    btn.RectHoverColor = value;
                    btn.Style = UIStyle.Custom;
                });
            }
        }

        [DefaultValue(typeof(Color), "64, 128, 204")]
        [Description("按钮鼠标按下时填充颜色"), Category("SunnyUI")]
        public Color ButtonFillPressColor
        {
            get => SafeInvoke(() => btn.FillPressColor, Color.FromArgb(64, 128, 204));
            set
            {
                SafeInvoke(() =>
                {
                    btn.FillPressColor = value;
                    btn.Style = UIStyle.Custom;
                });
            }
        }

        [DefaultValue(typeof(Color), "White")]
        [Description("按钮鼠标按下时字体颜色"), Category("SunnyUI")]
        public Color ButtonForePressColor
        {
            get => SafeInvoke(() => btn.ForePressColor, Color.White);
            set
            {
                SafeInvoke(() =>
                {
                    btn.SymbolPressColor = btn.ForePressColor = value;
                    btn.Style = UIStyle.Custom;
                });
            }
        }

        [DefaultValue(typeof(Color), "64, 128, 204")]
        [Description("按钮鼠标按下时边框颜色"), Category("SunnyUI")]
        public Color ButtonRectPressColor
        {
            get => SafeInvoke(() => btn.RectPressColor, Color.FromArgb(64, 128, 204));
            set
            {
                SafeInvoke(() =>
                {
                    btn.RectPressColor = value;
                    btn.Style = UIStyle.Custom;
                });
            }
        }

        [DefaultValue(true), Description("滚动条主题样式"), Category("SunnyUI")]
        public bool ButtonStyleInherited
        {
            get => SafeInvoke(() => btn != null && btn.Style == UIStyle.Inherited, true);
            set
            {
                try
                {
                    if (value && btn != null)
                    {
                        btn.Style = UIStyle.Inherited;
                    }
                }
                catch (Exception ex)
                {
                    SunnyLog.Logs(ex);
                }
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            try
            {
                ButtonClick?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                SunnyLog.Logs(ex);
            }
        }

        public event EventHandler ButtonClick;
        [DefaultValue(29), Category("SunnyUI"), Description("按钮宽度")]
        public int ButtonWidth
        {
            get => SafeInvoke(() => btn.Width, 29);
            set
            {
                SafeInvoke(() =>
                {
                    btn.Width = Math.Max(20, value);
                    SizeChange();
                });
            }
        }
    }
}


///******************************************************************************
// * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
// * CopyRight (C) 2012-2024 ShenYongHua(沈永华). - VIETNAMESE: T.ME/TRANTAIDAKLAK
// * QQ群：56829229 QQ：17612584 EMail：SunnyUI@QQ.Com
// *
// * Blog:   https://www.cnblogs.com/yhuse
// * Gitee:  https://gitee.com/yhuse/SunnyUI
// * GitHub: https://github.com/yhuse/SunnyUI
// *
// * SunnyUI.dll can be used for free under the GPL-3.0 license.
// * If you use this code, please keep this note.
// * 如果您使用此代码，请保留此说明。
// ******************************************************************************
// * 文件名称: UITextBox.cs
// * 文件说明: 输入框
// * 当前版本: V3.1
// * 创建日期: 2020-01-01
// *
// * 2020-01-01: V2.2.0 增加文件说明
// * 2020-06-03: V2.2.5 增加多行，增加滚动条
// * 2020-09-03: V2.2.7 增加FocusedSelectAll属性，激活时全选
// * 2021-04-15: V3.0.3 修改文字可以居中显示
// * 2021-04-17: V3.0.3 不限制高度为根据字体计算，可进行调整，解决多行输入时不能输入回车的问题
// * 2021-04-18: V3.0.3 增加ShowScrollBar属性，单独控制垂直滚动条
// * 2021-06-01: V3.0.4 增加图标和字体图标的显示
// * 2021-07-18: V3.0.5 修改Focus可用
// * 2021-08-03: V3.0.5 增加GotFocus和LostFocus事件
// * 2021-08-15: V3.0.6 重写了水印文字的画法，并增加水印文字颜色
// * 2021-09-07: V3.0.6 增加按钮
// * 2021-10-14: V3.0.8 调整最小高度限制
// * 2021-10-15: V3.0.8 支持修改背景色
// * 2022-01-07: V3.1.0 按钮支持自定义颜色
// * 2022-02-16: V3.1.1 增加了只读的颜色设置
// * 2022-03-14: V3.1.1 增加滚动条的颜色设置
// * 2022-04-11: V3.1.3 增加对按钮设置ToolTip
// * 2022-06-10: V3.1.9 尺寸改变时重绘
// * 2022-06-23: V3.2.0 重写水印文字，解决不同背景色下泛白的问题
// * 2022-07-17: V3.2.1 增加SelectionChanged事件
// * 2022-07-28: V3.2.2 修复了有水印文字时，不响应Click和DoubleClick事件的问题
// * 2022-09-05: V3.2.3 修复了无水印文字时，光标有时不显示的问题
// * 2022-09-16: V3.2.4 支持自定义右键菜单
// * 2022-09-16: V3.2.4 修改右侧Button可能不显示的问题
// * 2022-11-03: V3.2.6 增加了可设置垂直滚动条宽度的属性
// * 2022-11-12: V3.2.8 修改整数、浮点数大小离开判断为实时输入判断
// * 2022-11-12: V3.2.8 删除MaximumEnabled、MinimumEnabled、HasMaximum、HasMinimum属性
// * 2022-11-26: V3.2.9 增加MouseClick，MouseDoubleClick事件
// * 2023-02-07: V3.3.1 增加Tips小红点
// * 2023-02-10: V3.3.2 有水印时，系统响应触摸屏增加了TouchPressClick属性，默认关闭
// * 2023-06-14: V3.3.9 按钮图标位置修正
// * 2023-07-03: V3.3.9 增加Enabled为false时，可修改文字颜色
// * 2023-07-16: V3.4.0 修复了Enabled为false时，PasswordChar失效的问题
// * 2023-08-17: V3.4.1 修复了Enabled为false时，字体大小调整后，文字显示位置的问题
// * 2023-08-24: V3.4.2 修复了Enabled为false时，自定义颜色，文字不显示的问题
// * 2023-10-25: V3.5.1 修复在高DPI下，文字垂直不居中的问题
// * 2023-10-25: V3.5.1 修复在某些字体不显示下划线的问题
// * 2023-10-26: V3.5.1 字体图标增加旋转角度参数SymbolRotate
// * 2023-11-16: V3.5.2 重构主题
// * 2023-12-18: V3.6.2 修复高度不随字体改变
// * 2023-12-18: V3.6.2 修改显示按钮时Tips小红点的位置
// * 2023-12-25: V3.6.2 增加Text的属性编辑器
// * 2024-01-13: V3.6.3 调整Radius时，自动调整文本框的位置
// * 2024-06-11: V3.6.6 调整为可继承
// * 2024-08-12: V3.6.8 解决原生控件字体在微软雅黑时，显示不完整的问题
// * 2024-08-26: V3.6.9 修复微软雅黑字体显示不完整的问题
// * 2024-08-27: V3.6.9 AutoSize时根据字体自动调整控件高度
//******************************************************************************/

//using System;
//using System.Collections;
//using System.ComponentModel;
//using System.Drawing;
//using System.Drawing.Design;
//using System.Windows.Forms;

//namespace Sunny.UI
//{
//    [DefaultEvent("TextChanged")]
//    [DefaultProperty("Text")]
//    public partial class UITextBox : UIPanel, ISymbol, IToolTip
//    {
//        private readonly UIEdit edit = new UIEdit();
//        private readonly UIScrollBar bar = new UIScrollBar();
//        private readonly UISymbolButton btn = new UISymbolButton();

//        public UITextBox()
//        {
//            InitializeComponent();
//            InitializeComponentEnd = true;
//            SetStyleFlags(true, true, true);

//            ShowText = false;
//            MinimumSize = new Size(1, 16);

//            edit.AutoSize = false;
//            edit.Top = (Height - edit.Height) / 2;
//            edit.Left = 4;
//            edit.Width = Width - 8;
//            edit.Text = String.Empty;
//            edit.BorderStyle = BorderStyle.None;
//            edit.TextChanged += Edit_TextChanged;
//            edit.KeyDown += Edit_OnKeyDown;
//            edit.KeyUp += Edit_OnKeyUp;
//            edit.KeyPress += Edit_OnKeyPress;
//            edit.MouseEnter += Edit_MouseEnter;
//            edit.Click += Edit_Click;
//            edit.DoubleClick += Edit_DoubleClick;
//            edit.Leave += Edit_Leave;
//            edit.Validated += Edit_Validated;
//            edit.Validating += Edit_Validating;
//            edit.GotFocus += Edit_GotFocus;
//            edit.LostFocus += Edit_LostFocus;
//            edit.MouseLeave += Edit_MouseLeave;
//            edit.MouseWheel += Edit_MouseWheel;
//            edit.MouseDown += Edit_MouseDown;
//            edit.MouseUp += Edit_MouseUp;
//            edit.MouseMove += Edit_MouseMove;
//            edit.SelectionChanged += Edit_SelectionChanged;
//            edit.MouseClick += Edit_MouseClick;
//            edit.MouseDoubleClick += Edit_MouseDoubleClick;
//            edit.SizeChanged += Edit_SizeChanged;
//            edit.FontChanged += Edit_FontChanged;

//            btn.Parent = this;
//            btn.Visible = false;
//            btn.Text = "";
//            btn.Symbol = 361761;
//            btn.Top = 1;
//            btn.Height = 25;
//            btn.Width = 29;
//            btn.BackColor = Color.Transparent;
//            btn.Click += Btn_Click;
//            btn.Radius = 3;
//            btn.SymbolOffset = new Point(-1, 1);

//            edit.Invalidate();
//            Controls.Add(edit);
//            fillColor = Color.White;

//            bar.Parent = this;
//            bar.Dock = DockStyle.None;
//            bar.Visible = false;
//            bar.ValueChanged += Bar_ValueChanged;
//            bar.MouseEnter += Bar_MouseEnter;
//            TextAlignment = ContentAlignment.MiddleLeft;

//            lastEditHeight = edit.Height;
//            Width = 150;
//            Height = 29;

//            editCursor = Cursor;
//            TextAlignmentChange += UITextBox_TextAlignmentChange;
//        }

//        [Browsable(false)]
//        public override string[] FormTranslatorProperties { get; }

//        private void Edit_FontChanged(object sender, EventArgs e)
//        {
//            if (!edit.Multiline)
//            {
//                int height = edit.Font.Height;
//                edit.AutoSize = false;
//                edit.Height = height + 2;
//                SizeChange();
//            }
//        }

//        int lastEditHeight = -1;
//        private void Edit_SizeChanged(object sender, EventArgs e)
//        {
//            if (lastEditHeight != edit.Height)
//            {
//                lastEditHeight = edit.Height;
//                SizeChange();
//            }
//        }

//        public override void SetDPIScale()
//        {
//            base.SetDPIScale();
//            if (DesignMode) return;
//            if (!UIDPIScale.NeedSetDPIFont()) return;

//            edit.SetDPIScale();
//        }

//        [Description("开启后可响应某些触屏的点击事件"), Category("SunnyUI")]
//        [DefaultValue(false)]
//        public bool TouchPressClick
//        {
//            get => edit.TouchPressClick;
//            set => edit.TouchPressClick = value;
//        }

//        private bool _autoSize = false;
//        public new bool AutoSize
//        {
//            get => _autoSize;
//            set
//            {
//                _autoSize = value;
//                SizeChange();
//            }
//        }

//        private UIButton tipsBtn;
//        public void SetTipsText(ToolTip toolTip, string text)
//        {
//            if (tipsBtn == null)
//            {
//                tipsBtn = new UIButton();
//                tipsBtn.Cursor = System.Windows.Forms.Cursors.Hand;
//                tipsBtn.Size = new System.Drawing.Size(6, 6);
//                tipsBtn.Style = Sunny.UI.UIStyle.Red;
//                tipsBtn.StyleCustomMode = true;
//                tipsBtn.Text = "";
//                tipsBtn.Click += TipsBtn_Click;

//                Controls.Add(tipsBtn);
//                tipsBtn.Location = new System.Drawing.Point(Width - 8, 2);
//                tipsBtn.BringToFront();
//            }

//            toolTip.SetToolTip(tipsBtn, text);
//        }

//        public event EventHandler TipsClick;
//        private void TipsBtn_Click(object sender, EventArgs e)
//        {
//            TipsClick?.Invoke(this, EventArgs.Empty);
//        }

//        public void CloseTips()
//        {
//            if (tipsBtn != null)
//            {
//                tipsBtn.Click -= TipsBtn_Click;
//                tipsBtn.Dispose();
//                tipsBtn = null;
//            }
//        }

//        public new event EventHandler MouseDoubleClick;
//        public new event EventHandler MouseClick;

//        private void Edit_MouseDoubleClick(object sender, MouseEventArgs e)
//        {
//            MouseDoubleClick?.Invoke(this, e);
//        }

//        private void Edit_MouseClick(object sender, MouseEventArgs e)
//        {
//            MouseClick?.Invoke(this, e);
//        }

//        private int scrollBarWidth = 0;

//        [DefaultValue(0), Category("SunnyUI"), Description("垂直滚动条宽度，最小为原生滚动条宽度")]
//        public int ScrollBarWidth
//        {
//            get => scrollBarWidth;
//            set
//            {
//                scrollBarWidth = value;
//                SetScrollInfo();
//            }
//        }

//        private int scrollBarHandleWidth = 6;

//        [DefaultValue(6), Category("SunnyUI"), Description("垂直滚动条滑块宽度，最小为原生滚动条宽度")]
//        public int ScrollBarHandleWidth
//        {
//            get => scrollBarHandleWidth;
//            set
//            {
//                scrollBarHandleWidth = value;
//                if (bar != null) bar.FillWidth = value;
//            }
//        }

//        private void Edit_SelectionChanged(object sender, UITextBoxSelectionArgs e)
//        {
//            SelectionChanged?.Invoke(this, e);
//        }

//        public event OnSelectionChanged SelectionChanged;

//        public void SetButtonToolTip(ToolTip toolTip, string tipText)
//        {
//            toolTip.SetToolTip(btn, tipText);
//        }

//        protected override void OnContextMenuStripChanged(EventArgs e)
//        {
//            base.OnContextMenuStripChanged(e);
//            if (edit != null) edit.ContextMenuStrip = ContextMenuStrip;
//        }

//        /// <summary>
//        /// 填充颜色，当值为背景色或透明色或空值则不填充
//        /// </summary>
//        [Description("填充颜色，当值为背景色或透明色或空值则不填充"), Category("SunnyUI")]
//        [DefaultValue(typeof(Color), "White")]
//        public new Color FillColor
//        {
//            get
//            {
//                return fillColor;
//            }
//            set
//            {
//                if (fillColor != value)
//                {
//                    fillColor = value;
//                    Invalidate();
//                }

//                AfterSetFillColor(value);
//            }
//        }

//        /// <summary>
//        /// 字体只读颜色
//        /// </summary>
//        [DefaultValue(typeof(Color), "109, 109, 103")]
//        public Color ForeReadOnlyColor
//        {
//            get => foreReadOnlyColor;
//            set => SetForeReadOnlyColor(value);
//        }

//        /// <summary>
//        /// 边框只读颜色
//        /// </summary>
//        [DefaultValue(typeof(Color), "173, 178, 181")]
//        public Color RectReadOnlyColor
//        {
//            get => rectReadOnlyColor;
//            set => SetRectReadOnlyColor(value);
//        }

//        /// <summary>
//        /// 填充只读颜色
//        /// </summary>
//        [DefaultValue(typeof(Color), "244, 244, 244")]
//        public Color FillReadOnlyColor
//        {
//            get => fillReadOnlyColor;
//            set => SetFillReadOnlyColor(value);
//        }

//        private void Btn_Click(object sender, EventArgs e)
//        {
//            ButtonClick?.Invoke(this, e);
//        }

//        public event EventHandler ButtonClick;

//        [DefaultValue(29), Category("SunnyUI"), Description("按钮宽度")]
//        public int ButtonWidth { get => btn.Width; set { btn.Width = Math.Max(20, value); SizeChange(); } }

//        private bool showButton = false;
//        [DefaultValue(false), Category("SunnyUI"), Description("显示按钮")]
//        public bool ShowButton
//        {
//            get => showButton;
//            set
//            {
//                showButton = !multiline && value;
//                if (btn.IsValid()) btn.Visible = showButton;
//                SizeChange();
//            }
//        }

//        private void Edit_MouseMove(object sender, MouseEventArgs e)
//        {
//            MouseMove?.Invoke(this, e);
//        }

//        private void Edit_MouseUp(object sender, MouseEventArgs e)
//        {
//            MouseUp?.Invoke(this, e);
//        }

//        private void Edit_MouseDown(object sender, MouseEventArgs e)
//        {
//            MouseDown?.Invoke(this, e);
//        }

//        private void Edit_MouseLeave(object sender, EventArgs e)
//        {
//            MouseLeave?.Invoke(this, e);
//        }

//        /// <summary>
//        /// 需要额外设置ToolTip的控件
//        /// </summary>
//        /// <returns>控件</returns>
//        public Control ExToolTipControl()
//        {
//            return edit;
//        }

//        private void Edit_LostFocus(object sender, EventArgs e)
//        {
//            LostFocus?.Invoke(this, e);
//        }

//        private void Edit_GotFocus(object sender, EventArgs e)
//        {
//            GotFocus?.Invoke(this, e);
//        }

//        private void Edit_Validating(object sender, CancelEventArgs e)
//        {
//            Validating?.Invoke(this, e);
//        }

//        public new event MouseEventHandler MouseDown;
//        public new event MouseEventHandler MouseUp;
//        public new event MouseEventHandler MouseMove;
//        public new event EventHandler GotFocus;
//        public new event EventHandler LostFocus;
//        public new event CancelEventHandler Validating;
//        public new event EventHandler Validated;
//        public new event EventHandler MouseLeave;
//        public new event EventHandler DoubleClick;
//        public new event EventHandler Click;
//        [Browsable(true)]
//        public new event EventHandler TextChanged;
//        public new event KeyEventHandler KeyDown;
//        public new event KeyEventHandler KeyUp;
//        public new event KeyPressEventHandler KeyPress;
//        public new event EventHandler Leave;

//        private void Edit_Validated(object sender, EventArgs e)
//        {
//            Validated?.Invoke(this, e);
//        }

//        public new void Focus()
//        {
//            base.Focus();
//            edit.Focus();
//        }

//        [Browsable(false)]
//        public UIEdit TextBox => edit;

//        private void Edit_Leave(object sender, EventArgs e)
//        {
//            Leave?.Invoke(this, e);
//        }

//        protected override void OnEnabledChanged(EventArgs e)
//        {
//            base.OnEnabledChanged(e);
//            edit.BackColor = GetFillColor();

//            edit.Visible = true;
//            edit.Enabled = Enabled;
//            if (!Enabled)
//            {
//                if (NeedDrawDisabledText) edit.Visible = false;
//            }
//        }

//        private bool NeedDrawDisabledText => !Enabled && StyleCustomMode && (ForeDisableColor != Color.FromArgb(109, 109, 103) || FillDisableColor != Color.FromArgb(244, 244, 244));

//        public override bool Focused => edit.Focused;

//        [DefaultValue(false)]
//        [Description("激活时选中全部文字"), Category("SunnyUI")]
//        public bool FocusedSelectAll
//        {
//            get => edit.FocusedSelectAll;
//            set => edit.FocusedSelectAll = value;
//        }

//        private void UITextBox_TextAlignmentChange(object sender, ContentAlignment alignment)
//        {
//            if (edit == null) return;
//            if (alignment == ContentAlignment.TopLeft || alignment == ContentAlignment.MiddleLeft ||
//                alignment == ContentAlignment.BottomLeft)
//                edit.TextAlign = HorizontalAlignment.Left;

//            if (alignment == ContentAlignment.TopCenter || alignment == ContentAlignment.MiddleCenter ||
//                alignment == ContentAlignment.BottomCenter)
//                edit.TextAlign = HorizontalAlignment.Center;

//            if (alignment == ContentAlignment.TopRight || alignment == ContentAlignment.MiddleRight ||
//                alignment == ContentAlignment.BottomRight)
//                edit.TextAlign = HorizontalAlignment.Right;
//        }

//        private void Edit_DoubleClick(object sender, EventArgs e)
//        {
//            DoubleClick?.Invoke(this, e);
//        }

//        private void Edit_Click(object sender, EventArgs e)
//        {
//            Click?.Invoke(this, e);
//        }

//        protected override void OnCursorChanged(EventArgs e)
//        {
//            base.OnCursorChanged(e);
//            edit.Cursor = Cursor;
//        }

//        private Cursor editCursor;

//        private void Bar_MouseEnter(object sender, EventArgs e)
//        {
//            editCursor = Cursor;
//            Cursor = Cursors.Default;
//        }

//        private void Edit_MouseEnter(object sender, EventArgs e)
//        {
//            Cursor = editCursor;
//            if (FocusedSelectAll)
//            {
//                SelectAll();
//            }
//        }

//        private void Edit_MouseWheel(object sender, MouseEventArgs e)
//        {
//            OnMouseWheel(e);
//            if (bar != null && bar.Visible && edit != null)
//            {
//                var si = ScrollBarInfo.GetInfo(edit.Handle);
//                if (e.Delta > 10)
//                {
//                    if (si.nPos > 0)
//                    {
//                        ScrollBarInfo.ScrollUp(edit.Handle);
//                    }
//                }
//                else if (e.Delta < -10)
//                {
//                    if (si.nPos < si.ScrollMax)
//                    {
//                        ScrollBarInfo.ScrollDown(edit.Handle);
//                    }
//                }
//            }

//            SetScrollInfo();
//        }

//        private void Bar_ValueChanged(object sender, EventArgs e)
//        {
//            if (edit != null)
//            {
//                ScrollBarInfo.SetScrollValue(edit.Handle, bar.Value);
//            }
//        }

//        private bool multiline = false;

//        [DefaultValue(false)]
//        public bool Multiline
//        {
//            get => multiline;
//            set
//            {
//                multiline = value;
//                edit.Multiline = value;
//                // edit.ScrollBars = value ? ScrollBars.Vertical : ScrollBars.None;
//                // bar.Visible = multiline;

//                if (value && Type != UIEditType.String)
//                {
//                    Type = UIEditType.String;
//                }

//                SizeChange();
//            }
//        }

//        private bool showScrollBar;

//        [DefaultValue(false)]
//        [Description("显示垂直滚动条"), Category("SunnyUI")]
//        public bool ShowScrollBar
//        {
//            get => showScrollBar;
//            set
//            {
//                value = value && Multiline;
//                showScrollBar = value;
//                if (value)
//                {
//                    edit.ScrollBars = ScrollBars.Vertical;
//                    bar.Visible = true;
//                }
//                else
//                {
//                    edit.ScrollBars = ScrollBars.None;
//                    bar.Visible = false;
//                }
//            }
//        }

//        [DefaultValue(true)]
//        public bool WordWarp
//        {
//            get => edit.WordWrap;
//            set => edit.WordWrap = value;
//        }

//        public void Select(int start, int length)
//        {
//            edit.Focus();
//            edit.Select(start, length);
//        }

//        public void ScrollToCaret()
//        {
//            edit.ScrollToCaret();
//        }

//        private void Edit_OnKeyPress(object sender, KeyPressEventArgs e)
//        {
//            KeyPress?.Invoke(this, e);
//        }

//        private void Edit_OnKeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                DoEnter?.Invoke(this, e);
//            }

//            KeyDown?.Invoke(this, e);
//        }

//        public event EventHandler DoEnter;

//        private void Edit_OnKeyUp(object sender, KeyEventArgs e)
//        {
//            KeyUp?.Invoke(this, e);
//        }

//        [DefaultValue(null)]
//        [Description("水印文字"), Category("SunnyUI")]
//        public string Watermark
//        {
//            get => edit.Watermark;
//            set => edit.Watermark = value;
//        }

//        [DefaultValue(typeof(Color), "Gray")]
//        [Description("水印文字颜色"), Category("SunnyUI")]
//        public Color WatermarkColor
//        {
//            get => edit.WaterMarkColor;
//            set => edit.WaterMarkColor = value;
//        }

//        [DefaultValue(typeof(Color), "Gray")]
//        [Description("水印文字激活颜色"), Category("SunnyUI")]
//        public Color WatermarkActiveColor
//        {
//            get => edit.WaterMarkActiveForeColor;
//            set => edit.WaterMarkActiveForeColor = value;
//        }

//        public void SelectAll()
//        {
//            edit.Focus();
//            edit.SelectAll();
//        }

//        internal void CheckMaxMin()
//        {
//            edit.CheckMaxMin();
//        }

//        private void Edit_TextChanged(object s, EventArgs e)
//        {
//            if (IsDisposed) return;
//            TextChanged?.Invoke(this, e);
//            SetScrollInfo();
//        }

//        /// <summary>
//        /// 重载字体变更
//        /// </summary>
//        /// <param name="e">参数</param>
//        protected override void OnFontChanged(EventArgs e)
//        {
//            base.OnFontChanged(e);

//            if (DefaultFontSize < 0 && edit != null)
//            {
//                edit.Font = this.Font;
//            }

//            Invalidate();
//        }

//        /// <summary>
//        /// 重载控件尺寸变更
//        /// </summary>
//        /// <param name="e">参数</param>
//        protected override void OnSizeChanged(EventArgs e)
//        {
//            base.OnSizeChanged(e);
//            SizeChange();
//        }

//        public void SetScrollInfo()
//        {
//            if (bar == null)
//            {
//                return;
//            }

//            var si = ScrollBarInfo.GetInfo(edit.Handle);
//            if (si.ScrollMax > 0)
//            {
//                bar.Maximum = si.ScrollMax;
//                bar.Value = si.nPos;
//            }
//            else
//            {
//                bar.Maximum = si.ScrollMax;
//            }
//        }

//        protected override void OnRadiusChanged(int value)
//        {
//            base.OnRadiusChanged(value);
//            SizeChange();
//        }

//        private void SizeChange()
//        {
//            if (!InitializeComponentEnd) return;
//            if (edit == null) return;
//            if (btn == null) return;

//            if (!multiline)
//            {
//                //单行显示

//                //AutoSize自动设置高度
//                if (Dock == DockStyle.None && AutoSize)
//                {
//                    if (Height != edit.Height + 5)
//                        Height = edit.Height + 5;
//                }

//                //根据字体大小编辑框垂直居中
//                if (edit.Top != (Height - edit.Height) / 2 + 1)
//                {
//                    edit.Top = (Height - edit.Height) / 2 + 1;
//                }

//                int added = Radius <= 5 ? 0 : (Radius - 5) / 2;

//                if (icon == null && Symbol == 0)
//                {
//                    edit.Left = 4;
//                    edit.Width = Width - 8;
//                    edit.Left = edit.Left + added;
//                    edit.Width = edit.Width - added * 2;
//                }
//                else
//                {
//                    if (icon != null)
//                    {
//                        edit.Left = 4 + iconSize;
//                        edit.Width = Width - 8 - iconSize - added;
//                    }
//                    else if (Symbol > 0)
//                    {
//                        edit.Left = 4 + SymbolSize;
//                        edit.Width = Width - 8 - SymbolSize - added;
//                    }
//                }

//                btn.Left = Width - 2 - ButtonWidth - added;
//                btn.Top = 2;
//                btn.Height = Height - 4;

//                if (ShowButton)
//                {
//                    edit.Width = edit.Width - btn.Width - 3 - added;
//                }

//                if (tipsBtn != null)
//                {
//                    if (ShowButton)
//                        tipsBtn.Location = new System.Drawing.Point(Width - btn.Width - 10 - added, 2);
//                    else
//                        tipsBtn.Location = new System.Drawing.Point(Width - 8 - added, 2);
//                }
//            }
//            else
//            {
//                btn.Visible = false;
//                edit.Top = 3;
//                edit.Height = Height - 6;
//                edit.Left = 4;
//                edit.Width = Width - 8;

//                int barWidth = Math.Max(ScrollBarInfo.VerticalScrollBarWidth() + 2, ScrollBarWidth);
//                bar.Top = 2;
//                bar.Width = barWidth + 1;
//                bar.Left = Width - barWidth - 3;
//                bar.Height = Height - 4;
//                bar.BringToFront();

//                SetScrollInfo();
//            }
//        }

//        protected override void OnGotFocus(EventArgs e)
//        {
//            base.OnGotFocus(e);
//            edit.Focus();
//        }

//        public void Clear()
//        {
//            edit.Clear();
//        }

//        [DefaultValue('\0')]
//        [Description("密码掩码"), Category("SunnyUI")]
//        public char PasswordChar
//        {
//            get => edit.PasswordChar;
//            set => edit.PasswordChar = value;
//        }

//        [DefaultValue(false)]
//        [Description("是否只读"), Category("SunnyUI")]
//        public bool ReadOnly
//        {
//            get => isReadOnly;
//            set
//            {
//                isReadOnly = value;
//                edit.ReadOnly = value;
//                edit.BackColor = GetFillColor();
//                Invalidate();
//            }
//        }

//        [Description("输入类型"), Category("SunnyUI")]
//        [DefaultValue(UIEditType.String)]
//        public UIEditType Type
//        {
//            get => edit.Type;
//            set => edit.Type = value;
//        }

//        /// <summary>
//        /// 当InputType为数字类型时，能输入的最大值
//        /// </summary>
//        [Description("当InputType为数字类型时，能输入的最大值。"), Category("SunnyUI")]
//        [DefaultValue(2147483647D)]
//        public double Maximum
//        {
//            get => edit.MaxValue;
//            set => edit.MaxValue = value;
//        }

//        /// <summary>
//        /// 当InputType为数字类型时，能输入的最小值
//        /// </summary>
//        [Description("当InputType为数字类型时，能输入的最小值。"), Category("SunnyUI")]
//        [DefaultValue(-2147483648D)]
//        public double Minimum
//        {
//            get => edit.MinValue;
//            set => edit.MinValue = value;
//        }

//        [DefaultValue(0.00)]
//        [Description("浮点返回值"), Category("SunnyUI")]
//        public double DoubleValue
//        {
//            get => edit.DoubleValue;
//            set => edit.DoubleValue = value;
//        }

//        [DefaultValue(0)]
//        [Description("整型返回值"), Category("SunnyUI")]
//        public int IntValue
//        {
//            get => edit.IntValue;
//            set => edit.IntValue = value;
//        }

//        [Description("文本返回值"), Category("SunnyUI")]
//        [Browsable(true)]
//        [DefaultValue("")]
//        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
//        public override string Text
//        {
//            get => edit.Text;
//            set => edit.Text = value;
//        }

//        [Description("浮点数，显示文字小数位数"), Category("SunnyUI")]
//        [DefaultValue(2)]
//        public int DecimalPlaces
//        {
//            get => edit.DecLength;
//            set => edit.DecLength = Math.Max(value, 0);
//        }

//        [DefaultValue(false)]
//        [Description("整型或浮点输入时，是否可空显示"), Category("SunnyUI")]
//        public bool CanEmpty
//        {
//            get => edit.CanEmpty;
//            set => edit.CanEmpty = value;
//        }

//        public void Empty()
//        {
//            if (edit.CanEmpty)
//                edit.Text = "";
//        }

//        public bool IsEmpty => edit.Text == "";

//        /// <summary>
//        /// 重载鼠标按下事件
//        /// </summary>
//        /// <param name="e">鼠标参数</param>
//        protected override void OnMouseDown(MouseEventArgs e)
//        {
//            ActiveControl = edit;
//        }

//        [DefaultValue(32767)]
//        public int MaxLength
//        {
//            get => edit.MaxLength;
//            set => edit.MaxLength = Math.Max(value, 1);
//        }

//        /// <summary>
//        /// 设置主题样式
//        /// </summary>
//        /// <param name="uiColor">主题样式</param>
//        public override void SetStyleColor(UIBaseStyle uiColor)
//        {
//            base.SetStyleColor(uiColor);

//            fillColor = uiColor.EditorBackColor;
//            foreColor = UIFontColor.Primary;
//            edit.BackColor = GetFillColor();
//            edit.ForeColor = GetForeColor();
//            edit.ForeDisableColor = uiColor.ForeDisableColor;

//            if (bar != null && bar.Style == UIStyle.Inherited)
//            {
//                bar.ForeColor = uiColor.PrimaryColor;
//                bar.HoverColor = uiColor.ButtonFillHoverColor;
//                bar.PressColor = uiColor.ButtonFillPressColor;
//                bar.FillColor = fillColor;
//                scrollBarColor = uiColor.PrimaryColor;
//                scrollBarBackColor = fillColor;
//            }

//            if (btn != null && btn.Style == UIStyle.Inherited)
//            {
//                btn.ForeColor = uiColor.ButtonForeColor;
//                btn.FillColor = uiColor.ButtonFillColor;
//                btn.RectColor = uiColor.RectColor;

//                btn.FillHoverColor = uiColor.ButtonFillHoverColor;
//                btn.RectHoverColor = uiColor.ButtonRectHoverColor;
//                btn.ForeHoverColor = uiColor.ButtonForeHoverColor;

//                btn.FillPressColor = uiColor.ButtonFillPressColor;
//                btn.RectPressColor = uiColor.ButtonRectPressColor;
//                btn.ForePressColor = uiColor.ButtonForePressColor;
//            }
//        }

//        /// <summary>
//        /// 滚动条主题样式
//        /// </summary>
//        [DefaultValue(true), Description("滚动条主题样式"), Category("SunnyUI")]
//        public bool ScrollBarStyleInherited
//        {
//            get => bar != null && bar.Style == UIStyle.Inherited;
//            set
//            {
//                if (value)
//                {
//                    if (bar != null) bar.Style = UIStyle.Inherited;
//                    scrollBarColor = UIStyles.Blue.PrimaryColor;
//                    scrollBarBackColor = UIStyles.Blue.EditorBackColor;
//                }

//            }
//        }

//        protected override void SetForeDisableColor(Color color)
//        {
//            base.SetForeDisableColor(color);
//            edit.ForeDisableColor = color;
//        }

//        private Color scrollBarColor = Color.FromArgb(80, 160, 255);

//        /// <summary>
//        /// 填充颜色，当值为背景色或透明色或空值则不填充
//        /// </summary>
//        [Description("滚动条填充颜色"), Category("SunnyUI")]
//        [DefaultValue(typeof(Color), "80, 160, 255")]
//        public Color ScrollBarColor
//        {
//            get => scrollBarColor;
//            set
//            {
//                scrollBarColor = value;
//                bar.HoverColor = bar.PressColor = bar.ForeColor = value;
//                bar.Style = UIStyle.Custom;
//                Invalidate();
//            }
//        }

//        private Color scrollBarBackColor = Color.White;

//        /// <summary>
//        /// 填充颜色，当值为背景色或透明色或空值则不填充
//        /// </summary>
//        [Description("滚动条背景颜色"), Category("SunnyUI")]
//        [DefaultValue(typeof(Color), "White")]
//        public Color ScrollBarBackColor
//        {
//            get => scrollBarBackColor;
//            set
//            {
//                scrollBarBackColor = value;
//                bar.FillColor = value;
//                bar.Style = UIStyle.Custom;
//                Invalidate();
//            }
//        }

//        protected override void AfterSetForeColor(Color color)
//        {
//            base.AfterSetForeColor(color);
//            edit.ForeColor = GetForeColor();
//        }

//        protected override void AfterSetFillColor(Color color)
//        {
//            base.AfterSetFillColor(color);
//            edit.BackColor = GetFillColor();
//            bar.FillColor = color;
//        }

//        protected override void AfterSetFillReadOnlyColor(Color color)
//        {
//            base.AfterSetFillReadOnlyColor(color);
//            edit.BackColor = GetFillColor();
//        }

//        protected override void AfterSetForeReadOnlyColor(Color color)
//        {
//            base.AfterSetForeReadOnlyColor(color);
//            edit.ForeColor = GetForeColor();
//        }

//        public enum UIEditType
//        {
//            /// <summary>
//            /// 字符串
//            /// </summary>
//            String,

//            /// <summary>
//            /// 整数
//            /// </summary>
//            Integer,

//            /// <summary>
//            /// 浮点数
//            /// </summary>
//            Double
//        }

//        [DefaultValue(false)]
//        public bool AcceptsReturn
//        {
//            get => edit.AcceptsReturn;
//            set => edit.AcceptsReturn = value;
//        }

//        [DefaultValue(AutoCompleteMode.None), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
//        public AutoCompleteMode AutoCompleteMode
//        {
//            get => edit.AutoCompleteMode;
//            set => edit.AutoCompleteMode = value;
//        }

//        [
//            DefaultValue(AutoCompleteSource.None),
//            TypeConverterAttribute(typeof(TextBoxAutoCompleteSourceConverter)),
//            Browsable(true),
//            EditorBrowsable(EditorBrowsableState.Always)
//        ]
//        public AutoCompleteSource AutoCompleteSource
//        {
//            get => edit.AutoCompleteSource;
//            set => edit.AutoCompleteSource = value;
//        }

//        [
//            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
//            Localizable(true),
//            Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)),
//            Browsable(true),
//            EditorBrowsable(EditorBrowsableState.Always)
//        ]
//        public AutoCompleteStringCollection AutoCompleteCustomSource
//        {
//            get => edit.AutoCompleteCustomSource;
//            set => edit.AutoCompleteCustomSource = value;
//        }

//        [DefaultValue(CharacterCasing.Normal)]
//        public CharacterCasing CharacterCasing
//        {
//            get => edit.CharacterCasing;
//            set => edit.CharacterCasing = value;
//        }

//        public void Paste(string text)
//        {
//            edit.Paste(text);
//        }

//        internal class TextBoxAutoCompleteSourceConverter : EnumConverter
//        {
//            public TextBoxAutoCompleteSourceConverter(Type type) : base(type)
//            {
//            }

//            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
//            {
//                StandardValuesCollection values = base.GetStandardValues(context);
//                ArrayList list = new ArrayList();
//                int count = values.Count;
//                for (int i = 0; i < count; i++)
//                {
//                    string currentItemText = values[i].ToString();
//                    if (currentItemText != null && !currentItemText.Equals("ListItems"))
//                    {
//                        list.Add(values[i]);
//                    }
//                }

//                return new StandardValuesCollection(list);
//            }
//        }

//        [DefaultValue(false)]
//        public bool AcceptsTab
//        {
//            get => edit.AcceptsTab;
//            set => edit.AcceptsTab = value;
//        }

//        [DefaultValue(false)]
//        public bool EnterAsTab
//        {
//            get => edit.EnterAsTab;
//            set => edit.EnterAsTab = value;
//        }

//        [DefaultValue(true)]
//        public bool ShortcutsEnabled
//        {
//            get => edit.ShortcutsEnabled;
//            set => edit.ShortcutsEnabled = value;
//        }

//        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//        public bool CanUndo
//        {
//            get => edit.CanUndo;
//        }

//        [DefaultValue(true)]
//        public bool HideSelection
//        {
//            get => edit.HideSelection;
//            set => edit.HideSelection = value;
//        }

//        [
//            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
//            MergableProperty(false),
//            Localizable(true),
//            Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))
//        ]
//        public string[] Lines
//        {
//            get => edit.Lines;
//            set => edit.Lines = value;
//        }

//        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//        public bool Modified
//        {
//            get => edit.Modified;
//            set => edit.Modified = value;
//        }

//        [
//            Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced),
//            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
//        ]
//        public int PreferredHeight
//        {
//            get => edit.PreferredHeight;
//        }

//        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//        public string SelectedText
//        {
//            get => edit.SelectedText;
//            set => edit.SelectedText = value;
//        }

//        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//        public int SelectionLength
//        {
//            get => edit.SelectionLength;
//            set => edit.SelectionLength = value;
//        }

//        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//        public int SelectionStart
//        {
//            get => edit.SelectionStart;
//            set => edit.SelectionStart = value;
//        }

//        [Browsable(false)]
//        public int TextLength
//        {
//            get => edit.TextLength;
//        }

//        public void AppendText(string text)
//        {
//            edit.AppendText(text);
//        }

//        public void ClearUndo()
//        {
//            edit.ClearUndo();
//        }

//        public void Copy()
//        {
//            edit.Copy();
//        }

//        public void Cut()
//        {
//            edit.Cut();
//        }

//        public void Paste()
//        {
//            edit.Paste();
//        }

//        public char GetCharFromPosition(Point pt)
//        {
//            return edit.GetCharFromPosition(pt);
//        }

//        public int GetCharIndexFromPosition(Point pt)
//        {
//            return edit.GetCharIndexFromPosition(pt);
//        }

//        public int GetLineFromCharIndex(int index)
//        {
//            return edit.GetLineFromCharIndex(index);
//        }

//        public Point GetPositionFromCharIndex(int index)
//        {
//            return edit.GetPositionFromCharIndex(index);
//        }

//        public int GetFirstCharIndexFromLine(int lineNumber)
//        {
//            return edit.GetFirstCharIndexFromLine(lineNumber);
//        }

//        public int GetFirstCharIndexOfCurrentLine()
//        {
//            return edit.GetFirstCharIndexOfCurrentLine();
//        }

//        public void DeselectAll()
//        {
//            edit.DeselectAll();
//        }

//        public void Undo()
//        {
//            edit.Undo();
//        }

//        private Image icon;
//        [Description("图标"), Category("SunnyUI")]
//        [DefaultValue(null)]
//        public Image Icon
//        {
//            get => icon;
//            set
//            {
//                icon = value;
//                SizeChange();
//                Invalidate();
//            }
//        }

//        private int iconSize = 24;
//        [Description("图标大小(方形)"), Category("SunnyUI"), DefaultValue(24)]
//        public int IconSize
//        {
//            get => iconSize;
//            set
//            {
//                iconSize = Math.Min(UIGlobal.EditorMinHeight, value);
//                SizeChange();
//                Invalidate();
//            }
//        }

//        /// <summary>
//        /// 重载绘图
//        /// </summary>
//        /// <param name="e">绘图参数</param>
//        protected override void OnPaint(PaintEventArgs e)
//        {
//            base.OnPaint(e);

//            if (multiline) return;
//            if (icon != null)
//            {
//                e.Graphics.DrawImage(icon, new Rectangle(4, (Height - iconSize) / 2, iconSize, iconSize), new Rectangle(0, 0, icon.Width, icon.Height), GraphicsUnit.Pixel);
//            }
//            else if (Symbol != 0)
//            {
//                e.Graphics.DrawFontImage(Symbol, SymbolSize, SymbolColor, new Rectangle(4 + symbolOffset.X, (Height - SymbolSize) / 2 + 1 + symbolOffset.Y, SymbolSize, SymbolSize), SymbolOffset.X, SymbolOffset.Y, SymbolRotate);
//            }

//            if (Text.IsValid() && NeedDrawDisabledText)
//            {
//                string text = Text;
//                if (PasswordChar > 0)
//                {
//                    text = PasswordChar.ToString().Repeat(text.Length);
//                }

//                ContentAlignment textAlign = ContentAlignment.MiddleLeft;
//                if (TextAlignment == ContentAlignment.TopCenter || TextAlignment == ContentAlignment.MiddleCenter || TextAlignment == ContentAlignment.BottomCenter)
//                    textAlign = ContentAlignment.MiddleCenter;

//                if (TextAlignment == ContentAlignment.TopRight || TextAlignment == ContentAlignment.MiddleRight || TextAlignment == ContentAlignment.BottomRight)
//                    textAlign = ContentAlignment.MiddleRight;

//                e.Graphics.DrawString(text, edit.Font, ForeDisableColor, edit.Bounds, textAlign);
//            }
//        }

//        public Color _symbolColor = UIFontColor.Primary;

//        /// <summary>
//        /// 字体图标颜色
//        /// </summary>
//        [DefaultValue(typeof(Color), "48, 48, 48")]
//        [Description("字体图标颜色"), Category("SunnyUI")]
//        public Color SymbolColor
//        {
//            get => _symbolColor;
//            set
//            {
//                _symbolColor = value;
//                Invalidate();
//            }
//        }

//        private int _symbol;

//        /// <summary>
//        /// 字体图标
//        /// </summary>
//        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
//        [Editor("Sunny.UI.UIImagePropertyEditor, " + AssemblyRefEx.SystemDesign, typeof(UITypeEditor))]
//        [DefaultValue(0)]
//        [Description("字体图标"), Category("SunnyUI")]
//        public int Symbol
//        {
//            get => _symbol;
//            set
//            {
//                _symbol = value;
//                SizeChange();
//                Invalidate();
//            }
//        }

//        private int _symbolSize = 24;

//        /// <summary>
//        /// 字体图标大小
//        /// </summary>
//        [DefaultValue(24)]
//        [Description("字体图标大小"), Category("SunnyUI")]
//        public int SymbolSize
//        {
//            get => _symbolSize;
//            set
//            {
//                _symbolSize = Math.Max(value, 16);
//                _symbolSize = Math.Min(value, UIGlobal.EditorMaxHeight);
//                SizeChange();
//                Invalidate();
//            }
//        }

//        private Point symbolOffset = new Point(0, 0);

//        /// <summary>
//        /// 字体图标的偏移位置
//        /// </summary>
//        [DefaultValue(typeof(Point), "0, 0")]
//        [Description("字体图标的偏移位置"), Category("SunnyUI")]
//        public Point SymbolOffset
//        {
//            get => symbolOffset;
//            set
//            {
//                symbolOffset = value;
//                Invalidate();
//            }
//        }

//        private int _symbolRotate = 0;

//        /// <summary>
//        /// 字体图标旋转角度
//        /// </summary>
//        [DefaultValue(0)]
//        [Description("字体图标旋转角度"), Category("SunnyUI")]
//        public int SymbolRotate
//        {
//            get => _symbolRotate;
//            set
//            {
//                if (_symbolRotate != value)
//                {
//                    _symbolRotate = value;
//                    Invalidate();
//                }
//            }
//        }

//        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
//        [Editor("Sunny.UI.UIImagePropertyEditor, " + AssemblyRefEx.SystemDesign, typeof(UITypeEditor))]
//        [DefaultValue(361761)]
//        [Description("按钮字体图标"), Category("SunnyUI")]
//        public int ButtonSymbol
//        {
//            get => btn.Symbol;
//            set => btn.Symbol = value;
//        }

//        [DefaultValue(24)]
//        [Description("按钮字体图标大小"), Category("SunnyUI")]
//        public int ButtonSymbolSize
//        {
//            get => btn.SymbolSize;
//            set => btn.SymbolSize = value;
//        }

//        [DefaultValue(typeof(Point), "-1, 1")]
//        [Description("按钮字体图标的偏移位置"), Category("SunnyUI")]
//        public Point ButtonSymbolOffset
//        {
//            get => btn.SymbolOffset;
//            set => btn.SymbolOffset = value;
//        }

//        /// <summary>
//        /// 字体图标旋转角度
//        /// </summary>
//        [DefaultValue(0)]
//        [Description("按钮字体图标旋转角度"), Category("SunnyUI")]
//        public int ButtonSymbolRotate
//        {
//            get => btn.SymbolRotate;
//            set => btn.SymbolRotate = value;
//        }

//        /// <summary>
//        /// 填充颜色，当值为背景色或透明色或空值则不填充
//        /// </summary>
//        [Description("按钮填充颜色"), Category("SunnyUI")]
//        [DefaultValue(typeof(Color), "80, 160, 255")]
//        public Color ButtonFillColor
//        {
//            get => btn.FillColor;
//            set
//            {
//                btn.FillColor = value;
//                btn.Style = UIStyle.Custom;
//            }
//        }

//        /// <summary>
//        /// 字体颜色
//        /// </summary>
//        [Description("按钮字体颜色"), Category("SunnyUI")]
//        [DefaultValue(typeof(Color), "White")]
//        public Color ButtonForeColor
//        {
//            get => btn.ForeColor;
//            set
//            {
//                btn.SymbolColor = btn.ForeColor = value;
//                btn.Style = UIStyle.Custom;
//            }
//        }

//        /// <summary>
//        /// 边框颜色
//        /// </summary>
//        [Description("按钮边框颜色"), Category("SunnyUI")]
//        [DefaultValue(typeof(Color), "80, 160, 255")]
//        public Color ButtonRectColor
//        {
//            get => btn.RectColor;
//            set
//            {
//                btn.RectColor = value;
//                btn.Style = UIStyle.Custom;
//            }
//        }

//        [DefaultValue(typeof(Color), "115, 179, 255"), Category("SunnyUI")]
//        [Description("按钮鼠标移上时填充颜色")]
//        public Color ButtonFillHoverColor
//        {
//            get => btn.FillHoverColor;
//            set
//            {
//                btn.FillHoverColor = value;
//                btn.Style = UIStyle.Custom;
//            }
//        }

//        [DefaultValue(typeof(Color), "White"), Category("SunnyUI")]
//        [Description("按钮鼠标移上时字体颜色")]
//        public Color ButtonForeHoverColor
//        {
//            get => btn.ForeHoverColor;
//            set
//            {
//                btn.SymbolHoverColor = btn.ForeHoverColor = value;
//                btn.Style = UIStyle.Custom;
//            }
//        }

//        [DefaultValue(typeof(Color), "115, 179, 255"), Category("SunnyUI")]
//        [Description("鼠标移上时边框颜色")]
//        public Color ButtonRectHoverColor
//        {
//            get => btn.RectHoverColor;
//            set
//            {
//                btn.RectHoverColor = value;
//                btn.Style = UIStyle.Custom;
//            }
//        }

//        [DefaultValue(typeof(Color), "64, 128, 204"), Category("SunnyUI")]
//        [Description("按钮鼠标按下时填充颜色")]
//        public Color ButtonFillPressColor
//        {
//            get => btn.FillPressColor;
//            set
//            {
//                btn.FillPressColor = value;
//                btn.Style = UIStyle.Custom;
//            }
//        }

//        [DefaultValue(typeof(Color), "White"), Category("SunnyUI")]
//        [Description("按钮鼠标按下时字体颜色")]
//        public Color ButtonForePressColor
//        {
//            get => btn.ForePressColor;
//            set
//            {
//                btn.SymbolPressColor = btn.ForePressColor = value;
//                btn.Style = UIStyle.Custom;
//            }
//        }

//        [DefaultValue(typeof(Color), "64, 128, 204"), Category("SunnyUI")]
//        [Description("按钮鼠标按下时边框颜色")]
//        public Color ButtonRectPressColor
//        {
//            get => btn.RectPressColor;
//            set
//            {
//                btn.RectPressColor = value;
//                btn.Style = UIStyle.Custom;
//            }
//        }

//        /// <summary>
//        /// 滚动条主题样式
//        /// </summary>
//        [DefaultValue(true), Description("滚动条主题样式"), Category("SunnyUI")]
//        public bool ButtonStyleInherited
//        {
//            get => btn != null && btn.Style == UIStyle.Inherited;
//            set
//            {
//                if (value && btn != null)
//                {
//                    btn.Style = UIStyle.Inherited;
//                }
//            }
//        }
//    }
//}