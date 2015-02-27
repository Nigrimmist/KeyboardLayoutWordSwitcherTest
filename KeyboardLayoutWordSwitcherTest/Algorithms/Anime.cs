using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardLayoutWordSwitcherTest.Algorithms
{
    public class Anime : IAlg
    {
        private Dictionary<Language, char[]> langs;
        public void Init(Dictionary<Language, char[]> langs)
        {
            this.langs = langs;
        }

        public List<string> Start(string value, Language wordLanguage)
        {
            return Switch(value,wordLanguage);
        }
        private List<string> Switch(string s,Language wordLanguage)
        {
            List<string> toReturn = new List<string>();
            var wordLangSet = langs[wordLanguage];
            foreach (var lang in langs)
            {
                if (lang.Key != wordLanguage)
                {
                    foreach (var p in wordLangSet.Zip(lang.Value, (oldC, newC) => new { oldC, newC }))
                        s = s.Replace(p.oldC, p.newC);
                    toReturn.Add(s);
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
