using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardLayoutWordSwitcherTest.Algorithms
{
    class DPleshakov : IAlg
    {
        private static Dictionary<Language, char[]> keyboardLayouts;
        public void Init(Dictionary<Language, char[]> langs)
        {
            keyboardLayouts = langs;
        }

        public List<string> Start(string value, Language wordLanguage)
        {
            return Convert(value, wordLanguage);
        }

        private  string Convert(string input, char[] keysFrom, char[] keysTo)
        {
            if (keysFrom.Length != keysTo.Length)
            {
                throw new ArgumentException("Length of keys arrays should be equal.");
            }

            for (int index = 0; index < keysFrom.Length; ++index)
            {
                input = input.Replace(keysFrom[index], keysTo[index]);
            }
            return input;
        }

        public  string Convert(string input, Language fromLanguage, Language toLanguage)
        {
            return Convert(input, keyboardLayouts[fromLanguage], keyboardLayouts[toLanguage]);
        }

        public List<string> Convert(string input, Language toLanguage)
        {
            return keyboardLayouts.Keys.Where(l => l != toLanguage).Select(x=>Convert(input, x, toLanguage)).ToList();
        }

        public string AuthoredBy()
        {
            return "DPleshakov";
        }
    }

    public static class QuertyConverter
    {
        

        
    }
}
