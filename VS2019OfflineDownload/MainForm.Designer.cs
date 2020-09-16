namespace VS2019OfflineDownload
{
    partial class MainForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Visual Studio 核心编辑器");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Azure 开发");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("数据存储和处理");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("数据科学和分析应用程序");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode(".NET 桌面开发");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("使用 Unity 的游戏开发");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("使用 C++ 的 Linux 开发");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("使用 C++ 的桌面开发");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("使用 C++ 的游戏开发");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("使用 C++ 的移动开发");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode(".NET Core 跨平台开发");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("使用 .NET 的移动开发");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("ASP.NET 和 Web 开发");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Node.js 开发");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Office/SharePoint 开发");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Python 开发");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("通用 Windows 平台开发");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Visual Studio 扩展开发");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("使用 JavaScript 的移动开发");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("独立组件");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSaveDirectory = new System.Windows.Forms.TextBox();
            this.btnSelectDir = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.linkDown = new System.Windows.Forms.LinkLabel();
            this.btnDown = new System.Windows.Forms.Button();
            this.txtcmdLogTextArea = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.treeViewWorkload = new System.Windows.Forms.TreeView();
            this.btnBuild = new System.Windows.Forms.Button();
            this.linkClean = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "程序版本：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 62);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "缓存目录：";
            // 
            // txtSaveDirectory
            // 
            this.txtSaveDirectory.Enabled = false;
            this.txtSaveDirectory.Location = new System.Drawing.Point(124, 57);
            this.txtSaveDirectory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSaveDirectory.Name = "txtSaveDirectory";
            this.txtSaveDirectory.Size = new System.Drawing.Size(493, 27);
            this.txtSaveDirectory.TabIndex = 2;
            // 
            // btnSelectDir
            // 
            this.btnSelectDir.Location = new System.Drawing.Point(628, 53);
            this.btnSelectDir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSelectDir.Name = "btnSelectDir";
            this.btnSelectDir.Size = new System.Drawing.Size(46, 38);
            this.btnSelectDir.TabIndex = 3;
            this.btnSelectDir.Text = "...";
            this.btnSelectDir.UseVisualStyleBackColor = true;
            this.btnSelectDir.Click += new System.EventHandler(this.btnSelectDir_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Visual Studio Community 2019",
            "Visual Studio Professional 2019",
            "Visual Studio Enterprise 2019"});
            this.comboBox1.Location = new System.Drawing.Point(124, 15);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(493, 28);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // linkDown
            // 
            this.linkDown.AutoSize = true;
            this.linkDown.Location = new System.Drawing.Point(628, 20);
            this.linkDown.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkDown.Name = "linkDown";
            this.linkDown.Size = new System.Drawing.Size(69, 20);
            this.linkDown.TabIndex = 5;
            this.linkDown.TabStop = true;
            this.linkDown.Text = "点击下载";
            this.linkDown.Visible = false;
            this.linkDown.Click += new System.EventHandler(this.linkDown_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(628, 343);
            this.btnDown.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(120, 38);
            this.btnDown.TabIndex = 6;
            this.btnDown.Text = "执行脚本";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // txtcmdLogTextArea
            // 
            this.txtcmdLogTextArea.Location = new System.Drawing.Point(21, 227);
            this.txtcmdLogTextArea.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtcmdLogTextArea.Multiline = true;
            this.txtcmdLogTextArea.Name = "txtcmdLogTextArea";
            this.txtcmdLogTextArea.Size = new System.Drawing.Size(596, 167);
            this.txtcmdLogTextArea.TabIndex = 8;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(628, 233);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 38);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "保存设置";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // treeViewWorkload
            // 
            this.treeViewWorkload.CheckBoxes = true;
            this.treeViewWorkload.Location = new System.Drawing.Point(21, 413);
            this.treeViewWorkload.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.treeViewWorkload.Name = "treeViewWorkload";
            treeNode1.Name = "CoreEditor";
            treeNode1.Text = "Visual Studio 核心编辑器";
            treeNode1.ToolTipText = "Visual Studio 核心 shell 体验，包括语法感知代码编辑、源代码管理和工作项管理。";
            treeNode2.Name = "Azure";
            treeNode2.Text = "Azure 开发";
            treeNode2.ToolTipText = "用于开发云应用、创建资源以及生成包括 Docker 支持的容器的 Azure SDK、工具和项目。";
            treeNode3.Name = "Data";
            treeNode3.Text = "数据存储和处理";
            treeNode3.ToolTipText = "使用 SQL Server、Azure Data Lake 或 Hadoop 连接、开发和测试数据解决方案。";
            treeNode4.Name = "DataScience";
            treeNode4.Text = "数据科学和分析应用程序";
            treeNode4.ToolTipText = "用于创建数据科学应用程序的语言和工具（包括 Python、R 和 F#）。";
            treeNode5.Name = "ManagedDesktop";
            treeNode5.Text = ".NET 桌面开发";
            treeNode5.ToolTipText = "使用 C#、Visual Basic 和 F# 生成 WPF、Windows 窗体和控制台应用程序。";
            treeNode6.Name = "ManagedGame";
            treeNode6.Text = "使用 Unity 的游戏开发";
            treeNode6.ToolTipText = "使用 Unity（功能强大的跨平台开发环境）创建 2D 和 3D 游戏。";
            treeNode7.Name = "NativeCrossPlat";
            treeNode7.Text = "使用 C++ 的 Linux 开发";
            treeNode7.ToolTipText = "创建和调试在 Linux 环境中运行的应用程序。";
            treeNode8.Name = "NativeDesktop";
            treeNode8.Text = "使用 C++ 的桌面开发";
            treeNode8.ToolTipText = "使用 Microsoft C++ 工具集、ATL 或 MFC 生成 Windows 桌面应用程序。";
            treeNode9.Name = "NativeGame";
            treeNode9.Text = "使用 C++ 的游戏开发";
            treeNode9.ToolTipText = "以 DirectX、Unreal 或 Cocos2d 为后盾，利用 C++ 的强大功能生成专业游戏。";
            treeNode10.Name = "NativeMobile";
            treeNode10.Text = "使用 C++ 的移动开发";
            treeNode10.ToolTipText = "使用 C++ 生成适用于 iOS、Android 或 Windows 的跨平台应用程序。";
            treeNode11.Name = "NetCoreTools";
            treeNode11.Text = ".NET Core 跨平台开发";
            treeNode11.ToolTipText = "使用 .NET Core、ASP.NET Core、HTML/JavaScript 和包括 Docker 支持的容器生成跨平台应用程序。";
            treeNode12.Name = "NetCrossPlat";
            treeNode12.Text = "使用 .NET 的移动开发";
            treeNode12.ToolTipText = "使用 Xmarin 生成适用于 iOS、Android 或 Windows 的跨平台应用程序。";
            treeNode13.Name = "NetWeb";
            treeNode13.Text = "ASP.NET 和 Web 开发";
            treeNode13.ToolTipText = "使用 ASP.NET、ASP.NET Core、HTML/JavaScript 和包括 Docker 支持的容器生成 Web 应用程序。";
            treeNode14.Name = "Node";
            treeNode14.Text = "Node.js 开发";
            treeNode14.ToolTipText = "使用 Node.js（事件驱动的异步 JavaScript 运行时）生成可扩展的网络应用程序。";
            treeNode15.Name = "Office";
            treeNode15.Text = "Office/SharePoint 开发";
            treeNode15.ToolTipText = "使用 C#、VB 和 JavaScript 创建 Office 和 SharePoint 外接程序、SharePoint 解决方案和 VSTO 外接程序。";
            treeNode16.Name = "Python";
            treeNode16.Text = "Python 开发";
            treeNode16.ToolTipText = "适用于 Python 的编辑、调试、交互式开发和源代码管理。";
            treeNode17.Name = "Universal";
            treeNode17.Text = "通用 Windows 平台开发";
            treeNode17.ToolTipText = "使用 C#、VB 和 JavaScript 或 C++（可选）创建适用于通用 Windows 平台的应用程序。";
            treeNode18.Name = "VisualStudioExtension";
            treeNode18.Text = "Visual Studio 扩展开发";
            treeNode18.ToolTipText = "创建适用于 Visual Studio 的加载项和扩展，包括新命令、代码分析器和工具窗口。";
            treeNode19.Name = "WebCrossPlat";
            treeNode19.Text = "使用 JavaScript 的移动开发";
            treeNode19.ToolTipText = "使用用于 Apache Cordova 的工具生成 Android、iOS 和 UWP 应用。";
            treeNode20.Name = "Others";
            treeNode20.Text = "独立组件";
            treeNode20.ToolTipText = "这些组件不随附于任何工作负载，但可选择作为单个组件。";
            this.treeViewWorkload.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15,
            treeNode16,
            treeNode17,
            treeNode18,
            treeNode19,
            treeNode20});
            this.treeViewWorkload.ShowNodeToolTips = true;
            this.treeViewWorkload.Size = new System.Drawing.Size(730, 491);
            this.treeViewWorkload.TabIndex = 9;
            this.treeViewWorkload.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewWorkload_NodeMouseClick);
            // 
            // btnBuild
            // 
            this.btnBuild.Location = new System.Drawing.Point(628, 288);
            this.btnBuild.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(120, 38);
            this.btnBuild.TabIndex = 6;
            this.btnBuild.Text = "生成脚本";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // linkClean
            // 
            this.linkClean.AutoSize = true;
            this.linkClean.Location = new System.Drawing.Point(681, 63);
            this.linkClean.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkClean.Name = "linkClean";
            this.linkClean.Size = new System.Drawing.Size(39, 20);
            this.linkClean.TabIndex = 5;
            this.linkClean.TabStop = true;
            this.linkClean.Text = "清理";
            this.linkClean.Click += new System.EventHandler(this.linkClean_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(22, 102);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(724, 115);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "产品序列号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(21, 33);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(568, 40);
            this.label3.TabIndex = 0;
            this.label3.Text = "NYWVH-HT4XC-R2WYW-9Y3CM-X4V3Y";
            this.label3.Click += new System.EventHandler(this.Label3_Click);
            this.label3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label3_MouseMove);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 925);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.treeViewWorkload);
            this.Controls.Add(this.txtcmdLogTextArea);
            this.Controls.Add(this.btnBuild);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.linkClean);
            this.Controls.Add(this.linkDown);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnSelectDir);
            this.Controls.Add(this.txtSaveDirectory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visual Studio 2019 离线工具";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;


        private System.Windows.Forms.Label label2;


        private System.Windows.Forms.TextBox txtSaveDirectory;


        private System.Windows.Forms.Button btnSelectDir;


        private System.Windows.Forms.ComboBox comboBox1;


        private System.Windows.Forms.LinkLabel linkDown;


        private System.Windows.Forms.Button btnDown;


        private System.Windows.Forms.TextBox txtcmdLogTextArea;


        private System.Windows.Forms.Button btnSave;


        private System.Windows.Forms.TreeView treeViewWorkload;


        private System.Windows.Forms.Button btnBuild;


        private System.Windows.Forms.LinkLabel linkClean;


        private System.Windows.Forms.GroupBox groupBox1;


        private System.Windows.Forms.Label label3;
    }
}

