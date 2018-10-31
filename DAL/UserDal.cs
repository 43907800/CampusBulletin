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
     public class UserDal
    {

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(UserInfo model)
        {
            string sql = " insert into [dbo].[T_UserInfo](UserName, Password, Name, Sex, Mail, Region, Introduce, HeadPicAddress, isDisable) values(@userName, @pwd, @name, @sex,@mail,@region,@introduce,@headPicAddr,@isDisable); ";
            SqlParameter[] pms = {
                new SqlParameter("@userName",SqlDbType.VarChar,16) {Value=model.UserName },
                new SqlParameter("@pwd",SqlDbType.VarChar,32) {Value=model.Password },
                new SqlParameter("@name",SqlDbType.NVarChar,16) {Value=model.Name==null?DBNull.Value:(object)model.Name },
                new SqlParameter("@sex",SqlDbType.Bit) {Value=model.Sex },
                new SqlParameter("@mail",SqlDbType.VarChar,32) {Value=model.Mail },
                new SqlParameter("@region",SqlDbType.NVarChar,32) {Value=model.Region==null?DBNull.Value:(object)model.Region },
                 new SqlParameter("@introduce",SqlDbType.NVarChar,64) {Value=model.Introduce==null?DBNull.Value:(object)model.Introduce },
                 new SqlParameter("@headPicAddr",SqlDbType.VarChar,64) {Value=model.HeadPicAddress==null?DBNull.Value:(object)model.HeadPicAddress },
                 new SqlParameter("@isDisable",SqlDbType.Bit) {Value=model.isDisable }
            };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public int UpdateByUserName(UserInfo model)
        {
            string sql = "update [dbo].[T_UserInfo] set  Password=@pwd, Name =@name,Sex=@sex, Mail=@mail, Region=@region, Introduce=@introduce,  isDisable=@isDisable where UserName=@userName";
            SqlParameter[] pms = {
                new SqlParameter("@userName",SqlDbType.VarChar,16) {Value=model.UserName },
                new SqlParameter("@pwd",SqlDbType.VarChar,32) {Value=model.Password },
                new SqlParameter("@name",SqlDbType.NVarChar,16) {Value=model.Name==null?DBNull.Value:(object)model.Name },
                new SqlParameter("@sex",SqlDbType.Bit) {Value=model.Sex },
                new SqlParameter("@mail",SqlDbType.VarChar,32) {Value=model.Mail },
                new SqlParameter("@region",SqlDbType.NVarChar,32) {Value=model.Region==null?DBNull.Value:(object)model.Region },
                 new SqlParameter("@introduce",SqlDbType.NVarChar,64) {Value=model.Introduce==null?DBNull.Value:(object)model.Introduce },
                 new SqlParameter("@isDisable",SqlDbType.Bit) {Value=model.isDisable }
            };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
        }

        /// <summary>
        /// 更新 用户头像地址
        /// </summary>
        /// <param name="picAddr"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int UpdateHeadPic(string picAddr,string userName) {
            string sql = "update [dbo].[T_UserInfo] set HeadPicAddress=@HeadPicAddress  where UserName=@userName";
            SqlParameter[] pms = { new SqlParameter("@userName", SqlDbType.VarChar, 16) { Value = userName },
                new SqlParameter("@HeadPicAddress", SqlDbType.NVarChar, 64) { Value = picAddr}, };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
        }

        /// <summary>
        /// 获取指定范围 的数据
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<UserInfo> GetNavPageList(int start, int end)
        {
            string sql = "select * from( select *,ROW_NUMBER()over(order by Id) as num from [dbo].[T_UserInfo]) as t where t.num >= @start and t.num <= @end";
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
            string sql = " select count(1) from  [dbo].[T_UserInfo]";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql, CommandType.Text));
        }

        /// <summary>
        /// 根据id 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            string sql = "delete [dbo].[T_UserInfo] where Id=@id";
            SqlParameter pm = new SqlParameter("@id", SqlDbType.Int) { Value = id };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pm);
        }


        /// <summary>
        /// 根据Id获取一个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserInfo GetModelById(int id)
        {
            string sql = "select * from  [dbo].[T_UserInfo] where Id=@id";
            SqlParameter pm = new SqlParameter("@id", SqlDbType.Int) { Value = id };
            List<UserInfo> list = Table2List(SqlHelper.ExecuteDataTable(sql, CommandType.Text, pm));
            if (list.Count > 0) return list[0];
            else return null;
        }

        /// <summary>
        /// 根据 userName获取一个实体
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserInfo GetModelByUserName(string userName)
        {
            string sql = "select * from  [dbo].[T_UserInfo] where UserName=@userName";
            SqlParameter pm = new SqlParameter("@userName", SqlDbType.VarChar,16) { Value = userName };
            List<UserInfo> list = Table2List(SqlHelper.ExecuteDataTable(sql, CommandType.Text, pm));
            if (list.Count > 0) return list[0];
            else return null;
        }
        /// <summary>
        ///根据 userName 统计条数
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int CountByUserName(string userName)
        {
            string sql = " select count(1) from  [dbo].[T_UserInfo] where UserName=@userName";
            SqlParameter pm = new SqlParameter("@userName", SqlDbType.VarChar, 16) {Value=userName };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql, CommandType.Text,pm));
        }
        
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserInfo Login(string userName, string pwd)
        {
            string sql = " select * from  [dbo].[T_UserInfo] where UserName=@userName and Password =@pwd";
            SqlParameter[] pms = {
                new SqlParameter("@userName",SqlDbType.VarChar,16) {Value=userName },
                new SqlParameter("@pwd",SqlDbType.VarChar,32) {Value=pwd }
            };
            List<UserInfo> list = Table2List(SqlHelper.ExecuteDataTable(sql, CommandType.Text, pms));
            if (list.Count > 0) return list[0];
            else return null;
        }



        /// <summary>
        /// table 转 list 集合
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        private List<UserInfo> Table2List(DataTable tb)
        {
            List<UserInfo> list = new List<UserInfo>();
            foreach (DataRow dr in tb.Rows)
            {
                list.Add(DataRowToModel(dr));
            }
            return list;
        }

        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public UserInfo DataRowToModel(DataRow row)
        {
           UserInfo model = new UserInfo();
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
                if (row["Password"] != null)
                {
                    model.Password = row["Password"].ToString();
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["Sex"] != null && row["Sex"].ToString() != "")
                {
                    if ((row["Sex"].ToString() == "1") || (row["Sex"].ToString().ToLower() == "true"))
                    {
                        model.Sex = true;
                    }
                    else
                    {
                        model.Sex = false;
                    }
                }
                if (row["Mail"] != null)
                {
                    model.Mail = row["Mail"].ToString();
                }
                if (row["Region"] != null)
                {
                    model.Region = row["Region"].ToString();
                }
                if (row["Introduce"] != null)
                {
                    model.Introduce = row["Introduce"].ToString();
                }
                if (!string.IsNullOrEmpty(row["HeadPicAddress"] .ToString()))
                {
                    model.HeadPicAddress = row["HeadPicAddress"].ToString();
                }
                else
                {
                    model.HeadPicAddress = "/img/HeadPic/Default.png";
                }
                if (row["isDisable"] != null && row["isDisable"].ToString() != "")
                {
                    if ((row["isDisable"].ToString() == "1") || (row["isDisable"].ToString().ToLower() == "true"))
                    {
                        model.isDisable = true;
                    }
                    else
                    {
                        model.isDisable = false;
                    }
                }
            }
            return model;
        }
    }
}
