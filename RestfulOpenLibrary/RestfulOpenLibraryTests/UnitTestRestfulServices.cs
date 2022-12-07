using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestfulLibrary.Model;


namespace RestfulOpenLibraryTests
{
    [TestClass]
    public class UnitTestRestfulServices
    {
        [TestMethod]
        public void CreateListTest()
        {
            //arrange
            int countBefore = GetAll().count();

            //act
            int id = AddList("Dummy List");

            //assert
            Assert.AreEqual(GetAll().count(), countBefore);
            DeleteList(id);
        }

        [TestMethod]
        public void DeleteListTest()
        {
            //arrange
            List<List_Names> listbefore = GetAll();
            

            //act
            int id = 4;
            DeleteList(id);

            //assert
            List<List_Names> listafter = GetAll();
            Assert.AreNotEqual(listbefore.Count(), listafter.Count());



        }


    }
}