using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace project_api
{
    public class Project
    {
        public int id_project { get; set; }
        public string pname { get; set; }
        public string place { get; set; }
        internal Database Db { get; set; }

        public Project()
        {
        }

        internal Project(Database db)
        {
            Db = db;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM  project;";
            return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task<Project> FindOneAsync(int id_project)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM  course  WHERE  id_project  = @id_project";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id_project",
                DbType = DbType.Int32,
                Value = id_project,
            });
            var result = await ReturnAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }


        public async Task<int> InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO  project  (id_project, pname, place) VALUES (@id_project, @pname, @place);";
            BindId(cmd);
            BindParams(cmd);
            try
            {
                int affected=await cmd.ExecuteNonQueryAsync();
                return affected; 
            }
            catch (System.Exception)
            {   
                return 0;
            } 
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE  course  SET  id_project  = @id_project,  pname  = @pname,  place  = @place WHERE  id_project  = @id_project;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM  course  WHERE  id_project  = @id_project;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private async Task<List<Project>> ReturnAllAsync(DbDataReader reader)
        {
            var posts = new List<Project>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Project(Db)
                    {
                        id_project = reader.GetInt32(0),
                        pname = null,
                        place = null,
                    };
                    if (!reader.IsDBNull(1))
                        post.pname = reader.GetString(1);

                    if (!reader.IsDBNull(2))
                        post.place = reader.GetString(2);
                    posts.Add(post);
                    
                }
            }
            return posts;
        }       

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id_project",
                DbType = DbType.Int32,
                Value = id_project,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@pname",
                DbType = DbType.String,
                Value = pname,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@place",
                DbType = DbType.String,
                Value = place,
            });
        }
    }
}