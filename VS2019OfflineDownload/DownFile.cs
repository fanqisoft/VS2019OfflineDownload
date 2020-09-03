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
		private void DownloadFile(string url, string saveFile, Action<int> downloadProgressChanged, Action downloadFileCompleted)
		{
			WebClient webClient = new WebClient();
			webClient.Proxy = null;
			if (downloadProgressChanged != null)
			{
				webClient.DownloadProgressChanged += delegate (object sender, DownloadProgressChangedEventArgs e)
				{
					this.Invoke(downloadProgressChanged, new object[]
					{
						e.ProgressPercentage
					});
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

		private void DownloadFileCompleted()
		{
			using (var file = File.Open(downPath, FileMode.OpenOrCreate))
			{
				if (file.Length == 0)
				{
					this.label1.Text = "下载失败，请检查网络连接！";
					Thread.Sleep(1000);
					base.DialogResult = DialogResult.No;
					base.Close();
				}
				else
				{
					this.label1.Text = "下载完成";
					Thread.Sleep(1000);
					base.DialogResult = DialogResult.OK;
					base.Close();
				}
			}
			if (base.DialogResult == DialogResult.No)
            {
				File.Delete(downPath);
            }
		}

		private void DownloadProgressChanged(int val)
		{
			this.progressBar1.Value = val;
			this.progressBar1.PerformStep();
		}

		private void DownFile_Shown(object sender, EventArgs e)
		{
			this.DownloadFile(this.fromPath, this.downPath, new Action<int>(this.DownloadProgressChanged), new Action(this.DownloadFileCompleted));
		}

	}
}
