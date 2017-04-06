namespace Children_Down_Stairs_Client
{
    partial class GameOnlineForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameOnlineForm));
            this.recevBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.MapPanel = new System.Windows.Forms.Panel();
            this.EndButton = new System.Windows.Forms.Button();
            this.MapTimer = new System.Windows.Forms.Timer(this.components);
            this.DownStairsPic = new System.Windows.Forms.PictureBox();
            this.floorBoardPic = new System.Windows.Forms.PictureBox();
            this.floorText = new System.Windows.Forms.Label();
            this.floorBarText = new System.Windows.Forms.Label();
            this.P1Label = new System.Windows.Forms.Label();
            this.P2Label = new System.Windows.Forms.Label();
            this.P3Label = new System.Windows.Forms.Label();
            this.P4Label = new System.Windows.Forms.Label();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.EndLabel = new System.Windows.Forms.Label();
            this.MapPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DownStairsPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.floorBoardPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.SuspendLayout();
            // 
            // MapPanel
            // 
            this.MapPanel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.MapPanel.Controls.Add(this.EndLabel);
            this.MapPanel.Controls.Add(this.EndButton);
            this.MapPanel.Location = new System.Drawing.Point(57, 30);
            this.MapPanel.Name = "MapPanel";
            this.MapPanel.Size = new System.Drawing.Size(600, 700);
            this.MapPanel.TabIndex = 8;
            // 
            // EndButton
            // 
            this.EndButton.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.EndButton.Location = new System.Drawing.Point(203, 567);
            this.EndButton.Name = "EndButton";
            this.EndButton.Size = new System.Drawing.Size(186, 64);
            this.EndButton.TabIndex = 4;
            this.EndButton.Text = "End";
            this.EndButton.UseVisualStyleBackColor = true;
            this.EndButton.Visible = false;
            this.EndButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // MapTimer
            // 
            this.MapTimer.Interval = 1;
            this.MapTimer.Tick += new System.EventHandler(this.MapTimer_Tick);
            // 
            // DownStairsPic
            // 
            this.DownStairsPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.DownStairsPic.Image = global::Children_Down_Stairs_Client.Properties.Resources.down_stairs;
            this.DownStairsPic.Location = new System.Drawing.Point(663, 30);
            this.DownStairsPic.Name = "DownStairsPic";
            this.DownStairsPic.Size = new System.Drawing.Size(208, 134);
            this.DownStairsPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.DownStairsPic.TabIndex = 9;
            this.DownStairsPic.TabStop = false;
            // 
            // floorBoardPic
            // 
            this.floorBoardPic.Image = global::Children_Down_Stairs_Client.Properties.Resources.leaderboard;
            this.floorBoardPic.Location = new System.Drawing.Point(663, 170);
            this.floorBoardPic.Name = "floorBoardPic";
            this.floorBoardPic.Size = new System.Drawing.Size(208, 186);
            this.floorBoardPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.floorBoardPic.TabIndex = 10;
            this.floorBoardPic.TabStop = false;
            // 
            // floorText
            // 
            this.floorText.AutoSize = true;
            this.floorText.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.floorText.Font = new System.Drawing.Font("微軟正黑體", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.floorText.ForeColor = System.Drawing.Color.MediumBlue;
            this.floorText.Location = new System.Drawing.Point(744, 233);
            this.floorText.Name = "floorText";
            this.floorText.Size = new System.Drawing.Size(56, 61);
            this.floorText.TabIndex = 11;
            this.floorText.Text = "1";
            // 
            // floorBarText
            // 
            this.floorBarText.AutoSize = true;
            this.floorBarText.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.floorBarText.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.floorBarText.ForeColor = System.Drawing.Color.MediumBlue;
            this.floorBarText.Location = new System.Drawing.Point(692, 217);
            this.floorBarText.Name = "floorBarText";
            this.floorBarText.Size = new System.Drawing.Size(46, 21);
            this.floorBarText.TabIndex = 12;
            this.floorBarText.Text = "floor";
            // 
            // P1Label
            // 
            this.P1Label.AutoSize = true;
            this.P1Label.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.P1Label.Enabled = false;
            this.P1Label.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.P1Label.ForeColor = System.Drawing.Color.MediumBlue;
            this.P1Label.Location = new System.Drawing.Point(670, 400);
            this.P1Label.Name = "P1Label";
            this.P1Label.Size = new System.Drawing.Size(37, 27);
            this.P1Label.TabIndex = 13;
            this.P1Label.Text = "P1";
            // 
            // P2Label
            // 
            this.P2Label.AutoSize = true;
            this.P2Label.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.P2Label.Enabled = false;
            this.P2Label.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.P2Label.ForeColor = System.Drawing.Color.MediumBlue;
            this.P2Label.Location = new System.Drawing.Point(670, 450);
            this.P2Label.Name = "P2Label";
            this.P2Label.Size = new System.Drawing.Size(37, 27);
            this.P2Label.TabIndex = 14;
            this.P2Label.Text = "P2";
            // 
            // P3Label
            // 
            this.P3Label.AutoSize = true;
            this.P3Label.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.P3Label.Enabled = false;
            this.P3Label.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.P3Label.ForeColor = System.Drawing.Color.MediumBlue;
            this.P3Label.Location = new System.Drawing.Point(670, 500);
            this.P3Label.Name = "P3Label";
            this.P3Label.Size = new System.Drawing.Size(37, 27);
            this.P3Label.TabIndex = 15;
            this.P3Label.Text = "P3";
            // 
            // P4Label
            // 
            this.P4Label.AutoSize = true;
            this.P4Label.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.P4Label.Enabled = false;
            this.P4Label.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.P4Label.ForeColor = System.Drawing.Color.MediumBlue;
            this.P4Label.Location = new System.Drawing.Point(670, 550);
            this.P4Label.Name = "P4Label";
            this.P4Label.Size = new System.Drawing.Size(37, 27);
            this.P4Label.TabIndex = 16;
            this.P4Label.Text = "P4";
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(13, 13);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(75, 23);
            this.axWindowsMediaPlayer1.TabIndex = 17;
            // 
            // label1
            // 
            this.EndLabel.AutoSize = true;
            this.EndLabel.Font = new System.Drawing.Font("微軟正黑體", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.EndLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.EndLabel.Location = new System.Drawing.Point(192, 343);
            this.EndLabel.Name = "label1";
            this.EndLabel.Size = new System.Drawing.Size(219, 61);
            this.EndLabel.TabIndex = 5;
            this.EndLabel.Text = "遊戲結束";
            this.EndLabel.Visible = false;
            // 
            // GameOnlineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Children_Down_Stairs_Client.Properties.Resources.new_game_background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(928, 759);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Controls.Add(this.P4Label);
            this.Controls.Add(this.P3Label);
            this.Controls.Add(this.P2Label);
            this.Controls.Add(this.P1Label);
            this.Controls.Add(this.floorBarText);
            this.Controls.Add(this.floorText);
            this.Controls.Add(this.floorBoardPic);
            this.Controls.Add(this.DownStairsPic);
            this.Controls.Add(this.MapPanel);
            this.Name = "GameOnlineForm";
            this.Text = "GameOnlineForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameOnlineForm_FormClosing);
            this.Load += new System.EventHandler(this.GameOnlineForm_Load);
            this.Shown += new System.EventHandler(this.GameOnlineForm_Shown);
            this.MapPanel.ResumeLayout(false);
            this.MapPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DownStairsPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.floorBoardPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker recevBackgroundWorker;
        private System.Windows.Forms.Panel MapPanel;
        private System.Windows.Forms.Timer MapTimer;
        private System.Windows.Forms.Button EndButton;
        private System.Windows.Forms.PictureBox DownStairsPic;
        private System.Windows.Forms.PictureBox floorBoardPic;
        private System.Windows.Forms.Label floorText;
        private System.Windows.Forms.Label floorBarText;
        private System.Windows.Forms.Label P1Label;
        private System.Windows.Forms.Label P2Label;
        private System.Windows.Forms.Label P3Label;
        private System.Windows.Forms.Label P4Label;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.Label EndLabel;
    }
}