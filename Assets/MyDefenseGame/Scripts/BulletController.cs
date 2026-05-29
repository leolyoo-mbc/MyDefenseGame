using UnityEngine;

/// <summary>
/// 탄환을 관리하는 클래스
/// </summary>
public class BulletController : MonoBehaviour
{
    #region Variables
    public Transform target;
    [SerializeField] private float _bulletSpeed = 70f;
    #endregion

    #region Unity Event Method
    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 targetPosition = target.position;
        Vector3 dir = targetPosition - transform.position;//타겟으로의 방향

        float moveDistance = _bulletSpeed * Time.deltaTime;//이번 프레임에 원래 이동할 거리
        float distanceToTarget = Vector3.Distance(targetPosition, bullet.transform.position);

        //이번 프레임에 이동 시 타겟을 지나칠 예정이면 타겟에 도착한 것으로 판정
        if (moveDistance >= distanceToTarget)
        {
            //타겟 및 탄환 제거 및 반복문 탈출
            Debug.Log("Hit Target!!!");
            Destroy(bullet);
            if (target != null) Destroy(target.gameObject);//제거 직전 널체크
            break;
        }

        bullet.transform.Translate(moveDistance * dir.normalized, Space.World);
    }
    #endregion
}
