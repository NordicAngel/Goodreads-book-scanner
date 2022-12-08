using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using RestfulLibrary.Model;

namespace RestfulOpenLibrary.Managers
{
    public class ListManager: IListManager<List_Names>
    {
        
        public ListManager(string connectionstring)
        {
            _connectionString = connectionstring;
        }
        private readonly string _connectionString;
        public List_Names AddList(string name)
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

                return new List_Names() { ID = lists.Max(l => l.ID), List_Name = name };
            }
        }

       
        public List_Names DeleteList(int id)
        {
            List_Names list = GetById(id);
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

            return list;
        }



        public List_Names GetById(int id)
        {
            string sql = "Select * FROM List_Names WHERE ID = @id";
            
            using SqlConnection connection = new SqlConnection(_connectionString);
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();

                return ReadList(reader);
            }
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
    }
}
