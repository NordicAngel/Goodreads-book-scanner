using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}