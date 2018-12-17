using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Trees.Models
{
    /// <summary>
    /// 트리 메뉴 저장소에 대한 리포지토리 클래스 
    /// </summary>
    public class TreeRepository : ITreeRepository
    {
        private readonly string _connectionString;
        private readonly IDbConnection db;

        public TreeRepository(string connectionString)
        {
            _connectionString = connectionString;
            db = new SqlConnection(_connectionString);
        }

        /// <summary>
        /// 트리 메뉴 추가 
        /// </summary>
        public Tree Add(Tree model)
        {
            string sql = @"
                Insert Into Trees
                (
                    [TreeOrder], 
                    [ParentId], 
                    [TreeName], 
                    [TreePath], 
                    [IsVisible], 
                    [CommunityId], 
                    [IsBoard], 
                    [Target],
                    [BoardAlias]
                )
                Values
                (
                    @TreeOrder, 
                    @ParentId, 
                    @TreeName, 
                    @TreePath, 
                    @IsVisible, 
                    @CommunityId, 
                    @IsBoard, 
                    @Target,
                    @BoardAlias
                );

                Select Cast(SCOPE_IDENTITY() As Int);
            ";
            var treeId = db.Query<int>(sql, model).SingleOrDefault();
            model.TreeId = treeId;
            return model;
        }

        public List<Tree> GetTrees(int communityId = 0, bool isVisible = true)
        {
            List<Tree> trees = new List<Tree>();

            string sql = " Select * From Trees Where 1 = 1 ";

            // 특정 Community에 해당하는 트리 메뉴만 읽어오기 
            if (communityId != 0)
            {
                sql += " And CommunityId = " + communityId.ToString() + " ";
            }

            // IsVisible 속성이 참(1)인 트리 메뉴만 읽어오기 
            if (isVisible)
            {
                sql += " And IsVisible = 1 ";
            }

            sql += " Order By TreeOrder Asc, TreeId Asc "; 

            SqlConnection db = new SqlConnection(_connectionString);

            trees = db.Query<Tree>(sql).ToList();

            return GetTreeData(trees, 0);
        }

        /// <summary>
        /// 트리 관리자 페이지에서 전체 트리 리스트 출력
        /// </summary>
        public List<Tree> GetAll(int communityId = 0, bool isVisible = true)
        {
            List<Tree> trees = new List<Tree>();

            string sql = " Select * From Trees Where 1 = 1 ";

            // 특정 Community에 해당하는 트리 메뉴만 읽어오기 
            if (communityId != 0)
            {
                sql += " And CommunityId = " + communityId.ToString() + " ";
            }

            sql += " Order By TreeOrder Asc, TreeId Asc ";

            SqlConnection db = new SqlConnection(_connectionString);

            trees = db.Query<Tree>(sql).ToList();

            return trees;
        }
               
        /// <summary>
        /// 트리 메뉴 데이터를 트리 구조로 읽어오기 
        /// </summary>
        private List<Tree> GetTreeData(List<Tree> trees, int parentId)
        {
            List<Tree> lstTrees = new List<Tree>();

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
                    CommunityId = m.CommunityId,
                    IsBoard = m.IsBoard,
                    Target = m.Target,
                    BoardAlias = m.BoardAlias,
                    Trees = (m.TreeId != parentId)
                        ? GetTreeData(trees, m.TreeId) : new List<Tree>()
                };

            lstTrees = q.ToList();

            return lstTrees;
        }

        /// <summary>
        /// 특정 트리 메뉴의 하위 메뉴들 가져오기 
        /// </summary>
        public List<Tree> GetTreesByParentId(int parentId = 0, int communityId = 0)
        {
            string sql =
                "Select * From Trees Where 1 = 1 ";

            // 특정 부모에 해당하는 트리 메뉴만 읽어오기 
            if (parentId != 0)
            {
                sql += " And ParentId = " + parentId.ToString() + " ";
            }

            // 특정 Community에 해당하는 트리 메뉴만 읽어오기 
            if (communityId != 0)
            {
                sql += " And ComminityId = " + communityId.ToString() + " ";
            }

            return db.Query<Tree>(sql).ToList();
        }

        /// <summary>
        /// 트리 메뉴 삭제
        /// </summary>
        public void Remove(int treeId)
        {
            string query = "Delete Trees Where TreeId = @TreeId";
            db.Execute(query, new { TreeId = treeId });
        }

        public void Update(List<Tree> trees)
        {
            foreach (var model in trees)
            {
                var sql =
                    " Update Trees                  " +
                    " Set                            " +
                    "    TreeName = @TreeName,   " +
                    "    TreeOrder = @TreeOrder,   " +
                    "    TreePath = @TreePath,   " +
                    "    IsVisible = @IsVisible,   " +
                    "    IsBoard = @IsBoard,   " +
                    "    Target = @Target,   " +
                    "    BoardAlias = @BoardAlias " +
                    " Where TreeId = @TreeId ";
                db.Execute(sql, model);
            }
        }

        public int UpdateTreeOrder(int parentId = 0, int communityId = 0)
        {
            string sql = "Select * From Trees";
            int treeOrder = 0;

            if (parentId == 0)
            {
                // 기본은 상위 메뉴로 가정
                sql = @"
                    Select 
                        IsNull(Max(TreeOrder), 0) As MaxTreeOrder
                    From Trees 
                    Where CommunityId = @CommunityId;
                ";
                treeOrder = db.Query<int>(sql,
                    new { CommunityId = communityId }).SingleOrDefault();
            }
            else
            {
                // 서브 메뉴
                sql = @"
                    Declare @TreeOrder Int;
                    Select @TreeOrder = TreeOrder
                    From Trees 
                    Where TreeId = @ParentId;

                    Update Trees
                    Set
                        TreeOrder = TreeOrder + 1
                    Where 
                        CommunityId = @CommunityId
                        And
                        TreeId > @TreeOrder;

                    Select @TreeOrder;
                ";
                treeOrder = db.Query<int>(sql,
                    new
                    {
                        ParentId = parentId,
                        CommunityId = communityId
                    }).SingleOrDefault();
            }

            return (treeOrder + 1);
        }
    }
}
