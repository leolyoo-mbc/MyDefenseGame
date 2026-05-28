using System;
using UnityEngine;

namespace MySample
{
    /// <summary>
    /// 회전 예제 스크립트
    /// </summary>
    public class RotateTest : MonoBehaviour
    {
        #region Variables
        //private float rotationValue = 0f;
        //[SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private Transform target;
        [SerializeField] private float moveSpeed = 5f;

        #endregion

        #region Unity Event Method
        //private void Start()
        //{
        //    this.transform.rotation = Quaternion.Euler(0f, 90f, 0f);//y축 회전하여 오른쪽 바라보기
        //}
        private void Update()
        {
            //rotationValue += 1f;
            //this.transform.rotation = Quaternion.Euler(0f, rotationValue, 0f);//각도 직접 변경
            //this.transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.up);//Rotate 메서드 활용(자전)
            //this.transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.deltaTime);//RotateAround 메서드 활용(공전)
            Vector3 dir = target.position - this.transform.position;//목표로의 방향
            Quaternion lookRotation = Quaternion.LookRotation(dir);//목표 방향에 해당하는 회전값
            this.transform.rotation = lookRotation;//직접 대입
            //Quaternion qRotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            //Vector3 euler = qRotation.eulerAngles;//Quaternion에서 Euler값 구하기
            //this.transform.rotation = Quaternion.Euler(0f, euler.y, 0f);//y축만 회전시키기
            this.transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward, Space.Self);
        }
        #endregion
    }
}
