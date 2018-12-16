using System.Collections.Generic;

namespace Trees.Models
{
    /// <summary>
    /// 트리(Tree) 모델 클래스: TreeModel, TreeViewModel, .. 
    /// </summary>
    public class Tree
    {
        /// <summary>
        /// 일련번호+고유키
        /// </summary>
        public int TreeId { get; set; }

        /// <summary>
        /// 보여지는 순서
        /// </summary>
        public int TreeOrder { get; set; }

        /// <summary>
        /// 부모 트리 번호: 최상위(0)
        /// </summary>
        public int ParentId { get; set; } = 0;

        /// <summary>
        /// 트리 이름
        /// </summary>
        public string TreeName { get; set; }

        /// <summary>
        /// 선택할 때 이동 경로
        /// </summary>
        public string TreePath { get; set; }

        /// <summary>
        /// 트리 표시 여부
        /// </summary>
        public bool IsVisible { get; set; } = true;

        /// <summary>
        /// 커뮤니티 번호(CommunityId, ApplicationId, PortalId, ...)
        /// </summary>
        public int CommunityId { get; set; } = 0;

        /// <summary>
        /// 게시판 링크에 대한 여부
        /// </summary>
        public bool IsBoard { get; set; }

        /// <summary>
        /// 링크의 target 속성 설정 값
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// 게시판 별칭
        /// </summary>
        public string BoardAlias { get; set; }

        /// <summary>
        /// 자식 트리 리스트
        /// </summary>
        public List<Tree> Trees { get; set; } = new List<Tree>();

        /// <summary>
        /// ToString 메서드 오버라이드
        /// </summary>
        /// <returns>TreeName</returns>
        public override string ToString()
        {
            return TreeName;
        }
    }
}
