using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Transform startTransform;
    public Transform endTransform;
    public float speed;

    void Start()
    {
        transform.position = startTransform.position;
    }

    void Update()
    {
        //// 1. 종점까지의 방향과 거리를 구합니다.
        //Vector3 targetDelta = endTransform.position - transform.position;
        //Vector3 dir = targetDelta.normalized;
        //float distanceToTarget = targetDelta.magnitude; // 남은 거리

        //// 2. 이번 프레임에 원래 이동해야 할 거리를 계산합니다.
        //float moveDistance = speed * Time.deltaTime;

        //// 3. 만약 이동할 거리가 남은 거리보다 크거나 같다면? (과오버하는 상황)
        //if (moveDistance >= distanceToTarget)
        //{
        //    // 정확히 종점에 딱 맞춰 세우고 오브젝트를 파괴합니다.
        //    transform.position = endTransform.position;

        //    Destroy(gameObject);
        //    Debug.Log("종점 도착!!!!");
        //}
        //else
        //{
        //    // 남은 거리가 더 많다면 원래대로 정상 이동합니다.
        //    transform.Translate(dir * moveDistance);
        //}

        // MoveTowards는 (현재위치, 목적지, 이번 프레임에 이동할 최대 거리)를 넣어줍니다.
        transform.position = Vector3.MoveTowards(transform.position, endTransform.position, speed * Time.deltaTime);

        // MoveTowards가 목적지에 정확히 고정시켜주므로 == 비교가 안전해집니다.
        if (transform.position == endTransform.position)
        {
            Destroy(gameObject);
            Debug.Log("종점 도착!!!!");
        }
    }
}
