using System.Collections.Generic;

namespace Trees.Models
{
    /// <summary>
    /// 컨테이너 클래스
    /// - InMemory, XML, SQL의 데이터를 주입해서 각각의 모든 트리 리스트 반환
    /// </summary>
    public class TreeProviderContainer
    {
        private readonly TreeBase _repository; // _provider, _injector, ...

        // 생성자의 매개 변수로 넘어온 형식의 인스턴스를 사용
        public TreeProviderContainer(TreeBase provider)
        {
            _repository = provider;
        }

        public List<Tree> GetTrees()
        {
            return _repository.GetTrees(); 
        }
    }
}
