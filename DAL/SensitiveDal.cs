using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Common;

namespace DAL
{
    public class SensitiveDal
    {
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(Sensitive model)
        {
            string sql = "insert into [dbo].[T_Sensitive] values(@text,@banned,@mod) ";
            SqlParameter[] pms = {
                new SqlParameter("@text",SqlDbType.NVarChar,32) { Value=model.SensitiveText},
                new SqlParameter("@banned",SqlDbType.Bit) { Value=model.Banned==true?1:0},
                new SqlParameter("@mod",SqlDbType.Bit) { Value=model.Mod==true?1:0}
            };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            string sql = "delete [dbo].[T_Sensitive] where  id=@id ";
            SqlParameter pm = new SqlParameter("@id", SqlDbType.Int) { Value = id };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pm);
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="SensitiveText"></param>
        /// <returns></returns>
        public bool Exists(string SensitiveText)
        {
            string sql = "select count(1) from [dbo].[T_Sensitive] where [Sensitive]=@text";
            SqlParameter pm = new SqlParameter("@text", SqlDbType.NVarChar, 32) { Value = SensitiveText };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql, CommandType.Text, pm)) > 0;
        }

        /// <summary>
        /// 获取指定范围 的数据
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<Sensitive> GetPageList(int start, int end)
        {
            string sql = "select * from( select *,ROW_NUMBER()over(order by id) as num from [dbo].[T_Sensitive]) as t where t.num >= @start and t.num <= @end";
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
            string sql = "select count(1) from [dbo].[T_Sensitive]";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql, CommandType.Text));
        }

        /// <summary>
        /// 获取所有禁用词
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllBanned()
        {
            List<string> list = new List<string>();
            string sql = "select Sensitive from [dbo].[T_Sensitive] where Banned=1";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, CommandType.Text))
            {
                while (reader.Read())
                {
                    list.Add(reader["Sensitive"].ToString());
                }
            }
            return list;
        }

        /// <summary>
        /// 获取所有审查词
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllMod()
        {
            List<string> list = new List<string>();
            string sql = "select Sensitive from [dbo].[T_Sensitive] where Mod=1";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, CommandType.Text))
            {
                while (reader.Read())
                {
                    list.Add(reader["Sensitive"].ToString());
                }
            }
            return list;
        }
        /// <summary>
        /// table 转 list 集合
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        private List<Sensitive> Table2List(DataTable tb)
        {
            List<Sensitive> list = new List<Sensitive>();
            foreach (DataRow dr in tb.Rows)
            {
                list.Add(DataRowToModel(dr));
            }
            return list;
        }


        /// <summary>
		///  row 转 model  得到一个对象实体
		/// </summary>
		public Sensitive DataRowToModel(DataRow row)
        {
            Sensitive model = new Sensitive();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["Sensitive"] != null)
                {
                    model.SensitiveText = row["Sensitive"].ToString();
                }
                if (row["Banned"] != null && row["Banned"].ToString() != "")
                {
                    if ((row["Banned"].ToString() == "1") || (row["Banned"].ToString().ToLower() == "true"))
                    {
                        model.Banned = true;
                    }
                    else
                    {
                        model.Banned = false;
                    }
                }
                if (row["Mod"] != null && row["Mod"].ToString() != "")
                {
                    if ((row["Mod"].ToString() == "1") || (row["Mod"].ToString().ToLower() == "true"))
                    {
                        model.Mod = true;
                    }
                    else
                    {
                        model.Mod = false;
                    }
                }
            }
            return model;
        }

    }
}
