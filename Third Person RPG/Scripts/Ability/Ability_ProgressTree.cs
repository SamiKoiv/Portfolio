using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Ability_ProgressTree : MonoBehaviour
{

    Transform textTransform;
    public Ability ability;

    Text text;

    void Awake()
    {
        textTransform = transform.Find("Text");
        text = textTransform.GetComponent<Text>();
    }

    void Start()
    {
        text.text = ability.abilityName;
    }
}
