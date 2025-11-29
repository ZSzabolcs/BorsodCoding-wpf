using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorsodCoding_WPF_Admin
{
    internal class UserMezoi : Mezo
    {
        public UserMezoi() { }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime ModDate { get; set; }
    }
}
