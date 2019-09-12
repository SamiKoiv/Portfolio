using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SlideElement : UIAnimation
{
    [SerializeField] Vector2 endOffset = Vector2.zero;
    [SerializeField] float speed = 1;

    Vector2 startPosition = Vector2.zero;
    float prog = 0;
    IDisposable cancel;


    public override void Play()
    {
        startPosition = transform.position;
        cancel = Observable.FromCoroutine(PlayAnimation).Subscribe();
    }

    public override void Cancel()
    {
        if (cancel != null)
            cancel.Dispose();
    }

    IEnumerator PlayAnimation()
    {
        while (prog < 1)
        {
            transform.position = Vector2.Lerp(startPosition, startPosition + endOffset, prog);
            prog += Time.deltaTime * speed;
            yield return null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position,
            new Vector2(transform.position.x + endOffset.x, transform.position.y + endOffset.y));
    }
}
