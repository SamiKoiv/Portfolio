using UnityEngine;

public abstract class UIAnimation : MonoBehaviour
{
    static protected bool animationPlaying;
    public abstract void Play();
    public abstract void Cancel();
}
