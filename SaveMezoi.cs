﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorsodCoding_WPF_Admin
{
    internal class SaveMezoi
    {
        public SaveMezoi() { }
        public int Points { get; set; }
        public int Level { get; set; }
        public string Language { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
    }
}
