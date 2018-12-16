using System;
using System.Collections.Generic;
using System.Linq; 
using System.Xml.Linq;

namespace Trees.Models
{
    public class TreeDataInXml : TreeBase
    {
        private readonly string _connectionString;

        public TreeDataInXml(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override List<Tree> GetTrees()
        {
            // app_Data\\Trees.xml 파일 로드
            XElement xml = XElement.Load(_connectionString);

            return GetTreeData(xml, 0);
        }

        private List<Tree> GetTreeData(XElement xml, int parentId)
        {
            Tree t = new Tree();

            List<Tree> trees = new List<Tree>();

            var xmlTrees =
                from node in xml.Elements(nameof(Tree))
                where Convert.ToInt32(node.Element(nameof(t.ParentId)).Value) 
                    == parentId
                select new Tree
                {
                    TreeId = Convert.ToInt32(node.Element(nameof(t.TreeId)).Value),
                    TreeName = node.Element(nameof(t.TreeName)).Value,
                    // 자식 요소들을 재귀 함수를 사용하여 Trees 속성에 채워 넣음
                    Trees = 
                        (parentId != 
                            Convert.ToInt32(node.Element(nameof(t.TreeId)).Value)) 
                            ? 
                                GetTreeData(xml, 
                                    Convert.ToInt32(
                                        node.Element(nameof(t.TreeId)).Value)) 
                            : 
                                new List<Tree>()
                };

            if (trees != null)
            {
                trees = xmlTrees.ToList(); 
            }
                       
            return trees; 
        }
    }
}
