using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class NavInfo
    {
        public int Id { get; set; }
        public int Oder { get; set; }
        public string Text { get; set; }
        public string Address { get; set; }
        public string Remark { get; set; }
        public Setting ToSetting()
        {
            Setting s = new Setting();
            s.Id = this.Id;
            s.Remark = this.Oder.ToString();
            s.Name = "NavInfo";
            s.Value = this.Text.Trim() + "|" + this.Address.Trim() + "| " + this.Remark;
            return s;
        }


    }
}
