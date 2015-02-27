using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardLayoutWordSwitcherTest
{
    public partial class QwertyTest
    {
        #region Support methods
        private IEnumerable<RunInfo> RunTestForLangs(string str, Dictionary<Language, char[]> langs)
        {
            Stopwatch sw = new Stopwatch();

            for (int i = 0; i < algs.Count; i++)
            {
                var algorithm = algs[i];
                algorithm.Init(langs);

                List<TimeSpan> times = new List<TimeSpan>();
                for (int j = 0; j < 100; j++)
                {
                    sw.Start();
                    var variants = algorithm.Start(str, Language.l1);
                    sw.Stop();

                    if (i == 0 && !variants.First().StartsWith(@"!/NtcN9")) Console.WriteLine("*Неправильно отработавший алгоритм (" + algorithm.AuthoredBy());
                    times.Add(sw.Elapsed);
                    sw.Reset();
                }

                double doubleAverageTicks = times.Average(timeSpan => timeSpan.Ticks);
                long longAverageTicks = Convert.ToInt64(doubleAverageTicks);

                
                yield return new RunInfo()
                {
                    Elapsed = new TimeSpan(longAverageTicks),
                    TestName = algorithm.AuthoredBy()
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

        private void DisplayResults(List<RunInfo> infos, string caption)
        {
            Console.WriteLine("-----------------");
            Console.WriteLine(caption);
            Console.WriteLine("-----------------");
            foreach (RunInfo result in infos.OrderBy(x => x.Elapsed))
            {
                Console.WriteLine("#1 : {0} by {1}", result.Elapsed, result.TestName);
            }
            Console.WriteLine("-----------------");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(Environment.NewLine);
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
