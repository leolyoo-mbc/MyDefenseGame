using UnityEngine;

namespace MySample
{
    public class SingletonClass
    {
        //Singleton 클래스의 인스턴스(객체) 담을 정적(static) 변수
        private static SingletonClass _instance;

        //public한 속성으로 private한 instance에 전역적으로 접근하기
        public static SingletonClass Instance
        {
            get
            {
                if (_instance == null)
                {
                    //인스턴스 생성
                    _instance = new SingletonClass();
                }
                return _instance;
            }
        }

        //public한 메서드로 private한 instance에 전역적으로 접근하기
        //public static SingletonClass GetInstance()
        //{
        //    if (_instance == null)
        //    {
        //        //인스턴스 생성
        //        _instance = new SingletonClass();
        //    }
        //    return _instance;
        //}

        //필드 접근: 인스턴스이름.number ==> Instance.number
        public int number;
    }
}
