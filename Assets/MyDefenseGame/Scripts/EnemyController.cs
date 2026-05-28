//EnemyController.cs
using UnityEngine;

namespace MyDefenseGame
{
    /// <summary>
    /// 적(Enemy)을 관리하는 클래스
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        //필드 선언부
        #region Variables
        private Transform _destination;//이동 목적지 트랜스폼
        [SerializeField] private float _speed = 10f;
        #endregion

        //유니티 이벤트 함수 구현부
        #region Unity Event Method
        void Start()
        {
            //이동 목적지 오브젝트 찾기
            _destination = GameObject.FindGameObjectWithTag("End").transform;
        }

        void Update()
        {
            //목적지까지의 방향
            Vector3 dirNormalized = (_destination.position - this.transform.position).normalized;
            //타겟을 향해 이동
            this.transform.Translate(_speed * Time.deltaTime * dirNormalized, Space.World);

            //목적지까지의 거리
            float distanceToDestination = Vector3.Distance(_destination.position, this.transform.position);
            //이번 프레임에 원래 이동해야 할 거리
            float moveDistance = _speed * Time.deltaTime;

            //이동할 거리가 남은 거리보다 크면 도착 판정
            if (moveDistance >= distanceToDestination)
            {
                //도착 위치에 강제 이동시키기
                this.transform.position = _destination.position;
                ArriveTarget();
            }
        }
        #endregion

        //유저 구현 함수
        #region Custom Method
        void ArriveTarget()
        {
            Debug.Log("종점 도착!!!!");
            Destroy(this.gameObject);
        }
        #endregion
    }
}