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
        public int Add(NavInfo nav)
        {
            return settingBll.Add(nav.ToSetting());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="nav"></param>
        /// <returns></returns>
        public int Update(NavInfo nav)
        {
            return settingBll.Update(nav.ToSetting());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            return settingBll.Delete(id);
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
        /// 获取导航信息总条数
        /// </summary>
        /// <returns></returns>
        public int GetNavCount()
        {
            return settingBll.GetNavCount();
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
