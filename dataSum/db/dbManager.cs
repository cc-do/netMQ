using dataSum.model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dataSum.db
{
    public class dbManager : dbContext
    {
        //SimpleClient实现查询例子
        public void SearchData()
        {
            var data1 = sumModel.GetById(1);//根据ID查询
            var data2 = sumModel.GetList();//查询所有
            var data3 = sumModel.GetList(it => it.id == 1);  //根据条件查询  
            var data4 = sumModel.GetSingle(it => it.id == 1);//根据条件查询一条

            var p = new PageModel() { PageIndex = 1, PageSize = 2 };// 分页查询
            var data5 = sumModel.GetPageList(it => it.key == "", p);
            Console.Write(p.PageCount);//返回总数


            // 分页查询加排序
            var data6 = sumModel.GetPageList(it => it.key == "", p, it => it.key, OrderByType.Asc);
            Console.Write(p.PageCount);//返回总数


            //组装条件查询作为条件实现 分页查询加排序
            List<IConditionalModel> conModels = new List<IConditionalModel>();
            conModels.Add(new ConditionalModel() { FieldName = "id", ConditionalType = ConditionalType.Equal, FieldValue = "1" });//id=1
            var data7 = sumModel.GetPageList(conModels, p, it => it.key, OrderByType.Asc);

            //4.9.7.5支持了转换成queryable,我们可以用queryable实现复杂功能
            sumModel.AsQueryable().Where(x => x.id == 1).ToList();
        }

        //插入例子
        public void InsertData(SumModel sum)
        {

            //var sum = new SumModel() { key = "jack" };
            //var studentArray = new SumModel[] { sum };
            //sumModel.InsertRange(studentArray);//批量插入
            //var id = sumModel.InsertReturnIdentity(sum);//插入返回自增列

            sumModel.Insert(sum);//插入
        }


        //更新例子
        public void UpdateDemo()
        {
            var student = new SumModel() { id = 1, key = "jack" };
            var studentArray = new SumModel[] { student };

            sumModel.Update(student);//根据实体更新

            sumModel.UpdateRange(studentArray);//批量更新

            sumModel.Update(it => new SumModel() { key = "a", add_date = DateTime.Now.ToString() }, it => it.id == 1);// 只更新Name列和CreateTime列，其它列不更新，条件id=1

            //支持StudentDb.AsUpdateable(student)
        }

        //删除例子
        public void DeleteData()
        {
            var student = new SumModel() { id = 1, key = "jack" };

            sumModel.Delete(student);//根据实体删除
            sumModel.DeleteById(1);//根据主键删除
            sumModel.DeleteById(new int[] { 1, 2 });//根据主键数组删除
            sumModel.Delete(it => it.id == 1);//根据条件删除

            //支持StudentDb.AsDeleteable()
        }

        //使用事务的例子
        public void TranData()
        {

            var result = Db.Ado.UseTran(() =>
            {
                //这里写你的逻辑
            });
            if (result.IsSuccess)
            {
                //成功
            }
            else
            {
                Console.WriteLine(result.ErrorMessage);
            }
        }
    }
}
