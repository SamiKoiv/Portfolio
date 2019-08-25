using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerProfileNS;

/// <summary>
/// 
/// This was a nice and elegant idea but died due to following:
/// Nested and serialized classes reset when main script is edited.
/// This causes saving parameters in this manner extremely unstable.
/// Too bad. :/
/// 
/// </summary>

public class PlayerProfile : ScriptableObject
{
    public P_Camera camera;
    public P_Movement movement;
    public P_Gravity gravity;
    public P_EdgeCheck edgeCheck;
    public P_Jump jump;
    public P_Dash dash;
    public P_Attack attack;
    public P_Attack attackHeavy;
    public P_Attack attackFeint;
    public P_Animation animation;
}

namespace PlayerProfileNS
{
    [System.Serializable]
    public class P_Camera
    {
        [Header("Default camera positioning")]
        public Vector3 defaultPositionOffset;
        public Vector3 defaultRotationOffset;

        [Header("Zoomed camera positioning")]
        public Vector3 zoomedPositionOffset;
        public Vector3 zoomedRotationOffset;

        [Header("Camera follow and collision handling")]
        public float followPosSpeed;
        public float followRotSpeed;
        public float distanceFromCollision;

        [Header("Controls for looking around")]
        public float TurningSpeed;
        public float StickSensitivity;
        public float MouseSensitivity;

        [Header("Limits for turning the camera")]
        public float VerticalRotationMax;
        public float VerticalRotationMin;

        [Header("Debug")]
        public bool StopRotation;
        public bool ZoomedIn;
    }

    [System.Serializable]
    public class P_Movement
    {
        [Header("Basic Movement")]
        public float movementSpeed;
        public float strafingSpeed;
        public float turningLerp;
        public float attackTurnModifier;
        public float speedLerp;
    }

    [System.Serializable]
    public class P_Gravity
    {
        public float gravityMultiplier;
    }

    [System.Serializable]
    public class P_EdgeCheck
    {
        public bool check;
        public float checkPointForward;
        public float checkDepth;
    }

    [System.Serializable]
    public class P_Jump
    {
        public float gravityMultiplier;
        public float jumpForce;
        public float jumpTimeForInput;
        public float maxJumps;
        public bool jumpDuringDash;
    }

    [System.Serializable]
    public class P_Dash
    {
        public float dashDuration;
        public float dashMaxSpeed;
        public float maxSpeedWhileStrafing;
        public AnimationCurve dashSpeedCurve;
        public bool airborneDash;
        public bool backwardDash;
    }

    [System.Serializable]
    public class P_Attack
    {
        [Header("Basic Parameters")]
        public float damage = 5;
        [Tooltip("Length from transform center for OverlapCapsule")]
        public float attackDistance = 2;
        [Tooltip("Radius for OverlapCapsule")]
        public float attackRadius = 0.5f;
        public float cooldown = 0;

        [Header("Attack Timing")]
        [Tooltip("Delay before starting the movement")]
        public float moveDelay;
        [Tooltip("Time for movement which leads to Attack Frame")]
        public float moveTimePre;
        [Tooltip("Time for movement which follows Attack Frame")]
        public float moveTimePost;
        [Tooltip("Delay before releasing active state")]
        public float endDelay;

        [Header("Movement")]

        [Tooltip("Movement before Attack Frame")]
        public Vector3 movementPre;
        [Tooltip("Movement after Attack Frame")]
        public Vector3 movementPost;
    }

    [System.Serializable]
    public class P_Animation
    {
        public string SpeedName;
    }
}
