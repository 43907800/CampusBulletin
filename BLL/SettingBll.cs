using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SettingBll
    {
        SettingDal dal = new SettingDal();

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool Add(Setting s)
        {
            return dal.Insert(s)>0;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public int Update(Setting setting)
        {
            return dal.Update(setting);
        }

        /// <summary>
        /// 根据id 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            return dal.Delete(id);
        }


        /// <summary>
        /// 根据Id获取一个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Setting GetModelById(int id)
        {
            return dal.GetModelById(id);
        }

        /// <summary>
        /// 获取指定范围数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<Setting> GetNavPageList(int pageIndex, int pageSize)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            return dal.GetNavPageList(start, end);
        }
        /// <summary>
        /// 获取所有导航信息
        /// </summary>
        /// <returns></returns>
        public List<Setting> GetAllNavList()
        {
            return dal.GetAllNavList();
        }
        /// <summary>
        /// 获取导航信息总条数
        /// </summary>
        /// <returns></returns>
        public int GetNavCount()
        {
            return dal.GetNavCount();
        }
    }

}
