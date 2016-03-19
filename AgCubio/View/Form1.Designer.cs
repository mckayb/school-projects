namespace Client
{
    partial class Form1
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
            this.playerName = new System.Windows.Forms.TextBox();
            this.serverName = new System.Windows.Forms.TextBox();
            this.playerNameLabel = new System.Windows.Forms.Label();
            this.serverNameLabel = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.IntroPanel = new System.Windows.Forms.Panel();
            this.Subheading = new System.Windows.Forms.Label();
            this.Title = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.frameRateLabel = new System.Windows.Forms.Label();
            this.massLabel = new System.Windows.Forms.Label();
            this.foodLabel = new System.Windows.Forms.Label();
            this.widthLabel = new System.Windows.Forms.Label();
            this.IntroPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // playerName
            // 
            this.playerName.Font = new System.Drawing.Font("Quartz MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerName.Location = new System.Drawing.Point(417, 225);
            this.playerName.Margin = new System.Windows.Forms.Padding(6);
            this.playerName.Name = "playerName";
            this.playerName.Size = new System.Drawing.Size(363, 33);
            this.playerName.TabIndex = 0;
            this.playerName.Text = "Player1";
            // 
            // serverName
            // 
            this.serverName.Font = new System.Drawing.Font("Quartz MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverName.Location = new System.Drawing.Point(417, 283);
            this.serverName.Margin = new System.Windows.Forms.Padding(6);
            this.serverName.Name = "serverName";
            this.serverName.Size = new System.Drawing.Size(363, 33);
            this.serverName.TabIndex = 1;
            this.serverName.Text = "localhost";
            // 
            // playerNameLabel
            // 
            this.playerNameLabel.AutoSize = true;
            this.playerNameLabel.Font = new System.Drawing.Font("Quartz MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerNameLabel.Location = new System.Drawing.Point(252, 225);
            this.playerNameLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.playerNameLabel.Name = "playerNameLabel";
            this.playerNameLabel.Size = new System.Drawing.Size(153, 25);
            this.playerNameLabel.TabIndex = 2;
            this.playerNameLabel.Text = "Player Name";
            // 
            // serverNameLabel
            // 
            this.serverNameLabel.AutoSize = true;
            this.serverNameLabel.Font = new System.Drawing.Font("Quartz MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverNameLabel.Location = new System.Drawing.Point(317, 283);
            this.serverNameLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.serverNameLabel.Name = "serverNameLabel";
            this.serverNameLabel.Size = new System.Drawing.Size(88, 25);
            this.serverNameLabel.TabIndex = 3;
            this.serverNameLabel.Text = "Server";
            // 
            // connectButton
            // 
            this.connectButton.Font = new System.Drawing.Font("Quartz MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectButton.Location = new System.Drawing.Point(417, 340);
            this.connectButton.Margin = new System.Windows.Forms.Padding(6);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(363, 42);
            this.connectButton.TabIndex = 4;
            this.connectButton.Text = "Let\'s Go!";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // IntroPanel
            // 
            this.IntroPanel.BackColor = System.Drawing.Color.SlateBlue;
            this.IntroPanel.Controls.Add(this.Subheading);
            this.IntroPanel.Controls.Add(this.Title);
            this.IntroPanel.Controls.Add(this.errorLabel);
            this.IntroPanel.Controls.Add(this.connectButton);
            this.IntroPanel.Controls.Add(this.serverNameLabel);
            this.IntroPanel.Controls.Add(this.playerNameLabel);
            this.IntroPanel.Controls.Add(this.serverName);
            this.IntroPanel.Controls.Add(this.playerName);
            this.IntroPanel.Font = new System.Drawing.Font("Motorwerk", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IntroPanel.Location = new System.Drawing.Point(0, 0);
            this.IntroPanel.Margin = new System.Windows.Forms.Padding(6);
            this.IntroPanel.Name = "IntroPanel";
            this.IntroPanel.Size = new System.Drawing.Size(1210, 622);
            this.IntroPanel.TabIndex = 5;
            // 
            // Subheading
            // 
            this.Subheading.AutoSize = true;
            this.Subheading.Font = new System.Drawing.Font("Quartz MS", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Subheading.Location = new System.Drawing.Point(389, 171);
            this.Subheading.Name = "Subheading";
            this.Subheading.Size = new System.Drawing.Size(444, 33);
            this.Subheading.TabIndex = 7;
            this.Subheading.Text = "Not your mother\'s cube game!";
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Quartz MS", 72F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.ForeColor = System.Drawing.Color.Crimson;
            this.Title.Location = new System.Drawing.Point(397, 56);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(436, 115);
            this.Title.TabIndex = 6;
            this.Title.Text = "AgCubio";
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Font = new System.Drawing.Font("Quartz MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorLabel.Location = new System.Drawing.Point(252, 479);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(0, 25);
            this.errorLabel.TabIndex = 5;
            // 
            // frameRateLabel
            // 
            this.frameRateLabel.AutoSize = true;
            this.frameRateLabel.Font = new System.Drawing.Font("Quartz MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frameRateLabel.Location = new System.Drawing.Point(1071, 24);
            this.frameRateLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.frameRateLabel.Name = "frameRateLabel";
            this.frameRateLabel.Size = new System.Drawing.Size(67, 23);
            this.frameRateLabel.TabIndex = 6;
            this.frameRateLabel.Text = "FPS: 0";
            // 
            // massLabel
            // 
            this.massLabel.AutoSize = true;
            this.massLabel.Font = new System.Drawing.Font("Quartz MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.massLabel.Location = new System.Drawing.Point(1071, 59);
            this.massLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.massLabel.Name = "massLabel";
            this.massLabel.Size = new System.Drawing.Size(87, 23);
            this.massLabel.TabIndex = 7;
            this.massLabel.Text = "Mass: 0";
            // 
            // foodLabel
            // 
            this.foodLabel.AutoSize = true;
            this.foodLabel.Font = new System.Drawing.Font("Quartz MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.foodLabel.Location = new System.Drawing.Point(1071, 97);
            this.foodLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.foodLabel.Name = "foodLabel";
            this.foodLabel.Size = new System.Drawing.Size(79, 23);
            this.foodLabel.TabIndex = 8;
            this.foodLabel.Text = "Food: 0";
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Font = new System.Drawing.Font("Quartz MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.widthLabel.Location = new System.Drawing.Point(1071, 138);
            this.widthLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(93, 23);
            this.widthLabel.TabIndex = 9;
            this.widthLabel.Text = "Width: 0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 621);
            this.Controls.Add(this.IntroPanel);
            this.Controls.Add(this.frameRateLabel);
            this.Controls.Add(this.massLabel);
            this.Controls.Add(this.foodLabel);
            this.Controls.Add(this.widthLabel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.Text = "AgCubio";
            this.IntroPanel.ResumeLayout(false);
            this.IntroPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox playerName;
        private System.Windows.Forms.TextBox serverName;
        private System.Windows.Forms.Label playerNameLabel;
        private System.Windows.Forms.Label serverNameLabel;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Panel IntroPanel;
        private System.Windows.Forms.Label frameRateLabel;
        private System.Windows.Forms.Label massLabel;
        private System.Windows.Forms.Label foodLabel;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Label Subheading;
    }
}

