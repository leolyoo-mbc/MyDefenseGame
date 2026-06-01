using UnityEngine;

namespace MySample
{
    public class TargetTest : MonoBehaviour
    {
        #region Variables
        public int a = 10;
        private int _b;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //필드 초기화
            _b = 30;
        }
        #endregion

        #region Custom Method
        public void SetB(int b)
        {
            _b = b;
        }

        public int GetB()
        {
            return _b;
        }
        #endregion
    }
}
