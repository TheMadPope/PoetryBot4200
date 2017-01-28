using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PoetryBot4200.Rhymelib;

namespace PoetryBot4200
{
    public partial class MainMenu : Form
    {
        //TODO: Add and populate common word list.
        //TODO: Sort output by word occurrence
        //TODO: Allow random output of words
        //TODO: Allow spreadsheet export of words
        public TextComparitor TextComparitor { get; set; }
        public MainMenu()
        {
            InitializeComponent();
            comboOutputType.DataSource = AvailableOutputs;
            comboOutputType.SelectedIndex = 0;
            Reset();
        }

        private static readonly List<string> AvailableOutputs = new List<string>
        {
             "Words in Destination not in Source",
             "Words in Source not in Destination"
        };

        //TODO: Modify to allow compound words
        private static readonly List<string> ExceptionList = new List<string>
        {
            "'",
            "-",//Need to handle this differently, as i want to maintain compound words
            " "
        };

        private void ShowOutput(string s, bool ignoreCase)
        {
            switch (s)
            {
                case "Words in Destination not in Source":
                    txtOutput.DataSource = OutputDestinationNotInSource(ignoreCase);
                    break;
                case "Words in Source not in Destination":
                    txtOutput.DataSource = OutputSourceNotInDestination(ignoreCase);
                    break;
            }
        }

        private List<string> OutputSourceNotInDestination(bool ignoreCase)
        {
            return OutputANotInB(TextComparitor.SourceWords, TextComparitor.DestinationWords, ignoreCase);
        }

        private List<string> OutputDestinationNotInSource(bool ignoreCase)
        {
            return OutputANotInB(TextComparitor.DestinationWords, TextComparitor.SourceWords, ignoreCase);
        }
       
        private static List<string> OutputANotInB(IEnumerable<string> a, ICollection<string> b, bool ignoreCase)
        {
            var list = new List<string>();

            foreach (var word in a)
            {
                if (ignoreCase)
                {
                    var _in = b.FirstOrDefault(s => s.Contains(word, StringComparison.OrdinalIgnoreCase));
                    if (_in == null)
                    {
                        list.Add(word);
                    }
                }
                else
                {
                    if (!b.Contains(word))
                    {
                        list.Add(word);
                    }
                }

            }
            return list;
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
            //MessageBox.Show($@"{fileName} added to {listToWhichToAdd} Texts."); <--This is annoying.
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
                if (string.IsNullOrWhiteSpace(x))
                {
                    continue;
                }
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
            foreach (var c in s)
            {
                if (!char.IsLetter(c) && !ExceptionList.Contains(c.ToString()))
                {
                    while (s.Contains(c))
                    {
                        s = s.Replace(c.ToString(), " ");
                    }
                }
            }
            return s;
        }

        private void btnOutputGet_Click(object sender, EventArgs e)
        {
            ShowOutput(comboOutputType.SelectedItem.ToString(), chkIgnoreCase.Checked);
        }

        private void testbutton_Click(object sender, EventArgs e)
        {
            RhymingWords.GetRhymes(testbox.Text);
        }
    }
}
