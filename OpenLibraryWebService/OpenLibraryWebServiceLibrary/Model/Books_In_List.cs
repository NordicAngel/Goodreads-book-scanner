using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibraryWebServiceLibrary.Model
{
    public class Books_In_List
    {
        public int ID { get; set; }
        public int List_ID { get; set; }
        public int ISBN { get; set; }

        public Books_In_List()
        {

        }

        public Books_In_List(int id, int listID, int isbn)
        {
            ID = id;
            List_ID = listID;
            ISBN = isbn;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
