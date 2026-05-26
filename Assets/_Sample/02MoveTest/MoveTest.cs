using Unity.VisualScripting;
using UnityEngine;

namespace MySample
{
    public class MoveTest : MonoBehaviour
    {
        //목표 지점 변수 선언 밎 초기화
        private Vector3 targetPosition = new(10f, 1f, 10f);
        //목표 지점 오브젝트의 트랜스폼 인스턴스 참조
        public Transform target;
        public float speed = 10f;

        private void Start()
        {
            //this.gameObject, gameObject ==> 현재 MoveTest 스크립트가 붙어있는 게임오브젝트의 객체
            //this.gameObject.transform, gameObject.transform ==> 현재 MoveTest 스크립트가 붙어있는 게임오브젝트의 객체의 위치
            //this.transform, transform ==> 현재 MoveTest 스크립트가 붙어있는 게임오브젝트의 객체의 위치
            //transform.position = target.position; //순간이동


        }
        private void Update()
        {
            //플레이어의 위치를 앞으로 이동 ==> Z축 값이 증가
            //transform.position Z축 값이 증가 연산
            //transform.position.z += 1F; //에러: 축 따로 연산 불가
            //transform.position += Vector3.forward; //Vector3의 정적 속성 활용
            //transform.position += speed * Time.deltaTime * Vector3.forward; //동일 시간 동일 거리 이동하도록 Time.deltaTime 활용
            //transform.Translate(speed * Time.deltaTime * Vector3.forward); //Translate 메서드 활용
            //타겟까지 이동
            //이동 방향 ==> 목표지점 - 현재지점
            Vector3 dir = target.position - transform.position;
            transform.Translate(speed * Time.deltaTime * dir.normalized, Space.World);//월드 기준
            //transform.Translate(speed * Time.deltaTime * dir.normalized, Space.Self);//트랜스폼 바라보는 방향 기준
            //target.Translate(speed * Time.deltaTime * Vector3.forward / 2);
        }
    }
}
