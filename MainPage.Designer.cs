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
            DestinationTextBox = new TextBox();
            RemoveDirButton = new Button();
            BackupButton = new Button();
            ResetConfig = new Button();
            SuspendLayout();
            // 
            // SourceComboBox
            // 
            SourceComboBox.FormattingEnabled = true;
            SourceComboBox.Location = new Point(258, 171);
            SourceComboBox.Name = "SourceComboBox";
            SourceComboBox.Size = new Size(244, 23);
            SourceComboBox.TabIndex = 0;
            SourceComboBox.SelectedIndexChanged += SourceComboBox_SelectedIndexChanged;
            SourceComboBox.KeyDown += SourceComboBox_KeyDown;
            SourceComboBox.MouseDown += SourceComboBox_MouseDown;
            // 
            // DestinationTextBox
            // 
            DestinationTextBox.Location = new Point(258, 300);
            DestinationTextBox.Name = "DestinationTextBox";
            DestinationTextBox.Size = new Size(244, 23);
            DestinationTextBox.TabIndex = 1;
            DestinationTextBox.KeyDown += DestinationTextBox_KeyDown;
            DestinationTextBox.MouseDoubleClick += DestinationTextBox_DoubleClick;
            // 
            // RemoveDirButton
            // 
            RemoveDirButton.Location = new Point(218, 163);
            RemoveDirButton.Name = "RemoveDirButton";
            RemoveDirButton.Size = new Size(34, 36);
            RemoveDirButton.TabIndex = 2;
            RemoveDirButton.Text = "Del";
            RemoveDirButton.UseVisualStyleBackColor = true;
            RemoveDirButton.Click += RemoveDirButton_Click;
            // 
            // BackupButton
            // 
            BackupButton.Location = new Point(645, 209);
            BackupButton.Name = "BackupButton";
            BackupButton.Size = new Size(91, 91);
            BackupButton.TabIndex = 3;
            BackupButton.Text = "Backup";
            BackupButton.UseVisualStyleBackColor = true;
            BackupButton.Click += BackupButton_Click;
            // 
            // ResetConfig
            // 
            ResetConfig.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ResetConfig.ForeColor = Color.Red;
            ResetConfig.Location = new Point(12, 385);
            ResetConfig.Name = "ResetConfig";
            ResetConfig.Size = new Size(77, 53);
            ResetConfig.TabIndex = 4;
            ResetConfig.Text = "Reset Config";
            ResetConfig.UseVisualStyleBackColor = true;
            ResetConfig.Click += ResetConfig_Click;
            // 
            // MainPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ResetConfig);
            Controls.Add(BackupButton);
            Controls.Add(RemoveDirButton);
            Controls.Add(DestinationTextBox);
            Controls.Add(SourceComboBox);
            Name = "MainPage";
            Text = "Form1";
            FormClosing += MainPage_FormClosing;
            Load += MainPage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox SourceComboBox;
        private TextBox DestinationTextBox;
        private Button RemoveDirButton;
        private Button BackupButton;
        private Button ResetConfig;
    }
}
