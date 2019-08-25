using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LerpInPlace : ManagedBehaviour_Update
{
    [SerializeField] Float_ReadOnly globalLerpSpeed;

    new RectTransform transform;

    public Vector2 InactivePosition;
    public Vector2 ActivePosition;
    public float Speed;
    float prog;

    private void Awake()
    {
        transform = GetComponent<RectTransform>();
    }

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
        prog += Time.deltaTime * Speed;
        transform.position = Vector2.Lerp(InactivePosition, ActivePosition, prog);
    }
}
