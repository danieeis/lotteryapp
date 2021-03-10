using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Group
    {
        public string Result { get; set; }


        public IEnumerable<int> CounterResult10 { get; set; }
        public IEnumerable<int> CounterResult
        {
            get => GroupedCounter.Take(4).Select(x => x.Counter);
        }

        public IEnumerable<string> CounterOut10 { get; set; }
        public IEnumerable<string> CounterOut
        {
            get
            {
                List<string> list = new List<string>();
                var first = GroupedCounter.First().Number;
                list.Add(first);
                if (CounterResult.Count() > 1 && CounterResult.ElementAt(0) == CounterResult.ElementAt(1))
                {
                    var second = GroupedCounter.ElementAt(1).Number;
                    list.Add(second);
                }
                if (CounterResult.Count() > 2 && CounterResult.ElementAt(1) == CounterResult.ElementAt(2))
                {
                    var third = GroupedCounter.ElementAt(2).Number;
                    list.Add(third);
                }
                if (CounterResult.Count() > 3 && CounterResult.ElementAt(2) == CounterResult.ElementAt(3))
                {
                    var third = GroupedCounter.ElementAt(3).Number;
                    list.Add(third);
                }
                return list;
            }
        }
        public IEnumerable<DrawsCounter> GroupedCounter { get; set; }
        public List<string> Items { get; set; }
        public Group(List<string> items)
        {
            this.Items = items;
        }
    }

    public class DrawsGrouped
    {
        public DrawsGrouped(Group a, Group b, Group c, Group d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }

        public Group A { get; set; }
        public Group B { get; set; }
        public Group C { get; set; }
        public Group D { get; set; }
    }

    public class DrawsCounter
    {
        public string Number { get; set; }
        public int Counter { get; set; }

        public DrawsCounter(string number, int counter) => (Number, Counter) = (number, counter);
    }

    public class LotteryForecast
    {
        public DrawsGrouped Group { get; set; }

        public LotteryForecast(DrawsGrouped group) => (Group) = (group);


        public LotteryForecast GenerateCounterGrouped(int quantity, bool inverse = false)
        {
            if (!inverse)
            {
                //Group.A.GroupedCounter = GenerateCounterGrouped(Group.A.Items.Take(quantity));
                //Group.B.GroupedCounter = GenerateCounterGrouped(Group.B.Items.Take(quantity));
                //Group.C.GroupedCounter = GenerateCounterGrouped(Group.C.Items.Take(quantity));
                //Group.D.GroupedCounter = GenerateCounterGrouped(Group.D.Items.Take(quantity));

                Group.A.GroupedCounter = GenerateCounterGrouped(Group.A.Items.Take(quantity)).OrderByDescending(x => x.Counter);
                Group.B.GroupedCounter = GenerateCounterGrouped(Group.B.Items.Take(quantity)).OrderByDescending(x => x.Counter);
                Group.C.GroupedCounter = GenerateCounterGrouped(Group.C.Items.Take(quantity)).OrderByDescending(x => x.Counter);
                Group.D.GroupedCounter = GenerateCounterGrouped(Group.D.Items.Take(quantity)).OrderByDescending(x => x.Counter);
            }
            else
            {
                //Group.A.GroupedCounter = GenerateCounterGrouped(Group.A.Items.TakeLast(quantity));
                //Group.B.GroupedCounter = GenerateCounterGrouped(Group.B.Items.TakeLast(quantity));
                //Group.C.GroupedCounter = GenerateCounterGrouped(Group.C.Items.TakeLast(quantity));
                //Group.D.GroupedCounter = GenerateCounterGrouped(Group.D.Items.TakeLast(quantity));
                Group.A.GroupedCounter = GenerateCounterGrouped(Group.A.Items.TakeLast(quantity)).OrderByDescending(x => x.Counter);
                Group.B.GroupedCounter = GenerateCounterGrouped(Group.B.Items.TakeLast(quantity)).OrderByDescending(x => x.Counter);
                Group.C.GroupedCounter = GenerateCounterGrouped(Group.C.Items.TakeLast(quantity)).OrderByDescending(x => x.Counter);
                Group.D.GroupedCounter = GenerateCounterGrouped(Group.D.Items.TakeLast(quantity)).OrderByDescending(x => x.Counter);
            }
            return this;
        }

        public List<DrawsCounter> GenerateCounterGrouped(IEnumerable<string> items)
        {
            List<DrawsCounter> returnValues = new List<DrawsCounter>();
            foreach (var item in items.GroupBy(x => x))
            {
                returnValues.Add(new DrawsCounter(item.Key, item.Count()));
            }

            return returnValues;
        }
    }
}
