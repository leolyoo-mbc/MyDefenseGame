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
        #endregion
    }
}
