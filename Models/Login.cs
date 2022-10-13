using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace project_api
{
    public class Login
    {
        //public Int32? id_person { get; set; }      
        public string? password { get; set; }

        internal Database? Db { get; set; }

        public Login()
        {
        }

        internal Login(Database db)
        {
            Db = db;
        }


        public async Task<string> GetPassword(string id_person)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT  password   FROM  person  WHERE  id_person  = @id_person";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id_person",
                DbType = DbType.String,
                Value =  id_person,
            });
            var result = await ReturnPassword(await cmd.ExecuteReaderAsync());
            return result;
        }

        private async Task<string> ReturnPassword(DbDataReader reader)
        {
            var loginPerson = new Login();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var person = new Login(Db)
                    {
                        password = reader.GetString(0)
                    };
                    loginPerson=person;
                }
            }

            return loginPerson.password;
        }    
    }
}