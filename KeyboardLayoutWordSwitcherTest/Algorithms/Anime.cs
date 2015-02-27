using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardLayoutWordSwitcherTest.Algorithms
{
    public class Anime : IAlg
    {
        class Replacement
        {
            public char[] Chars;

            public Replacement()
            {
                
            }
        }

        private Dictionary<Language, KeyValuePair<Language, char[]>[]> _switches =
            new Dictionary<Language, KeyValuePair<Language, char[]>[]>();

        public void Init(Dictionary<Language, char[]> langs)
        {
            foreach (var lang1 in langs)
            {
                var lst = new List<KeyValuePair<Language, char[]>>();
                foreach (var lang2 in langs)
                {
                    var replacement = new char[char.MaxValue];
                    for (char c = (char) 0; c < char.MaxValue; c++)
                        replacement[c] = c;

                    for (var c = 0; c < lang1.Value.Length; c++)
                        replacement[lang1.Value[c]] = lang2.Value[c];
                    lst.Add(new KeyValuePair<Language, char[]>(lang2.Key, replacement));
                }
                _switches[lang1.Key] = lst.ToArray();
            }
        }

        public List<string> Start(string value, Language wordLanguage)
        {
            return Switch(value,wordLanguage);
        }

        private List<string> Switch(string s, Language wordLanguage)
        {
            var toReturn = new List<string>();
            foreach (var lang in _switches[wordLanguage])
            {
                if (lang.Key != wordLanguage)
                {
                    var retstr =new char[s.Length];
                    var len = s.Length;
                    for (var c = 0; c < len; c++)
                        retstr[c] = lang.Value[s[c]];

                    toReturn.Add(new string(retstr));
                }
            }
            return toReturn;
        }

        public string AuthoredBy()
        {
            return "(౪_ರೃ)";
        }
    }
}
