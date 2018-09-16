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
    public class AdminInfoBll
    {
        AdminInfoDal dal = new AdminInfoDal();
        /// <summary>
        /// 登陆
        /// </summary>
        public AdminInfo Login(string userName, string pwd)
        {
            AdminInfo model = new AdminInfo();
            model.UserName = userName;
            model.Password =Md5.Md5String( pwd);//MD5加密
            //根据UserName Pwd  查询用户是否存在  存在则返回该实体对象
            return dal.GetModel(model);
        }

        /// <summary>
        /// 更新一条数据 model Id 不能为空
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(AdminInfo model)
        {
            //密码加密处理
            model.Password = Md5.Md5String(model.Password);
            return dal.Upadate(model)>0;
        } 
    }
}
