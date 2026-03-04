using FrostPlayer.Controls;
using System.Drawing;
using System.Windows.Forms;

namespace FrostPlayer
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.trackInfoLabel = new System.Windows.Forms.Label();
            this.currentTimeLabel = new System.Windows.Forms.Label();
            this.durationLabel = new System.Windows.Forms.Label();
            this.playButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.prevButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.playlist = new FrostPlayer.Controls.PlaylistControl();
            this.volumeControl1 = new FrostPlayer.Controls.VolumeControl();
            this.progressBar = new FrostPlayer.Controls.MediaProgressBar();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackInfoLabel
            // 
            this.trackInfoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackInfoLabel.AutoSize = true;
            this.trackInfoLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.trackInfoLabel.Location = new System.Drawing.Point(12, 15);
            this.trackInfoLabel.Name = "trackInfoLabel";
            this.trackInfoLabel.Size = new System.Drawing.Size(45, 15);
            this.trackInfoLabel.TabIndex = 0;
            this.trackInfoLabel.Text = "Трек: -";
            // 
            // currentTimeLabel
            // 
            this.currentTimeLabel.AutoSize = true;
            this.currentTimeLabel.Location = new System.Drawing.Point(12, 69);
            this.currentTimeLabel.Name = "currentTimeLabel";
            this.currentTimeLabel.Size = new System.Drawing.Size(34, 13);
            this.currentTimeLabel.TabIndex = 2;
            this.currentTimeLabel.Text = "00:00";
            // 
            // durationLabel
            // 
            this.durationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.durationLabel.AutoSize = true;
            this.durationLabel.Location = new System.Drawing.Point(578, 69);
            this.durationLabel.Name = "durationLabel";
            this.durationLabel.Size = new System.Drawing.Size(28, 13);
            this.durationLabel.TabIndex = 3;
            this.durationLabel.Text = "0:00";
            this.durationLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(95, 3);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(86, 30);
            this.playButton.TabIndex = 4;
            this.playButton.Text = "▶️ Воспр.";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(187, 3);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 30);
            this.stopButton.TabIndex = 5;
            this.stopButton.Text = "⏹️ Стоп";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // prevButton
            // 
            this.prevButton.Location = new System.Drawing.Point(14, 3);
            this.prevButton.Name = "prevButton";
            this.prevButton.Size = new System.Drawing.Size(75, 30);
            this.prevButton.TabIndex = 6;
            this.prevButton.Text = "⏮️ Пред.";
            this.prevButton.UseVisualStyleBackColor = true;
            this.prevButton.Click += new System.EventHandler(this.PrevButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(268, 3);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 30);
            this.nextButton.TabIndex = 7;
            this.nextButton.Text = "⏭️ След.";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addButton.Location = new System.Drawing.Point(12, 3);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(90, 30);
            this.addButton.TabIndex = 11;
            this.addButton.Text = "➕ Добавить";
            this.addButton.UseVisualStyleBackColor = true;
            // 
            // removeButton
            // 
            this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeButton.Location = new System.Drawing.Point(108, 3);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(90, 30);
            this.removeButton.TabIndex = 12;
            this.removeButton.Text = "➖ Удалить";
            this.removeButton.UseVisualStyleBackColor = true;
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearButton.Location = new System.Drawing.Point(463, 3);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(90, 30);
            this.clearButton.TabIndex = 13;
            this.clearButton.Text = "🗑️ Очистить";
            this.clearButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "Плейлист";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.prevButton);
            this.panel1.Controls.Add(this.playButton);
            this.panel1.Controls.Add(this.stopButton);
            this.panel1.Controls.Add(this.nextButton);
            this.panel1.Location = new System.Drawing.Point(12, 85);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 36);
            this.panel1.TabIndex = 15;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.addButton);
            this.panel3.Controls.Add(this.removeButton);
            this.panel3.Controls.Add(this.clearButton);
            this.panel3.Location = new System.Drawing.Point(12, 352);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(583, 37);
            this.panel3.TabIndex = 17;
            // 
            // playlist
            // 
            this.playlist.AlternateRowBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.playlist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playlist.DurationTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.playlist.HeaderBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.playlist.HeaderHeight = 20;
            this.playlist.HeaderTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.playlist.Location = new System.Drawing.Point(12, 160);
            this.playlist.Name = "playlist";
            this.playlist.PlayingIndicatorColor = System.Drawing.Color.DodgerBlue;
            this.playlist.RowBackgroundColor = System.Drawing.Color.White;
            this.playlist.RowHeight = 20;
            this.playlist.SelectedItem = null;
            this.playlist.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            this.playlist.Size = new System.Drawing.Size(583, 186);
            this.playlist.TabIndex = 20;
            this.playlist.Text = "playlist";
            this.playlist.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.playlist.WidthDurationColumn = 90;
            // 
            // volumeControl1
            // 
            this.volumeControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.volumeControl1.FillColor = System.Drawing.Color.DodgerBlue;
            this.volumeControl1.Location = new System.Drawing.Point(524, 43);
            this.volumeControl1.Name = "volumeControl1";
            this.volumeControl1.Size = new System.Drawing.Size(88, 23);
            this.volumeControl1.TabIndex = 18;
            this.volumeControl1.Text = "volumeControl1";
            this.volumeControl1.ThumbBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.volumeControl1.ThumbColor = System.Drawing.Color.White;
            this.volumeControl1.TrackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.volumeControl1.ValueChanged += new System.EventHandler(this.VolumeTrackBar_ValueChanged);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.BarBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.progressBar.BufferColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.progressBar.CurrentTime = 0D;
            this.progressBar.Duration = 0D;
            this.progressBar.Location = new System.Drawing.Point(12, 43);
            this.progressBar.Name = "progressBar";
            this.progressBar.ProgressColor = System.Drawing.Color.DodgerBlue;
            this.progressBar.ShowTimeTooltip = false;
            this.progressBar.Size = new System.Drawing.Size(506, 23);
            this.progressBar.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 461);
            this.Controls.Add(this.playlist);
            this.Controls.Add(this.volumeControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.durationLabel);
            this.Controls.Add(this.currentTimeLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.trackInfoLabel);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.MinimumSize = new System.Drawing.Size(640, 500);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frost Player";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label trackInfoLabel;
        private MediaProgressBar progressBar;
        private System.Windows.Forms.Label currentTimeLabel;
        private System.Windows.Forms.Label durationLabel;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button prevButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private VolumeControl volumeControl1;
        private PlaylistControl playlist;
    }
}