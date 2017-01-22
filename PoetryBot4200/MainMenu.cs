using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoetryBot4200
{
    public partial class MainMenu : Form
    {
        //TODO: Add striplist
        //TODO: Use striplist
        //TODO: Return list of word from DEstination that don't appear in Source
        //TODO: Striplist should contain "" null or whitespace kind of things


        public TextComparitor TextComparitor { get; set; }
        public MainMenu()
        {
            InitializeComponent();
            Reset();
        }

        private void Reset()
        {
            TextComparitor = new TextComparitor();
        }

        private void btnGetFile_Source_Click(object sender, EventArgs e)
        {
            AddFileToComparitor("source");
        }

        private void btnGetFile_Destination_Click(object sender, EventArgs e)
        {
            AddFileToComparitor("destination");
        }

        private void AddFileToComparitor(string listToWhichToAdd)
        {
            string fileName;
            var s = GetTextFile(out fileName);
            if (string.IsNullOrWhiteSpace(s)) return;
            switch (listToWhichToAdd.ToLowerInvariant())
            {
                case "source":
                    TextComparitor.SourceTexts.Add(s);
                    break;
                case "destination":
                    TextComparitor.DestinationTexts.Add(s);
                    break;
                default:
                    return;
            }
            AddToSourceWords(s, listToWhichToAdd);
            RefreshCounts(listToWhichToAdd);
            MessageBox.Show($@"{fileName} added to {listToWhichToAdd} Texts.");
        }

        private void RefreshCounts(string listToWhichToAdd)
        {
            switch (listToWhichToAdd)
            {
                case "source":
                    txtSourceStats.Text =
                        $@"{TextComparitor.SourceTexts.Count} files, with {TextComparitor.SourceWords.Count} unique words.";
                    return;
                case "destination":
                    txtDestinationStats.Text =
                        $@"{TextComparitor.DestinationTexts.Count} files, with {TextComparitor.DestinationWords.Count} unique words.";
                    return;
            }
        }

        private void AddToSourceWords(string s, string listToWhichToAdd)
        {
            var list = s.Split();
            foreach (var x in list)
            {
                switch (listToWhichToAdd)
                {
                    case "source":
                        TextComparitor.SourceWords.Add(x);
                        break;
                    case "destination":
                        TextComparitor.DestinationWords.Add(x);
                        break;
                }
            }
        }

        private static string GetTextFile(out string fileName)
        {
            fileName = string.Empty;
            var dialog = new OpenFileDialog
            {
                InitialDirectory = "C:\\",
                Filter = @"txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (dialog.ShowDialog() != DialogResult.OK) return string.Empty;
            try
            {
                fileName = dialog.FileName;
                using (var sr = new StreamReader(fileName))
                {
                    // Read the stream to a string, and write the string to the console.
                    var t = sr.ReadToEnd();
                    var s = CleanText(t);
                    return s;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Error: Could not read file from disk - Maybe it's still open? Original error: {ex.Message}");
                return string.Empty;
            }
        }

        private static string CleanText(string s)
        {
            while (s.Contains("  "))
            {
                s = s.Replace("  ", " ");
            }
            while (s.Contains("\r\n"))
            {
                s = s.Replace("\r\n", " ");
            }

            while (s.Contains("\r"))
            {
                s = s.Replace("\r", " ");
            }

            while (s.Contains("\n"))
            {
                s = s.Replace("\n", " ");
            }

            while (s.Contains(")"))
            {
                s = s.Replace("", " ");
            }
            while (s.Contains("("))
            {
                s = s.Replace("", " ");
            }
            return s;
        }

    }
}
