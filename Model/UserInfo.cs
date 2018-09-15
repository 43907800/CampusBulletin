using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool Sex { get; set; }
        public string Mail { get; set; }
        public string Region { get; set; }
        public string Introduce { get; set; }
        public string HeadPicAddress { get; set; }
        public bool isDisable { get; set; }
    }
}
