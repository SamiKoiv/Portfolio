using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralAnimation_Walk : MonoBehaviour
{

    public bool play;
    [Range (0f, 1f)] public float animationCue;

    public AnimationCurve footPosX;
    public AnimationCurve footPosY;
    public AnimationCurve footPosZ;

    public Transform footL;
    public Transform footR;

    void Awake()
    {

    }

    void Start()
    {

    }

    void Update()
    {
        footL.localPosition = CuePosition(animationCue);


        if (play)
        {
            animationCue += Time.deltaTime;

            if (animationCue > 1)
                animationCue = 0;
        }
    }

    public Vector3 CuePosition(float animationCue)
    {
        Vector3 position = new Vector3(footPosX.Evaluate(animationCue), footPosY.Evaluate(animationCue), footPosZ.Evaluate(animationCue));
        return position;
    }
}
