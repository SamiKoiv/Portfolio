using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_HP : MonoBehaviour
{
    public CharacterProfile characterProfile;
    public Event_Float playerHP;

    void OnEnable()
    {
        playerHP.Subscribe(ChangeHP);
    }

    void OnDisable()
    {
        playerHP.Unsubscribe(ChangeHP);
    }

    //-------------------------------------------------

    void ChangeHP()
    {
        transform.localScale = new Vector3(playerHP.Value / characterProfile.MaxHP, 1, 1);
    }

}
