using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorsodCoding_WPF_Admin.JsonBodies
{
    public class UserJsonBody : JsonBody
    {
        public UserJsonBody() : base() { }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
