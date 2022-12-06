using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulLibrary.Model
{
    public class List_Names
    {
        public int ID { get; set; }
        public string List_Name { get; set; }

        public List_Names()
        {

        }

        public List_Names(int id, string listname)
        {
            ID = id;
            List_Name = listname;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
