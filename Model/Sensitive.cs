using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Sensitive
    {
        public int Id { get; set; }
        public string SensitiveText { get; set; }
        public bool Banned { get; set; }
        public bool Mod { get; set; }

    }
}
