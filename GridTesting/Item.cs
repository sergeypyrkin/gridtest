using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GridTesting
{
    public class Item
    {
        public int A1 { get; set; }
        public int A2 { get; set; }
        public int A3 { get; set; }
        public int A4 { get; set; }
        public int A5 { get; set; }
        public int A6 { get; set; }
        public int A7 { get; set; }
        public int A8 { get; set; }

        public String st
        {
            get
            {
                return A1.ToString("000000") + A2.ToString("000000") + A3.ToString("000000") + A4.ToString("000000");
            }
        }

        public Item()
        {
            Thread.Sleep(1);
            Random rand = new Random(unchecked((int)(DateTime.Now.Ticks)));
            A1 = rand.Next(0, 10000);
            A2 = rand.Next(0, 10000);
            A3 = rand.Next(0, 10000);
            A4 = rand.Next(0, 10000);
            A5 = rand.Next(0, 10000);
            A6 = rand.Next(0, 10000);
            A7 = rand.Next(0, 10000);
            A8 = rand.Next(0, 10000);

        }
    }
}
