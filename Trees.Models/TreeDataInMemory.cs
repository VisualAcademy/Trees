using System.Collections.Generic;

namespace Trees.Models
{
    public class TreeDataInMemory : TreeBase
    {
        public override List<Tree> GetTrees()
        {
            // 상위 메뉴 등록
            List<Tree> trees = new List<Tree>() {
                new Tree { TreeId = 1, TreeName = "책" },
                new Tree { TreeId = 2, TreeName = "강의" },
                new Tree { TreeId = 3, TreeName = "컴퓨터" }
            };

            // 서브 메뉴 등록
            Tree tree;

            tree = new Tree();
            tree.TreeId = 4;
            tree.TreeName = "좋은 책";
            tree.ParentId = 1;
            trees.Find(m => m.TreeId == 1).Trees.Add(tree);

            tree = new Tree();
            tree.TreeId = 5;
            tree.TreeName = "나쁜 책";
            tree.ParentId = 1;
            trees.Find(m => m.TreeId == 1).Trees.Add(tree);

            tree = new Tree();
            tree.TreeId = 6;
            tree.TreeName = "오프라인 강의";
            tree.ParentId = 2;
            trees.Find(m => m.TreeId == 2).Trees.Add(tree);

            tree = new Tree();
            tree.TreeId = 7;
            tree.TreeName = "온라인 강의";
            tree.ParentId = 2;
            trees.Find(m => m.TreeId == 2).Trees.Add(tree);

            tree = new Tree();
            tree.TreeId = 8;
            tree.TreeName = "데스크톱";
            tree.ParentId = 3;
            trees.Find(m => m.TreeId == 3).Trees.Add(tree);

            tree = new Tree();
            tree.TreeId = 9;
            tree.TreeName = "노트북";
            tree.ParentId = 3;
            trees.Find(m => m.TreeId == 3).Trees.Add(tree);

            return trees;
        }
    }
}
