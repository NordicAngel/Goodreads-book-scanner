using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using RestfulLibrary.Model;

namespace RestfulOpenLibrary.Managers
{
    public class ListManager
    {
        private const string _connectionString = @"Data Source=(localdb)\\ProjectsV13;Initial Catalog=RestTestDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public int AddList(string name)
        {
            string sql = "INSERT INTO List_Names values(@name)";
            string sql2 = "SELECT ID, List_Name FROM List_Names WHERE List_Name = @name";
            using(SqlConnection connection = new SqlConnection(_connectionString))
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
        private List_Names ReadList(SqlDataReader reader)
        {
            List_Names list = new List_Names();

            list.ID = reader.GetInt32(0);
            list.List_Name = reader.GetString(1);

            return list;
        }
    }
}
