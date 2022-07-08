using System.Collections.Generic;
using System.Linq;

namespace WordFinder
{
    public static class Finder
    {
        /// <summary>
        /// Create a dictionnary of every letter and the words that begin with that letter as well as putting each word in upper-case
        public static Dictionary<char, string[]> Index(IEnumerable<string> words) => "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToDictionary(l => l, l => words.Where(w => char.ToUpper(w[0]) == l).Select(w => w.ToUpper()).ToArray());

        public static IEnumerable<string> Find(string sample, string[] words)
        {
            if (string.IsNullOrEmpty(sample) || words is null || words.Length == 0)
                yield break;

            sample = sample.ToUpper();

            var index = Index(words);

            for (int i = 0; i < sample.Length; i++)
            {
                string[] potentialWords = index[sample[i]];

                if (potentialWords.Length == 0)
                    continue;

                string wordSample = sample[i].ToString();
                int offset = 0;

                bool found = false;

                foreach (string word in search())
                    yield return word;

                while (found && i + offset < sample.Length)
                {
                    wordSample += sample[i + offset];
                    potentialWords = potentialWords.Where(w => w.StartsWith(wordSample)).ToArray();

                    foreach (string word in search())
                        yield return word;
                }

                IEnumerable<string> search()
                {
                    if (potentialWords.Length == 0)
                    {
                        found = false;
                        yield break;
                    }

                    if (potentialWords.Any(w => w == wordSample))
                    {
                        found = true;
                        yield return wordSample;
                    }

                    offset++;
                }
            }
        }
        public static IEnumerable<string> Hex(IEnumerable<string> words) => from word in words
                                                                            where word.Length == 4
                                                                            let upper = word.ToUpper()
                                                                            where upper.All(l => l is 'O' or 'I' or >= 'A' and <= 'F')
                                                                            select $"0x{upper.Replace('O', '0').Replace('I', '1')}";
    }
}
