using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

namespace Trees.Models.Tests
{
    [TestClass]
    public class TreeTest
    {
        [TestMethod]
        public void TreeModelTest()
        {
            // 단일 개체
            Tree tree = new Tree();
            tree.TreeId = 1;
            tree.TreeName = "책";
            tree.TreePath = "/Books/";
            Console.WriteLine($"{tree.TreeId} - {tree.TreeName}");

            // 컬렉션 개체
            List<Tree> trees = new List<Tree>()
            {
                new Tree { TreeId = 1, TreeName = "책" },
                new Tree { TreeId = 2, TreeName = "강의" },
                new Tree { TreeId = 3, TreeName = "컴퓨터" }
            };
            foreach (var t in trees)
            {
                Console.WriteLine($"{t.TreeId} - {t.TreeName}");
            }
        }
    }
}
