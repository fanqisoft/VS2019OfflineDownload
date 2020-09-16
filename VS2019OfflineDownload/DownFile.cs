using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace VS2019OfflineDownload
{
    public partial class DownFile : Form
    {
		private string fromPath = string.Empty;

		private string downPath = string.Empty;

		public DownFile(string fromPath, string downPath)
        {
            InitializeComponent();
            this.fromPath = fromPath;
            this.downPath = downPath;
        }

		/// <summary>
		/// 下载文件
		/// </summary>
		/// <param name="url"></param>
		/// <param name="saveFile"></param>
		/// <param name="downloadProgressChanged"></param>
		/// <param name="downloadFileCompleted"></param>
		private void DownloadFile(string url, string saveFile, Action<int> downloadProgressChanged, Action downloadFileCompleted)
		{
			WebClient webClient = new WebClient();
			webClient.Proxy = null;
			if (downloadProgressChanged != null)
			{
				webClient.DownloadProgressChanged += delegate (object sender, DownloadProgressChangedEventArgs e)
				{
					this.Invoke(downloadProgressChanged, e.ProgressPercentage);
				};
			}
			if (downloadFileCompleted != null)
			{
				webClient.DownloadFileCompleted += delegate (object sender, AsyncCompletedEventArgs e)
				{
					this.Invoke(downloadFileCompleted);
				};
			}
			webClient.DownloadFileAsync(new Uri(url), saveFile);
		}

		/// <summary>
		/// 下载完成
		/// </summary>
		private void DownloadFileCompleted()
		{
			using (var file = File.Open(downPath, FileMode.OpenOrCreate))
			{
				if (file.Length == 0)
				{
					this.label1.Text = "下载失败，请检查网络连接！";
					Thread.Sleep(1000);
					this.DialogResult = DialogResult.No;
					this.Close();
				}
				else
				{
					this.label1.Text = "下载完成";
					Thread.Sleep(1000);
					this.DialogResult = DialogResult.OK;
					this.Close();
				}
			}
			if (this.DialogResult == DialogResult.No)
            {
				File.Delete(downPath);
            }
		}

		/// <summary>
		/// 显示进度
		/// </summary>
		/// <param name="val"></param>
		private void DownloadProgressChanged(int val)
		{
			this.progressBar1.Value = val;
			this.progressBar1.PerformStep();
		}

		private void DownFile_Shown(object sender, EventArgs e)
		{
            DownloadFile(this.fromPath, this.downPath, DownloadProgressChanged, DownloadFileCompleted);
		}

	}
}
