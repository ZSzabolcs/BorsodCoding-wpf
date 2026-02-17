using BorsodCoding_WPF_Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorsodCoding_WPF_Admin.Dtos
{
    public class PutSaveDto
    {
        public PutSaveDto() { }

        public string Id { get; set; }
        public int Points { get; set; }
        public int Level { get; set; }
        public string Language { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}; Points: {Points}; Level: {Level}; Language: {Language}";
        }
    }
}
