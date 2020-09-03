namespace VS2019OfflineDownload
{
    partial class DownFile
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 29);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(386, 23);
            this.progressBar1.TabIndex = 0;
            this.label1.Location = new System.Drawing.Point(140, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "正在下载中...";
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(386, 52);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.progressBar1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            base.Name = "DownFile";
            base.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "DownFile";
            base.TransparencyKey = System.Drawing.Color.Transparent;
            base.Shown += new System.EventHandler(this.DownFile_Shown);
            base.ResumeLayout(false);
        }

        private System.Windows.Forms.ProgressBar progressBar1;

        private System.Windows.Forms.Label label1;

        #endregion
    }
}