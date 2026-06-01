using UnityEngine;

namespace MySample
{
    /// <summary>
    /// 컴포넌트 객체(인스턴스) 가져오기 연습
    /// </summary>
    public class ComponentTest : MonoBehaviour
    {
        #region Variables
        //접근 제한자를 'public' 또는 '[SerializeField] private'로 지정하여 에디터상에서 할당하는 방법
        public GameObject targetGameObject;
        [SerializeField] private Transform _targetTransform;
        public TargetTest cTest;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            ////MonoBehaviour를 상속받은 클래스는 new를 통해 인스턴스를 생성하지 않음
            //TargetTest cTest = new();
            //Debug.Log(cTest.a);

            //TargetTest 스크립트가 붙어 있는 게임오브젝트의 인스턴스를 가져와서 접근
            TargetTest gTest = targetGameObject.GetComponent<TargetTest>();
            Debug.Log(gTest.a);
            gTest.SetB(50);
            Debug.Log(gTest.GetB());

            //TargetTest 스크립트가 붙어 있는 트랜스폼의 인스턴스를 가져와서 접근
            TargetTest tTest = _targetTransform.GetComponent<TargetTest>();
            Debug.Log(tTest.a);
            tTest.SetB(70);
            Debug.Log(tTest.GetB());

            //TargetTest 스크립트가 붙어 있는 게임오브젝트에서 직접 TargetTest 클래스의 인스턴스에 접근
            Debug.Log(cTest.a);
            cTest.SetB(70);
            Debug.Log(cTest.GetB());

            //cTest.gameObject
            //cTest.transform
            //cTest.gameObject.GetComponent
            //cTest.transform.GetComponent

            //ComponentTest 스크립트와 같은 오브젝트에 함께 부착되어 있는 TargetTest 클래스의 인스턴스에 접근
            //TargetTest aTest = this.gameObject.GetComponent<TargetTest>();
            //TargetTest bTest = this.transform.GetComponent<TargetTest>();
            //TargetTest eTest = this.GetComponent<TargetTest>();

        }
        #endregion
    }
}
