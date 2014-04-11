namespace Hax
{
    partial class MainMenu1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu1));
            this.mainCharPicture = new System.Windows.Forms.PictureBox();
            this.HaxTitle = new System.Windows.Forms.Label();
            this.playButton = new System.Windows.Forms.Button();
            this.quitButton = new System.Windows.Forms.Button();
            this.jumpMainPicture = new System.Windows.Forms.PictureBox();
            this.enemyPicture = new System.Windows.Forms.PictureBox();
            this.elevatorPicture = new System.Windows.Forms.PictureBox();
            this.boxPicrure = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.mainCharPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jumpMainPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemyPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.elevatorPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boxPicrure)).BeginInit();
            this.SuspendLayout();
            // 
            // mainCharPicture
            // 
            this.mainCharPicture.Image = ((System.Drawing.Image)(resources.GetObject("mainCharPicture.Image")));
            this.mainCharPicture.Location = new System.Drawing.Point(228, 10);
            this.mainCharPicture.Name = "mainCharPicture";
            this.mainCharPicture.Size = new System.Drawing.Size(55, 85);
            this.mainCharPicture.TabIndex = 0;
            this.mainCharPicture.TabStop = false;
            // 
            // HaxTitle
            // 
            this.HaxTitle.AutoSize = true;
            this.HaxTitle.Font = new System.Drawing.Font("Courier New", 72F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HaxTitle.Location = new System.Drawing.Point(37, 10);
            this.HaxTitle.Name = "HaxTitle";
            this.HaxTitle.Size = new System.Drawing.Size(219, 107);
            this.HaxTitle.TabIndex = 1;
            this.HaxTitle.Text = "Hax";
            this.HaxTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // playButton
            // 
            this.playButton.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playButton.Location = new System.Drawing.Point(380, 210);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(195, 70);
            this.playButton.TabIndex = 2;
            this.playButton.Text = "Play!";
            this.playButton.UseVisualStyleBackColor = true;
            // 
            // quitButton
            // 
            this.quitButton.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quitButton.Location = new System.Drawing.Point(380, 311);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(195, 70);
            this.quitButton.TabIndex = 3;
            this.quitButton.Text = "Quit";
            this.quitButton.UseVisualStyleBackColor = true;
            // 
            // jumpMainPicture
            // 
            this.jumpMainPicture.Image = ((System.Drawing.Image)(resources.GetObject("jumpMainPicture.Image")));
            this.jumpMainPicture.Location = new System.Drawing.Point(428, 489);
            this.jumpMainPicture.Name = "jumpMainPicture";
            this.jumpMainPicture.Size = new System.Drawing.Size(55, 85);
            this.jumpMainPicture.TabIndex = 4;
            this.jumpMainPicture.TabStop = false;
            // 
            // enemyPicture
            // 
            this.enemyPicture.Image = ((System.Drawing.Image)(resources.GetObject("enemyPicture.Image")));
            this.enemyPicture.Location = new System.Drawing.Point(752, 546);
            this.enemyPicture.Name = "enemyPicture";
            this.enemyPicture.Size = new System.Drawing.Size(55, 57);
            this.enemyPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.enemyPicture.TabIndex = 5;
            this.enemyPicture.TabStop = false;
            // 
            // elevatorPicture
            // 
            this.elevatorPicture.Image = ((System.Drawing.Image)(resources.GetObject("elevatorPicture.Image")));
            this.elevatorPicture.Location = new System.Drawing.Point(12, 518);
            this.elevatorPicture.Name = "elevatorPicture";
            this.elevatorPicture.Size = new System.Drawing.Size(55, 85);
            this.elevatorPicture.TabIndex = 6;
            this.elevatorPicture.TabStop = false;
            // 
            // boxPicrure
            // 
            this.boxPicrure.Image = ((System.Drawing.Image)(resources.GetObject("boxPicrure.Image")));
            this.boxPicrure.Location = new System.Drawing.Point(452, 560);
            this.boxPicrure.Name = "boxPicrure";
            this.boxPicrure.Size = new System.Drawing.Size(49, 43);
            this.boxPicrure.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.boxPicrure.TabIndex = 7;
            this.boxPicrure.TabStop = false;
            // 
            // MainMenu1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 604);
            this.Controls.Add(this.boxPicrure);
            this.Controls.Add(this.elevatorPicture);
            this.Controls.Add(this.enemyPicture);
            this.Controls.Add(this.mainCharPicture);
            this.Controls.Add(this.quitButton);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.HaxTitle);
            this.Controls.Add(this.jumpMainPicture);
            this.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainMenu1";
            this.Text = "Hax Main Menu";
            ((System.ComponentModel.ISupportInitialize)(this.mainCharPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jumpMainPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemyPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.elevatorPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boxPicrure)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mainCharPicture;
        private System.Windows.Forms.Label HaxTitle;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.PictureBox jumpMainPicture;
        private System.Windows.Forms.PictureBox enemyPicture;
        private System.Windows.Forms.PictureBox elevatorPicture;
        private System.Windows.Forms.PictureBox boxPicrure;
    }
}