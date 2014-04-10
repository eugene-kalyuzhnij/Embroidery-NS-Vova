namespace NSEmbroidery.UI
{
    partial class ResultImage
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
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxResultImage = new System.Windows.Forms.PictureBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCencel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResultImage)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBoxResultImage);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(607, 476);
            this.panel1.TabIndex = 0;
            // 
            // pictureBoxResultImage
            // 
            this.pictureBoxResultImage.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxResultImage.Name = "pictureBoxResultImage";
            this.pictureBoxResultImage.Size = new System.Drawing.Size(599, 471);
            this.pictureBoxResultImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxResultImage.TabIndex = 0;
            this.pictureBoxResultImage.TabStop = false;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(451, 497);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCencel
            // 
            this.buttonCencel.Location = new System.Drawing.Point(532, 497);
            this.buttonCencel.Name = "buttonCencel";
            this.buttonCencel.Size = new System.Drawing.Size(75, 23);
            this.buttonCencel.TabIndex = 2;
            this.buttonCencel.Text = "Cencel";
            this.buttonCencel.UseVisualStyleBackColor = true;
            this.buttonCencel.Click += new System.EventHandler(this.buttonCencel_Click);
            // 
            // ResultImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 526);
            this.Controls.Add(this.buttonCencel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.panel1);
            this.Name = "ResultImage";
            this.Text = "ResultImage";
            this.SizeChanged += new System.EventHandler(this.ResultImage_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResultImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBoxResultImage;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCencel;
    }
}