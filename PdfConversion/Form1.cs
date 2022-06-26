using System.ComponentModel;
namespace PdfConversion
{
    public partial class Form1 : Form
    {
        public static Dictionary<string, string> _dictionary = new Dictionary<string, string>();
        public Form1()
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();

        }
     

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            
            openFileDialog.Title = "File Picker";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog.FileName;
                
            }
                
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ParamArgs param = new ParamArgs(textBox2.Text, textBox1.Text, listBox1.Text);
            if(String.IsNullOrEmpty(param.SourceName))
            {
                MessageBox.Show("Please, Set the path to the file name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (String.IsNullOrEmpty(param.DestinationName))
            {
                MessageBox.Show("Please, Set the Destination path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(listBox1.Text == "")
            {
                MessageBox.Show("Please, Select the type of document you want to convert your pdf file to.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                BackgroundWorker worker = new BackgroundWorker();
                ConvertPdfComputation convertPdf = new ConvertPdfComputation(worker);
                ClientSide clientSide = new ClientSide(convertPdf);
                clientSide.RunAsync("Converting pdf to doc. Please wait....", param);
                textBox1.Text = "";
                textBox2.Text = "";
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            _dictionary.Add("Docx", "doc");
            _dictionary.Add("Images", "jpeg");
            _dictionary.Add("Xml", "xml");
            _dictionary.Add("Text", "txt");
            foreach (string key in _dictionary.Keys)
            {
                listBox1.Items.Add(key);
            }
        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            MessageBox.Show("Hello");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var results = MessageBox.Show(
                "Do you really want to close this app?",
                "Close App",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );
            if (results == DialogResult.Yes)
            {
                Thread.Sleep(1000);
                
            }
            else
                e.Cancel = true;

        }
    }
}