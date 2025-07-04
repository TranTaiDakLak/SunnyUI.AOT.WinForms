﻿/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2024 ShenYongHua(沈永华). - VIETNAMESE: T.ME/TRANTAIDAKLAK
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@QQ.Com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI.dll can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UIInputForm.cs
 * 文件说明: 输入窗体
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2024-05-30: V3.6.6 修复调用时自定义主题参数不起作用的问题
******************************************************************************/

namespace Sunny.UI
{
    public sealed partial class UIInputForm : UIEditForm
    {
        public UIInputForm()
        {
            InitializeComponent();
        }

        public int MaxLength
        {
            get => edit.MaxLength;
            set => edit.MaxLength = value;
        }

        public UITextBox Editor => edit;

        public UILabel Label => label;

        public bool CheckInputEmpty
        {
            get; set;
        }

        protected override bool CheckData()
        {
            Editor.CheckMaxMin();

            if (CheckInputEmpty)
            {
                bool result = edit.Text.IsValid();
                if (!result) this.ShowWarningDialog("编辑框内容不能为空。");
                return result;
            }

            return true;
        }

        protected override void DoEnter()
        {
            if (btnCancel.Focused || btnOK.Focused) return;
            btnOK.PerformClick();
        }

        private void UIInputForm_Shown(object sender, System.EventArgs e)
        {
            edit.SelectAll();
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);

            if (btnCancel != null)
            {
                btnCancel.SetStyleColor(uiColor);
            }

            if (label != null)
            {
                label.SetStyleColor(uiColor);
            }

            if (btnOK != null)
            {
                btnOK.SetStyleColor(uiColor);
            }

            if (pnlBtm != null)
            {
                pnlBtm.SetStyleColor(uiColor);
            }

            if (edit != null)
            {
                edit.SetStyleColor(uiColor);
            }
        }
    }
}