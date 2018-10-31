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
    public class DiscussDal
    {
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public int Insert(Discuss model)
        {
            string sql = "insert into [dbo].[T_Discuss](UserName, BulletinId, Content, State) values(@UserName, @BulletinId, @Content, @State )";
            SqlParameter[] pms = {
                new SqlParameter("@UserName",SqlDbType.VarChar,16) {Value=model.UserName },
                new SqlParameter("@BulletinId",SqlDbType.Int) {Value=model.BulletinId },
                new SqlParameter("@Content",SqlDbType.NVarChar,128) {Value=model.Content },
                new SqlParameter("@State",SqlDbType.NVarChar,6) {Value=model.State },
            };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public List<Discuss> GetAll() {
            string sql = "select * from [dbo].[T_Discuss] order by [DiscussTime] desc";
           return Table2List(SqlHelper.ExecuteDataTable(sql, CommandType.Text));
        }


        /// <summary>
        /// 获取审核通过的所有信息
        /// </summary>
        /// <returns></returns>
        public List<Discuss> GetAllByBulletinId(int bulletinId)
        {
            string sql = "select * from [dbo].[T_Discuss] where State='审核通过' and BulletinId=@bulletinId  order by [DiscussTime] desc";
            SqlParameter pm = new SqlParameter("@bulletinId", SqlDbType.Int) { Value=bulletinId};
            return Table2List(SqlHelper.ExecuteDataTable(sql, CommandType.Text,pm));
        }

        /// <summary>
        /// 根据id 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            string sql = "delete [dbo].[T_Discuss] where Id=@id";
            SqlParameter pm = new SqlParameter("@id", SqlDbType.Int) { Value = id };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pm);
        }

        /// <summary>
        /// table 转 list 集合
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        private List<Discuss> Table2List(DataTable tb)
        {
            List<Discuss> list = new List<Discuss>();
            foreach (DataRow dr in tb.Rows)
            {
                list.Add(DataRowToModel(dr));
            }
            return list;
        }


        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Discuss DataRowToModel(DataRow row)
        {
            Discuss model = new Discuss();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["UserName"] != null)
                {
                    model.UserName = row["UserName"].ToString();
                }
                if (row["BulletinId"] != null && row["BulletinId"].ToString() != "")
                {
                    model.BulletinId = int.Parse(row["BulletinId"].ToString());
                }
                if (row["Content"] != null)
                {
                    model.Content = row["Content"].ToString();
                }
                if (row["DiscussTime"] != null && row["DiscussTime"].ToString() != "")
                {
                    model.DiscussTime = DateTime.Parse(row["DiscussTime"].ToString());
                }
                if (row["State"] != null)
                {
                    model.State = row["State"].ToString();
                }
            }
            return model;
        }
    }
}
