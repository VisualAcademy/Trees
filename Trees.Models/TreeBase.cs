using System.Collections.Generic;

namespace Trees.Models
{
    /// <summary>
    /// 트리 관리자의 부모 클래스 => 추상 클래스로 구현 => 나중에 인터페이스로 구현
    /// </summary>
    public abstract class TreeBase
    {
        public abstract List<Tree> GetTrees(); 
    }
}
