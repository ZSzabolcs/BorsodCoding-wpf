using BorsodCoding_WPF_Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorsodCoding_WPF_Admin.Dtos
{
    internal class InsertSaveDto
    {
        public InsertSaveDto() { }
        public string Name { get; set; }
        public int Points { get; set; }
        public int Level { get; set; }
        public string Language { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}\nPoints: {Points}\nLevel: {Level}\n Language: {Language}";
        }
    }
}
