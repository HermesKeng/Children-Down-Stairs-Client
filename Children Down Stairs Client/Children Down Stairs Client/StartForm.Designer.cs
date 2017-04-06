namespace Children_Down_Stairs_Client {

    partial class StartForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.startButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.IDTextBox = new System.Windows.Forms.TextBox();
            this.serverIPTextBox = new System.Windows.Forms.TextBox();
            this.IDLabel = new System.Windows.Forms.Label();
            this.onlineCheckBox = new System.Windows.Forms.CheckBox();
            this.IPLable = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.startButton.Location = new System.Drawing.Point(503, 283);
            this.startButton.Margin = new System.Windows.Forms.Padding(4);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(169, 34);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.exitButton.Location = new System.Drawing.Point(503, 321);
            this.exitButton.Margin = new System.Windows.Forms.Padding(4);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(169, 34);
            this.exitButton.TabIndex = 4;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Children_Down_Stairs_Client.Properties.Resources.down_stairs;
            this.pictureBox1.Location = new System.Drawing.Point(99, 47);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(624, 341);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // IDTextBox
            // 
            this.IDTextBox.Location = new System.Drawing.Point(231, 283);
            this.IDTextBox.Name = "IDTextBox";
            this.IDTextBox.Size = new System.Drawing.Size(236, 25);
            this.IDTextBox.TabIndex = 0;
            this.IDTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IDTextBox_KeyDown);
            // 
            // serverIPTextBox
            // 
            this.serverIPTextBox.Enabled = false;
            this.serverIPTextBox.Location = new System.Drawing.Point(231, 325);
            this.serverIPTextBox.Name = "serverIPTextBox";
            this.serverIPTextBox.Size = new System.Drawing.Size(236, 25);
            this.serverIPTextBox.TabIndex = 2;
            this.serverIPTextBox.Text = "Click online to unlock";
            this.serverIPTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.serverIPTextBox_KeyDown);
            // 
            // IDLabel
            // 
            this.IDLabel.AutoSize = true;
            this.IDLabel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.IDLabel.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.IDLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.IDLabel.Location = new System.Drawing.Point(168, 283);
            this.IDLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.IDLabel.Name = "IDLabel";
            this.IDLabel.Size = new System.Drawing.Size(44, 30);
            this.IDLabel.TabIndex = 5;
            this.IDLabel.Text = "ID:";
            // 
            // onlineCheckBox
            // 
            this.onlineCheckBox.AutoSize = true;
            this.onlineCheckBox.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.onlineCheckBox.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.onlineCheckBox.Location = new System.Drawing.Point(300, 395);
            this.onlineCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.onlineCheckBox.Name = "onlineCheckBox";
            this.onlineCheckBox.Size = new System.Drawing.Size(95, 29);
            this.onlineCheckBox.TabIndex = 1;
            this.onlineCheckBox.Text = "Online";
            this.onlineCheckBox.UseVisualStyleBackColor = true;
            this.onlineCheckBox.CheckedChanged += new System.EventHandler(this.onlineCheckBox_CheckedChanged);
            // 
            // IPLable
            // 
            this.IPLable.AutoSize = true;
            this.IPLable.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.IPLable.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.IPLable.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.IPLable.Location = new System.Drawing.Point(94, 320);
            this.IPLable.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.IPLable.Name = "IPLable";
            this.IPLable.Size = new System.Drawing.Size(118, 30);
            this.IPLable.TabIndex = 6;
            this.IPLable.Text = "Server IP:";
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(849, 506);
            this.Controls.Add(this.IPLable);
            this.Controls.Add(this.onlineCheckBox);
            this.Controls.Add(this.IDLabel);
            this.Controls.Add(this.serverIPTextBox);
            this.Controls.Add(this.IDTextBox);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.pictureBox1);
            this.Name = "StartForm";
            this.Text = "Children Down Stairs Start";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.TextBox IDTextBox;
        private System.Windows.Forms.TextBox serverIPTextBox;
        private System.Windows.Forms.Label IDLabel;
        private System.Windows.Forms.CheckBox onlineCheckBox;
        private System.Windows.Forms.Label IPLable;
    }
}

