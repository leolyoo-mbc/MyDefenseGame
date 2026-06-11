using System;
using UnityEngine;

namespace MyDefenseGame
{
    public class GameData
    {
        #region Variables
        public static int money = 400;
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
        public static int lives = 10;
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
        public static int rounds = 0;
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
    }
}
