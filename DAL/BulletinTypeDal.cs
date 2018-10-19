using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BulletinTypeDal
    {
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int Insert(BulletinType type)
        {
            string sql = "insert into [dbo].[T_BulletinType] values(@typeName)";
            SqlParameter pm = new SqlParameter("@typeName", SqlDbType.NVarChar, 16) { Value = type.TypeName };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pm);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public int Update(BulletinType type)
        {
            string sql = "update  [dbo].[T_BulletinType] set TypeName=@typeName where Id=@id";
            SqlParameter[] pms = {
                new SqlParameter("@typeName",SqlDbType.NVarChar,16) { Value=type.TypeName},
                new SqlParameter("@id",SqlDbType.Int) { Value=type.Id}
            };
            return SqlHelper.ExecuteNonQuery(sql,CommandType.Text,pms);

        }


        /// <summary>
        /// 获取指定范围 的数据
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<BulletinType> GetNavPageList(int start, int end)
        {
            string sql = "select * from( select *,ROW_NUMBER()over(order by Id) as num from [dbo].[T_BulletinType]) as t where t.num >= @start and t.num <= @end";
            SqlParameter[] pms = {
                new SqlParameter("@start",SqlDbType.Int) { Value=start},
                new SqlParameter("@end",SqlDbType.Int) { Value=end},
            };
            DataTable tb = SqlHelper.ExecuteDataTable(sql, CommandType.Text, pms);
            return Table2List(tb);
        }

        /// <summary>
        /// 获取总条数
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            string sql = " select count(1) from  [dbo].[T_BulletinType]";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql, CommandType.Text));
        }

        /// <summary>
        /// 根据id 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            string sql = "delete [dbo].[T_BulletinType] where Id=@id";
            SqlParameter pm = new SqlParameter("@id", SqlDbType.Int) { Value = id };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pm);
        }


        /// <summary>
        /// 根据Id获取一个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BulletinType GetModelById(int id)
        {
            string sql = "select * from  [dbo].[T_BulletinType] where Id=@id";
            SqlParameter pm = new SqlParameter("@id", SqlDbType.Int) { Value = id };
            List<BulletinType> list = Table2List(SqlHelper.ExecuteDataTable(sql, CommandType.Text, pm));
            if (list.Count > 0) return list[0];
            else return null;
        }



        /// <summary>
        /// table 转 list 集合
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        private List<BulletinType> Table2List(DataTable tb)
        {
            List<BulletinType> list = new List<BulletinType>();
            foreach (DataRow dr in tb.Rows)
            {
                list.Add(DataRowToModel(dr));
            }
            return list;
        }



        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public BulletinType DataRowToModel(DataRow row)
        {
            BulletinType model = new BulletinType();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["TypeName"] != null)
                {
                    model.TypeName = row["TypeName"].ToString();
                }
            }
            return model;
        }
    }
}
