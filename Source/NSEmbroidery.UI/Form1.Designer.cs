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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBoxCurrentImage = new System.Windows.Forms.PictureBox();
            this.buttonCreateScheme = new System.Windows.Forms.Button();
            this.labelCount = new System.Windows.Forms.Label();
            this.comboBoxResolution = new System.Windows.Forms.ComboBox();
            this.Resolution = new System.Windows.Forms.Label();
            this.labelresolution = new System.Windows.Forms.Label();
            this.panelColors = new System.Windows.Forms.Panel();
            this.checkBoxGrid = new System.Windows.Forms.CheckBox();
            this.pictureBoxColorChoice = new System.Windows.Forms.PictureBox();
            this.panelColorChoice = new System.Windows.Forms.Panel();
            this.textBoxCells = new System.Windows.Forms.TextBox();
            this.buttonAddSymbols = new System.Windows.Forms.Button();
            this.panelSymbols = new System.Windows.Forms.Panel();
            this.pictureBoxSymbolColor = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonLine = new System.Windows.Forms.RadioButton();
            this.radioButtonPoints = new System.Windows.Forms.RadioButton();
            this.resultLabel = new System.Windows.Forms.Label();
            this.labelWaitResolution = new System.Windows.Forms.Label();
            this.panelCreateColor = new System.Windows.Forms.Panel();
            this.buttonCencel = new System.Windows.Forms.Button();
            this.pictureBoxColorCreated = new System.Windows.Forms.PictureBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxB = new System.Windows.Forms.TextBox();
            this.textBoxG = new System.Windows.Forms.TextBox();
            this.textBoxR = new System.Windows.Forms.TextBox();
            this.buttonCreateColor = new System.Windows.Forms.Button();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCurrentImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxColorChoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSymbolColor)).BeginInit();
            this.panelCreateColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxColorCreated)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.MediumTurquoise;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(708, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openImageToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openImageToolStripMenuItem
            // 
            this.openImageToolStripMenuItem.Name = "openImageToolStripMenuItem";
            this.openImageToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openImageToolStripMenuItem.Text = "Open Image";
            this.openImageToolStripMenuItem.Click += new System.EventHandler(this.openImageToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // pictureBoxCurrentImage
            // 
            this.pictureBoxCurrentImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxCurrentImage.Location = new System.Drawing.Point(12, 47);
            this.pictureBoxCurrentImage.Name = "pictureBoxCurrentImage";
            this.pictureBoxCurrentImage.Size = new System.Drawing.Size(403, 328);
            this.pictureBoxCurrentImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCurrentImage.TabIndex = 2;
            this.pictureBoxCurrentImage.TabStop = false;
            // 
            // buttonCreateScheme
            // 
            this.buttonCreateScheme.Location = new System.Drawing.Point(475, 352);
            this.buttonCreateScheme.Name = "buttonCreateScheme";
            this.buttonCreateScheme.Size = new System.Drawing.Size(105, 23);
            this.buttonCreateScheme.TabIndex = 6;
            this.buttonCreateScheme.TabStop = false;
            this.buttonCreateScheme.Text = "Create Embroidery";
            this.buttonCreateScheme.UseVisualStyleBackColor = true;
            this.buttonCreateScheme.Click += new System.EventHandler(this.buttonCreateEmbroidery_Click);
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(471, 153);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(74, 13);
            this.labelCount.TabIndex = 8;
            this.labelCount.Text = "Count of cells:";
            // 
            // comboBoxResolution
            // 
            this.comboBoxResolution.FormattingEnabled = true;
            this.comboBoxResolution.Location = new System.Drawing.Point(548, 185);
            this.comboBoxResolution.Name = "comboBoxResolution";
            this.comboBoxResolution.Size = new System.Drawing.Size(130, 21);
            this.comboBoxResolution.TabIndex = 10;
            this.comboBoxResolution.DropDown += new System.EventHandler(this.comboBoxResolution_DropDown);
            // 
            // Resolution
            // 
            this.Resolution.AutoSize = true;
            this.Resolution.Location = new System.Drawing.Point(472, 188);
            this.Resolution.Name = "Resolution";
            this.Resolution.Size = new System.Drawing.Size(60, 13);
            this.Resolution.TabIndex = 11;
            this.Resolution.Text = "Resolution:";
            // 
            // labelresolution
            // 
            this.labelresolution.AutoSize = true;
            this.labelresolution.Location = new System.Drawing.Point(21, 485);
            this.labelresolution.Name = "labelresolution";
            this.labelresolution.Size = new System.Drawing.Size(0, 13);
            this.labelresolution.TabIndex = 17;
            // 
            // panelColors
            // 
            this.panelColors.AutoScroll = true;
            this.panelColors.Location = new System.Drawing.Point(468, 91);
            this.panelColors.Name = "panelColors";
            this.panelColors.Size = new System.Drawing.Size(209, 45);
            this.panelColors.TabIndex = 4;
            // 
            // checkBoxGrid
            // 
            this.checkBoxGrid.AutoSize = true;
            this.checkBoxGrid.Location = new System.Drawing.Point(475, 223);
            this.checkBoxGrid.Name = "checkBoxGrid";
            this.checkBoxGrid.Size = new System.Drawing.Size(45, 17);
            this.checkBoxGrid.TabIndex = 14;
            this.checkBoxGrid.Text = "Grid";
            this.checkBoxGrid.UseVisualStyleBackColor = true;
            this.checkBoxGrid.CheckedChanged += new System.EventHandler(this.checkBoxGrid_CheckedChanged);
            // 
            // pictureBoxColorChoice
            // 
            this.pictureBoxColorChoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.pictureBoxColorChoice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxColorChoice.Location = new System.Drawing.Point(468, 59);
            this.pictureBoxColorChoice.Name = "pictureBoxColorChoice";
            this.pictureBoxColorChoice.Size = new System.Drawing.Size(27, 26);
            this.pictureBoxColorChoice.TabIndex = 19;
            this.pictureBoxColorChoice.TabStop = false;
            this.pictureBoxColorChoice.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // panelColorChoice
            // 
            this.panelColorChoice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelColorChoice.Location = new System.Drawing.Point(501, 47);
            this.panelColorChoice.Name = "panelColorChoice";
            this.panelColorChoice.Size = new System.Drawing.Size(158, 58);
            this.panelColorChoice.TabIndex = 20;
            this.panelColorChoice.Visible = false;
            // 
            // textBoxCells
            // 
            this.textBoxCells.Location = new System.Drawing.Point(548, 150);
            this.textBoxCells.Name = "textBoxCells";
            this.textBoxCells.Size = new System.Drawing.Size(58, 20);
            this.textBoxCells.TabIndex = 21;
            this.textBoxCells.TextChanged += new System.EventHandler(this.textBoxCells_TextChanged);
            this.textBoxCells.GotFocus += new System.EventHandler(this.textBoxCells_GotFocus);
            this.textBoxCells.LostFocus += new System.EventHandler(this.textBoxCells_LostFocus);
            // 
            // buttonAddSymbols
            // 
            this.buttonAddSymbols.Location = new System.Drawing.Point(474, 257);
            this.buttonAddSymbols.Name = "buttonAddSymbols";
            this.buttonAddSymbols.Size = new System.Drawing.Size(75, 23);
            this.buttonAddSymbols.TabIndex = 22;
            this.buttonAddSymbols.Text = "Add symbols";
            this.buttonAddSymbols.UseVisualStyleBackColor = true;
            this.buttonAddSymbols.Click += new System.EventHandler(this.buttonAddSymbols_Click);
            // 
            // panelSymbols
            // 
            this.panelSymbols.AutoScroll = true;
            this.panelSymbols.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panelSymbols.Location = new System.Drawing.Point(468, 286);
            this.panelSymbols.Name = "panelSymbols";
            this.panelSymbols.Size = new System.Drawing.Size(209, 51);
            this.panelSymbols.TabIndex = 23;
            // 
            // pictureBoxSymbolColor
            // 
            this.pictureBoxSymbolColor.BackColor = System.Drawing.Color.Black;
            this.pictureBoxSymbolColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxSymbolColor.Location = new System.Drawing.Point(655, 257);
            this.pictureBoxSymbolColor.Name = "pictureBoxSymbolColor";
            this.pictureBoxSymbolColor.Size = new System.Drawing.Size(22, 23);
            this.pictureBoxSymbolColor.TabIndex = 24;
            this.pictureBoxSymbolColor.TabStop = false;
            this.pictureBoxSymbolColor.Click += new System.EventHandler(this.pictureBoxSymbolColor_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(575, 262);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Symbol\'s color:";
            // 
            // radioButtonLine
            // 
            this.radioButtonLine.AutoSize = true;
            this.radioButtonLine.Location = new System.Drawing.Point(561, 222);
            this.radioButtonLine.Name = "radioButtonLine";
            this.radioButtonLine.Size = new System.Drawing.Size(45, 17);
            this.radioButtonLine.TabIndex = 26;
            this.radioButtonLine.TabStop = true;
            this.radioButtonLine.Text = "Line";
            this.radioButtonLine.UseVisualStyleBackColor = true;
            this.radioButtonLine.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButtonPoints
            // 
            this.radioButtonPoints.AutoSize = true;
            this.radioButtonPoints.Location = new System.Drawing.Point(623, 222);
            this.radioButtonPoints.Name = "radioButtonPoints";
            this.radioButtonPoints.Size = new System.Drawing.Size(54, 17);
            this.radioButtonPoints.TabIndex = 27;
            this.radioButtonPoints.TabStop = true;
            this.radioButtonPoints.Text = "Points";
            this.radioButtonPoints.UseVisualStyleBackColor = true;
            this.radioButtonPoints.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Location = new System.Drawing.Point(596, 357);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(0, 13);
            this.resultLabel.TabIndex = 28;
            // 
            // labelWaitResolution
            // 
            this.labelWaitResolution.AutoSize = true;
            this.labelWaitResolution.Location = new System.Drawing.Point(639, 169);
            this.labelWaitResolution.Name = "labelWaitResolution";
            this.labelWaitResolution.Size = new System.Drawing.Size(0, 13);
            this.labelWaitResolution.TabIndex = 29;
            // 
            // panelCreateColor
            // 
            this.panelCreateColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCreateColor.Controls.Add(this.buttonCencel);
            this.panelCreateColor.Controls.Add(this.pictureBoxColorCreated);
            this.panelCreateColor.Controls.Add(this.buttonAdd);
            this.panelCreateColor.Controls.Add(this.label4);
            this.panelCreateColor.Controls.Add(this.label3);
            this.panelCreateColor.Controls.Add(this.label2);
            this.panelCreateColor.Controls.Add(this.textBoxB);
            this.panelCreateColor.Controls.Add(this.textBoxG);
            this.panelCreateColor.Controls.Add(this.textBoxR);
            this.panelCreateColor.Location = new System.Drawing.Point(468, 47);
            this.panelCreateColor.Name = "panelCreateColor";
            this.panelCreateColor.Size = new System.Drawing.Size(205, 82);
            this.panelCreateColor.TabIndex = 21;
            this.panelCreateColor.Visible = false;
            // 
            // buttonCencel
            // 
            this.buttonCencel.Location = new System.Drawing.Point(117, 52);
            this.buttonCencel.Name = "buttonCencel";
            this.buttonCencel.Size = new System.Drawing.Size(75, 23);
            this.buttonCencel.TabIndex = 8;
            this.buttonCencel.Text = "Cencel";
            this.buttonCencel.UseVisualStyleBackColor = true;
            this.buttonCencel.Click += new System.EventHandler(this.buttonCencel_Click);
            // 
            // pictureBoxColorCreated
            // 
            this.pictureBoxColorCreated.BackColor = System.Drawing.Color.Black;
            this.pictureBoxColorCreated.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxColorCreated.Location = new System.Drawing.Point(162, 3);
            this.pictureBoxColorCreated.Name = "pictureBoxColorCreated";
            this.pictureBoxColorCreated.Size = new System.Drawing.Size(38, 38);
            this.pictureBoxColorCreated.TabIndex = 7;
            this.pictureBoxColorCreated.TabStop = false;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(36, 52);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 6;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(100, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Blue";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Green";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Red";
            // 
            // textBoxB
            // 
            this.textBoxB.Location = new System.Drawing.Point(103, 21);
            this.textBoxB.Name = "textBoxB";
            this.textBoxB.Size = new System.Drawing.Size(44, 20);
            this.textBoxB.TabIndex = 2;
            this.textBoxB.Click += new System.EventHandler(this.textBoxR_Click);
            this.textBoxB.TextChanged += new System.EventHandler(this.textBoxR_TextChanged);
            // 
            // textBoxG
            // 
            this.textBoxG.Location = new System.Drawing.Point(53, 21);
            this.textBoxG.Name = "textBoxG";
            this.textBoxG.Size = new System.Drawing.Size(44, 20);
            this.textBoxG.TabIndex = 1;
            this.textBoxG.Click += new System.EventHandler(this.textBoxR_Click);
            this.textBoxG.TextChanged += new System.EventHandler(this.textBoxR_TextChanged);
            // 
            // textBoxR
            // 
            this.textBoxR.Location = new System.Drawing.Point(3, 21);
            this.textBoxR.Name = "textBoxR";
            this.textBoxR.Size = new System.Drawing.Size(44, 20);
            this.textBoxR.TabIndex = 0;
            this.textBoxR.Click += new System.EventHandler(this.textBoxR_Click);
            this.textBoxR.TextChanged += new System.EventHandler(this.textBoxR_TextChanged);
            // 
            // buttonCreateColor
            // 
            this.buttonCreateColor.Location = new System.Drawing.Point(521, 62);
            this.buttonCreateColor.Name = "buttonCreateColor";
            this.buttonCreateColor.Size = new System.Drawing.Size(75, 23);
            this.buttonCreateColor.TabIndex = 30;
            this.buttonCreateColor.Text = "Create color";
            this.buttonCreateColor.UseVisualStyleBackColor = true;
            this.buttonCreateColor.Click += new System.EventHandler(this.buttonCreateColor_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(708, 406);
            this.Controls.Add(this.panelCreateColor);
            this.Controls.Add(this.panelColorChoice);
            this.Controls.Add(this.buttonCreateColor);
            this.Controls.Add(this.labelWaitResolution);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.radioButtonPoints);
            this.Controls.Add(this.radioButtonLine);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBoxSymbolColor);
            this.Controls.Add(this.panelSymbols);
            this.Controls.Add(this.buttonAddSymbols);
            this.Controls.Add(this.textBoxCells);
            this.Controls.Add(this.pictureBoxColorChoice);
            this.Controls.Add(this.labelresolution);
            this.Controls.Add(this.checkBoxGrid);
            this.Controls.Add(this.Resolution);
            this.Controls.Add(this.comboBoxResolution);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.buttonCreateScheme);
            this.Controls.Add(this.panelColors);
            this.Controls.Add(this.pictureBoxCurrentImage);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.Name = "Form1";
            this.Text = "Embroidery Creator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCurrentImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxColorChoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSymbolColor)).EndInit();
            this.panelCreateColor.ResumeLayout(false);
            this.panelCreateColor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxColorCreated)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pictureBoxCurrentImage;
        private System.Windows.Forms.Button buttonCreateScheme;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.ComboBox comboBoxResolution;
        private System.Windows.Forms.Label Resolution;
        private System.Windows.Forms.Label labelresolution;
        private System.Windows.Forms.Panel panelColors;
        private System.Windows.Forms.CheckBox checkBoxGrid;
        private System.Windows.Forms.PictureBox pictureBoxColorChoice;
        private System.Windows.Forms.Panel panelColorChoice;
        private System.Windows.Forms.TextBox textBoxCells;
        private System.Windows.Forms.ToolStripMenuItem openImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.Button buttonAddSymbols;
        private System.Windows.Forms.Panel panelSymbols;
        private System.Windows.Forms.PictureBox pictureBoxSymbolColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButtonLine;
        private System.Windows.Forms.RadioButton radioButtonPoints;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.Label labelWaitResolution;
        private System.Windows.Forms.Panel panelCreateColor;
        private System.Windows.Forms.Button buttonCencel;
        private System.Windows.Forms.PictureBox pictureBoxColorCreated;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxB;
        private System.Windows.Forms.TextBox textBoxG;
        private System.Windows.Forms.TextBox textBoxR;
        private System.Windows.Forms.Button buttonCreateColor;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

