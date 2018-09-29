using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SensitiveBll
    {
        SensitiveDal dal = new SensitiveDal();

        /// <summary>
        /// 添加一条信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Sensitive model)
        {
            int count = 0;
            //判断数据是否存在 再添加
            if (!dal.Exists(model.SensitiveText)) count = dal.Insert(model);
            return count;
        }

        /// <summary>
        /// 根据Id删除一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Remove(int id)
        {
            return dal.Delete(id)>0;
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int AddList(List<Sensitive> list)
        {
            int count = 0;
            foreach (var model in list)
            {
                count += Add(model);
            }
            return count;
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int AddList(string str)
        {
            List<Sensitive> list = new List<Sensitive>();

            String[] strs = str.Split('\n');
            foreach (var s in strs)
            {
                string[] t = s.Split('=');
                Sensitive temp = new Sensitive();
                try
                {
                    temp.SensitiveText = t[0].Trim();
                    switch (t[1].Trim())
                    {
                        case "{BANNED}":
                            temp.Banned = true;
                            break;
                        case "{MOD}":
                            temp.Mod = true;
                            break;
                    }
                    list.Add(temp);
                }
                catch (Exception)
                {
                }
            }
            return AddList(list);
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
        /// 获取指定范围数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<Sensitive> GetPageList(int pageIndex, int pageSize)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            return dal.GetPageList(start, end);
        }


    }
}
