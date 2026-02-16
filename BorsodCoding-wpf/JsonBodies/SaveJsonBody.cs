using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorsodCoding_WPF_Admin.JsonBodies
{
    public class SaveJsonBody : JsonBody
    {
        public SaveJsonBody() : base() { }

        public string? Id { get; set; }
        public string? Name { get; set; }
        public int Points { get; set; }
        public int Level { get; set; }
        public string Language { get; set; }
    }
}
