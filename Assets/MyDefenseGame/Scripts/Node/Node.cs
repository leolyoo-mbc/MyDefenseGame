using UnityEngine;

namespace MyDefenseGame
{
    /// <summary>
    /// 적의 웨이포인트를 관리하는 클래스
    /// 이번 노드 다음으로 이동할 정보만 보유
    /// </summary>
    public class Node : MonoBehaviour
    {
        #region Variables
        [Tooltip("이 노드에 도착한 적이 다음으로 이동할 노드입니다.")]
        [SerializeField] private Node _nextNode;

        // C# 최신 문법을 활용해 람다식으로 간결하게 단축
        public Node NextNode => _nextNode;

        // 적 스크립트에서 transform.position을 매번 치지 않도록 캡슐화
        public Vector3 Position => transform.position;
        #endregion

        #region Unity Editor Method
        // 유니티 에디터의 씬(Scene) 창에 실시간으로 이동 경로를 그려줍니다.
        private void OnDrawGizmos()
        {
            // 1. 현재 노드의 위치에 하늘색 구체 그리기
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(transform.position, 0.3f);

            // 2. 다음 노드가 연결되어 있다면 녹색 선으로 이어주기
            if (_nextNode != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, _nextNode.transform.position);

                // (선택) 진행 방향을 알 수 있도록 중간에 작은 구체 하나 더 그리기
                Vector3 direction = (_nextNode.transform.position - transform.position) * 0.25f;
                Gizmos.DrawSphere(transform.position + direction, 0.15f);
            }
        }
        #endregion
    }
}
