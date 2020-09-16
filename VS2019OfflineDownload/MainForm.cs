using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using VS2019OfflineDownload.Properties;

namespace VS2019OfflineDownload
{
    public partial class MainForm : Form
    {
		// 1.定义委托事件
		public event Action<string> ReadStdOutput;

        public event Action<string> ReadErrOutput;

		private string baseFromPath = "https://aka.ms/vs/16/release/";

		private string baseDownPath = Environment.CurrentDirectory;

		/// <summary>
		/// 节点实体
		/// </summary>
		private class DicNode
		{
			/// <summary>
			/// 父节点名称
			/// </summary>
			public string parentName { get; set; }

			/// <summary>
			/// 名称
			/// </summary>
			public string name { get; set; }

			/// <summary>
			/// 显示名称
			/// </summary>
			public string text { get; set; }

			/// <summary>
			/// 提示信息
			/// </summary>
			public string toolTip { get; set; }

			/// <summary>
			/// 是否勾选
			/// </summary>
			public bool isChecked { get; set; }
		}

		/// <summary>
		/// 工作负载列表
		/// </summary>
		private List<DicNode> workloadList = new List<DicNode>
		{
			//核心编辑器默认选择
			new DicNode
			{
				name = "CoreEditor",
				text = "Visual Studio 核心编辑器",
				toolTip = "Visual Studio 核心 shell 体验，包括语法感知代码编辑、源代码管理和工作项管理。",
				isChecked = true
			},
			new DicNode
			{
				name = "Azure",
				text = "Azure 开发",
				toolTip = "用于开发云应用、创建资源以及生成包括 Docker 支持的容器的 Azure SDK、工具和项目。"
			},
			new DicNode
			{
				name = "Data",
				text = "数据存储和处理",
				toolTip = "使用 SQL Server、Azure Data Lake 或 Hadoop 连接、开发和测试数据解决方案。"
			},
			new DicNode
			{
				name = "DataScience",
				text = "数据科学和分析应用程序",
				toolTip = "用于创建数据科学应用程序的语言和工具（包括 Python、R 和 F#）。"
			},
			new DicNode
			{
				name = "ManagedDesktop",
				text = ".NET 桌面开发",
				toolTip = "使用 C#、Visual Basic 和 F# 生成 WPF、Windows 窗体和控制台应用程序。"
			},
			new DicNode
			{
				name = "ManagedGame",
				text = "使用 Unity 的游戏开发",
				toolTip = "使用 Unity（功能强大的跨平台开发环境）创建 2D 和 3D 游戏。"
			},
			new DicNode
			{
				name = "NativeCrossPlat",
				text = "使用 C++ 的 Linux 开发",
				toolTip = "创建和调试在 Linux 环境中运行的应用程序。"
			},
			new DicNode
			{
				name = "NativeDesktop",
				text = "使用 C++ 的桌面开发",
				toolTip = "使用 Microsoft C++ 工具集、ATL 或 MFC 生成 Windows 桌面应用程序。"
			},
			new DicNode
			{
				name = "NativeGame",
				text = "使用 C++ 的游戏开发",
				toolTip = "以 DirectX、Unreal 或 Cocos2d 为后盾，利用 C++ 的强大功能生成专业游戏。"
			},
			new DicNode
			{
				name = "NativeMobile",
				text = "使用 C++ 的移动开发",
				toolTip = "使用 C++ 生成适用于 iOS、Android 或 Windows 的跨平台应用程序。"
			},
			new DicNode
			{
				name = "NetCoreTools",
				text = ".NET Core 跨平台开发",
				toolTip = "使用 .NET Core、ASP.NET Core、HTML/JavaScript 和包括 Docker 支持的容器生成跨平台应用程序。"
			},
			new DicNode
			{
				name = "NetCrossPlat",
				text = "使用 .NET 的移动开发",
				toolTip = "使用 Xmarin 生成适用于 iOS、Android 或 Windows 的跨平台应用程序。"
			},
			new DicNode
			{
				name = "NetWeb",
				text = "ASP.NET 和 Web 开发",
				toolTip = "使用 ASP.NET、ASP.NET Core、HTML/JavaScript 和包括 Docker 支持的容器生成 Web 应用程序。"
			},
			new DicNode
			{
				name = "Node",
				text = "Node.js 开发",
				toolTip = "使用 Node.js（事件驱动的异步 JavaScript 运行时）生成可扩展的网络应用程序。"
			},
			new DicNode
			{
				name = "Office",
				text = "Office/SharePoint 开发",
				toolTip = "使用 C#、VB 和 JavaScript 创建 Office 和 SharePoint 外接程序、SharePoint 解决方案和 VSTO 外接程序。"
			},
			new DicNode
			{
				name = "Python",
				text = "Python 开发",
				toolTip = "适用于 Python 的编辑、调试、交互式开发和源代码管理。"
			},
			new DicNode
			{
				name = "Universal",
				text = "通用 Windows 平台开发",
				toolTip = "使用 C#、VB 和 JavaScript 或 C++（可选）创建适用于通用 Windows 平台的应用程序。"
			},
			new DicNode
			{
				name = "VisualStudioExtension",
				text = "Visual Studio 扩展开发",
				toolTip = "创建适用于 Visual Studio 的加载项和扩展，包括新命令、代码分析器和工具窗口。"
			},
			new DicNode
			{
				name = "WebCrossPlat",
				text = "使用 JavaScript 的移动开发",
				toolTip = "使用用于 Apache Cordova 的工具生成 Android、iOS 和 UWP 应用。"
			},
			new DicNode
			{
				name = "Others",
				text = "独立组件",
				toolTip = "这些组件不随附于任何工作负载，但可选择作为单个组件。"
			}
		};

