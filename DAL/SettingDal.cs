using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace DAL
{
    public class SettingDal
    {

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public int Insert(Setting setting)
        {
            string sql = "insert into [dbo].[T_Setting] values(@Name,@value,@Remark)";
            SqlParameter[] pms = {
                new SqlParameter("@name", SqlDbType.NVarChar, 32) { Value=setting.Name},
                new SqlParameter("@value", SqlDbType.NVarChar, 128) { Value = setting.Value },
                new SqlParameter("@Remark", SqlDbType.NVarChar, 32) { Value = setting.Remark==null?DBNull.Value:(object)setting.Remark} };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public int Update(Setting setting)
        {
            string sql = "update [dbo].[T_Setting] set  Name=@Name, value=@value, Remark=@Remark where Id=@Id";
            SqlParameter[] pms = {
                new SqlParameter("@name", SqlDbType.NVarChar, 32) { Value=setting.Name},
                new SqlParameter("@value", SqlDbType.NVarChar, 128) { Value = setting.Value },
                new SqlParameter("@Remark", SqlDbType.NVarChar, 32) {Value = setting.Remark==null?DBNull.Value:(object)setting.Remark},
            new SqlParameter("@id", SqlDbType.Int) { Value = setting.Id }};
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
        }

        /// <summary>
        /// 根据id 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            string sql = "delete [dbo].[T_Setting] where Id=@id";
            SqlParameter pm = new SqlParameter("@id", SqlDbType.Int) { Value = id };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pm);
        }

        /// <summary>
        /// 获取指定范围 的数据
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<Setting> GetNavPageList(int start, int end)
        {
            string sql = "select * from( select *,ROW_NUMBER()over(order by Remark) as num from [dbo].[T_Setting] where Name='NavInfo') as t where t.num >= @start and t.num <= @end";
            SqlParameter[] pms = {
                new SqlParameter("@start",SqlDbType.Int) { Value=start},
                new SqlParameter("@end",SqlDbType.Int) { Value=end},
            };
            DataTable tb = SqlHelper.ExecuteDataTable(sql, CommandType.Text, pms);
            return Table2List(tb);
        }

        /// <summary>
        /// 获取导航信息总条数
        /// </summary>
        /// <returns></returns>
        public int GetNavCount()
        {
            string sql = "select count(1) from [dbo].[T_Setting] where [Name]='NavInfo'";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql, CommandType.Text));
        }


        /// <summary>
        /// 根据Id获取一个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Setting GetModelById(int id)
        {
            string sql = "select * from [dbo].[T_Setting] where Id=@id";
            SqlParameter pm = new SqlParameter("@id", SqlDbType.Int) { Value = id };
            List<Setting> list = Table2List(SqlHelper.ExecuteDataTable(sql, CommandType.Text, pm));
            if (list.Count > 0) return list[0];
            else return null;
        }

        /// <summary>
        /// table 转 list 集合
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        private List<Setting> Table2List(DataTable tb)
        {
            List<Setting> list = new List<Setting>();
            foreach (DataRow dr in tb.Rows)
            {
                list.Add(DataRowToModel(dr));
            }
            return list;
        }


        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Setting DataRowToModel(DataRow row)
        {
            Setting model = new Setting();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["value"] != null)
                {
                    model.Value = row["value"].ToString();
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
            }
            return model;
        }
    }
}
