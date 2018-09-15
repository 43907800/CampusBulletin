using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Bulletin
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int TypeId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime ReleaseTime { get; set; }
        public int Clicks { get; set; }
        public string State { get; set; }
        public UserInfo User { get; set; }
        public BulletinType Type { get; set; }
    }
}
