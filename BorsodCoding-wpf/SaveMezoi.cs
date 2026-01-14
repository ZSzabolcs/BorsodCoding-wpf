using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorsodCoding_WPF_Admin
{
    internal class SaveMezoi : Mezo
    {
        public SaveMezoi() { }
        public string Id { get; set; }
        public int Points { get; set; }
        public int Level { get; set; }
        public string Language { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime ModDate { get; set; }

    }
}
