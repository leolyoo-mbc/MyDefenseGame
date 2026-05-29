using UnityEngine;

namespace MySample
{
    public struct CBox
    {
        public float x;//x좌표
        public float y;//y좌표
        public float w;//width
        public float h;//height
    }

    public struct CCricle
    {
        public float x;//x좌표
        public float y;//y좌표
        public float z;//z좌표
        public float r;//반지름
    }

    /// <summary>
    /// 충돌 테스트 예제
    /// </summary>
    public class HitTest : MonoBehaviour
    {
        #region Variables
        public Transform target;
        public float moveSpeed;
        #endregion

        #region Unity Event Method

        #endregion

        #region Custom Method
        //매개변수로 받은 2개의 사각형이 충돌했는지 체크하는 함수
        //충돌여부 bool 반환
        public bool CheckHitBox(CBox a, CBox b)
        {
            float xDistance = (a.x < b.x) ? (b.x - a.x) : (a.x - b.x);
            float yDistance = (a.y < b.y) ? (b.y - a.y) : (a.y - b.y);
            if (xDistance <= (a.w / 2 + b.w / 2) && yDistance <= (a.h / 2 + b.h / 2))
            {
                return true;
            }
            return false;
        }

        //매개변수로 받은 2개의 원이 충돌했는지 체크하는 함수
        //충돌여부 bool 반환
        public bool CheckHitCircle(CCricle a, CCricle b)
        {
            float xDistance = (a.x < b.x) ? (b.x - a.x) : (a.x - b.x);
            float yDistance = (a.y < b.y) ? (b.y - a.y) : (a.y - b.y);
            float distance = Mathf.Sqrt((xDistance * xDistance) + (yDistance * yDistance));//두 원의 중점 거리
            if (distance <= a.r + b.r)
            {
                return true;
            }
            return false;
        }

        //도착 판정으로 충돌 체크
        //두 오브젝트 간의 거리가 일정거리(0.5f) 안에 있으면 충돌이라고 판정
        public bool CheckArrivePosition(Transform target)
        {
            float distance = Vector3.Distance(this.transform.position, target.position);
            if (distance < 0.5f)
            {
                return true;
            }
            return false;
        }

        //이동 시 타겟까지 남은 거리와 이번 프레임에 이동할 거리를 비교하여 충돌 판정
        public bool CheckPassPosition(Transform target)
        {
            float distance = Vector3.Distance(this.transform.position, target.position);//남은 거리
            
            float distanceThisFrame = Time.deltaTime * moveSpeed;//이번 프레임에 이동할 거리

            if (distance <= distanceThisFrame)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}