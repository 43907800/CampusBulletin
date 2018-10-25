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
    public class BulletinDal
    {
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(Bulletin model)
        {
            string sql = "insert into [dbo].[T_Bulletin] values (@UserName, @TypeId, @Title, @Content,default, default, @State)";
            SqlParameter[] pms = {
                   new SqlParameter("@UserName",SqlDbType.VarChar,16) {Value=model.UserName } ,
                    new SqlParameter("@TypeId",SqlDbType.Int) {Value=model.TypeId } ,
                     new SqlParameter("@Title",SqlDbType.NVarChar,64) {Value=model.Title } ,
                      new SqlParameter("@Content",SqlDbType.NVarChar) {Value=model.Content }  ,
                      new SqlParameter("@State",SqlDbType.NVarChar,6) {Value=model.State }
                };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(Bulletin model)
        {
            string sql = "update [dbo].[T_Bulletin] set  UserName=@UserName, TypeId=@TypeId, Title=@Title, Content=@Content, ReleaseTime=default,  State=@State where Id=@id";
            SqlParameter[] pms = {
                   new SqlParameter("@UserName",SqlDbType.VarChar,16) {Value=model.UserName } ,
                    new SqlParameter("@TypeId",SqlDbType.Int) {Value=model.TypeId } ,
                     new SqlParameter("@Title",SqlDbType.NVarChar,64) {Value=model.Title } ,
                      new SqlParameter("@Content",SqlDbType.NVarChar) {Value=model.Content }  ,
                      new SqlParameter("@State",SqlDbType.NVarChar,6) {Value=model.State } ,
                      new SqlParameter("@id",SqlDbType.Int) {Value=model.Id }
                };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
        }

        /// <summary>
        /// 访问量加一
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int CicksAutoById(int id) {
            string sql = "update [dbo].[T_Bulletin] set Clicks=Clicks+1 where Id=@id";
            SqlParameter pm = new SqlParameter("@id", SqlDbType.Int) { Value=id};
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pm);

        }
        /// <summary>
        /// 修改公告状态
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public int UpdataStateById(string state, int id)
        {
            string sql = "update [dbo].[T_Bulletin] set [State]=@state where [Id]=@id";
            SqlParameter[] pms = {
                      new SqlParameter("@State",SqlDbType.NVarChar,6) {Value= state} ,
                      new SqlParameter("@id",SqlDbType.Int) {Value=id }
                };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
        /// <summary>
        /// 根据id 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            string sql = "delete [dbo].[T_Bulletin] where Id=@id";
            SqlParameter pm = new SqlParameter("@id", SqlDbType.Int) { Value = id };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pm);
        }

        /// <summary>
        /// 获取指定范围 的数据
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<Bulletin> GetPageList(int start, int end)
        {
            string sql = "select * from( select *,ROW_NUMBER()over(order by Id) as num from [dbo].[T_Bulletin]) as t where t.num >= @start and t.num <= @end";
            SqlParameter[] pms = {
                new SqlParameter("@start",SqlDbType.Int) { Value=start},
                new SqlParameter("@end",SqlDbType.Int) { Value=end},
            };
            DataTable tb = SqlHelper.ExecuteDataTable(sql, CommandType.Text, pms);
            return Table2List(tb);
        }

        /// <summary>
        /// 获取 信息总条数
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            string sql = "select count(1) from [dbo].[T_Bulletin] ";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql, CommandType.Text));
        }


        /// <summary>
        /// 根据Id获取一个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Bulletin GetModelById(int id)
        {
            string sql = "select * from [dbo].[T_Bulletin] where Id=@id";
            SqlParameter pm = new SqlParameter("@id", SqlDbType.Int) { Value = id };
            List<Bulletin> list = Table2List(SqlHelper.ExecuteDataTable(sql, CommandType.Text, pm));
            if (list.Count > 0) return list[0];
            else return null;
        }

        /// <summary>
        /// table 转 list 集合
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        private List<Bulletin> Table2List(DataTable tb)
        {
            List<Bulletin> list = new List<Bulletin>();
            foreach (DataRow dr in tb.Rows)
            {
                list.Add(DataRowToModel(dr));
            }
            return list;
        }

        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Bulletin DataRowToModel(DataRow row)
        {
            Bulletin model = new Bulletin();
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
                if (row["TypeId"] != null && row["TypeId"].ToString() != "")
                {
                    model.TypeId = int.Parse(row["TypeId"].ToString());
                }
                if (row["Title"] != null)
                {
                    model.Title = row["Title"].ToString();
                }
                if (row["Content"] != null)
                {
                    model.Content = row["Content"].ToString();
                }
                if (row["ReleaseTime"] != null && row["ReleaseTime"].ToString() != "")
                {
                    model.ReleaseTime = DateTime.Parse(row["ReleaseTime"].ToString());
                }
                if (row["Clicks"] != null && row["Clicks"].ToString() != "")
                {
                    model.Clicks = int.Parse(row["Clicks"].ToString());
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
