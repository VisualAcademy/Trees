using System.Collections.Generic;

namespace Trees.Models
{
    /// <summary>
    /// 트리 메뉴 저장소에 대한 리포지토리 인터페이스 
    /// </summary>
    public interface ITreeRepository
    {
        /// <summary>
        /// 트리 데이터 전체 읽어오기: 커뮤니티ID에 해당하는 + 모든 항목 보여줄건지 여부
        /// </summary>
        List<Tree> GetTrees(int communityId = 0, bool isVisible = true);

        /// <summary>
        /// 수정 
        /// </summary>
        void Update(List<Tree> trees);

        /// <summary>
        /// 삭제
        /// </summary>
        void Remove(int treeId);

        /// <summary>
        /// 추가 
        /// </summary>
        Tree Add(Tree model);

        /// <summary>
        /// 특정 부모 요소에 해당하는 트리 리스트 가져오기: 커뮤니티에 따른  
        /// </summary>
        List<Tree> GetTreesByParentId(int parentId = 0, int communityId = 0);

        /// <summary>
        /// 트리 순서 변경
        /// </summary>
        int UpdateTreeOrder(int parentId = 0, int communityId = 0);
    }
}
