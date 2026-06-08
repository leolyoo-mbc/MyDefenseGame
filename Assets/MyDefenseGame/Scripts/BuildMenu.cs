using MyDefenseGame;
using UnityEngine;

namespace MyDefenseGame
{

    public class BuildMenu : MonoBehaviour
    {
        #region Variables
        [SerializeField] private TowerBlueprint _machineGunTower;
        [SerializeField] private TowerBlueprint _missileLauncherTower;
        #endregion

        #region Custom Method
        public void OnSelectTowerMachineGun()
        {
            SelectTower(_machineGunTower);
            Debug.Log("머신건 타워를 선택 하였습니다!!");

        }

        public void OnSelectTowerMissileLauncher()
        {
            SelectTower(_missileLauncherTower);
            Debug.Log("미사일 런처 타워 선택 하였습니다!!");
        }

        private void SelectTower(TowerBlueprint blueprint)
        {
            BuildManager.Instance.SelectTower(blueprint);
        }
        #endregion
    }
}
