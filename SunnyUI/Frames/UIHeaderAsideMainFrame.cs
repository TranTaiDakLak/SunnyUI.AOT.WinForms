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
 * 文件名称: UIHeaderAsideMainFrame.cs
 * 文件说明: 页面框架(Header-Aside-Main)
 * 当前版本: V3.1
 * 创建日期: 2020-05-05
 *
 * 2020-05-05: V2.2.5 页面框架(Header-Aside-Main)
******************************************************************************/

using System.Windows.Forms;

namespace Sunny.UI
{
    public partial class UIHeaderAsideMainFrame : UIHeaderMainFrame
    {
        public UIHeaderAsideMainFrame()
        {
            InitializeComponent();
            Controls.SetChildIndex(MainTabControl, 0);
            Header.Parent = this;
            Aside.Parent = this;
            MainTabControl.Parent = this;
            Aside.BringToFront();
            MainTabControl.BringToFront();
            Aside.TabControl = MainTabControl;
        }

        public override bool SelectPage(int pageIndex)
        {
            bool result = base.SelectPage(pageIndex);
            TreeNode node = Aside.GetTreeNode(pageIndex);
            if (node != null) Aside.SelectedNode = node;
            return result;
        }
    }
}