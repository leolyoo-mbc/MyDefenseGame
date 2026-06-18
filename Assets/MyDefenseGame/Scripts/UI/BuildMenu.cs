using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyDefenseGame
{
    /// <summary>
    /// 타워 데이터와 그에 대응하는 UI 컴포넌트들을 하나의 세트로 묶어주는 클래스
    /// </summary>
    [System.Serializable]
    public class TowerMenuSlot
    {
        public TowerBlueprint blueprint; // 타워 데이터 (ScriptableObject)
        public TMP_Text costText;        // 가격을 표시할 텍스트 컴포넌트
        public Button selectButton;      // 해당 타워를 선택하는 버튼 컴포넌트
    }

    public class BuildMenu : MonoBehaviour
    {
        #region Variables
        [SerializeField] private TowerMenuSlot[] _towerSlots;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //배열에 등록된 모든 타워 슬롯을 순회하며 한 번에 세팅
            foreach (TowerMenuSlot slot in _towerSlots)
            {
                //타워 데이터가 할당되지 않은 빈 슬롯이라면 건너뜀 (안전 장치)
                if (slot.blueprint == null) continue;

                //텍스트 초기화
                if (slot.costText != null)
                {
                    slot.costText.text = $"{slot.blueprint.Cost}";
                }

                //버튼 이벤트 동적 연결 (가장 중요한 부분)
                if (slot.selectButton != null)
                {
                    //기존에 인스펙터 창에서 수동으로 연결했던 OnClick 이벤트를 코드로 자동화
                    //버튼을 누르면 해당 슬롯의 blueprint를 SelectTower 함수로 전달하도록 설정
                    slot.selectButton.onClick.AddListener(() => SelectTowerBlueprint(slot.blueprint));
                }
            }
        }
        #endregion

        #region Custom Method
        private void SelectTowerBlueprint(TowerBlueprint blueprint)
        {
            BuildManager.Instance.SelectTower(blueprint);
            Debug.Log($"{blueprint.Prefab.ToString()} 선택 하였습니다!!");
        }
        #endregion
    }
}
