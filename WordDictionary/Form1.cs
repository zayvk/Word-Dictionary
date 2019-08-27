using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WordDictionary
{
    public partial class Form1 : Form
    {
        #region Constants
        public static string rootPath = Application.StartupPath;
        public static string destination = rootPath + @"\TempLocation\WordDictionary.txt";
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Select the file for word dictionary",
                CheckFileExists = true,
                CheckPathExists = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                browseTextbox.Text = openFileDialog.SafeFileName;
                label2.Text = openFileDialog.FileName;
            }
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            string fileToUpload;
            FileInfo fileInfo = new FileInfo(label2.Text);
            long fileSize = fileInfo.Length / (1024 * 1024);
            if (fileSize < 200)
            {
                using (StreamReader reader = new StreamReader(label2.Text))
                {
                    fileToUpload = reader.ReadToEnd();
                }

                if (!string.IsNullOrEmpty(fileToUpload))
                {
                    using (StreamWriter writer = new StreamWriter(destination, true))
                    {
                        writer.Write(" " + fileToUpload);
                        label2.Text = "Upload Successful";
                    }
                }
            }
            else
                label2.Text = "File Size Exceeds. Must be less than 200MB";
            browseTextbox.Text = "";

        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            FileStream fileStream = new FileStream(destination, FileMode.Open, FileAccess.Read);

            string fileContent;
            using (StreamReader reader = new StreamReader(fileStream))
            {
                fileContent = reader.ReadToEnd();
            }

            string[] contentArr = fileContent.Split(' ');
            string searchKeyword = searchTextbox.Text;

            var searchWord = Array.FindAll(contentArr, str => str.ToLower().Equals(searchKeyword.ToLower()));
            if (searchWord.Length > 0)
            {
                label3.Text = "Search successful. The word is " + searchWord.Length + " times in the dictionary";
            }
            else
                label3.Text = "Word is not present in the dictionary";

            searchTextbox.Text = "";
        }
    }
}
