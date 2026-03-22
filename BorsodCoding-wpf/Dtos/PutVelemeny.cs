using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorsodCoding_WPF_Admin.Dtos
{
    internal class PutVelemeny
    {
        public string Id { get; set; }
        public string Ertekeles { get; set; }
        public string Megjegyzes { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} Ertekeles: {Ertekeles} Megjegyzes: {Megjegyzes}";
        }
    }

}
