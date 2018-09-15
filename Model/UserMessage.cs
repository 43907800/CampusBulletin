using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public  class UserMessage
    {
        public int Id { get; set; }
        public int BulletinId { get; set; }
        public string UserName { get; set; }
        public string State { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string Remark { get; set; }
        public UserInfo User { get; set; }

    }
}
