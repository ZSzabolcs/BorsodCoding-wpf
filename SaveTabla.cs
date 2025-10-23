using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorsodCoding_WPF_Admin
{
    class SaveTabla : Tabla
    {
        public int Id { get; set; }
        public int Points { get; set; }
        public int Level { get; set; }
        public string Language { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }

        public override void GetAllData()
        {
            throw new NotImplementedException();
        }

        public override void InsertAData()
        {
            throw new NotImplementedException();
        }

        public override void DeleteAData()
        {
            throw new NotImplementedException();
        }

        public override void UpdateAData()
        {
            throw new NotImplementedException();
        }
    }
}
