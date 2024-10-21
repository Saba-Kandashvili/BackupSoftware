namespace BackupSoftware
{
    partial class MainPage
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SourceComboBox = new ComboBox();
            SuspendLayout();
            // 
            // SourceComboBox
            // 
            SourceComboBox.FormattingEnabled = true;
            SourceComboBox.Location = new Point(306, 103);
            SourceComboBox.Name = "SourceComboBox";
            SourceComboBox.Size = new Size(121, 23);
            SourceComboBox.TabIndex = 0;
            SourceComboBox.KeyDown += SourceComboBox_KeyDown;
            // 
            // MainPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(SourceComboBox);
            Name = "MainPage";
            Text = "Form1";
            FormClosing += MainPage_FormClosing;
            Load += MainPage_Load;
            ResumeLayout(false);
        }

        #endregion

        private ComboBox SourceComboBox;
    }
}
