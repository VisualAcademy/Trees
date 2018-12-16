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
            // ���� ��ü
            Tree tree = new Tree();
            tree.TreeId = 1;
            tree.TreeName = "å";
            tree.TreePath = "/Books/";
            Console.WriteLine($"{tree.TreeId} - {tree.TreeName}");

            // �÷��� ��ü
            List<Tree> trees = new List<Tree>()
            {
                new Tree { TreeId = 1, TreeName = "å" },
                new Tree { TreeId = 2, TreeName = "����" },
                new Tree { TreeId = 3, TreeName = "��ǻ��" }
            };
            foreach (var t in trees)
            {
                Console.WriteLine($"{t.TreeId} - {t.TreeName}");
            }
        }
    }
}
