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
    public class RegionDal
    {
        /// <summary>
        /// 根据 Pid 查询
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public List<Region> getListByPid(int pid)
        {
            string sql = " select * from [dbo].[T_Region] where Pid=@pid ";
            SqlParameter pm = new SqlParameter("@pid", System.Data.SqlDbType.Int) { Value=pid};
            return Table2List(SqlHelper.ExecuteDataTable(sql, System.Data.CommandType.Text, pm));
        }



        /// <summary>
        /// table 转 list 集合
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        private List<Region> Table2List(DataTable tb)
        {
            List<Region> list = new List<Region>();
            foreach (DataRow dr in tb.Rows)
            {
                list.Add(DataRowToModel(dr));
            }
            return list;
        }

        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Region DataRowToModel(DataRow row)
        {
            Region model = new Region();
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
                if (row["Pid"] != null && row["Pid"].ToString() != "")
                {
                    model.Pid = int.Parse(row["Pid"].ToString());
                }
            }
            return model;
        }
    }
}
