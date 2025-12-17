using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorsodCoding_WPF_Admin
{
    public class SaveJsonBody
    {
        public SaveJsonBody() { }

        public string Name { get; set; }
        public int Points { get; set; }
        public int Level { get; set; }
        public string Language { get; set; }
    }
}
