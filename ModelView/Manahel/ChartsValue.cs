using ManhaleAspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManhaleAspNetCore.ModelView.Manahel
{
    public class ChartsValue
    {
        //int resultCount = 0;
        static int level1 = 0, level2 = 0, level3 = 0, level4 = 0;
        static int counter1 = 0, counter2 = 0, counter3 = 0, counter4 = 0;
        public static List<Tuple<string, double>> list = new List<Tuple<string, double>>()
        {
            new Tuple<string, double>("Level 1 Excellent",20),
            new Tuple<string, double>("Level 2 Strong",30),
            new Tuple<string, double>("Level 3 Medium",35),
            new Tuple<string, double> ("Level 4 Weak",15)
        };
        public static List<Tuple<string, double>> list2 = new List<Tuple<string, double>>()
        {
            new Tuple<string, double>("All",50),
            new Tuple<string, double> ("Complete",30),
            new Tuple<string, double>("Medium",5),
            new Tuple<string, double> ("Small",15)
        };
        public static List<Tuple<string, double>> list3 = new List<Tuple<string, double>>()
        {
            new Tuple<string, double>("Fertilized",35),
            new Tuple<string, double> ("not Fertilized",5),
            new Tuple<string, double>("Stacked",3),
            new Tuple<string, double> ("without Queue",7)
        };
        public static List<Tuple<string, double>> GetForAllManahel(ManahelContext context)
        {
            level1 = calc("Excellent", context);
            level2 = calc("Strong", context);
            level3 = calc("Medium", context);
            level4 = calc("Weak", context);
            list = new List<Tuple<string, double>>()
            {
                new Tuple<string, double>("Level 1 Excellent",level1),
                new Tuple<string, double>("Level 2 Strong",level2),
                new Tuple<string, double>("Level 3 Medium",level3),
                new Tuple<string, double>("Level 4 Weak",level4),
            };
            return list;
        }

        public static List<Tuple<string, double>> GetKhaliaCount(ManahelContext context)
        {
            
            return list2;
        }

        public static List<Tuple<string, double>> GetQueueCount(ManahelContext context)
        {
            calcQueueStatus(context);
            int sum = level4 + level3 + level2 + level1;
            counter4 = sum - (counter1 + counter2 + counter3);
            list3 = new List<Tuple<string, double>>()
            {
                new Tuple<string, double>("Fertilized",counter1),
                new Tuple<string, double> ("not Fertilized ",counter2),
                new Tuple<string, double>("Stacked",counter3),
                new Tuple<string, double> ("without Queue",counter4)
            };
            return list3;
        }
        private static int calc(string str, ManahelContext context)
        {
            //List<Khalias> khalias = context.khaliases.Where(b => b.KhaliaLevel == str).Count();
            //return khalias.Count;
            return context.khaliases.Where(b => b.KhaliaLevel == str).Count();
        }

        private static void calcQueueStatus(ManahelContext context)
        {
            var queueus = context.Queues;
            foreach (var item in queueus)
            {
                if (item != null)
                {
                    if (item.QueueStatus == "ملقحة")
                        counter3++;
                    else if (item.QueueStatus == "عذراء")
                        counter1++;
                    else                     // مكدبة
                        counter2++;
                }
                else
                    counter4++;
            }

        }
    }
}