		/// <summary>
		/// 组件列表
		/// </summary>
		private List<DicNode> componentList = new List<DicNode>
		{
			new DicNode
			{ 
				parentName = "Others", 
				name = "Component.Android.Emulator", 
				text = "适用于 Android 的 Visual Studio 仿真程序" 
			},
			new DicNode
			{
				parentName = "Others",
				name = "Component.Android.NDK.R11C",
				text = "Android NDK (R11C)"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Component.Android.NDK.R11C_3264",
				text = "Android NDK (R11C)（32 位）"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Component.Android.SDK23",
				text = "Android SDK 安装程序（API 级别 23）（全局安装）"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Component.Android.SDK25",
				text = "Android SDK 安装程序（API 级别 25）"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Component.GitHub.VisualStudio",
				text = "适用于 Visual Studio 的 GitHub 扩展"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Component.Google.Android.Emulator.API23.V2",
				text = "Google Android Emulator（API 级别 23）（全局安装）"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Component.Google.Android.Emulator.API25",
				text = "Google Android Emulator（API 级别 25）"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.Component.Blend.SDK.WPF",
				text = "用于 .NET 的 Blend for Visual Studio SDK "
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.Component.HelpViewer",
				text = "帮助查看器"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.LinqToSql",
				text = "LINQ to SQL 工具"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.Phone.Emulator Windows",
				text = "10 移动版仿真程序（周年纪念版）"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.Phone.Emulator.15063",
				text = "Windows 10 Mobile 仿真器（创意者更新）"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.Runtime.Node.x86.6.4.0",
				text = "基于 Node.js v6.4.0 (x86) 的组件运行时"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.Runtime.Node.x86.7.4.0",
				text = "基于 Node.js v7.4.0 (x86) 的组件运行时"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.TestTools.CodedUITest",
				text = "编码的 UI 测试"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.TestTools.FeedbackClient",
				text = "Microsoft Feedback Client"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.TestTools.MicrosoftTestManager Microsoft",
				text = "测试管理器"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.TypeScript.2.0",
				text = "TypeScript 2.0 SDK"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.TypeScript.2.1",
				text = "TypeScript 2.1 SDK"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.TypeScript.2.2",
				text = "TypeScript 2.2 SDK"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.TypeScript.2.5",
				text = "TypeScript 2.5 SDK"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.TypeScript.2.6",
				text = "TypeScript 2.6 SDK"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.TypeScript.2.7",
				text = "TypeScript 2.7 SDK"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.UWP.VC.ARM64",
				text = "适用于 ARM64 的 C++ 通用 Windows 平台工具"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.VC.ATL.ARM.Spectre",
				text = "带有 Spectre 缓解措施的 Visual C++ ATL for ARM"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.VC.ATL.ARM64.Spectre",
				text = "带有 Spectre 缓解措施的 Visual C++ ATL for ARM64"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.VC.ATL.Spectre",
				text = "带有 Spectre 缓解措施的 Visual C++ ATL (x86/x64)"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.VC.ATLMFC.Spectre",
				text = "带有 Spectre 缓解措施的 Visual C++ MFC for x86/x64"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.VC.ClangC2",
				text = "Clang/C2（实验）"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.VC.MFC.ARM",
				text = "Visual C++ MFC for ARM"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.VC.MFC.ARM.Spectre",
				text = "带有 Spectre 缓解措施的 Visual C++ MFC for ARM"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.VC.MFC.ARM64",
				text = "Visual C++ MFC for ARM64"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.VC.MFC.ARM64.Spectre",
				text = "带有 Spectre 缓解措施的针对 ARM64 的 Visual C++ MFC 支持"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.VC.Runtimes.ARM.Spectre",
				text = "面向 Spectre 的 VC++ 2017 版本 15.7 v14.14 库 (ARM)"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.VC.Runtimes.ARM64.Spectre",
				text = "面向 Spectre 的 VC++ 2017 版本 15.7 v14.14 库 (ARM64)"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.VC.Runtimes.x86.x64.Spectre",
				text = "面向 Spectre 的 VC++ 2017 版本 15.7 v14.14 库 (x86 和 x64)"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.VC.Tools.14.11",
				text = "VC++ 2017 版本 15.4 v14.11 工具集"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.VC.Tools.14.12",
				text = "VC++ 2017 版本 15.5 v14.12 工具集"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.VC.Tools.14.13",
				text = "VC++ 2017 版本 15.6 v14.13 工具集"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.VC.Tools.ARM64",
				text = "用于 ARM64 的 Visual C++ 编译器和库"
			},
			new DicNode
			{
				parentName = "Others",
				name = "Microsoft.VisualStudio.Component.Windows10SDK.16299.Desktop.arm",
				text = "适用于桌面 C++ [ARM 和 ARM64] 的 Windows 10 SDK"
			}
		};

