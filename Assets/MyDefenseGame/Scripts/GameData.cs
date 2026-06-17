using System;
using UnityEngine;

namespace MyDefenseGame
{
    public class GameData
    {
        #region Variables
        public static int money;
        public static event Action OnMoneyChanged;
        public static int Money
        {
            get => money;
            set
            {
                money = value;
                // 돈이 바뀌면 등록된 UI들에게 "지금이야! 갱신해!"라고 알림
                OnMoneyChanged?.Invoke();
            }
        }

        // 2. 목숨 (라이프) 데이터 - 초기값 10
        public static int lives;
        public static event Action OnLivesChanged;
        public static int Lives
        {
            get => lives;
            set
            {
                lives = value;
                OnLivesChanged?.Invoke();
            }
        }

        // 3. 라운드 (웨이브) 데이터 - 초기값 0
        public static int rounds;
        public static event Action OnRoundsChanged;
        public static int Rounds
        {
            get => rounds;
            set
            {
                rounds = value;
                OnRoundsChanged?.Invoke();
            }
        }
        #endregion

        #region Custom Method
        public static void ResetData()
        {
            Money = 400; // 프로퍼티를 통해 호출해야 이벤트가 발생하여 UI도 갱신됩니다.
            Lives = 10;
            Rounds = 0;
        }
        #endregion
    }
}
