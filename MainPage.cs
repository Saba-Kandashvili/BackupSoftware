namespace BackupSoftware
{
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public static string CONFIG = String.Join(@"\", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BackerUpper", "init.ini");
        public static List<string> SourcePaths = new List<string>();

        private void MainPage_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(Path.GetDirectoryName(CONFIG)))
            {
                if (File.Exists(CONFIG))
                {
                    SourcePaths.AddRange(File.ReadAllLines(CONFIG));
                    SourceComboBox.Items.AddRange(SourcePaths.ToArray());
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
        }

        private void MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            SourcePaths.Clear();

            foreach (string path in SourceComboBox.Items)
            {
                SourcePaths.Add(path);
            }

            string[] readLines = File.ReadAllLines(CONFIG);

            foreach (string line in readLines)
            {
                int ind = SourcePaths.IndexOf(line);
                if (ind != -1)
                {
                    SourcePaths.RemoveAt(ind);
                }
            }

            using (StreamWriter configSaver = new StreamWriter(CONFIG, true))
            {
                foreach (string path in SourcePaths)
                {
                    configSaver.WriteLine(path);
                }
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



                SourceComboBox.Items.Add(SourceComboBox.Text);
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
                    SourceComboBox.Text = "";

                    return;
                }
            }
        }
    }
}
