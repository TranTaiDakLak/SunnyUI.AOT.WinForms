namespace SunnyDemo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            uiButton1 = new Sunny.UI.UIButton();
            uiHeaderButton1 = new Sunny.UI.UIHeaderButton();
            SuspendLayout();
            // 
            // uiButton1
            // 
            uiButton1.Font = new Font("Microsoft Sans Serif", 12F);
            uiButton1.Location = new Point(192, 172);
            uiButton1.MinimumSize = new Size(1, 1);
            uiButton1.Name = "uiButton1";
            uiButton1.Size = new Size(100, 35);
            uiButton1.TabIndex = 0;
            uiButton1.Text = "uiButton1";
            uiButton1.TipsFont = new Font("Microsoft Sans Serif", 9F);
            // 
            // uiHeaderButton1
            // 
            uiHeaderButton1.Font = new Font("Microsoft Sans Serif", 12F);
            uiHeaderButton1.Location = new Point(586, 97);
            uiHeaderButton1.MinimumSize = new Size(1, 1);
            uiHeaderButton1.Name = "uiHeaderButton1";
            uiHeaderButton1.Padding = new Padding(0, 8, 0, 3);
            uiHeaderButton1.Radius = 0;
            uiHeaderButton1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            uiHeaderButton1.RectSides = ToolStripStatusLabelBorderSides.None;
            uiHeaderButton1.Size = new Size(100, 88);
            uiHeaderButton1.TabIndex = 1;
            uiHeaderButton1.Text = "uiHeaderButton1";
            uiHeaderButton1.TipsFont = new Font("Microsoft Sans Serif", 9F);
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(800, 450);
            Controls.Add(uiHeaderButton1);
            Controls.Add(uiButton1);
            Name = "Form1";
            Text = "Form1";
            ZoomScaleRect = new Rectangle(15, 15, 800, 450);
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIButton uiButton1;
        private Sunny.UI.UIHeaderButton uiHeaderButton1;
    }
}
