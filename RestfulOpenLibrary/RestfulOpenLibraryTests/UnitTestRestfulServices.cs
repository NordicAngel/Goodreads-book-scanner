using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestfulLibrary.Model;
using RestfulOpenLibrary.Managers;

namespace RestfulOpenLibraryTests
{
    [TestClass]
    public class UnitTestRestfulServices
    {
        [TestMethod]
        public void CreateListTest()
        {
            //arrange
            ListManager mgr = new ListManager();
            int countBefore = mgr.GetAll().Count;


            //act
            mgr = new ListManager();
            int id = mgr.AddList("Dummy List");

            //assert
            mgr = new ListManager();
            Assert.AreEqual(mgr.GetAll().Count(), countBefore);
            mgr.DeleteList(id);
        }

        

        [TestMethod]
        public void DeleteListTest()
        {
            //arrange
            ListManager mgr = new ListManager();
            List<List_Names> listbefore = mgr.GetAll();
            

            //act
            int id = 4;
            mgr.DeleteList(id);

            //assert
            List<List_Names> listafter = mgr.GetAll();
            Assert.AreNotEqual(listbefore.Count(), listafter.Count());



        }


    }
}