		public MainForm()
        {
            InitializeComponent();
            this.Init();
        }

        private void Init()
        {
			//2.将相应函数注册到委托事件中
			this.ReadStdOutput += this.ReadStdOutputAction;
            this.ReadErrOutput += this.ReadErrOutputAction;
        }

		/// <summary>
		/// 窗体显示时操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Shown(object sender, EventArgs e)
		{
			if (Settings.Default["程序版本"].ToString() != "")
			{
				this.comboBox1.SelectedItem = Settings.Default["程序版本"].ToString();
			}
			if (Settings.Default["缓存目录"].ToString() != "")
			{
				this.txtSaveDirectory.Text = Settings.Default["缓存目录"].ToString();
			}

			string checkWorkloadStr = "";
			string checkComponentStr = "";

			if (Settings.Default["选择负载"].ToString() != "")
			{
				checkWorkloadStr = Settings.Default["选择负载"].ToString();
			}
			if (Settings.Default["选择组件"].ToString() != "")
			{
				checkComponentStr = Settings.Default["选择组件"].ToString();
			}

			this.treeViewWorkload.Nodes.Clear();

			foreach (DicNode dicNode in workloadList)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Name = dicNode.name;
				treeNode.Text = dicNode.text;
				treeNode.ToolTipText = dicNode.toolTip;

				if (checkWorkloadStr.IndexOf(dicNode.name) > -1)
				{
					dicNode.isChecked = true;
					treeNode.Checked = true;
				}

				if (dicNode.name == "CoreEditor")
				{
					treeNode.Checked = true;
				}

				if (dicNode.name == "Others")
				{
					int checkedNum = 0;
					foreach (DicNode tempdicNode in componentList)
					{
						TreeNode childNode = new TreeNode();
						childNode.Name = tempdicNode.name;
						childNode.Text = tempdicNode.text;
						childNode.ToolTipText = tempdicNode.toolTip;
						treeNode.Nodes.Add(childNode);

						if (checkComponentStr.IndexOf(tempdicNode.name) > -1)
						{
							tempdicNode.isChecked = true;
							childNode.Checked = true;
							checkedNum++;
						}
					}
					if (treeNode.Nodes.Count != checkedNum)
					{
						treeNode.Checked = false;
						dicNode.isChecked = false;
					}
				}
				this.treeViewWorkload.Nodes.Add(treeNode);
			}
		}

