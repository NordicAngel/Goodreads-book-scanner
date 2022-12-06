using Microsoft.Data.SqlClient;

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
                
                List<>

                while (reader.Read())
                {

                }

            }
        }
    }
}
