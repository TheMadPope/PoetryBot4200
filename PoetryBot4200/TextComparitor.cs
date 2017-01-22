using System.Collections.Generic;

namespace PoetryBot4200
{
    public class TextComparitor
    {
        public List<string> SourceTexts { get; set; }
        public HashSet<string> SourceWords { get; set; }
        public List<string> DestinationTexts { get; set; }
        public HashSet<string> DestinationWords { get; set; }
        public TextComparitor()
        {
            SourceTexts = new List<string>();
            SourceWords = new HashSet<string>();
            DestinationTexts = new List<string>();
            DestinationWords = new HashSet<string>();
        }
    }
}