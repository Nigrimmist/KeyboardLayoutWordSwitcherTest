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

                    if (i == 0 && !variants.First().StartsWith(@"!/NtcN9")) Console.WriteLine("*Incorrect Algoritm behaviour(" + algorithm.AuthoredBy());
                    times.Add(sw.Elapsed);
                    sw.Reset();
                }

                double doubleAverageTicks = times.Average(timeSpan => timeSpan.Ticks);
                double sum = times.Sum(t => t.Ticks);
                yield return new RunInfo()
                {
                    AvElapsed = new TimeSpan((long)doubleAverageTicks),
                    TestName = algorithm.AuthoredBy(),
                    ElapsedSum = new TimeSpan((long)sum),
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
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("# |      AVERAGE     |       TOTAL      |     By");
            Console.WriteLine("--------------------------------------------------");
            int i = 0;
            foreach (RunInfo result in infos.OrderBy(orderByFunc))
            {
                Console.WriteLine("{2} | {0} | {3} | {1}", result.AvElapsed, result.TestName, ++i,result.ElapsedSum);
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
        public TimeSpan AvElapsed { get; set; }
        public TimeSpan ElapsedSum { get; set; }
    }

    #endregion
}
