using BorsodCoding_WPF_Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorsodCoding_WPF_Admin.Dtos
{
    public class UserDto
    {
        public UserDto()  { }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
