using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Setting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Remark { get; set; }

        public NavInfo ToNavInfo()
        {
            NavInfo nav = new NavInfo();
            nav.Id = this.Id;
            nav.Oder = int.Parse(this.Remark);
            string[] str = this.Value.Split('|');
            nav.Text = str[0];
            nav.Address = str[1];
            nav.Remark = str[2].Trim();
            return nav;
        }
    }
}
