using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RegionBll
    {
        RegionDal dal = new RegionDal();
        /// <summary>
        /// 根据 Pid 查询
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public List<Region> getListByPid(int pid)
        {
            return dal.getListByPid(pid);
        }
    }
}
