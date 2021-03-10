using System;
using Domain;
using Facet.Combinatorics;
using System.Collections.Generic;
using System.Linq;

namespace FloridaLottery.Services.GemsBuilder
{
    public static class DiamontGem
    {
        public static IEnumerable<Draw> GenerateDiamont(this IEnumerable<Draw> LastDraws)
        {
            var Forecast20 = new LotteryForecast(GroupingNumbersInArray(LastDraws))
                .GenerateCounterGrouped(20);
            var Forecast10 = new LotteryForecast(GroupingNumbersInArray(LastDraws))
                .GenerateCounterGrouped(10, inverse: true);

            GenerateGroupCounter10Last(Forecast10.Group.A, Forecast20.Group.A);
            GenerateGroupCounter10Last(Forecast10.Group.B, Forecast20.Group.B);
            GenerateGroupCounter10Last(Forecast10.Group.C, Forecast20.Group.C);
            GenerateGroupCounter10Last(Forecast10.Group.D, Forecast20.Group.D);

            Forecast10.Group.A.Result = CalculateResults10(Forecast10.Group.A);
            Forecast10.Group.B.Result = CalculateResults10(Forecast10.Group.B);
            Forecast10.Group.C.Result = CalculateResults10(Forecast10.Group.C);
            Forecast10.Group.D.Result = CalculateResults10(Forecast10.Group.D);

            Forecast20.Group.A.Result = CalculeResults(Forecast20.Group.A, Forecast10.Group.A);
            Forecast20.Group.B.Result = CalculeResults(Forecast20.Group.B, Forecast10.Group.B);
            Forecast20.Group.C.Result = CalculeResults(Forecast20.Group.C, Forecast10.Group.C);
            Forecast20.Group.D.Result = CalculeResults(Forecast20.Group.D, Forecast10.Group.D);

            List<string> DataProcessing = new List<string>();
            DataProcessing.Add(Forecast20.Group.A.Result);
            DataProcessing.Add(Forecast20.Group.B.Result);
            DataProcessing.Add(Forecast20.Group.C.Result);
            DataProcessing.Add(Forecast20.Group.D.Result);
            Console.WriteLine(String.Format("{{{0} {1} {2} {3}}}", Forecast20.Group.A.Result, Forecast20.Group.B.Result, Forecast20.Group.C.Result, Forecast20.Group.D.Result));
            Permutations<string> P1 = new Permutations<string>(DataProcessing,
                  GenerateOption.WithoutRepetition);
            string format1 = "Permutations without repetition; size = {0}";
            Console.WriteLine(String.Format(format1, P1.Count));
            foreach (IList<string> p in P1)
            {
                Console.WriteLine(String.Format("{{{0} {1} {2} {3}}}", p[0], p[1], p[2], p[3]));
                yield return new Draw($"{p[0]}{p[1]}{p[2]}{p[3]}");
            }
        }

        private static DrawsGrouped GroupingNumbersInArray(IEnumerable<Draw> LastDrawings)
        {
            Group A, B, C, D;
            A = new Group(LastDrawings.Select(x =>
                {
                    if (string.IsNullOrEmpty(x.Number))
                        return string.Empty;
                    return x.Number.ElementAt(0).ToString();
                }
            ).ToList());
            B = new Group(LastDrawings.Select(x =>
            {
                if (string.IsNullOrEmpty(x.Number))
                    return string.Empty;
                return x.Number.ElementAt(1).ToString();
            }
                ).ToList());
            C = new Group(LastDrawings.Select(x =>
            {
                if (string.IsNullOrEmpty(x.Number))
                    return string.Empty;
                return x.Number.ElementAt(2).ToString();
            }
                ).ToList());
            D = new Group(LastDrawings.Select(x =>
            {
                if (string.IsNullOrEmpty(x.Number))
                    return string.Empty;
                return x.Number.ElementAt(3).ToString();
            }
                ).ToList());
            return new DrawsGrouped(A, B, C, D);
        }

        private static void GenerateGroupCounter10Last(Group group10, Group group20)
        {
            var list = new List<int>();
            var list2 = new List<string>();
            foreach (var item in group10.GroupedCounter)
            {
                var count = group20.CounterOut.Count(x => x == item.Number);
                list.Add(count);

                if (count == 1)
                {
                    list2.Add(item.Number);
                }
            }
            group10.CounterOut10 = list2;
            group10.CounterResult10 = list;
        }



        static string CalculateResults10(Group group)
        {
            string result = string.Empty;
            if (group.CounterOut.Count() > 0)
            {
                result = group.CounterOut.First();
            }

            return result;
        }

        static string CalculeResults(Group first, Group second)
        {
            string result;
            if (first.CounterOut.Count() == 1 || string.IsNullOrEmpty(second.Result))
            {
                result = first.CounterOut.First();
            }
            else
            {
                result = second.Result;
            }

            return result;
        }
    }
}
