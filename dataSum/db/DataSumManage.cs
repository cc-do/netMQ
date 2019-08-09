using dataSum.inter;
using dataSum.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dataSum.db
{
    public class DataSumManage : DbInterface<SumModel>
    {
        public override List<SumModel> GetListByFilter(string date, string key)
        {
            return base.sumModel.GetList(it => it.add_date == date && it.key == key);
        }

        public override bool Exits(string date)
        {
            return base.sumModel.IsAny(it => it.add_date == date);
        }
    }
}
