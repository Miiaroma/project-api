using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace project_api
{
    public class Hour
    {
        public int? id_hour { get; set; }
        public int? id_project { get; set; }        
        public int? id_person { get; set; }
        public int? work_hour { get; set; }
       

        internal Database? Db { get; set; }

        public Hour()
        {
        }

        internal Hour(Database db)
        {
            Db = db;
        }

        public async Task<List<Hour>> GetAllAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM  hour ;";
            var result=await ReturnAllAsync(await cmd.ExecuteReaderAsync());
           // Console.WriteLine(result);
            return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task<Hour> FindOneAsync(int id_hour)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM  hour  WHERE  id_hour  = @id_hour";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id_hour",
                DbType = DbType.Int32,
                Value = id_hour,
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
            cmd.CommandText = @"DELETE FROM hour";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }
    

        public async Task<int> InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText=@"insert into hour(id_project,id_person,work_hour) 
            values(@id_project,@id_person,@work_hour);";
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
            
            cmd.CommandText = @"UPDATE  hour  SET  id_project  = @id_project,  id_person  = @id_person, work_hour = @work_hour WHERE  id_hour  = @id_hour;";
            BindParams(cmd);
            BindId(cmd);
            //Console.WriteLine("id="+id_hour);
            int returnValue=await cmd.ExecuteNonQueryAsync();
            return returnValue;
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM  hour  WHERE  id_hour = @id_hour;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private async Task<List<Hour>> ReturnAllAsync(DbDataReader reader)
        {
            var posts = new List<Hour>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Hour(Db)
                    {
                        id_hour = reader.GetInt32(0),
                        id_project = reader.GetInt32(1),
                        id_person = reader.GetInt32(2),                        
                        work_hour = null
                    };
                    if (!reader.IsDBNull(3))
                        post.work_hour = reader.GetInt32(3);                    
                    posts.Add(post);
                }
            }
            return posts;
        }
        
        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id_hour",
                DbType = DbType.Int32,
                Value = id_hour,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id_project",
                DbType = DbType.Int32,
                Value = id_project,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id_person",
                DbType = DbType.Int32,
                Value = id_person,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@work_hour",
                DbType = DbType.Int32,
                Value = work_hour,
            });           
        }
    }
}