using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorsodCoding_WPF_Admin
{
    abstract class Tabla
    {
        public string tablaNev = "";
        protected Tabla() { }

        public List<T> GetDataBySQLSelect<T>(MySqlDataReader data) where T : Mezo, new()
        {
            T osztaly = new T();
            List<T> rekordok = new List<T>();

            if (osztaly is UserMezoi)
            {
                while (data.Read())
                {
                    T ujElem = new T();
                    (ujElem as UserMezoi).Id = data.GetInt32("Id");
                    (ujElem as UserMezoi).Name = data.GetString("Name");
                    (ujElem as UserMezoi).Password = data.GetString("Password");
                    (ujElem as UserMezoi).Date = data.GetDateTime("Date");
                    rekordok.Add(ujElem);
                }
            }
            else
            {
                while (data.Read())
                {
                    T ujElem = new T();
                    (ujElem as SaveMezoi).UserId = data.GetInt32("UserId");
                    (ujElem as SaveMezoi).Level = data.GetInt32("Level");
                    (ujElem as SaveMezoi).Language = data.GetString("Language");
                    (ujElem as SaveMezoi).Points = data.GetInt32("Points");
                    (ujElem as SaveMezoi).Date = data.GetDateTime("Date");
                    rekordok.Add(ujElem);

                }
            }
                


            

            return rekordok;
        }

        public abstract void DeleteAData();

        public abstract void InsertAData();

        public abstract void UpdateAData();

    }
}
