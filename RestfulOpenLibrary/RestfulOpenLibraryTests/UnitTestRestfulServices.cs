using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestfulLibrary.Model;
using RestfulOpenLibrary.Managers;

namespace RestfulOpenLibraryTests
{
    [TestClass]
    public class UnitTestRestfulServices
    {
        private const string connectionstring = "Server=tcp:datamatiker-daniel.database.windows.net,1433;Initial Catalog=OpenLibrary;Persist Security Info=False;User ID=DanielAdmin;Password=AdminDaniel1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        [TestMethod]
        public void CreateListTest()
        {
            //arrange
            ListManager mgr = new ListManager(connectionstring);
            int countBefore = mgr.GetAll().Count;


            //act
            
            int id = mgr.AddList("Dummy List").ID;

            //assert
            Assert.AreEqual(countBefore + 1, mgr.GetAll().Count());
            mgr.DeleteList(id);
        }

        

        [TestMethod]
        public void DeleteListTest()
        {
            //arrange
            ListManager mgr = new ListManager(connectionstring);
            mgr.AddList("test");
            List<List_Names> listbefore = mgr.GetAll();
            

            //act
            
            mgr.DeleteList(listbefore[^1].ID);
            List<List_Names> listafter = mgr.GetAll();
            //assert
            

            Assert.AreEqual(listbefore.Count - 1, listafter.Count);



        }


    }
}