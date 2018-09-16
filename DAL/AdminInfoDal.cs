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
    public class AdminInfoDal
    {
        /// <summary>
        /// 获取 Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AdminInfo GetModel(AdminInfo model)
        {
            string sql = "select * from T_AdminInfo where UserName=@Name and Password=@Pwd;";
            SqlParameter[] pms = {
                new SqlParameter("@Name",SqlDbType.VarChar,16) { Value=model.UserName},
                new SqlParameter("@Pwd",SqlDbType.VarChar,32) { Value=model.Password}
            };
            DataTable tb = SqlHelper.ExecuteDataTable(sql, CommandType.Text, pms);
            List<AdminInfo> list = DataTableToList(tb);
            if (list.Count > 0) return list[0];
            else return null;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Upadate(AdminInfo model)
        {
            string sql = "update T_AdminInfo set UserName=@UserName,Password=@Password where Id=@Id";
            SqlParameter[] pms = {
                new SqlParameter("@UserName",SqlDbType.VarChar,16) { Value=model.UserName},
                new SqlParameter("@Password",SqlDbType.VarChar,32) { Value=model.Password},
                new SqlParameter("@Id",SqlDbType.Int) { Value=model.Id}
            };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
        }

        /// <summary>
        /// DataTable转 List集合
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        private List<AdminInfo> DataTableToList(DataTable tb)
        {
            List<AdminInfo> list = new List<AdminInfo>();
            foreach (DataRow dr in tb.Rows)
            {
                AdminInfo admin = new AdminInfo();
                admin.Id = int.Parse(dr[0].ToString());
                admin.UserName = dr[1].ToString();
                admin.Password = dr[2].ToString();
                list.Add(admin);
            }
            return list;
        }
    }
}
