using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class BulletinBll
    {
        BulletinDal dal = new BulletinDal();
        BulletinTypeBll typeBll = new BulletinTypeBll();

        /// <summary>
        /// 添加 数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(Bulletin model) {
           return dal.Insert(model)>0;
        }


        /// <summary>
        /// 根据id 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Bulletin GetModelById(int id) {
            GetModelById(id, 0);
            return dal.GetModelById(id);
            
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(Bulletin model)
        {
            
            return dal.Update(model)>0;
        }
        /// <summary>
        /// 根据id 获取一条数据 点击量加一
        /// </summary>
        /// <param name="id"></param>
        /// <param name="o">给任意值 点击量加一</param>
        /// <returns></returns>
        public Bulletin GetModelById(int id,object o)
        {
            if (dal.CicksAutoById(id) == 0)
                return null;
            return dal.GetModelById(id);

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
        /// 获取数据 总条数
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return dal.GetCount();
        }

        /// <summary>
        /// 获取 分类后 数据 总条数
        /// </summary>
        /// <returns></returns>
        public int GetCount(int typeId)
        {
            return dal.GetCount(typeId);
        }

        /// <summary>
        /// 获取指定范围数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<Bulletin> GetPageList(int pageIndex, int pageSize)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            List<Bulletin>list= dal.GetPageList(start, end);
            foreach (var i in list)
            {
                i.Type= typeBll.GetModelById(i.TypeId);
            }
            return list;
        }

        /// <summary>
        /// 根据类型 获取 指定范围数据   
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<Bulletin> GetPageListByTypeId(int pageIndex, int pageSize,int typeId)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            List<Bulletin> list = dal.GetPageListByTypeId(start, end, typeId);
            //foreach (var i in list)
            //{
            //    i.Type = typeBll.GetModelById(i.TypeId);
            //}
            return list;
        }
    }
}
