using dataSum.db;
using dataSum.model;
using dataSum.tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace dataSum
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigModel configModel = CommonTools.GetJsonConfig();
            var sm = new DataSumManage();
            while (true)
            {
                if (CommonTools.WorkingDate())
                {
                    DataStatistics(configModel, sm);
                    Thread.Sleep(600000);
                }
                else
                {
                    Console.Clear();
                }
            }

        }

        static void DataStatistics(ConfigModel configModel, DataSumManage sm)
        {
            string[] path = configModel.Path;
            string config_key = configModel.key;

            DataSum dataSum = new DataSum();
            dataSum.StatFunc(path);

            string total = CommonTools.GetSizeStr(dataSum.totalBytes);
            string addDate = dataSum.addDate;
            string updateDate = dataSum.updateDate;

            var dataList = sm.GetListByFilter(addDate, config_key);
            int count = dataList.Count;

            var sum = new SumModel()
            {
                key = config_key,
                sum = total,
                add_date = addDate,
                update_date = updateDate,
                path = string.Join(",", path)
        };
            
            if (count != 0)
            {
                sum.id = dataList[0].id;
                sm.Update(sum);
            }
            else
            {
                sm.Insert(sum);
            }
            
            Console.WriteLine(addDate);
            Console.WriteLine(updateDate);
            Console.WriteLine(total);
            Console.WriteLine("数据更新成功");
            Console.WriteLine("*****************************");
            
        }
    }
}
