using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] EventInt collection;
    [SerializeField] int amount;

    [Header("Optional")]
    [SerializeField] GameObject parentGO;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collection.Value += amount;
            Despawn();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collection.Value += amount;
            Despawn();
        }
    }

    void Despawn()
    {
        if (parentGO != null)
        {
            parentGO.SetActive(false);
            return;
        }

        gameObject.SetActive(false);
    }
}
