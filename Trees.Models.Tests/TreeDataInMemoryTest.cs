using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Trees.Models.Tests
{
    [TestClass]
    public class TreeDataInMemoryTest
    {
        [TestMethod]
        public void GetTreesMethodTest()
        {
            var trees = (new TreeDataInMemory()).GetTrees();

            foreach (var tree in trees)
            {
                Console.WriteLine($"{tree.TreeId} - {tree.TreeName}");
            }

            Assert.AreEqual(3, trees.Count);
        }
    }
}
