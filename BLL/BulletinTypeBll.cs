using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BulletinTypeBll
    {
        BulletinTypeDal dal = new BulletinTypeDal();
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Add(BulletinType t)
        {
            return dal.Insert(t)>0;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Update(BulletinType t)
        {
            return dal.Update(t)>0;
        }

        /// <summary>
        /// 根据id 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            return dal.Delete(id)>0;
        }

        /// <summary>
        /// 根据Id获取一个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BulletinType GetModelById(int id)
        {
            return dal.GetModelById(id);
        }

        /// <summary>
        /// 获取指定范围数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<BulletinType> GetPageList(int pageIndex, int pageSize)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            return dal.GetNavPageList(start, end);
        }

        /// <summary>
        /// 获取总条数
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return dal.GetCount();
        }
    }
}
