using CharacterFeatures;
using Rewired;
using System;
using UnityEngine;

public class PlayerController : Character
{
    // CORE
    Core core;

    // PUBLIC
    public int playerID = 0;
    public PlayerProfile playerProfile;
    public Transform Target;

    // INTERNAL
    internal Player player;
    internal PlayerInput playerInput;
    internal CharacterController characterController;
    internal Rigidbody rb;

    internal Vector3 horizontalInput;

    internal bool isGrounded;
    internal bool onEdge;

    internal bool isStrafing;
    internal bool isDashing;
    internal bool isAttacking;

    internal bool animationLock;
    internal bool allowCancel;
    internal bool preventTurn;
    internal bool preventInput;

    // PRIVATE

    InputLibrary inputLibrary = new InputLibrary();
    Attack activeAttack;

    // FEATURES

    Turn turn;
    FaceTarget faceTarget;
    Move move;
    Strafing strafing;
    CustomGravity customGravity;
    GroundedCheck groundedCheck;
    EdgeCheck edgeCheck;
    Jump jump;
    Dash dash;
    Attack attackFast;
    Attack attackHeavy;
    Attack attackFeint;
    AnimationControl animationControl;

    // DEBUG
    [Space(10)]
    public DebugPlayer debug = new DebugPlayer();


    #region Awake, OnEnable, OnDisable, Update

    void Awake()
    {
        core = Core.instance;
        core.SetPlayer(transform);
        playerInput = PlayerInput.instance;

        player = ReInput.players.GetPlayer(playerID);
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

        turn = new Turn(this);
        faceTarget = new FaceTarget(this, inputLibrary.Left_Shoulder);
        move = new Move(this);
        strafing = new Strafing(this, inputLibrary.Left_Trigger);
        customGravity = new CustomGravity(this);
        groundedCheck = new GroundedCheck(this);
        edgeCheck = new EdgeCheck(this);
        jump = new Jump(this, inputLibrary.A);
        dash = new Dash(this, inputLibrary.Right_Shoulder);
        attackFast = new Attack(this, inputLibrary.X, playerProfile.attack);
        attackHeavy = new Attack(this, inputLibrary.Y, playerProfile.attackHeavy);
        attackFeint = new Attack(this, inputLibrary.B, playerProfile.attackFeint);

        // AnimationControl should be instantiated last to enable
        // connections to former components
        //animationControl = new AnimationControl(this, move);
    }

    void Start()
    {
        hp = characterProfile.MaxHP;
    }

    void Update()
    {
        isGrounded = groundedCheck.IsGrounded;
        onEdge = edgeCheck.OnEdge;
        customGravity.Update(isGrounded);

        // Move is to be skipped by appropriate bool status (like animationLock)
        move.Update();
        //animationControl.Update();

        if (AnyAttackActive(out activeAttack))
        {
            activeAttack.Update();

            characterController.Move(
            (
            customGravity.Velocity
            + attackFast.Velocity
            + attackHeavy.Velocity
            + attackFeint.Velocity
            )
            * Time.deltaTime);
        }
        else
        {
            strafing.Update();
            faceTarget.Update();
            turn.Update();
            jump.Update();
            dash.Update();

            attackFast.Update();
            attackHeavy.Update();
            attackFeint.Update();

            characterController.Move(
            (
            move.Velocity
            + jump.Velocity
            + customGravity.Velocity
            + dash.Velocity
            )
            * Time.deltaTime);
        }
    }

    bool AnyAttackActive()
    {
        if (attackFast.Active)
            return true;
        else if (attackHeavy.Active)
            return true;
        else if (attackFeint.Active)
            return true;
        else
            return false;
    }
    bool AnyAttackActive(out Attack active)
    {
        if (attackFast.Active)
        {
            active = attackFast;
            return true;
        }
        else if (attackHeavy.Active)
        {
            active = attackHeavy;
            return true;
        }
        else if (attackFeint.Active)
        {
            active = attackFeint;
            return true;
        }
        else
        {
            active = null;
            return false;
        }
    }

    public void Print(string p)
    {
        print(p);
    }

    #endregion

    void OnDrawGizmos()
    {

        if (debug.debugCombat)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                transform.position + Vector3.up,
                transform.position + Vector3.up + transform.forward * playerProfile.attack.attackDistance);
            Gizmos.DrawSphere(
                transform.position + Vector3.up + transform.forward * playerProfile.attack.attackDistance - transform.forward * playerProfile.attack.attackRadius,
                playerProfile.attack.attackRadius);
        }
    }
}

[Serializable]
public class DebugPlayer
{
    [Header("Master switch")]
    public bool active;

    [Header("Debug by category")]
    public bool debugCombat;
}
