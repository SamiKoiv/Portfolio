using UnityEngine;

public class CastFireball : MonoBehaviour, ISpell
{
    public PlayerAssets m_PlayerAssets;
    Fireball fireball;

    public void Cast()
    {
        if (fireball == null)
        {
            fireball = GameObject.Instantiate(m_PlayerAssets.FireBall, transform.position + transform.forward * 1.2f + transform.up, transform.rotation).GetComponent<Fireball>();
        }
        else
        {
            Destroy(fireball.gameObject);
        }

    }

    public void Uncast()
    {
        // Intentionally left blank.
    }
}
