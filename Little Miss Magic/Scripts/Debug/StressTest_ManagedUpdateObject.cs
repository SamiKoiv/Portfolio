using UnityEngine;

namespace StressTest
{
    public class StressTest_ManagedUpdateObject : ManagedBehaviour_Update
    {
        private void OnEnable()
        {
            Subscribe_Update();
        }

        private void OnDisable()
        {
            Unsubscribe_Update();
        }

        public override void M_Update()
        {
            transform.Rotate(Vector3.up * 60 * Time.deltaTime);
        }
    }
}
