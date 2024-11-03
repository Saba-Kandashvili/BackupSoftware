using System.ComponentModel;

namespace BackupSoftware
{
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public static readonly string CONFIG = String.Join(@"\", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BackerUpper", "init.ini");
        public static List<string> SourcePaths = new List<string>();
        public static string DESTPATH;

        public static readonly string START_SOURCE = "#START_SOURCE";
        public static readonly string END_SOURCE = "#END_SOURCE";

        public static readonly string START_DEST = "#START_DEST";
        public static readonly string END_DEST = "#END_DEST";



        private void MainPage_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(Path.GetDirectoryName(CONFIG)))
            {
                if (File.Exists(CONFIG))
                {
                    List<string> readLines = File.ReadLines(CONFIG).ToList();
                    List<string> readSourcePaths = new List<string>();
                    List<string> readDestPaths = new List<string>();


                    if (readLines.Contains(START_SOURCE))
                    {
                        int regStartIndex = readLines.IndexOf(START_SOURCE) + 1;
                        int regEndIndex = readLines.IndexOf(END_SOURCE);

                        if (regStartIndex == -1 || regEndIndex == -1)
                        { // TODO: maybe add am option to delete it from this dialog
                            MessageBox.Show($"config file is corrupted. delete it\nfile at: {CONFIG}\nend region not found for source", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit(); // close the program to avoid doing anything stupid
                            return; // idk if this is necessary
                        }


                        readSourcePaths.AddRange(readLines.GetRange(regStartIndex, regEndIndex - regStartIndex));
                    }
                    else
                    {
                        MessageBox.Show($"config file is corrupted. delete it\nfile at: {CONFIG}\nnot valid region definitions", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit(); // close the program to avoid doing anything stupid
                        return; // idk if this is necessary
                    }


                    if (readLines.Contains(START_DEST))
                    {
                        int regStartIndex = readLines.IndexOf(START_DEST) + 1;
                        int regEndIndex = readLines.IndexOf(END_DEST);

                        if (regStartIndex == -1 || regEndIndex == -1)
                        { // TODO: maybe add am option to delete it from this dialog
                            MessageBox.Show($"config file is corrupted. delete it\nfile at: {CONFIG}\nend region not found for destination", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit(); // close the program to avoid doing anything stupid
                            return; // idk if this is necessary
                        }


                        readDestPaths.AddRange(readLines.GetRange(regStartIndex, regEndIndex - regStartIndex));
                        DESTPATH = readDestPaths[0];
                    }
                    else
                    {
                        MessageBox.Show($"config file is corrupted. delete it\nfile at: {CONFIG}\nnot valid region definitions", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit(); // close the program to avoid doing anything stupid
                        return; // idk if this is necessary
                    }


                    SourcePaths.AddRange(readSourcePaths);
                    SourceComboBox.Items.AddRange(SourcePaths.ToArray());
                    DestinationTextBox.Text = DESTPATH;
                }
                else
                {
                    using (File.Create(CONFIG)) { }
                }
            }
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(CONFIG));
                using (File.Create(CONFIG)) { }
            }


            RemoveDirButton.Visible = false;
            RemoveDirButton.Enabled = false;
        }

        private void MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            SourcePaths.Clear();

            foreach (string path in SourceComboBox.Items)
            {
                SourcePaths.Add(path);
            }

            List<string> readLines = File.ReadAllLines(CONFIG).ToList();
            List<string> readSourcePaths = new List<string>();

            if (readLines.Contains(START_SOURCE))
            {
                int regStartIndex = readLines.IndexOf(START_SOURCE) + 1;
                int regEndIndex = readLines.IndexOf(END_SOURCE);

                if (regStartIndex == -1 || regEndIndex == -1)
                { // TODO: maybe add am option to delete it from this dialog
                    MessageBox.Show($"config file is corrupted. delete it\nfile at: {CONFIG}\nend region not found for source", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit(); // close the program to avoid doing anything stupid
                    return; // idk if this is necessary
                }


                readSourcePaths.AddRange(readLines.GetRange(regStartIndex, regEndIndex - regStartIndex));
            }


            foreach (string line in SourcePaths)
            {
                int ind = readSourcePaths.IndexOf(line);
                if (ind != -1)
                {
                    readSourcePaths.RemoveAt(ind);
                }
            }

            using (StreamWriter configSaver = new StreamWriter(CONFIG))
            {

                configSaver.WriteLine(START_SOURCE);
                foreach (string path in SourcePaths)
                {
                    configSaver.WriteLine(path);
                }
                configSaver.WriteLine(END_SOURCE);

                configSaver.WriteLine(START_DEST);
                configSaver.WriteLine(DESTPATH);
                configSaver.WriteLine(END_DEST);
            }
        }

        private void SourceComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (SourceComboBox.Text == "")
                {
                    MessageBox.Show("You must enter a valid path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SourceComboBox.Text = "";

                    return;
                }

                if (!Directory.Exists(SourceComboBox.Text))
                {
                    MessageBox.Show("Directory doesn't exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SourceComboBox.Text = "";

                    return;
                }

                if (SourcePaths.Contains(SourceComboBox.Text))
                {
                    MessageBox.Show("Directory already backed up.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SourceComboBox.Text = "";

                    return;
                }

                if (SourceComboBox.Text == DESTPATH)
                {
                    MessageBox.Show("can not back up folder into itself", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SourceComboBox.Text = "";

                    return;
                }




                SourceComboBox.Items.Add(SourceComboBox.Text);
                SourcePaths.Add(SourceComboBox.Text);
                SourceComboBox.Text = "";
            }
        }

        private void DestinationTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (DestinationTextBox.Text == "")
                {
                    MessageBox.Show("You must enter a valid path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DestinationTextBox.Text = "";

                    return;
                }


                if (!Directory.Exists(DestinationTextBox.Text))
                {
                    MessageBox.Show("Directory doesn't exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DestinationTextBox.Text = "";

                    return;
                }


                if (SourcePaths.Contains(DestinationTextBox.Text))
                {
                    MessageBox.Show("You cant backup into an already backed up directory", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DestinationTextBox.Text = "";

                    return;
                }
                // TODO: add more validation to check if subfolder of any folder is being backed up into it's parent
                DESTPATH = DestinationTextBox.Text;
                MessageBox.Show(DESTPATH);
            }
        }

        private void SourceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SourcePaths.Contains(SourceComboBox.Text))
            {
                RemoveDirButton.Enabled = true;
                RemoveDirButton.Visible = true;
            }
        }

        private void SourceComboBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                {
                    folderBrowserDialog.Description = "Select a Directory to Backup";
                    folderBrowserDialog.ShowNewFolderButton = true;

                    DialogResult result = folderBrowserDialog.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                    {
                        string selectedPath = folderBrowserDialog.SelectedPath;

                        if (SourcePaths.Contains(selectedPath))
                        {
                            MessageBox.Show("Directory already backed up.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        SourcePaths.Add(selectedPath);
                        SourceComboBox.Items.Add(selectedPath);  // Update ComboBox items
                        SourceComboBox.Text = "";  // Clear the text in ComboBox
                    }
                }
            }
        }


        private void RemoveDirButton_Click(object sender, EventArgs e)
        {
            SourcePaths.Remove(SourceComboBox.Text);
            SourceComboBox.Items.Remove(SourceComboBox.Text);

            RemoveDirButton.Visible = false;
            RemoveDirButton.Enabled = false;

        }

        private void BackupButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DESTPATH))
            {
                MessageBox.Show("Please specify a destination path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            BackupAllSourcePaths();
        }

        private void CopyDirectory(string sourceDir, string destDir)
        {
            DirectoryInfo source = new DirectoryInfo(sourceDir);
            DirectoryInfo dest = new DirectoryInfo(destDir);

            if (!source.Exists)
            {
                MessageBox.Show("Source directory doesn't exist. Exiting immediately to avoid a catastrophe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            // Ensure destination directory exists
            if (!dest.Exists)
            {
                dest.Create();  // Create the destination directory if it doesn't exist
            }

            // Copy all files
            foreach (FileInfo file in source.GetFiles())
            {
                string tempPath = Path.Combine(destDir, file.Name);
                using (FileStream sourceStream = file.OpenRead())
                using (FileStream destStream = File.Create(tempPath))
                {
                    sourceStream.CopyTo(destStream);
                }
            }

            // Copy all subdirectories
            foreach (DirectoryInfo subdir in source.GetDirectories())
            {
                string tempPath = Path.Combine(destDir, subdir.Name);
                CopyDirectory(subdir.FullName, tempPath);
            }
        }


        private void BackupAllSourcePaths()
        {
            try
            {
                foreach (string sourcePath in SourcePaths)
                {
                    string destinationPath = Path.Combine(DESTPATH, new DirectoryInfo(sourcePath).Name);
                    CopyDirectory(sourcePath, destinationPath);
                }

                MessageBox.Show("Backup completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during backup: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DestinationTextBox_DoubleClick(object sender, MouseEventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select Destination Directory";

                // Show the dialog and get the result
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    // Set the selected folder path to the DestinationTextBox
                    DestinationTextBox.Text = folderBrowserDialog.SelectedPath;

                }
            }
        }

        private void ResetConfig_Click(object sender, EventArgs e)
        {
            // Show a confirmation dialog
            DialogResult result = MessageBox.Show("Are you sure you want to reset the configuration?",
                                                  "Confirm Reset",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            // Check if the user clicked 'Yes'
            if (result == DialogResult.Yes)
            {
                // Perform the reset operation here
                ResetConfiguration();
                MessageBox.Show("Configuration has been reset.", "Reset Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // If the user clicked 'No', do nothing or handle as needed
                MessageBox.Show("Configuration reset canceled.", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ResetConfiguration()
        {
            // Clear the contents of the existing config file
            File.WriteAllText(CONFIG, string.Empty);

            // Reset UI elements and paths
            SourceComboBox.Items.Clear();
            SourcePaths.Clear();
            DestinationTextBox.Text = string.Empty;
            DESTPATH = string.Empty;
        }
    }
}
