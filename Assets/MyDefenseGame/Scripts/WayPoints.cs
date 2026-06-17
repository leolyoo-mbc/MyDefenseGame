using UnityEngine;

namespace MyDefenseGame
{
    public class WayPoints : MonoBehaviour
    {
        #region Variables
        public Transform[] points;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            points = new Transform[transform.childCount];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = transform.GetChild(i);
            }
        }
        #endregion
    }
}