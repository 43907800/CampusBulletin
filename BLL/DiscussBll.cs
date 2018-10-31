using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DiscussBll
    {

        DiscussDal dal = new DiscussDal();
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(Discuss model)
        {
            return dal.Insert(model) > 0;
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public List<Discuss> GetAll()
        {
            return dal.GetAll();
        }

        /// <summary>
        /// 获取审核通过的所有信息
        /// </summary>
        /// <returns></returns>
        public List<Discuss> GetAllByBulletinId(int bulletinId)
        {
            return dal.GetAllByBulletinId(bulletinId);
        }

        /// <summary>
        /// 根据id 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            return dal.Delete(id) > 0;
        }
    }
}
