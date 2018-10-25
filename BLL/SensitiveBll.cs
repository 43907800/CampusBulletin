using Common;
using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        /// 检查 禁用词
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool CheckBanned(string str)
        {
            object obj = CacheHelper.Get("banned");
            List<string> list = null;
            if (obj==null)
            {
                list = dal.GetAllBanned();
                CacheHelper.Set("banned", list);
            }
            else
            {
                list= obj as List<string>;
            }
            string regex = string.Join("|", list.ToArray());
            return Regex.IsMatch(str,regex);
        }

        
        public bool CheckMod(string str)
        {
            object obj = CacheHelper.Get("mod");
            List<string> list = null;
            if (obj == null)
            {
                list = dal.GetAllMod();
                CacheHelper.Set("mod", list);
            }
            else
            {
                list = obj as List<string>;
            }
            string regex = string.Join("|", list.ToArray());
            regex = regex.Replace(@"\", @"\\").Replace("{2}", ".{0,2}");
            return Regex.IsMatch(str, regex);
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
