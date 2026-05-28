using UnityEngine;
using System.Collections;
using NUnit.Framework.Constraints;

public class PrefabTest : MonoBehaviour
{
    #region Variables
    //생성할 프리팹 게임 오브젝트 가져오기
    public GameObject prefab;
    public Transform parent;
    [SerializeField] private float tileInterval = 1.0f;
    //private float countdown = 0f;
    #endregion

    #region Unity Event Method
    private void Start()
    {
        //프리팹 게임오브젝트의 사본 만들기
        //Instantiate(prefab);

        //지정된 위치에 만들기
        //Instantiate(prefab, new Vector3(5.0f, 0.05f, 1f), Quaternion.identity);

        //10*10 간격 5로 배치하기
        //GenerateMapTile(10, 10);

        //랜덤위치에 타일 10개 찍기
        //for (int i = 0; i < 10; i++)
        //{
        //    GenerateRandomMapTile(10, 10);
        //}

        //타이머 초기화
        //countdown = 0f;

        //코루틴 활용 1초마다 랜덤 맵타일 찍기
        print("코루틴 호출 전");
        StartCoroutine(CreateMapTile());
        print("코루틴 호출 완료");
    }

    //private void Update()
    //{
    //    //랜덤위치에 1초마다 타일 찍기
    //    countdown += Time.deltaTime;
    //    if (countdown >= tileInterval)
    //    {
    //        //타이머 기능 실행
    //        GenerateRandomMapTile(10, 10);
    //        //타이머 초기화
    //        countdown = 0f;
    //    }
    //}
    #endregion

    #region Custom Method
    //코루틴 함수: 맵타일 찍기
    IEnumerator CreateMapTile()
    {
        //랜덤타일 찍기
        for (int i = 0; i < 10; i++)
        {
            GenerateRandomMapTile(10, 10);
            print($"{i + 1}번째 타일");
            //1초 지연
            yield return new WaitForSeconds(tileInterval);
        }
    }
    //매개변수로 행 개수, 열 개수를 입력받아 맵타일 찍는 함수
    private void GenerateMapTile(int row, int column)
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Instantiate(prefab, new Vector3(5.0f * j, 0.05f, 5.0f * i), Quaternion.identity, parent);
            }
        }
    }

    //랜덤한 타일 찍기
    private void GenerateRandomMapTile(int row, int column)
    {
        int randRow = Random.Range(0, row);
        int randColumn = Random.Range(0, column);
        Vector3 position = new(5.0f * randColumn, 0.05f, 5.0f * randRow);
        Instantiate(prefab, position, Quaternion.identity, parent);
    }
    #endregion
}
