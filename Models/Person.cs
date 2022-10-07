using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace project_api.Models
{
    public class Person
    {
        public int? idperson { get; set; }
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public string? city { get; set; }
        public int? birth_year { get; set; }
        public double? salary { get; set; }
        

        internal Database? Db { get; set; }

        public Person()
        {
        }

        internal Person(Database db)
        {
            Db = db;
        }

        public async Task<List<Person>> GetAllAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM  person ;";
            var result=await ReturnAllAsync(await cmd.ExecuteReaderAsync());
           // Console.WriteLine(result);
            return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task<Person> FindOneAsync(int idperson)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM  User  WHERE  idperson  = @idperson";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@idperson",
                DbType = DbType.Int32,
                Value = idperson,
            });
            var result = await ReturnAllAsync(await cmd.ExecuteReaderAsync());
            //Console.WriteLine(result.Count);
            if(result.Count > 0){
                return result[0];
            }
            else {
                return null;
            }
            //return result.Count > 0 ? result[0] : null;
        }


        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM  person ";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }
    

        public async Task<int> InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText=@"insert into person(firstname,lastname,city,birth_year,salary) 
            values(@firstname,@lastname,@city,@birth_year,@salary);";
            BindParams(cmd);
            try
            {
                await cmd.ExecuteNonQueryAsync();
                int lastInsertId = (int) cmd.LastInsertedId; 
                return lastInsertId;
            }
            catch (System.Exception)
            {   
                return 0;
            } 
        }

        public async Task<int> UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            
            cmd.CommandText = @"UPDATE  user  SET  firstname  = @firstname,  lastname=@lastnam, city  = @city, birth_year = @birth_year, salary=@salary WHERE  idperson  = @idperson;";
            BindParams(cmd);
            BindId(cmd);
            Console.WriteLine("id="+idperson);
            int returnValue=await cmd.ExecuteNonQueryAsync();
            return returnValue;
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM  person  WHERE  idperson  = @idperson;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private async Task<List<Person>> ReturnAllAsync(DbDataReader reader)
        {
            var posts = new List<Person>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Person(Db)
                    {
                        idperson = reader.GetInt32(0),
                        /*firstname = reader.GetString(1),
                        lastname = reader.GetString(2),*/
                        city = reader.GetString(3),
                        birth_year = reader.GetInt32(4),
                        salary = reader.GetInt32(5),
                        firstname = null,
                        lastname = null
                    };
                    if (!reader.IsDBNull(1))
                        post.firstname = reader.GetString(4);

                    if (!reader.IsDBNull(2))
                        post.lastname = reader.GetString(5);
                    posts.Add(post);
                }
            }
            return posts;
        }
        
        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@idperson",
                DbType = DbType.Int32,
                Value = idperson,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@firstname",
                DbType = DbType.String,
                Value = firstname,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@lastname",
                DbType = DbType.String,
                Value = lastname,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@city",
                DbType = DbType.String,
                Value = city,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@birth_year",
                DbType = DbType.Int32,
                Value = birth_year,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@salary",
                DbType = DbType.Int32,
                Value = lastname,
            });
        }
    }
}