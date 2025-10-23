using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorsodCoding_WPF_Admin
{
    abstract class Tabla
    {
        protected Tabla() 
        {
        
        }

        public abstract void GetAllData();

        public abstract void DeleteAData();

        public abstract void InsertAData();

        public abstract void UpdateAData();

    }
}
