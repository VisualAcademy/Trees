using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Trees.Models
{
    public class TreeDataInSql : TreeBase
    {
        private readonly string _connectionString;

        public TreeDataInSql(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override List<Tree> GetTrees()
        {
            List<Tree> trees = new List<Tree>();

            string sql = "Select * From Trees";

            SqlConnection db = new SqlConnection(_connectionString);

            trees = db.Query<Tree>(sql).ToList();
                       
            return GetTreeData(trees, 0); 
        }

        private List<Tree> GetTreeData(List<Tree> trees, int parentId)
        {
            List<Tree> lst = new List<Tree>();

            var q =
                from m in trees
                where m.ParentId == parentId
                orderby m.TreeOrder
                select new Tree
                {
                    TreeId = m.TreeId,
                    TreeOrder = m.TreeOrder,
                    ParentId = m.ParentId,
                    TreeName = m.TreeName,
                    TreePath = m.TreePath,
                    IsVisible = m.IsVisible,

                    Trees = (m.TreeId != parentId)
                        ? GetTreeData(trees, m.TreeId) : new List<Tree>()
                };

            lst = q.ToList();

            return lst;
        }
    }
}
