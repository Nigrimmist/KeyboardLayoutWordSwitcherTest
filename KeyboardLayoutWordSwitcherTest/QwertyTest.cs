using System;
using System.Collections.Generic;
using KeyboardLayoutWordSwitcherTest.Algorithms;

namespace KeyboardLayoutWordSwitcherTest
{
    public partial class QwertyTest
    {
        private List<IAlg> algs = new List<IAlg>()
        {
            new Nigrimmist(), 
            new DPleshakov(),
            new SMLL(), 
            new Anime()
        };

        private static Dictionary<Language, char[]> langSamples = new Dictionary<Language, char[]> {
            { Language.l1, "йцукенгшщзхъфывапролджэячсмитьбю.ЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ,".ToCharArray() },
            { Language.l2, "qwertyuiop[]asdfghjkl;'zxcvbnm,./QWERTYUIOP{}ASDFGHJKL:\"ZXCVBNM<>?".ToCharArray() }
        };

        public void Start()
        {
            string str = "!.ТесТ9. эники, бэники, ели ВАРЕНИКИ!";
            Console.WriteLine("Test init");
            for (var i = 0; i <= 10; i++)
            {
                str += str;
            }
            

            Console.WriteLine("Test run... wait please");
            Console.WriteLine("str.length:" + str.Length);
            List<RunInfo> results = new List<RunInfo>();
            results.AddRange(RunTestForLangs(str, GetLangSet(2)));

            DisplayResults(results, "2 langs. order : AVERAGE",info => info.AvElapsed);
            DisplayResults(results, "2 langs. order : TOTAL", info => info.ElapsedSum);
            results.Clear();
            results.AddRange(RunTestForLangs(str, GetLangSet(10)));
            DisplayResults(results, "10 languages. order : AVERAGE", info => info.AvElapsed);
            DisplayResults(results, "10 languages. order : TOTAL", info => info.ElapsedSum);
            results.Clear();
            results.AddRange(RunTestForLangs(str, GetLangSet(30)));
            DisplayResults(results, "30 languages. order : AVERAGE", info => info.AvElapsed);
            DisplayResults(results, "30 languages. order : TOTAL", info => info.ElapsedSum);

            Console.ReadLine();
        }


    }


}