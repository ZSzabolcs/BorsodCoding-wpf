using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorsodCoding_WPF_Admin.Dtos
{
    internal class loginresponseDto
    {
        public object Value { get; private set; }
        public string Message { get; private set; }
        public string Token { get; private set; }
    }
}
