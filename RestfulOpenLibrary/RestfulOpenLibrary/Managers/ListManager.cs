using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using RestfulLibrary.Model;

namespace RestfulOpenLibrary.Managers
{
    public class ListManager: IListManager<List_Names>
    {
        private const string _connectionString = @"Data Source=(localdb)\\ProjectsV13;Initial Catalog=RestTestDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public int AddList(string name)
        {
            string sql = "INSERT INTO List_Names values(@name)";
            string sql2 = "SELECT ID, List_Name FROM List_Names WHERE List_Name = @name";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@name", name);

                int rows = cmd.ExecuteNonQuery();

                if (rows != 1)
                    throw new ArgumentException("Can't add list");


                SqlCommand cmd2 = new SqlCommand(sql2, connection);

                cmd2.Parameters.AddWithValue("@name", name);

                SqlDataReader reader = cmd2.ExecuteReader();

                List<List_Names> lists = new List<List_Names>();

                while (reader.Read())
                {
                    List_Names list = ReadList(reader);
                    lists.Add(list);

                }

                return lists.Max(l => l.ID);
            }
        }

        



        public int DeleteList(int id)
        {
            string sql = "DELETE FROM List_Names WHERE ID = @id";

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("id", id);

                int rows = cmd.ExecuteNonQuery();

                if (rows != 1)
                    throw new ArgumentException("Can't delete list");

            }

            return id;
        }



        public int GetByID(int id)
        {
            string sql = "Select FROM List_Names WHERE ID =@id";
            
            using SqlConnection connection = new SqlConnection(_connectionString);
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("id", id);

                int rows = cmd.ExecuteNonQuery();

                if (rows != 1)
                    throw new ArgumentException("Cant find list");
            }

            return id;
        }

        public List<List_Names> GetAll()
        {
            List<List_Names> lists = new List<List_Names>();
            string sql = "SELECT * FROM List_Names";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    List_Names list = ReadList(reader);
                    lists.Add(list);
                }

                return lists;
            }
        }

        private List_Names ReadList(SqlDataReader reader)
        {
            List_Names list = new List_Names();

            list.ID = reader.GetInt32(0);
            list.List_Name = reader.GetString(1);

            return list;
        }

        public List_Names Addlist(List_Names List_Names)
        {
            throw new NotImplementedException();
        }

        public List_Names Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List_Names GetBýId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
