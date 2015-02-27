using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardLayoutWordSwitcherTest.Algorithms
{
    
        public class Nigrimmist : IAlg
        {
            private static Dictionary<Language, char[]> langKeyBoardDictionary;
            private static readonly object _dictLocker = new object();

            private static Dictionary<Language, Dictionary<Language, Dictionary<char, char>>> _languageDictionary;


            public static Dictionary<Language, Dictionary<Language, Dictionary<char, char>>> LanguageDictionary
            {
                get
                {
                    if (_languageDictionary == null)
                    {
                        lock (_dictLocker)
                        {
                            if (_languageDictionary == null)
                            {
                                _languageDictionary = new Dictionary<Language, Dictionary<Language, Dictionary<char, char>>>();
                                foreach (var langSetI in langKeyBoardDictionary)
                                {
                                    Dictionary<Language, Dictionary<char, char>> tDictionary = new Dictionary<Language, Dictionary<char, char>>();
                                    foreach (var langSetJ in langKeyBoardDictionary)
                                    {

                                        if (langSetJ.Key == langSetI.Key) continue; //skip duplicates
                                        //Dictionary<char, char> charIndexDict = langSetJ.Value.Zip(langSetI.Value, (a, b) => new { Key = a, Val = b }).ToDictionary(kvp => kvp.Val, kvp => kvp.Key);

                                        var charIndexDict = new Dictionary<char, char>();
                                        for (int i = 0; i < langSetJ.Value.Length; i++)
                                        {
                                            charIndexDict.Add(langSetI.Value[i], langSetJ.Value[i]);
                                        }

                                        tDictionary.Add(langSetJ.Key, charIndexDict);

                                    }
                                    _languageDictionary.Add(langSetI.Key, tDictionary);
                                }
                            }
                        }
                    }
                    return _languageDictionary;
                }
                set { _languageDictionary = value; }
            }

            public void Init(Dictionary<Language, char[]> langs)
            {
                LanguageDictionary = null;
                langKeyBoardDictionary = langs;
                var s = LanguageDictionary;
            }

            public List<string> Start(string value, Language wordLanguage)
            {
                var toReturn = new List<string>();

                foreach (var lang in langKeyBoardDictionary)
                {
                    if (lang.Key != wordLanguage)
                    {
                        var currLangSetDict = LanguageDictionary[wordLanguage][lang.Key]; //from lang to lang

                        StringBuilder sb = new StringBuilder(value);
                        for (int i = 0; i < value.Length; i++)
                        {
                            char foundChar;
                            if (currLangSetDict.TryGetValue(sb[i], out foundChar)) //if char pos found to replace
                            {
                                sb[i] = foundChar;
                            }
                        }
                        toReturn.Add(sb.ToString());
                    }
                }

                return toReturn;
            }

            public string AuthoredBy()
            {
                return "Nigrimmist";
            }
        }
    }

