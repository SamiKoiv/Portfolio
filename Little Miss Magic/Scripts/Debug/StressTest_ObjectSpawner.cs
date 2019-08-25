using UnityEngine;

namespace StressTest
{
    public class StressTest_ObjectSpawner : MonoBehaviour
    {
        public Setup TestWith;
        public GameObject Prefab1;
        public GameObject Prefab2;
        public float Spacing;
        [Space(10)]
        public int X;
        public int Y;
        [Space(10)]
        public int Total;

        public enum Setup
        {
            Prefab1,
            Prefab2
        }

        void Start()
        {
            switch (TestWith)
            {
                case Setup.Prefab1:

                    for (int y = 0; y < Y; y++)
                    {
                        for (int x = 0; x < X; x++)
                        {
                            Instantiate(Prefab1, new Vector3(x * Spacing, 0, y * Spacing), transform.rotation);
                        }
                    }

                    break;

                case Setup.Prefab2:

                    for (int y = 0; y < Y; y++)
                    {
                        for (int x = 0; x < X; x++)
                        {
                            Instantiate(Prefab2, new Vector3(x * Spacing, 0, y * Spacing), transform.rotation);
                        }
                    }

                    break;
            }
        }

        private void OnValidate()
        {
            Total = X * Y;
        }
    }
}