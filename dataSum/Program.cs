using dataSum.db;
using dataSum.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dataSum
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> path_list = new List<string>();
            path_list.Add(@"D:\share");
            path_list.Add(@"D:\data");

            string[] path = path_list.ToArray();

            DataSum dataSum = new DataSum();
            dataSum.StatFunc(path);

            var sum = new SumModel() {
                key = "cc",
                sum = dataSum.totalBytes.ToString(),
                add_date = dataSum.addDate,
                update_date = dataSum.updateDate
            };

            var sm = new DataSumManage();
            sm.Insert(sum);

            Console.WriteLine(dataSum.dataPath.Count);
            Console.WriteLine(dataSum.addDate);
            Console.WriteLine(dataSum.updateDate);
            Console.WriteLine(dataSum.totalBytes);
            Console.WriteLine("*****************************");


            Console.ReadLine();
        }
    }
}
