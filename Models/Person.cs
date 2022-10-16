using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace project_api.Models
{
    public class Person
    {
        public int? id_person { get; set; }
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public string? city { get; set; }
        public int? birth_year { get; set; }
        public double? salary { get; set; }
        public string password { get; set; }


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
            var result = await ReturnAllAsync(await cmd.ExecuteReaderAsync());
            // Console.WriteLine(result);
            return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task<Person> FindOneAsync(int id_person)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM  person  WHERE  id_person  = @id_person";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id_person",
                DbType = DbType.Int32,
                Value = id_person,
            });
            var result = await ReturnAllAsync(await cmd.ExecuteReaderAsync());
            //Console.WriteLine(result.Count);
            if (result.Count > 0)
            {
                return result[0];
            }
            else
            {
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
            cmd.CommandText = @"INSERT INTO person(id_person,firstname,lastname,city,birth_year,salary, password) 
            values(@id_person,@firstname,@lastname,@city,@birth_year,@salary,@password);";
            BindParams(cmd);
            BindId(cmd);
            try
            {
                await cmd.ExecuteNonQueryAsync();
                int lastInsertId =(int)@id_person;
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

            cmd.CommandText = @"UPDATE  person  SET  firstname  = @firstname,  lastname=@lastname, city  = @city, birth_year = @birth_year, salary=@salary WHERE  id_person  = @id_person;";
            BindParams(cmd);
            BindId(cmd);
            Console.WriteLine("id=" + id_person);
            int returnValue = await cmd.ExecuteNonQueryAsync();
            return returnValue;
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM  person  WHERE  id_person  = @id_person;";
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
                        id_person = reader.GetInt32(0),
                        firstname = null,
                        lastname = null,
                        city = null,
                        birth_year = null,
                        salary = null,
                        password = reader.GetString(6),

                    };
                    if (!reader.IsDBNull(1))
                        post.firstname = reader.GetString(1);
                    if (!reader.IsDBNull(2))
                        post.lastname = reader.GetString(2);
                    if (!reader.IsDBNull(3))
                        post.city = reader.GetString(3);
                    if (!reader.IsDBNull(4))
                        post.birth_year = reader.GetInt32(4);
                    if (!reader.IsDBNull(5))
                        post.salary = reader.GetDouble(5);
                    if (!reader.IsDBNull(6))
                        post.password = reader.GetString(6);

                    posts.Add(post);
                }
            }
            return posts;
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id_person",
                DbType = DbType.Int32,
                Value = id_person,
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
                DbType = DbType.Double,
                Value = salary,
            });

             cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@password",
                DbType = DbType.String,
                Value = password,
            });
        }
    }
}