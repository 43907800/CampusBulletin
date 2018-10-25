using Common;
using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserBll
    {
        UserDal dal = new UserDal();
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(UserInfo model)
        {
            model.Password= Md5.Md5String(model.Password);
            return dal.Insert(model) > 0;
        }



        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(UserInfo model)
        {
            if (model.Password.Length<32)
                model.Password= Md5.Md5String(model.Password);
            return dal.UpdateByUserName(model) > 0;
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

        /// <summary>
        /// 根据Id获取一个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserInfo GetModelById(int id)
        {
            return dal.GetModelById(id);
        }

        /// <summary>
        /// 根据UserName获取一个实体
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public UserInfo GetModelByUserName(string UserName)
        {
            return dal.GetModelByUserName(UserName);
        }

        /// <summary>
        /// 获取指定范围数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<UserInfo> GetPageList(int pageIndex, int pageSize)
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
        /// <summary>
        /// 用户是否存在
        /// </summary>
        /// <returns></returns>
        public bool ExistByUserName(string userName) {
           return dal.CountByUserName(userName) > 0;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public UserInfo Login(string userName ,string pwd) {
            if (pwd.Length<32) pwd = Md5.Md5String(pwd);
            return dal.Login(userName, pwd);
        }
    }
}
