using System;

namespace PoetryBot4200.Rhymelib
{
    /// <summary>
    /// Courtesy Sean Michael Murphy, https://www.codeproject.com/Articles/875964/Rhyming-Assist
    /// </summary>
    public class RhymedWord : IComparable
    {
        public RhymedWord(string raw)
        {
            //     " {\"word\":\"bore\",\"freq\":22,\"score\":300,\"flags\":\"bc\",\"syllables\":\"1\"},"
            //        ^^    ^^ ^^    ^^ ^^    ^^    ^^     ^^     ^^     ^^ ^^  ^^ ^^         ^^ ^^ ^^
            //       0    1   2    3   4    5    6      7     8       9    10 11  12     13     14 15

            // Parse the raw JSON to get the data.
            var tokens = raw.Split('\"');

            Word = tokens[3];                      // bore
            var temp = tokens[6];                // :22
            Frequency = int.Parse(temp.Substring(1, temp.Length - 2));

            temp = tokens[8];                       // :300
            Score = int.Parse(temp.Substring(1, temp.Length - 2));
            Flags = tokens[11];                    // bc
            Syllables = int.Parse(tokens[15]);   // 1
        }

        public int Syllables { get; }
        public string Flags { get; }
        public int Score { get; }
        public string Word { get; }

        public int Frequency { get; }

        /// <summary>
        /// Overridden method to allow sorting of List<RhymedWord/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            // Put the highest fidelity rhymes at the top of the list.
            var result = ((RhymedWord)obj).Score.CompareTo(Score);

            // If the rhymed word has the same Score as the RhymedWord being compared, sort them alphabetically.
            if (result == 0)
                return string.Compare(Word, ((RhymedWord)obj).Word, StringComparison.InvariantCultureIgnoreCase);

            return result;
        }

        public override string ToString()
        {
            return Word + " (" + Score + ")";
        }
    }
}
