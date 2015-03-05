using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardLayoutWordSwitcherTest.Algorithms
{
    public class Yafimau : IAlg
    {
        private List<LanguageLayout> normLanguages;
        private char[] langResult = new char[100000];
        private char[] buffer = new char[100000];

        public void Init(Dictionary<Language, char[]> langs)
        {
            normLanguages = new List<LanguageLayout>(langs.Count);

            foreach (var lang in langs.Keys)
            {
                Dictionary<char, int> charNum = new Dictionary<char, int>();
                Dictionary<int, char> numChar = new Dictionary<int, char>();

                int i = 0;
                foreach (var l in langs[lang])
                {
                    charNum.Add(l, i);
                    numChar.Add(i++, l);
                }

                normLanguages.Add(new LanguageLayout()
                {
                    Language = lang,
                    CharNum = charNum,
                    NumChar = numChar
                });
            }
        }

        public List<string> Start(string value, Language wordLanguage)
        {
            List<string> result = new List<string>(normLanguages.Count - 1);

            var words = value.Split(new[] { ' ' });

            var uniqueWords = GetUniqueWords(words);

            var encodedValue = EncodeWords(words, uniqueWords);

            var currentLanguage = normLanguages.SingleOrDefault(l => l.Language == wordLanguage);

            foreach (var lang in normLanguages)
            {
                if (lang.Language == wordLanguage)
                {
                    continue;
                }

                var wordsTranscripted = new List<string>(uniqueWords.Count);

                foreach (var uniqueWord in uniqueWords.Keys)
                {
                    CopyToBufferTranscriptedWord(uniqueWord, currentLanguage, lang);

                    wordsTranscripted.Add(new string(buffer, 0, uniqueWord.Length));
                }

                int ci = 0;
                for (int i = 0; i < encodedValue.Count; i++)
                {
                    var wordToAppend = wordsTranscripted[encodedValue[i]] + ' ';
                    
                    wordToAppend.CopyTo(0, langResult, ci, wordToAppend.Length);
                    ci += wordToAppend.Length;
                }

                result.Add(new string(langResult, 0, value.Length).Trim());
            }

            return result;
            //return new List<string>{@"!/NtcN9"};
        }

        private void CopyToBufferTranscriptedWord(string uniqueWord, LanguageLayout currentLanguage, LanguageLayout lang)
        {
            uniqueWord.CopyTo(0, buffer, 0, uniqueWord.Length);

            for (int i = 0; i < uniqueWord.Length; i++)
            {
                if (!currentLanguage.CharNum.ContainsKey(uniqueWord[i]))
                {
                    continue;
                }

                var charIndexInCurrentLayout = currentLanguage.CharNum[uniqueWord[i]];

                buffer[i] = lang.NumChar[charIndexInCurrentLayout];
            }
        }

        private static List<int> EncodeWords(string[] words, Dictionary<string, int> uniqueWords)
        {
            List<int> encodedValue = new List<int>();

            for (int i = 0; i < words.Length; i++)
            {
                encodedValue.Add(uniqueWords[words[i]]);
            }
            return encodedValue;
        }

        private static Dictionary<string, int> GetUniqueWords(string[] words)
        {
            Dictionary<string, int> uniqueWords = new  Dictionary<string, int> ();
            int total = 0;

            for (int i = 0; i < words.Count(); i++)
            {
                if (!uniqueWords.ContainsKey(words[i]))
                {
                    uniqueWords.Add(words[i], total++);
                }
            }
            return uniqueWords;
        }

        public string AuthoredBy()
        {
            return "Yafimau";
        }

        private class LanguageLayout
        {
            public Language Language { get; set; }

            public Dictionary<char, int> CharNum { get; set; }
        
            public Dictionary<int, char> NumChar { get; set; }
        }
    }
}
