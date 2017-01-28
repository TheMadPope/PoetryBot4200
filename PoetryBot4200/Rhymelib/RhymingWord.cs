using System;
using System.Collections.Generic;
using System.Linq;

namespace PoetryBot4200.Rhymelib
{
    //TODO: Build a no-DB solution to store the rhyme matches for retrieval later, including the rhymescore provided by the api.
    //TODO: This can be used later.
    /// <summary>
    /// A class with a static method to interface with the rhyming web service 
    /// and manage caching of previously looked up words.
    /// Courtesy Sean Michael Murphy, https://www.codeproject.com/Articles/875964/Rhyming-Assist
    /// </summary>
    public static class RhymingWords
    {
        // http://rhymebrain.com/api.html
        private const string WordDirectory = @".\Words";
        private const string LookupUrl = "http://rhymebrain.com/talk?function=getRhymes&word=";

        static RhymingWords()
        {
            if (!System.IO.Directory.Exists(WordDirectory))
                System.IO.Directory.CreateDirectory(WordDirectory);
        }

        /// <summary>
        /// Method to call to retrieve a list of words that rhyme with a particular word.
        /// </summary>
        /// <param name="word">The word to rhyme.</param>
        /// <returns></returns>
        public static List<string> GetRhymes(string word)
        {
            var cachedWordFileName = WordDirectory + @"\" + word + ".txt";

            var results = new List<string>();

            // Check to see if we have a local file (i.e. we called the web service before).
            if (System.IO.File.Exists(cachedWordFileName))
            {
                // We did.  Read the cached words.
                var raw = string.Empty;
                using (var sr = new System.IO.StreamReader(cachedWordFileName))
                {
                    raw = sr.ReadToEnd();
                    sr.Close();
                }
                results.AddRange(raw.Split('\n').Where(wordLine => wordLine.Trim().Length > 0));
                //foreach (var wordLine in raw.Split('\n'))
                //    if (wordLine.Trim().Length > 0)
                //        results.Add(wordLine);
            }
            else
            {
                // No local file exists.  Go get the new word rhymes.
                var webRequest = System.Net.WebRequest.Create(LookupUrl + word);

                var proxy = webRequest.Proxy;
                var proxyUri = proxy.GetProxy(webRequest.RequestUri).ToString();

                webRequest.UseDefaultCredentials = true;
                webRequest.Proxy = new System.Net.WebProxy(proxyUri, false)
                {
                    Credentials = System.Net.CredentialCache.DefaultCredentials
                };

                try
                {
                    var webResponse = webRequest.GetResponse();
                    string response;

                    using (var sr = new System.IO.StreamReader(webResponse.GetResponseStream()))
                    {
                        response = sr.ReadToEnd();
                        sr.Close();
                    }

                    var words = new List<RhymedWord>();

                    // Iterate over the results.  Each line contains a rhyming word to be parsed.
                    foreach (var line in response.Split('\n'))
                    {
                        if (line.Trim().Length > 0)
                        {
                            var rhymedWord = new RhymedWord(line);
                            // The word to be rhymed always comes back too.  Skip it, and all of the
                            // other words that were rhymed with less than perfect (Score < 300) confidence.
                            //TODO: Add a variable for the rhymedWord.Score with 300 as 100% and the rest in relation to that.
                            //TODO: Fetch and save all matches as json and parse them later.
                            if (!string.Equals(rhymedWord.Word, word, StringComparison.InvariantCultureIgnoreCase)/* && rhymedWord.Score >= 200*/)
                                words.Add(rhymedWord);
                        }
                    }

                    words.Sort();

                    // Cache the results to prevent future requests for rhymes of the same word.
                    using (var sw = new System.IO.StreamWriter(cachedWordFileName))
                    {
                        foreach (var rw in words)
                        {
                            results.Add(rw.Word);
                            sw.WriteLine(rw.Word);
                        }

                        sw.Close();
                    }
                }
                catch (Exception) { /* Some problem with the web service or writing the cache file.  Can't do much. */ }
            }

            return results;
        }
    }
}