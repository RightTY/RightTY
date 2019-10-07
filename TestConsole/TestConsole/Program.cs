using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Test test1 = new Test("3");
            Test test2 = new Test("3");
            //Dictionary<string, string> dic = new Dictionary<string, string>
            //{
            //    {"A","2" },
            //    {"B","2" }
            //};
            //Console.WriteLine($"{dic["A"] == dic["B"]}");
            //Console.WriteLine($"{(string)dic["A"]==(string)dic["B"]}");
            //Console.WriteLine($"{dic["A"].ToString() == dic["B"].ToString()}");
            //Console.WriteLine($"{Convert.ToInt32(dic["A"]) == Convert.ToInt32(dic["B"])}");

            Dictionary<string, object> dic = new Dictionary<string, object>()
            {
                {"A",test1 },
                {"B",test2 }
            };

            Dictionary<string, object> dic2 = new Dictionary<string, object>()
            {
                {"A",test1 },
                {"B",test1 }
            };

            Console.WriteLine($"{dic["A"] == dic["B"]}");
            Console.WriteLine($"{dic2["A"] == dic2["B"]}");
            Console.ReadKey();
        }
    }
    class Test
    {
        public string T { get; set; }

        public Test(string value)
        {
            T = value;
        }
    }
}
