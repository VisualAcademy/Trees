using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Trees.Models.Tests
{
    [TestClass]
    public class TreeProviderContainerTest
    {
        [TestMethod]
        public void GetTreesMethodTest()
        {
            // 넘겨주는 인스턴에 따른 개체 생성 
            var inMemory = new TreeProviderContainer(new TreeDataInMemory());

            var trees = inMemory.GetTrees();

            Assert.AreEqual(3, trees.Count); 
        }
    }
}
