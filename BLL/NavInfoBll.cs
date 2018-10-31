using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NavInfoBll
    {
        SettingBll settingBll = new SettingBll();

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="nav"></param>
        /// <returns></returns>
        public bool Add(NavInfo nav)
        {
            CacheHelper.Reomve("navInfo");
            return settingBll.Add(nav.ToSetting());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="nav"></param>
        /// <returns></returns>
        public bool Update(NavInfo nav)
        {
            CacheHelper.Reomve("navInfo");
            return settingBll.Update(nav.ToSetting())>0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Remove(int id)
        {
            CacheHelper.Reomve("navInfo");
            return settingBll.Delete(id)>0;
        }

        /// <summary>
        /// 获取指定范围数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<NavInfo> GetPageList(int pageIndex, int pageSize)
        {
            List<NavInfo> navList = new List<NavInfo>();
            List<Setting> list = settingBll.GetNavPageList(pageIndex, pageSize);
            foreach (var item in list)
            {
                navList.Add(item.ToNavInfo());
            }
            return navList;
        }

        /// <summary>
        /// 获取所有导航信息
        /// </summary>
        /// <returns></returns>
        public List<NavInfo> GetAllNav()
        {
            object obj= CacheHelper.Get("navInfo");
            List<NavInfo> navList = null;
            if (obj==null)
            {
                navList = GetSettingToNavList();
                CacheHelper.Set("navInfo", navList);
            }
            else
            {
                navList = obj as List<NavInfo>;
            }
            return navList;
        }

        /// <summary>
        /// 配置list 转成 导航 对象
        /// </summary>
        /// <returns></returns>
        private List<NavInfo> GetSettingToNavList()
        {
            List<NavInfo> navList = new List<NavInfo>();
            List<Setting> list = settingBll.GetAllNavList();
            foreach (var item in list)
            {
                navList.Add(item.ToNavInfo());
            }
            return navList;
        }
        /// <summary>
        /// 获取导航信息总条数
        /// </summary>
        /// <returns></returns>
        public int GetNavCount()
        {
            return settingBll.GetNavCount();
        }






        /// <summary>
        /// 根据Id获取一个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NavInfo GetModelById(int id)
        {
           return settingBll.GetModelById(id).ToNavInfo();
        }

        /// <summary>
        /// 交换位置
        /// </summary>
        /// <param name="list"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void Swap(List<NavInfo> list, int i, int j)
        {
            NavInfo temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }


       private int Partition(List<NavInfo> list, int left, int right)  // 划分函数
        {
            NavInfo pivot = list[right];               // 这里每次都选择最后一个元素作为基准
            int tail = left - 1;                // tail为小于基准的子数组最后一个元素的索引
            for (int i = left; i < right; i++)  // 遍历基准以外的其他元素
            {
                if (list[i].Oder <= pivot.Oder)   // 把小于等于基准的元素放到前一个子数组末尾
                {
                    Swap(list, ++tail, i);
                }
            }
            Swap(list, tail + 1, right); 
            // 最后把基准放到前一个子数组的后边，剩下的子数组既是大于基准的子数组
            // 该操作很有可能把后面元素的稳定性打乱，所以快速排序是不稳定的排序算法
            return tail + 1;    // 返回基准的索引
        }

        /// <summary>
        /// 按照  oder 快速排序 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        private void SortByOder(List<NavInfo> list, int left, int right)
        {
            if (left >= right)
                return;
            int pivot_index = Partition(list, left, right); // 基准的索引
            SortByOder(list, left, pivot_index - 1);
            SortByOder(list, pivot_index + 1, right);
        }


    }
}
