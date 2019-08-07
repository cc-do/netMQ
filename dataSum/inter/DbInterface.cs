using dataSum.model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace dataSum.inter
{
    public class DbInterface<T> where T: class,new()
    {
        public DbInterface()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["connectStr"].ConnectionString.ToString(),
                DbType = DbType.MySql,
                InitKeyType = InitKeyType.SystemTable,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了

            });
        }

        //注意：不能写成静态的
        public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作
        public SimpleClient<SumModel> sumModel { get { return new SimpleClient<SumModel>(Db); } }//用来处理Student表的常用操作
        public SimpleClient<T> CurrentDb { get { return new SimpleClient<T>(Db); } }//用来处理T表的常用操作

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetList()
        {
            return CurrentDb.GetList();
        }

        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetListByFilter(string filter)
        {
            return CurrentDb.GetList();
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool Insert(T obj)
        {
            return CurrentDb.Insert(obj);//插入
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(dynamic id)
        {
            return CurrentDb.Delete(id);
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Update(T obj)
        {
            return CurrentDb.Update(obj);
        }
    }
}
