using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KeyboardLayoutWordSwitcherTest
{
    public partial class QwertyTest
    {
        #region Support methods
        private IEnumerable<RunInfo> RunTestForLangs(string str, Dictionary<Language, char[]> langs)
        {
            for (int i = 0; i < algs.Count; i++)
            {
                var algorithm = algs[i];
                algorithm.Init(langs);

                algorithm.Start(str, Language.l1);
                var results = new List<List<string>>(1000);
                var sw = Stopwatch.StartNew();
                for (int j = 0; j < 100; j++)
                {
                    var variants = algorithm.Start(str, Language.l1);
                    results.Add(variants);
                    
                }
                sw.Stop();
                if (results[0] == null || results[0].Count == 0 || !results[0].First().StartsWith(@"!/NtcN9"))
                    Console.WriteLine("*Incorrect Algoritm behaviour(" + algorithm.AuthoredBy());
                

                yield return new RunInfo()
                {
                    TestName = algorithm.AuthoredBy(),
                    Elapsed = sw.Elapsed
                };
            }
        }

        private Dictionary<Language, char[]> GetLangSet(int count)
        {
            Dictionary<Language, char[]> toReturn = new Dictionary<Language, char[]>();

            for (var i = 0; i < count; i++)
            {
                Language lang;
                Language.TryParse("l" + (i+1), false, out lang);
                toReturn.Add(lang, langSamples.ContainsKey(lang) ? langSamples[lang] : langSamples.First().Value);
            }
            return toReturn;
        }

        private void DisplayResults(List<RunInfo> infos, string caption, Func<RunInfo,TimeSpan> orderByFunc)
        {
            Console.WriteLine(caption );
            Console.WriteLine("-----------------------------");
            Console.WriteLine("# |       TOTAL      |     By");
            Console.WriteLine("-----------------------------");
            int i = 0;
            foreach (RunInfo result in infos.OrderBy(orderByFunc))
            {
                Console.WriteLine("{1} | | {2} | {0}", result.TestName, ++i, result.Elapsed);
            }
            Console.WriteLine("--------------------------------------------------");
        }
        #endregion
    }

    #region Base
    public interface IAlg
    {
        void Init(Dictionary<Language, char[]> langs);
        List<string> Start(string value, Language wordLanguage);
        string AuthoredBy();
    }

    public enum Language
    {
        l1 = 0,
        l2,
        l3,
        l4,
        l5,
        l6,
        l7,
        l8,
        l9,
        l10,
        l11,
        l12,
        l13,
        l14,
        l15,
        l16,
        l17,
        l18,
        l19,
        l20,
        l21,
        l22,
        l23,
        l24,
        l25,
        l26,
        l27,
        l28,
        l29,
        l30
    }

    public class RunInfo
    {
        public string TestName { get; set; }
        public TimeSpan Elapsed { get; set; }
    }

    #endregion
}
