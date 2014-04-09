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
            this.panelSymbols = new System.Windows.Forms.Panel();
            this.buttonMinus = new System.Windows.Forms.Button();
            this.addTextBox = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.checkBoxGrid = new System.Windows.Forms.CheckBox();
            this.labelresolution = new System.Windows.Forms.Label();
            this.buttonSymbolColor = new System.Windows.Forms.Button();
            this.textBoxRatio = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCurrentImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).BeginInit();
            this.panelSymbols.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(12, 47);
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
            this.menuStrip1.Size = new System.Drawing.Size(1000, 24);
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
            this.pictureBoxCurrentImage.Location = new System.Drawing.Point(12, 85);
            this.pictureBoxCurrentImage.Name = "pictureBoxCurrentImage";
            this.pictureBoxCurrentImage.Size = new System.Drawing.Size(418, 389);
            this.pictureBoxCurrentImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCurrentImage.TabIndex = 2;
            this.pictureBoxCurrentImage.TabStop = false;
            // 
            // buttonChooseColor
            // 
            this.buttonChooseColor.Location = new System.Drawing.Point(334, 47);
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
            this.panelColors.Location = new System.Drawing.Point(450, 52);
            this.panelColors.Name = "panelColors";
            this.panelColors.Size = new System.Drawing.Size(50, 202);
            this.panelColors.TabIndex = 4;
            // 
            // pictureBoxResult
            // 
            this.pictureBoxResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxResult.Location = new System.Drawing.Point(0, 3);
            this.pictureBoxResult.Name = "pictureBoxResult";
            this.pictureBoxResult.Size = new System.Drawing.Size(414, 389);
            this.pictureBoxResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxResult.TabIndex = 5;
            this.pictureBoxResult.TabStop = false;
            // 
            // buttonCreateScheme
            // 
            this.buttonCreateScheme.Location = new System.Drawing.Point(524, 47);
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
            this.labelCount.Location = new System.Drawing.Point(129, 43);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(87, 26);
            this.labelCount.TabIndex = 8;
            this.labelCount.Text = "Count of squares\n(horisontal)";
            // 
            // comboBoxSquareCount
            // 
            this.comboBoxSquareCount.FormattingEnabled = true;
            this.comboBoxSquareCount.Location = new System.Drawing.Point(222, 48);
            this.comboBoxSquareCount.Name = "comboBoxSquareCount";
            this.comboBoxSquareCount.Size = new System.Drawing.Size(93, 21);
            this.comboBoxSquareCount.TabIndex = 9;
            this.comboBoxSquareCount.SelectedIndexChanged += new System.EventHandler(this.comboBoxSquareCount_SelectedIndexChanged);
            // 
            // comboBoxResolution
            // 
            this.comboBoxResolution.FormattingEnabled = true;
            this.comboBoxResolution.Location = new System.Drawing.Point(794, 49);
            this.comboBoxResolution.Name = "comboBoxResolution";
            this.comboBoxResolution.Size = new System.Drawing.Size(130, 21);
            this.comboBoxResolution.TabIndex = 10;
            this.comboBoxResolution.DropDown += new System.EventHandler(this.comboBoxResolution_DropDown);
            // 
            // Resolution
            // 
            this.Resolution.AutoSize = true;
            this.Resolution.Location = new System.Drawing.Point(731, 52);
            this.Resolution.Name = "Resolution";
            this.Resolution.Size = new System.Drawing.Size(57, 13);
            this.Resolution.TabIndex = 11;
            this.Resolution.Text = "Resolution";
            // 
            // panelSymbols
            // 
            this.panelSymbols.AutoScroll = true;
            this.panelSymbols.Controls.Add(this.buttonMinus);
            this.panelSymbols.Controls.Add(this.addTextBox);
            this.panelSymbols.Location = new System.Drawing.Point(436, 260);
            this.panelSymbols.Name = "panelSymbols";
            this.panelSymbols.Size = new System.Drawing.Size(82, 270);
            this.panelSymbols.TabIndex = 5;
            // 
            // buttonMinus
            // 
            this.buttonMinus.Location = new System.Drawing.Point(49, 3);
            this.buttonMinus.Name = "buttonMinus";
            this.buttonMinus.Size = new System.Drawing.Size(30, 23);
            this.buttonMinus.TabIndex = 2;
            this.buttonMinus.Text = "-";
            this.buttonMinus.UseVisualStyleBackColor = true;
            this.buttonMinus.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // addTextBox
            // 
            this.addTextBox.Location = new System.Drawing.Point(3, 3);
            this.addTextBox.Name = "addTextBox";
            this.addTextBox.Size = new System.Drawing.Size(28, 23);
            this.addTextBox.TabIndex = 1;
            this.addTextBox.Text = "+";
            this.addTextBox.UseVisualStyleBackColor = true;
            this.addTextBox.Click += new System.EventHandler(this.addTextBox_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBoxResult);
            this.panel1.Location = new System.Drawing.Point(524, 82);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(435, 411);
            this.panel1.TabIndex = 12;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(525, 507);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 13;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // checkBoxGrid
            // 
            this.checkBoxGrid.AutoSize = true;
            this.checkBoxGrid.Location = new System.Drawing.Point(222, 25);
            this.checkBoxGrid.Name = "checkBoxGrid";
            this.checkBoxGrid.Size = new System.Drawing.Size(45, 17);
            this.checkBoxGrid.TabIndex = 14;
            this.checkBoxGrid.Text = "Grid";
            this.checkBoxGrid.UseVisualStyleBackColor = true;
            // 
            // labelresolution
            // 
            this.labelresolution.AutoSize = true;
            this.labelresolution.Location = new System.Drawing.Point(791, 29);
            this.labelresolution.Name = "labelresolution";
            this.labelresolution.Size = new System.Drawing.Size(35, 13);
            this.labelresolution.TabIndex = 17;
            this.labelresolution.Text = "label1";
            // 
            // buttonSymbolColor
            // 
            this.buttonSymbolColor.Location = new System.Drawing.Point(334, 480);
            this.buttonSymbolColor.Name = "buttonSymbolColor";
            this.buttonSymbolColor.Size = new System.Drawing.Size(96, 23);
            this.buttonSymbolColor.TabIndex = 18;
            this.buttonSymbolColor.Text = "Symbol Color";
            this.buttonSymbolColor.UseVisualStyleBackColor = true;
            this.buttonSymbolColor.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // textBoxRatio
            // 
            this.textBoxRatio.Location = new System.Drawing.Point(650, 28);
            this.textBoxRatio.Name = "textBoxRatio";
            this.textBoxRatio.Size = new System.Drawing.Size(52, 20);
            this.textBoxRatio.TabIndex = 19;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 549);
            this.Controls.Add(this.textBoxRatio);
            this.Controls.Add(this.buttonSymbolColor);
            this.Controls.Add(this.labelresolution);
            this.Controls.Add(this.checkBoxGrid);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.panelSymbols);
            this.Controls.Add(this.Resolution);
            this.Controls.Add(this.comboBoxResolution);
            this.Controls.Add(this.comboBoxSquareCount);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.buttonCreateScheme);
            this.Controls.Add(this.panelColors);
            this.Controls.Add(this.buttonChooseColor);
            this.Controls.Add(this.pictureBoxCurrentImage);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCurrentImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).EndInit();
            this.panelSymbols.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.Panel panelSymbols;
        private System.Windows.Forms.Button addTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.CheckBox checkBoxGrid;
        private System.Windows.Forms.Button buttonMinus;
        private System.Windows.Forms.Label labelresolution;
        private System.Windows.Forms.Button buttonSymbolColor;
        private System.Windows.Forms.TextBox textBoxRatio;
    }
}

