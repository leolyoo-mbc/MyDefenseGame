using UnityEngine;

namespace MySample
{
    public class SingletonMono : MonoBehaviour
    {
        private static SingletonMono _instance;

        public static SingletonMono Instance
        {
            get
            {
                if (_instance == null)
                {
                    // 1. 씬에 이미 배치되어 있는지 확인
                    _instance = FindFirstObjectByType<SingletonMono>();

                    // 2. 씬에 없다면, 빈 오브젝트를 하나 만들어서 컴포넌트를 붙여줌 (자동 생성)
                    if (_instance == null)
                    {
                        GameObject go = new(typeof(SingletonMono).Name);
                        _instance = go.AddComponent<SingletonMono>();
                    }
                }
                return _instance;
            }
        }

        public int number = 10;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;

                // 씬이 바뀌어도 파괴되지 않도록 설정 (선택 사항)
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                // 이미 인스턴스가 존재한다면 중복된 것은 파괴
                if (_instance != this)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}