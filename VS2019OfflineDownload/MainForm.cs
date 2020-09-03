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
        public event Action<string> ReadStdOutput;

        public event Action<string> ReadErrOutput;

        public MainForm()
        {
            InitializeComponent();
            this.Init();
        }

        private void Init()
        {
            this.ReadStdOutput += this.ReadStdOutputAction;
            this.ReadErrOutput += this.ReadErrOutputAction;
        }

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
			string text = "";
			string text2 = "";
			if (Settings.Default["选择负载"].ToString() != "")
			{
				text = Settings.Default["选择负载"].ToString();
			}
			if (Settings.Default["选择组件"].ToString() != "")
			{
				text2 = Settings.Default["选择组件"].ToString();
			}
			this.treeViewWorkload.Nodes.Clear();
			foreach (MainForm.DicNode dicNode in this.workloadList)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Name = dicNode.name;
				treeNode.Text = dicNode.text;
				treeNode.ToolTipText = dicNode.toolTip;
				if (text.IndexOf(dicNode.name) > -1)
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
					int num = 0;
					foreach (MainForm.DicNode dicNode2 in this.componentList)
					{
						TreeNode treeNode2 = new TreeNode();
						treeNode2.Name = dicNode2.name;
						treeNode2.Text = dicNode2.text;
						treeNode2.ToolTipText = dicNode2.toolTip;
						treeNode.Nodes.Add(treeNode2);
						if (text2.IndexOf(dicNode2.name) > -1)
						{
							dicNode2.isChecked = true;
							treeNode2.Checked = true;
							num++;
						}
					}
					if (treeNode.Nodes.Count != num)
					{
						treeNode.Checked = false;
						dicNode.isChecked = false;
					}
				}
				this.treeViewWorkload.Nodes.Add(treeNode);
			}
		}

		private void treeViewWorkload_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Node.Name == "CoreEditor")
			{
				if (!e.Node.Checked)
				{
					e.Node.Checked = true;
					return;
				}
			}
			else
			{
				if (e.Node.Nodes.Count > 0)
				{
					foreach (object obj in e.Node.Nodes)
					{
						((TreeNode)obj).Checked = e.Node.Checked;
					}
					foreach (MainForm.DicNode dicNode in (from a in this.componentList
														  where a.parentname == e.Node.Name
														  select a).ToList<MainForm.DicNode>())
					{
						dicNode.isChecked = e.Node.Checked;
					}
				}
				if (e.Node.Parent != null)
				{
					int count = e.Node.Parent.Nodes.Count;
					int num = 0;
					foreach(var item in e.Node.Parent.Nodes)
                    {
						if (((TreeNode)item).Checked)
						{
							num++;
						}
					}

					if (count == num)
					{
						e.Node.Parent.Checked = true;
					}
					else
					{
						e.Node.Parent.Checked = false;
					}
				}
				MainForm.DicNode dicNode2 = this.workloadList.Find((MainForm.DicNode a) => a.name == e.Node.Name);
				if (dicNode2 != null)
				{
					dicNode2.isChecked = e.Node.Checked;
				}
				dicNode2 = this.componentList.Find((MainForm.DicNode a) => a.name == e.Node.Name);
				if (dicNode2 != null)
				{
					dicNode2.isChecked = e.Node.Checked;
				}
			}
		}

		private string getFileNameByTypeCombox()
		{
			string result = "";
			string a = this.comboBox1.SelectedItem.ToString();
			if (a == "Visual Studio Community 2019")
			{
				result = "vs_community.exe";
			}
			else if (a == "Visual Studio Professional 2019")
			{
				result = "vs_professional.exe";
			}
			else if (a == "Visual Studio Enterprise 2019")
			{
				result = "vs_enterprise.exe";
			}
			return result;
		}

		private void RealAction(string StartCmd)
		{
			Process process = new Process();
			process.StartInfo.FileName = "cmd.exe";
			process.StartInfo.WorkingDirectory = ".";
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardInput = true;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardError = true;
			process.OutputDataReceived += this.p_OutputDataReceived;
			process.ErrorDataReceived += this.p_ErrorDataReceived;
			process.EnableRaisingEvents = true;
			process.Exited += this.CmdProcess_Exited;
			process.Start();
			process.StandardInput.WriteLine(StartCmd);
			process.BeginOutputReadLine();
			process.BeginErrorReadLine();
		}

		private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			if (e.Data != null)
			{
				base.Invoke(this.ReadStdOutput, new object[]
				{
					e.Data
				});
			}
		}

		private void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
		{
			if (e.Data != null)
			{
				base.Invoke(this.ReadErrOutput, new object[]
				{
					e.Data
				});
			}
		}

		private void ReadStdOutputAction(string result)
		{
			this.txtcmdLogTextArea.AppendText(result + "\r\n");
		}

		private void ReadErrOutputAction(string result)
		{
		}

		private void CmdProcess_Exited(object sender, EventArgs e)
		{
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			new DirectoryInfo(Environment.CurrentDirectory).GetFiles(this.getFileNameByTypeCombox());
			if (this.comboBox1.Text == "Visual Studio Community 2019")
			{
				this.label3.Text = "社区版无需序列号";
				return;
			}
			if (this.comboBox1.Text == "Visual Studio Professional 2019")
			{
				this.label3.Text = "NYWVH-HT4XC-R2WYW-9Y3CM-X4V3Y";
				return;
			}
			if (this.comboBox1.Text == "Visual Studio Enterprise 2019")
			{
				this.label3.Text = "BF8Y8-GN2QH-T84XB-QVY3B-RC4DF";
			}
		}

		private void btnSelectDir_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.ShowNewFolderButton = true;
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				this.txtSaveDirectory.Text = folderBrowserDialog.SelectedPath;
			}
		}

		private void linkDown_Click(object sender, EventArgs e)
		{
			string fromPath = this.baseFromPath + this.getFileNameByTypeCombox();
			string downPath = this.baseDownPath + "\\" + this.getFileNameByTypeCombox();
			DialogResult result = new DownFile(fromPath, downPath)
			{
				StartPosition = FormStartPosition.CenterParent
			}.ShowDialog();

			if (result == DialogResult.OK)
			{
				this.comboBox1_SelectedIndexChanged(null, null);
				MessageBoxEx.Show("下载成功！");
				return;
			}
			MessageBoxEx.Show("下载失败，请检查网络连接！");
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			string filename = Application.ExecutablePath + ".config";
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(filename);
			string xpath = "configuration/applicationSettings/vs2017离线安装操作.Properties.Settings/setting[@name='程序版本']/value";
			XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
			if (xmlNode != null && this.comboBox1.SelectedIndex > -1)
			{
				xmlNode.InnerText = this.comboBox1.SelectedItem.ToString();
				xmlDocument.Save(filename);
			}
			xpath = "configuration/applicationSettings/vs2017离线安装操作.Properties.Settings/setting[@name='缓存目录']/value";
			xmlNode = xmlDocument.SelectSingleNode(xpath);
			if (xmlNode != null)
			{
				xmlNode.InnerText = this.txtSaveDirectory.Text.Trim();
				xmlDocument.Save(filename);
				Settings.Default.Reload();
			}
			string text = "";
			xpath = "configuration/applicationSettings/vs2017离线安装操作.Properties.Settings/setting[@name='选择负载']/value";
			xmlNode = xmlDocument.SelectSingleNode(xpath);
			if (xmlNode != null)
			{
				text = "";
				foreach (MainForm.DicNode dicNode in (from a in this.workloadList
													  where a.isChecked
													  select a).ToList<MainForm.DicNode>())
				{
					if (text != "")
					{
						text += "|";
					}
					text += dicNode.name;
				}
				xmlNode.InnerText = text;
				xmlDocument.Save(filename);
				Settings.Default.Reload();
			}
			xpath = "configuration/applicationSettings/vs2017离线安装操作.Properties.Settings/setting[@name='选择组件']/value";
			xmlNode = xmlDocument.SelectSingleNode(xpath);
			if (xmlNode != null)
			{
				text = "";
				foreach (MainForm.DicNode dicNode2 in (from a in this.componentList
													   where a.isChecked
													   select a).ToList<MainForm.DicNode>())
				{
					if (text != "")
					{
						text += "|";
					}
					text += dicNode2.name;
				}
				xmlNode.InnerText = text;
				xmlDocument.Save(filename);
				Settings.Default.Reload();
			}
			MessageBoxEx.Show(this, "保存成功", "提示");
		}

		private void btnBuild_Click(object sender, EventArgs e)
		{
			if (this.comboBox1.SelectedIndex == -1)
			{
				MessageBoxEx.Show(this, "请选择程序版本", "提示");
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
			List<MainForm.DicNode> list = (from a in this.workloadList
										   where a.isChecked
										   select a).ToList<MainForm.DicNode>();
			List<MainForm.DicNode> list2 = (from a in this.componentList
											where a.isChecked
											select a).ToList<MainForm.DicNode>();
			if (list.Count == 1 && list2.Count == 0 && MessageBoxEx.Show("是否确认只安装核心组件?", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
			{
				return;
			}
			string text = "";
			foreach (MainForm.DicNode dicNode in list)
			{
				if (dicNode.name == "Others")
				{
					List<MainForm.DicNode> second = (from a in this.componentList
													 where a.parentname == "Others"
													 select a).ToList<MainForm.DicNode>();
					list2 = list2.Concat(second).ToList<MainForm.DicNode>();
				}
				else
				{
					text = text + "--add Microsoft.VisualStudio.Workload." + dicNode.name + " ";
				}
			}
			foreach (MainForm.DicNode dicNode2 in list2)
			{
				text = text + "--add " + dicNode2.name + " ";
			}
			if (text != "")
			{
				text += "--includeRecommended ";
			}
			string text2 = string.Concat(new string[]
			{
				this.getFileNameByTypeCombox(),
				" --layout ",
				this.txtSaveDirectory.Text.Trim(),
				" ",
				text,
				" --lang Zh-cn"
			});
			this.txtcmdLogTextArea.Text = text2;
		}

		private void btnDown_Click(object sender, EventArgs e)
		{
			this.RealAction(this.txtcmdLogTextArea.Text);
		}

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
			DirectoryInfo[] directories = new DirectoryInfo(this.txtSaveDirectory.Text.ToString()).GetDirectories("Archive");
			if (directories.Length == 0)
			{
				MessageBoxEx.Show(this, "当前缓存目录下文件无需清理", "提示");
				return;
			}
			directories = directories[0].GetDirectories();
			if (directories.Length == 0)
			{
				MessageBoxEx.Show(this, "当前缓存目录下文件无需清理", "提示");
				return;
			}
			string text;
			if (directories.Length > 1)
			{
				MessageBoxEx.Show("当前目录存在多个旧文件记录，请选择指定文件夹下Catalog.json文件，清理旧文件", "提示");
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.InitialDirectory = this.txtSaveDirectory.Text + "\\Archive";
				openFileDialog.Multiselect = false;
				if (openFileDialog.ShowDialog() != DialogResult.OK)
				{
					return;
				}
				text = openFileDialog.FileName;
			}
			else
			{
				text = this.txtSaveDirectory.Text.Trim() + "\\Archive\\" + directories[0].Name + "\\Catalog.json";
			}
			string startCmd = string.Concat(new string[]
			{
				this.getFileNameByTypeCombox(),
				" --layout ",
				this.txtSaveDirectory.Text.Trim(),
				" --clean ",
				text
			});
			this.RealAction(startCmd);
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

		private string baseFromPath = "https://aka.ms/vs/16/release/";

		private string baseDownPath = Environment.CurrentDirectory;

		private List<MainForm.DicNode> workloadList = new List<MainForm.DicNode>
		{
			new MainForm.DicNode
			{
				name = "CoreEditor",
				text = "Visual Studio 核心编辑器",
				toolTip = "Visual Studio 核心 shell 体验，包括语法感知代码编辑、源代码管理和工作项管理。",
				isChecked = true
			},
			new MainForm.DicNode
			{
				name = "Azure",
				text = "Azure 开发",
				toolTip = "用于开发云应用、创建资源以及生成包括 Docker 支持的容器的 Azure SDK、工具和项目。"
			},
			new MainForm.DicNode
			{
				name = "Data",
				text = "数据存储和处理",
				toolTip = "使用 SQL Server、Azure Data Lake 或 Hadoop 连接、开发和测试数据解决方案。"
			},
			new MainForm.DicNode
			{
				name = "DataScience",
				text = "数据科学和分析应用程序",
				toolTip = "用于创建数据科学应用程序的语言和工具（包括 Python、R 和 F#）。"
			},
			new MainForm.DicNode
			{
				name = "ManagedDesktop",
				text = ".NET 桌面开发",
				toolTip = "使用 C#、Visual Basic 和 F# 生成 WPF、Windows 窗体和控制台应用程序。"
			},
			new MainForm.DicNode
			{
				name = "ManagedGame",
				text = "使用 Unity 的游戏开发",
				toolTip = "使用 Unity（功能强大的跨平台开发环境）创建 2D 和 3D 游戏。"
			},
			new MainForm.DicNode
			{
				name = "NativeCrossPlat",
				text = "使用 C++ 的 Linux 开发",
				toolTip = "创建和调试在 Linux 环境中运行的应用程序。"
			},
			new MainForm.DicNode
			{
				name = "NativeDesktop",
				text = "使用 C++ 的桌面开发",
				toolTip = "使用 Microsoft C++ 工具集、ATL 或 MFC 生成 Windows 桌面应用程序。"
			},
			new MainForm.DicNode
			{
				name = "NativeGame",
				text = "使用 C++ 的游戏开发",
				toolTip = "以 DirectX、Unreal 或 Cocos2d 为后盾，利用 C++ 的强大功能生成专业游戏。"
			},
			new MainForm.DicNode
			{
				name = "NativeMobile",
				text = "使用 C++ 的移动开发",
				toolTip = "使用 C++ 生成适用于 iOS、Android 或 Windows 的跨平台应用程序。"
			},
			new MainForm.DicNode
			{
				name = "NetCoreTools",
				text = ".NET Core 跨平台开发",
				toolTip = "使用 .NET Core、ASP.NET Core、HTML/JavaScript 和包括 Docker 支持的容器生成跨平台应用程序。"
			},
			new MainForm.DicNode
			{
				name = "NetCrossPlat",
				text = "使用 .NET 的移动开发",
				toolTip = "使用 Xmarin 生成适用于 iOS、Android 或 Windows 的跨平台应用程序。"
			},
			new MainForm.DicNode
			{
				name = "NetWeb",
				text = "ASP.NET 和 Web 开发",
				toolTip = "使用 ASP.NET、ASP.NET Core、HTML/JavaScript 和包括 Docker 支持的容器生成 Web 应用程序。"
			},
			new MainForm.DicNode
			{
				name = "Node",
				text = "Node.js 开发",
				toolTip = "使用 Node.js（事件驱动的异步 JavaScript 运行时）生成可扩展的网络应用程序。"
			},
			new MainForm.DicNode
			{
				name = "Office",
				text = "Office/SharePoint 开发",
				toolTip = "使用 C#、VB 和 JavaScript 创建 Office 和 SharePoint 外接程序、SharePoint 解决方案和 VSTO 外接程序。"
			},
			new MainForm.DicNode
			{
				name = "Python",
				text = "Python 开发",
				toolTip = "适用于 Python 的编辑、调试、交互式开发和源代码管理。"
			},
			new MainForm.DicNode
			{
				name = "Universal",
				text = "通用 Windows 平台开发",
				toolTip = "使用 C#、VB 和 JavaScript 或 C++（可选）创建适用于通用 Windows 平台的应用程序。"
			},
			new MainForm.DicNode
			{
				name = "VisualStudioExtension",
				text = "Visual Studio 扩展开发",
				toolTip = "创建适用于 Visual Studio 的加载项和扩展，包括新命令、代码分析器和工具窗口。"
			},
			new MainForm.DicNode
			{
				name = "WebCrossPlat",
				text = "使用 JavaScript 的移动开发",
				toolTip = "使用用于 Apache Cordova 的工具生成 Android、iOS 和 UWP 应用。"
			},
			new MainForm.DicNode
			{
				name = "Others",
				text = "独立组件",
				toolTip = "这些组件不随附于任何工作负载，但可选择作为单个组件。"
			}
		};

		// Token: 0x0400000C RID: 12
		private List<MainForm.DicNode> componentList = new List<MainForm.DicNode>
		{
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Component.Android.Emulator",
				text = "适用于 Android 的 Visual Studio 仿真程序"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Component.Android.NDK.R11C",
				text = "Android NDK (R11C)"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Component.Android.NDK.R11C_3264",
				text = "Android NDK (R11C)（32 位）"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Component.Android.SDK23",
				text = "Android SDK 安装程序（API 级别 23）（全局安装）"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Component.Android.SDK25",
				text = "Android SDK 安装程序（API 级别 25）"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Component.GitHub.VisualStudio",
				text = "适用于 Visual Studio 的 GitHub 扩展"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Component.Google.Android.Emulator.API23.V2",
				text = "Google Android Emulator（API 级别 23）（全局安装）"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Component.Google.Android.Emulator.API25",
				text = "Google Android Emulator（API 级别 25）"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.Component.Blend.SDK.WPF",
				text = "用于 .NET 的 Blend for Visual Studio SDK "
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.Component.HelpViewer",
				text = "帮助查看器"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.LinqToSql",
				text = "LINQ to SQL 工具"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.Phone.Emulator Windows",
				text = "10 移动版仿真程序（周年纪念版）"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.Phone.Emulator.15063",
				text = "Windows 10 Mobile 仿真器（创意者更新）"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.Runtime.Node.x86.6.4.0",
				text = "基于 Node.js v6.4.0 (x86) 的组件运行时"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.Runtime.Node.x86.7.4.0",
				text = "基于 Node.js v7.4.0 (x86) 的组件运行时"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.TestTools.CodedUITest",
				text = "编码的 UI 测试"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.TestTools.FeedbackClient",
				text = "Microsoft Feedback Client"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.TestTools.MicrosoftTestManager Microsoft",
				text = "测试管理器"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.TypeScript.2.0",
				text = "TypeScript 2.0 SDK"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.TypeScript.2.1",
				text = "TypeScript 2.1 SDK"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.TypeScript.2.2",
				text = "TypeScript 2.2 SDK"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.TypeScript.2.5",
				text = "TypeScript 2.5 SDK"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.TypeScript.2.6",
				text = "TypeScript 2.6 SDK"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.TypeScript.2.7",
				text = "TypeScript 2.7 SDK"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.UWP.VC.ARM64",
				text = "适用于 ARM64 的 C++ 通用 Windows 平台工具"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.VC.ATL.ARM.Spectre",
				text = "带有 Spectre 缓解措施的 Visual C++ ATL for ARM"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.VC.ATL.ARM64.Spectre",
				text = "带有 Spectre 缓解措施的 Visual C++ ATL for ARM64"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.VC.ATL.Spectre",
				text = "带有 Spectre 缓解措施的 Visual C++ ATL (x86/x64)"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.VC.ATLMFC.Spectre",
				text = "带有 Spectre 缓解措施的 Visual C++ MFC for x86/x64"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.VC.ClangC2",
				text = "Clang/C2（实验）"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.VC.MFC.ARM",
				text = "Visual C++ MFC for ARM"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.VC.MFC.ARM.Spectre",
				text = "带有 Spectre 缓解措施的 Visual C++ MFC for ARM"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.VC.MFC.ARM64",
				text = "Visual C++ MFC for ARM64"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.VC.MFC.ARM64.Spectre",
				text = "带有 Spectre 缓解措施的针对 ARM64 的 Visual C++ MFC 支持"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.VC.Runtimes.ARM.Spectre",
				text = "面向 Spectre 的 VC++ 2017 版本 15.7 v14.14 库 (ARM)"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.VC.Runtimes.ARM64.Spectre",
				text = "面向 Spectre 的 VC++ 2017 版本 15.7 v14.14 库 (ARM64)"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.VC.Runtimes.x86.x64.Spectre",
				text = "面向 Spectre 的 VC++ 2017 版本 15.7 v14.14 库 (x86 和 x64)"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.VC.Tools.14.11",
				text = "VC++ 2017 版本 15.4 v14.11 工具集"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.VC.Tools.14.12",
				text = "VC++ 2017 版本 15.5 v14.12 工具集"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.VC.Tools.14.13",
				text = "VC++ 2017 版本 15.6 v14.13 工具集"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.VC.Tools.ARM64",
				text = "用于 ARM64 的 Visual C++ 编译器和库"
			},
			new MainForm.DicNode
			{
				parentname = "Others",
				name = "Microsoft.VisualStudio.Component.Windows10SDK.16299.Desktop.arm",
				text = "适用于桌面 C++ [ARM 和 ARM64] 的 Windows 10 SDK"
			}
		};

		private class DicNode
		{
			/// <summary>
			/// 父节点名称
			/// </summary>
			public string parentname { get; set; }

			/// <summary>
			/// 名称
			/// </summary>
			public string name { get; set; }

			/// <summary>
			/// 文本
			/// </summary>
			public string text { get; set; }

			/// <summary>
			/// 工具提示
			/// </summary>
			public string toolTip { get; set; }

			/// <summary>
			/// 是否选中
			/// </summary>
			public bool isChecked { get; set; }
		}

		public delegate string MyDelegate();

	}
}
