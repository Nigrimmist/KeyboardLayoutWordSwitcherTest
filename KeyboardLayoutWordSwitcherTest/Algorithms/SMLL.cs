using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardLayoutWordSwitcherTest.Algorithms
{
    public class SMLL:IAlg
    {
        private static Dictionary<Language, char[]> langKeyBoardDictionary = new Dictionary<Language, char[]>();
        
        private static Dictionary<Language, Dictionary<char, char[]>> _languageDictionary;

        public static Dictionary<Language, Dictionary<char, char[]>> LanguageDictionary
        {
            get
            {
                if (_languageDictionary == null)
                {
                    if (_languageDictionary == null)
                    {
                        _languageDictionary = new Dictionary<Language, Dictionary<char, char[]>>();
                        foreach (var langSetI in langKeyBoardDictionary)
                        {
                            var tDictionary = new Dictionary<char, char[]>();
                            int i = 0;
                            var langList = langKeyBoardDictionary.Where(x => x.Key != langSetI.Key).ToList();
                            foreach (char t in langSetI.Value)
                            {
                                var langChars = new char[langList.Count];
                                int j = 0;
                                foreach (var langSetJ in langList)
                                {
                                    langChars[j] = langSetJ.Value[i];
                                    j++;
                                }
                                tDictionary.Add(t, langChars);
                                i++;
                            }
                            _languageDictionary.Add(langSetI.Key, tDictionary);
                        }
                    }
                }

                return _languageDictionary;
            }
            set { _languageDictionary = value; }
        }

        public void Init(Dictionary<Language, char[]> langs)
        {
            _languageDictionary = null;
            langKeyBoardDictionary = langs;
            var s = LanguageDictionary;
        }
        /// <summary>
        /// return set of words for other language sets using current keyboard layout language as default
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<string> GetOtherKeyboardLayoutWords2(string value, Language currentKeyboardLang)
        {
            
            var requiredLangs = langKeyBoardDictionary.Where(x => x.Key != currentKeyboardLang).Select(x => x.Key).ToList();
            List<StringBuilder> sbs = requiredLangs.Select(x => new StringBuilder(value)).ToList();


            Dictionary<char, char[]> langDict = LanguageDictionary[currentKeyboardLang];
            for (int i = 0; i < value.Length; i++)
            {
                int j = 0;
                char[] currLang;
                if(langDict.TryGetValue(value[i], out currLang))
                {
                    foreach (var lang in requiredLangs)
                    {
                        sbs[j][i] = currLang[j];
                        j++;
                    }
                }

            }

            return sbs.Select(x => x.ToString()).ToList();
        }

        public List<string> Start(string value, Language wordLanguage)
        {
            return GetOtherKeyboardLayoutWords2(value, wordLanguage);
        }

        public string AuthoredBy()
        {
            return "SMLL";
        }
    }
}
