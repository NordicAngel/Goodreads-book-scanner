using Microsoft.Data.SqlClient;
using OpenLibraryWebServiceLibrary.Model;

namespace OpenLibraryWebService.Managers
{
    public class BookManager : IBookManager<Books_In_List>
    {
        private const string connectionstring = "Server=tcp:datamatiker-daniel.database.windows.net,1433;Initial Catalog=OpenLibrary;Persist Security Info=False;User ID=DanielAdmin;Password=AdminDaniel1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public Books_In_List Create(Books_In_List item)
        {
            string sql = "INSERT INTO Books_In_List1 values(@List_ID, @ISBN)";
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@List_ID", item.List_ID);
                cmd.Parameters.AddWithValue("@ISBN", item.ISBN);

                int rows = cmd.ExecuteNonQuery();

                if (rows != 1)
                {
                    throw new ArgumentException("Can't Create Book");
                }

                return item;

            }
        }


        public List<Books_In_List> GetAll()
        {
            List<Books_In_List> liste = new List<Books_In_List>();
            string sql = "SELECT * FROM Books_In_List1";


            using (SqlConnection connection = new SqlConnection(connectionstring))
            {

                connection.Open();


                SqlCommand cmd = new SqlCommand(sql, connection);


                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    Books_In_List list = ReadList(reader);
                    liste.Add(list);
                }

            }
            return liste;
        }

        public List<Books_In_List> GetByID(int id)
        {
            string sql = "SELECT * FROM Books_In_List1 where List_ID=@ID";

            List<Books_In_List> list = new List<Books_In_List> ();
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    Books_In_List BookNames = ReadList(reader);
                    list.Add(BookNames);
                    
                }
            }
            return list;
            
        }

        private Books_In_List ReadList(SqlDataReader reader)
        {
            Books_In_List BookNames = new Books_In_List();

            BookNames.ID = reader.GetInt32(0);
            BookNames.List_ID = reader.GetInt32(1);
            BookNames.ISBN = reader.GetInt64(2);

            return BookNames;
        }
    }
}
