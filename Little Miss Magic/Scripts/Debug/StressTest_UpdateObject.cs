using UnityEngine;

namespace StressTest
{
    public class StressTest_UpdateObject : MonoBehaviour
    {
        void Update()
        {
            transform.Rotate(Vector3.up * 60 * Time.deltaTime);
        }
    }
}