		/// <summary>
		/// 控制节点选中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeViewWorkload_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			//VS2019的核心组件，必选
			if (e.Node.Name == "CoreEditor")
			{
				if (!e.Node.Checked)
				{
					e.Node.Checked = true;
				}
			}
			else
			{
				if (e.Node.Nodes.Count > 0)
				{
					foreach (TreeNode node in e.Node.Nodes)
					{
						node.Checked = e.Node.Checked;
					}

					List<DicNode> tempcomponentList = componentList.Where(a => a.parentName == e.Node.Name).ToList();

					foreach (DicNode node in tempcomponentList)
					{
						node.isChecked = e.Node.Checked;
					}
				}

				if (e.Node.Parent != null)
				{
					int childCount = e.Node.Parent.Nodes.Count;
					int checkedCount = 0;

					foreach(TreeNode node in e.Node.Parent.Nodes)
                    {
						if (node.Checked)
						{
							checkedCount++;
						}
					}

					if (childCount == checkedCount)
					{
						e.Node.Parent.Checked = true;
					}
					else
					{
						e.Node.Parent.Checked = false;
					}
				}

				DicNode dicNode = workloadList.Find(a => a.name == e.Node.Name);

				if (dicNode != null)
				{
					dicNode.isChecked = e.Node.Checked;
				}

				dicNode = componentList.Find(a => a.name == e.Node.Name);

				if (dicNode != null)
				{
					dicNode.isChecked = e.Node.Checked;
				}
			}
		}

		/// <summary>
		/// 根据下拉选项判断文件名称
		/// </summary>
		/// <returns></returns>
		private string getFileNameByTypeCombox()
		{
			string fromPath = string.Empty;
			string typestr = this.comboBox1.SelectedItem.ToString();
			if (typestr == "Visual Studio Community 2019")
			{
				fromPath = "vs_community.exe";
			}
			else if (typestr == "Visual Studio Professional 2019")
			{
				fromPath = "vs_professional.exe";
			}
			else if (typestr == "Visual Studio Enterprise 2019")
			{
				fromPath = "vs_enterprise.exe";
			}
			return fromPath;
		}

		public delegate string MyDelegate();

		/// <summary>
		/// 执行命令
		/// </summary>
		/// <param name="StartCmd"></param>
		private void RealAction(string StartCmd)
		{
			Process cmdProcess = new Process();

			cmdProcess.StartInfo.FileName = "cmd.exe";  // 命令
			//cmdProcess.StartInfo.Arguments = StartFileArg;    // 参数
			cmdProcess.StartInfo.WorkingDirectory = ".";

			cmdProcess.StartInfo.CreateNoWindow = true; // 不创建新窗口
			cmdProcess.StartInfo.UseShellExecute = false;   // 是否使用shell
			cmdProcess.StartInfo.RedirectStandardInput = true;  // 重定向输入
			cmdProcess.StartInfo.RedirectStandardOutput = true; // 重定向标准输出
			cmdProcess.StartInfo.RedirectStandardError = true;  // 重定向错误输出
			//cmdProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

			cmdProcess.OutputDataReceived += this.p_OutputDataReceived;
			cmdProcess.ErrorDataReceived += this.p_ErrorDataReceived;

			cmdProcess.EnableRaisingEvents = true;  // 启用Exited事件
			cmdProcess.Exited += this.CmdProcess_Exited;    // 注册进程结束事件

			cmdProcess.Start();

			//输入命令
			cmdProcess.StandardInput.WriteLine(StartCmd);
			cmdProcess.BeginOutputReadLine();
			cmdProcess.BeginErrorReadLine();

			// 如果打开注释，则以同步方式执行命令，此例子中用Exited事件异步执行。
			//cmdProcess.WaitForExit();  
		}

		private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			if (e.Data != null)
			{
				// 3. 异步调用，需要invoke
				this.Invoke(ReadStdOutput, new object[]
				{
					e.Data
				});
			}
		}

		private void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
		{
			if (e.Data != null)
			{
				this.Invoke(ReadErrOutput, new object[]
				{
					e.Data
				});
			}
		}

		/// <summary>
		/// 输出命令信息
		/// </summary>
		/// <param name="result"></param>
		private void ReadStdOutputAction(string result)
		{
			this.txtcmdLogTextArea.AppendText(result + "\r\n");
		}

		/// <summary>
		/// 输出错误信息
		/// </summary>
		/// <param name="result"></param>
		private void ReadErrOutputAction(string result)
		{
			//this.textBoxShowErrRet.AppendText(result + "\r\n");
		}

		/// <summary>
		/// 执行结束后触发
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CmdProcess_Exited(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// 选择程序版本
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			// 显示激活密钥
			if (this.comboBox1.Text == "Visual Studio Community 2019")
			{
				this.label3.Text = "社区版无需序列号";
			}
			if (this.comboBox1.Text == "Visual Studio Professional 2019")
			{
				this.label3.Text = "NYWVH-HT4XC-R2WYW-9Y3CM-X4V3Y";
			}
			if (this.comboBox1.Text == "Visual Studio Enterprise 2019")
			{
				this.label3.Text = "BF8Y8-GN2QH-T84XB-QVY3B-RC4DF";
			}

			// 根据文件确定是否允许下载
			DirectoryInfo di = new DirectoryInfo(System.Environment.CurrentDirectory);
			FileInfo[] files = di.GetFiles(getFileNameByTypeCombox());

			if (files.Length > 0)
			{
				linkDown.Visible = false;
			}
			else
			{
				linkDown.Visible = true;
			}
		}

		/// <summary>
		/// 选择缓存目录
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelectDir_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			fbd.ShowNewFolderButton = true;
			if (fbd.ShowDialog() == DialogResult.OK)
			{
				this.txtSaveDirectory.Text = fbd.SelectedPath;
			}
		}

		/// <summary>
		/// 下载指定版本官方工具
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void linkDown_Click(object sender, EventArgs e)
		{
			string fromPath = this.baseFromPath + this.getFileNameByTypeCombox();
			string savePath = this.baseDownPath + "\\" + this.getFileNameByTypeCombox();

			DownFile df = new DownFile(fromPath, savePath);
			df.StartPosition = FormStartPosition.CenterParent;
			DialogResult result = df.ShowDialog();

			if (result == DialogResult.OK)
			{
				this.comboBox1_SelectedIndexChanged(null, null);
				MessageBoxEx.Show("下载成功！");
				return;
			}
			MessageBoxEx.Show("下载失败，请检查网络连接！");
		}

		/// <summary>
		/// 保存设置
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, EventArgs e)
		{
			string configFileName = Application.ExecutablePath + ".config";
			XmlDocument doc = new XmlDocument();
			doc.Load(configFileName);
			string configString = @"configuration/applicationSettings/vs2019离线安装操作.Properties.Settings/setting[@name='程序版本']/value";
			XmlNode configNode = doc.SelectSingleNode(configString);

			if (configNode != null && this.comboBox1.SelectedIndex > -1)
			{
				configNode.InnerText = comboBox1.SelectedItem.ToString();
				doc.Save(configFileName);
			}

			configString = @"configuration/applicationSettings/vs2019离线安装操作.Properties.Settings/setting[@name='缓存目录']/value";
			configNode = doc.SelectSingleNode(configString);
			if (configNode != null)
			{
				configNode.InnerText = this.txtSaveDirectory.Text.Trim();
				doc.Save(configFileName);
				// 刷新应用程序设置，这样下次读取时才能读到最新的值。
				Settings.Default.Reload();
			}

			string text = "";
			List<DicNode> checklist = null;
			configString = @"configuration/applicationSettings/vs2019离线安装操作.Properties.Settings/setting[@name='选择负载']/value";
			configNode = doc.SelectSingleNode(configString);
			if (configNode != null)
			{
				text = "";
				checklist = workloadList.Where(a => a.isChecked == true).ToList();

				foreach (DicNode dicNode in checklist)
				{
					if (text != "")
					{
						text += "|";
					}
					text += dicNode.name;
				}
				configNode.InnerText = text;
				doc.Save(configFileName);
				// 刷新应用程序设置，这样下次读取时才能读到最新的值。
				Settings.Default.Reload();
			}

			configString = @"configuration/applicationSettings/vs2017离线安装操作.Properties.Settings/setting[@name='选择组件']/value";
			configNode = doc.SelectSingleNode(configString);
			if (configNode != null)
			{
				text = "";
				checklist = componentList.Where(a => a.isChecked == true).ToList();

				foreach (DicNode dicNode in checklist)
				{
					if (text != "")
					{
						text += "|";
					}
					text += dicNode.name;
				}
				configNode.InnerText = text;
				doc.Save(configFileName);
				// 刷新应用程序设置，这样下次读取时才能读到最新的值。
				Settings.Default.Reload();
			}

            //程序运行后只有操作xml方式才能修改配置文件
            //if (comboBox1.SelectedIndex > -1)
            //{
            //    Settings.Default["程序版本"] = comboBox1.SelectedItem.ToString();
            //}
            //else
            //{
            //    Settings.Default["程序版本"] = "";
            //}

            //Settings.Default["缓存目录"] = txtSaveDirectory.Text.Trim();
            //Settings.Default.Save();

            MessageBoxEx.Show(this, "保存成功", "提示");
		}

		/// <summary>
		/// 根据设置生成命令
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnBuild_Click(object sender, EventArgs e)
		{
			if (this.comboBox1.SelectedIndex == -1)
			{
				MessageBoxEx.Show(this, "请选择程序版本", "提示");
				return;
			}
            else
            {
				if (linkDown.Visible)
				{
					MessageBoxEx.Show(this, "请先下载相关程序", "提示");
					return;
				}
			}

			if (this.txtSaveDirectory.Text.ToString() == "")
			{
				MessageBoxEx.Show(this, "请选择缓存目录", "提示");
				return;
			}
			if (!Directory.Exists(this.txtSaveDirectory.Text.Trim()))
			{
				MessageBoxEx.Show(this, "缓存目录不存在，请检查后重试", "提示");
				return;
			}

			//挑选组件
			List<DicNode> checkedworkloadList = workloadList.Where(a => a.isChecked == true).ToList();
			List<DicNode> checkedcomponentList = componentList.Where(a => a.isChecked == true).ToList();


			if (checkedworkloadList.Count == 1 && checkedcomponentList.Count == 0)
			{
				DialogResult result = MessageBoxEx.Show("是否确认只安装核心组件?", "提示", MessageBoxButtons.YesNo);
				if (result == DialogResult.No)
				{
					return;
				}
			}

			//最好不要使用下面的写法
			//if (checkedworkloadList.Count == 1 && checkedcomponentList.Count == 0 && MessageBoxEx.Show("是否确认只安装核心组件?", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
			//{
			//	return;
			//}

			string includeStr = "";
			foreach (DicNode dicNode in checkedworkloadList)
			{
				//其他组件分开处理
				if (dicNode.name == "Others")
				{
					List<DicNode> others = componentList.Where(a => a.parentName == "Others").ToList();
					checkedcomponentList = checkedcomponentList.Concat(others).ToList();
				}
				else
				{
					includeStr += "--add Microsoft.VisualStudio.Workload." + dicNode.name + " ";
				}
			}
			foreach (DicNode dicNode in checkedcomponentList)
			{
				includeStr += "--add " + dicNode.name + " ";
			}
			if (includeStr != "")
			{
				includeStr += "--includeRecommended ";
			}

			//默认缓存中文版全部组件
			string cmdStr = getFileNameByTypeCombox() + " --layout " + txtSaveDirectory.Text.Trim() + " " + includeStr + " --lang Zh-cn";

			this.txtcmdLogTextArea.Text = cmdStr;
		}

		/// <summary>
		/// 开始缓存下载
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDown_Click(object sender, EventArgs e)
		{
			this.RealAction(this.txtcmdLogTextArea.Text);
		}

		/// <summary>
		/// 清理旧数据
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void linkClean_Click(object sender, EventArgs e)
		{
			if (this.comboBox1.SelectedIndex == -1)
			{
				MessageBoxEx.Show(this, "请选择程序版本", "提示");
				return;
			}
			if (this.linkDown.Visible)
			{
				MessageBoxEx.Show(this, "请先下载相关程序", "提示");
				return;
			}
			if (this.txtSaveDirectory.Text.ToString() == "")
			{
				MessageBoxEx.Show(this, "请选择缓存目录", "提示");
				return;
			}
			if (!Directory.Exists(this.txtSaveDirectory.Text.Trim()))
			{
				MessageBoxEx.Show(this, "缓存目录不存在，请检查后重试", "提示");
				return;
			}


			//获取最新目录的名称
			string cleanPath = "";

			//判断根目录
			DirectoryInfo di = new DirectoryInfo(txtSaveDirectory.Text.ToString());
			DirectoryInfo[] diList = di.GetDirectories("Archive");

			if (diList.Length == 0)
			{
				MessageBoxEx.Show(this, "当前缓存目录下文件无需清理", "提示");
				return;
			}

			//判断指定目录
			di = diList[0];
			diList = di.GetDirectories();

			if (diList.Length == 0)
			{
				MessageBoxEx.Show(this, "当前缓存目录下文件无需清理", "提示");
				return;
			}

			if (diList.Length > 1)
			{
				MessageBoxEx.Show("当前目录存在多个旧文件记录，请选择指定文件夹下Catalog.json文件，清理旧文件", "提示");
				OpenFileDialog ofd = new OpenFileDialog();
				ofd.InitialDirectory = this.txtSaveDirectory.Text + "\\Archive";
				ofd.Multiselect = false;
				DialogResult result = ofd.ShowDialog();
				if (result != DialogResult.OK)
				{
					return;
				}
				cleanPath = ofd.FileName;
			}
			else
			{
				//获取最新目录的名称
				cleanPath = this.txtSaveDirectory.Text.Trim() + "\\Archive\\" + diList[0].Name + "\\Catalog.json";
			}

			string cmdstr = getFileNameByTypeCombox() + " --layout " + txtSaveDirectory.Text.Trim() + " --clean " + cleanPath;
			this.RealAction(cmdstr);
		}

		private void Label3_MouseMove(object sender, MouseEventArgs e)
		{
			new ToolTip
			{
				ShowAlways = true
			}.SetToolTip(this.label3, "单击复制序列号");
		}

		private void Label3_Click(object sender, EventArgs e)
		{
			if (this.label3.Text == "社区版无需序列号")
			{
				MessageBox.Show(this, "社区版不用序列号的哦", "温馨提示");
				return;
			}
			Clipboard.SetText(this.label3.Text);
			MessageBox.Show(this, "序列号已写入剪切板", "温馨提示");
		}

	}
}
