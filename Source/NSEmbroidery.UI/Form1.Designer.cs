namespace NSEmbroidery.UI
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
            this.buttonOpen = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBoxCurrentImage = new System.Windows.Forms.PictureBox();
            this.buttonChooseColor = new System.Windows.Forms.Button();
            this.panelColors = new System.Windows.Forms.Panel();
            this.pictureBoxResult = new System.Windows.Forms.PictureBox();
            this.buttonCreateScheme = new System.Windows.Forms.Button();
            this.labelCount = new System.Windows.Forms.Label();
            this.comboBoxSquareCount = new System.Windows.Forms.ComboBox();
            this.comboBoxResolution = new System.Windows.Forms.ComboBox();
            this.Resolution = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCurrentImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(12, 44);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(75, 23);
            this.buttonOpen.TabIndex = 0;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(998, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // pictureBoxCurrentImage
            // 
            this.pictureBoxCurrentImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxCurrentImage.Location = new System.Drawing.Point(12, 82);
            this.pictureBoxCurrentImage.Name = "pictureBoxCurrentImage";
            this.pictureBoxCurrentImage.Size = new System.Drawing.Size(418, 389);
            this.pictureBoxCurrentImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCurrentImage.TabIndex = 2;
            this.pictureBoxCurrentImage.TabStop = false;
            // 
            // buttonChooseColor
            // 
            this.buttonChooseColor.Location = new System.Drawing.Point(334, 44);
            this.buttonChooseColor.Name = "buttonChooseColor";
            this.buttonChooseColor.Size = new System.Drawing.Size(96, 23);
            this.buttonChooseColor.TabIndex = 3;
            this.buttonChooseColor.Text = "Choose color";
            this.buttonChooseColor.UseVisualStyleBackColor = true;
            this.buttonChooseColor.Click += new System.EventHandler(this.buttonChooseColor_Click);
            // 
            // panelColors
            // 
            this.panelColors.AutoScroll = true;
            this.panelColors.Location = new System.Drawing.Point(451, 82);
            this.panelColors.Name = "panelColors";
            this.panelColors.Size = new System.Drawing.Size(50, 389);
            this.panelColors.TabIndex = 4;
            // 
            // pictureBoxResult
            // 
            this.pictureBoxResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxResult.Location = new System.Drawing.Point(524, 82);
            this.pictureBoxResult.Name = "pictureBoxResult";
            this.pictureBoxResult.Size = new System.Drawing.Size(414, 389);
            this.pictureBoxResult.TabIndex = 5;
            this.pictureBoxResult.TabStop = false;
            // 
            // buttonCreateScheme
            // 
            this.buttonCreateScheme.Location = new System.Drawing.Point(524, 44);
            this.buttonCreateScheme.Name = "buttonCreateScheme";
            this.buttonCreateScheme.Size = new System.Drawing.Size(96, 23);
            this.buttonCreateScheme.TabIndex = 6;
            this.buttonCreateScheme.TabStop = false;
            this.buttonCreateScheme.Text = "Create scheme";
            this.buttonCreateScheme.UseVisualStyleBackColor = true;
            this.buttonCreateScheme.Click += new System.EventHandler(this.buttonCreateScheme_Click);
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(129, 40);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(87, 26);
            this.labelCount.TabIndex = 8;
            this.labelCount.Text = "Count of squares\n(horisontal)";
            // 
            // comboBoxSquareCount
            // 
            this.comboBoxSquareCount.FormattingEnabled = true;
            this.comboBoxSquareCount.Location = new System.Drawing.Point(222, 45);
            this.comboBoxSquareCount.Name = "comboBoxSquareCount";
            this.comboBoxSquareCount.Size = new System.Drawing.Size(93, 21);
            this.comboBoxSquareCount.TabIndex = 9;
            // 
            // comboBoxResolution
            // 
            this.comboBoxResolution.FormattingEnabled = true;
            this.comboBoxResolution.Location = new System.Drawing.Point(794, 46);
            this.comboBoxResolution.Name = "comboBoxResolution";
            this.comboBoxResolution.Size = new System.Drawing.Size(130, 21);
            this.comboBoxResolution.TabIndex = 10;
            // 
            // Resolution
            // 
            this.Resolution.AutoSize = true;
            this.Resolution.Location = new System.Drawing.Point(731, 49);
            this.Resolution.Name = "Resolution";
            this.Resolution.Size = new System.Drawing.Size(57, 13);
            this.Resolution.TabIndex = 11;
            this.Resolution.Text = "Resolution";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 539);
            this.Controls.Add(this.Resolution);
            this.Controls.Add(this.comboBoxResolution);
            this.Controls.Add(this.comboBoxSquareCount);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.buttonCreateScheme);
            this.Controls.Add(this.pictureBoxResult);
            this.Controls.Add(this.panelColors);
            this.Controls.Add(this.buttonChooseColor);
            this.Controls.Add(this.pictureBoxCurrentImage);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCurrentImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pictureBoxCurrentImage;
        private System.Windows.Forms.Button buttonChooseColor;
        private System.Windows.Forms.Panel panelColors;
        private System.Windows.Forms.PictureBox pictureBoxResult;
        private System.Windows.Forms.Button buttonCreateScheme;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.ComboBox comboBoxSquareCount;
        private System.Windows.Forms.ComboBox comboBoxResolution;
        private System.Windows.Forms.Label Resolution;
    }
}

