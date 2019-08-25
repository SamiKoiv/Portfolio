using PlayerProfileNS;
using Rewired;
using System;
using UnityEngine;

namespace CharacterFeatures
{

    public class FeatureContainer
    {

    }

    // Left Analog Stick
    public class Turn
    {
        PlayerController playerController;
        Player player;
        PlayerProfile playerProfile;
        Transform transform;

        InputLibrary inputLibrary = new InputLibrary();

        Camera_Controller camera_Controller;

        Quaternion lookingDirection;
        Quaternion lookingDirection_old;
        Vector3 horizontalInput;
        Vector3 savedInput;

        Vector3 rotation;

        public Turn(PlayerController pc)
        {
            playerController = pc;
            player = playerController.player;
            playerProfile = playerController.playerProfile;
            transform = playerController.transform;
            camera_Controller = Core.instance.CameraController;

            lookingDirection = transform.rotation;
            lookingDirection_old = transform.rotation;
        }

        public void Update()
        {
            if (playerController.preventTurn == false)
            {
                if (playerController.isDashing == false)
                {
                    transform.eulerAngles = Rotation;
                }

                if (camera_Controller.Target != null && playerController.isStrafing)
                {
                    transform.eulerAngles = Rotation;
                }
            }
        }

        public Vector3 Rotation
        {
            get
            {
                if (playerController.isStrafing)
                {
                    // Receive looking direction from input
                    lookingDirection = Quaternion.Lerp(
                        lookingDirection_old,
                        Quaternion.Euler(0, Core.instance.MainCamera.transform.eulerAngles.y, 0),
                        playerProfile.movement.turningLerp * Time.deltaTime);

                    lookingDirection_old = lookingDirection;

                    rotation = lookingDirection.eulerAngles;

                    //// Translate Quaternion to Vector3 for easier modifications
                    //Vector3 eulers = lookingDirection.eulerAngles;
                    //// Get camera angle in Y axis
                    //Vector3 cameraAngle = Vector3.up * Camera.main.transform.eulerAngles.y;
                    //// Align movement with camera Y axis
                    //rotation = eulers + cameraAngle;

                }
                else
                {
                    horizontalInput = new Vector3(
                    player.GetAxis(inputLibrary.Left_Stick_X),
                    0,
                    player.GetAxis(inputLibrary.Left_Stick_Y));


                    if (horizontalInput != Vector3.zero)
                        savedInput = horizontalInput;

                    if (horizontalInput != Vector3.zero)
                    {
                        // Receive looking direction from input
                        lookingDirection = Quaternion.Lerp(
                            lookingDirection_old,
                            Quaternion.LookRotation(savedInput),
                            playerProfile.movement.turningLerp * Time.deltaTime);

                        lookingDirection_old = lookingDirection;

                        // Translate Quaternion to Vector3 for easier modifications
                        Vector3 eulers = lookingDirection.eulerAngles;
                        // Get camera angle in Y axis
                        Vector3 cameraAngle = Vector3.up * Core.instance.MainCamera.transform.eulerAngles.y;
                        // Align movement with camera Y axis
                        rotation = eulers + cameraAngle;
                    }
                }

                return rotation;
            }
        }
    }

    public class FaceTarget
    {
        PlayerController playerController;
        Player player;
        Transform transform;
        string inputPath;

        Camera_Controller cameraController;

        bool following;

        Character target;

        public FaceTarget(PlayerController pc, string inputPath)
        {
            playerController = pc;
            player = playerController.player;
            transform = playerController.transform;
            this.inputPath = inputPath;

            cameraController = Core.instance.CameraController;
        }

        public void Update()
        {
            if (player.GetButtonDown(inputPath))
            {
                if (!following)
                {
                    RaycastHit hit;
                    Physics.Raycast(transform.position + Vector3.up, transform.forward, out hit, 1000);

                    try
                    {
                        if (target = hit.transform.GetComponentInParent<Character>())
                        {
                            cameraController.Target = hit.transform;
                            following = true;
                        }
                    }
                    catch (NullReferenceException)
                    {

                    }
                }
                else
                {
                    following = false;
                    cameraController.Target = null;
                }
            }
            else if (following)
            {
                try
                {
                    if (target.dead)
                    {
                        following = false;
                        cameraController.Target = null;
                    }
                }
                catch (NullReferenceException)
                {
                    following = false;
                    cameraController.Target = null;
                }
            }

        }

    }

    // Left Analog Stick
    public class Move
    {
        PlayerController playerController;
        Player player;
        PlayerProfile playerProfile;
        Transform transform;

        InputLibrary inputLibrary = new InputLibrary();

        Vector3 inputVelocity;
        Vector3 velocity_old;
        float deltaTime;

        Vector3 _velocity;
        float clampedMagnitude;

        public Vector3 Velocity
        {
            get
            {
                return _velocity;
            }
        }

        public Move(PlayerController pc)
        {
            playerController = pc;
            player = playerController.player;
            playerProfile = playerController.playerProfile;
            transform = playerController.transform;
        }

        public void Update()
        {
            if (playerProfile.edgeCheck.check == true)
            {
                if (playerController.onEdge == false)
                {
                    if (playerController.animationLock == false)
                    {
                        Calculate();
                    }
                    else
                    {
                        _velocity = Vector3.zero;
                        velocity_old = Vector3.zero;
                    }
                }
                else
                {
                    _velocity = Vector3.zero;
                    velocity_old = Vector3.zero;
                }
            }
            else
            {
                if (playerController.animationLock == false)
                {
                    Calculate();
                }
                else
                {
                    _velocity = Vector3.zero;
                    velocity_old = Vector3.zero;
                }
            }
        }

        void Calculate()
        {
            deltaTime = Time.deltaTime;

            inputVelocity = new Vector3(
            player.GetAxis(inputLibrary.Left_Stick_X),
            0,
            player.GetAxis(inputLibrary.Left_Stick_Y));

            clampedMagnitude = Mathf.Min(1, inputVelocity.magnitude);
            inputVelocity.Normalize();

            if (inputVelocity.magnitude != 0)    // Check if moving
            {
                // MOVEMENT


                if (playerController.isStrafing)
                {
                    // STRAFE

                    if (inputVelocity.z >= 0)   // Normal movement speed on forward, strafespeed on sideways
                    {
                        _velocity =
                        transform.forward * inputVelocity.z * clampedMagnitude * playerProfile.movement.movementSpeed
                        + transform.right * inputVelocity.x * clampedMagnitude * playerProfile.movement.strafingSpeed;
                    }
                    else    // Strafe speed applied to backward and sideways movement
                    {
                        _velocity =
                        (transform.forward * inputVelocity.z + transform.right * inputVelocity.x)
                        * playerProfile.movement.strafingSpeed;
                    }

                }
                else
                {
                    // TURN TO MOVEMENT DIRECTION
                    _velocity = transform.forward * playerProfile.movement.movementSpeed * clampedMagnitude;
                }

                // Lerp velocity for smoother transitions
                _velocity = Vector3.Lerp(
                velocity_old,
                _velocity,
                playerProfile.movement.speedLerp * deltaTime);
            }
            else
            {
                _velocity = Vector3.zero;

                // Lerp velocity for smoother transitions
                _velocity = Vector3.Lerp(
                velocity_old,
                _velocity,
                playerProfile.movement.speedLerp * deltaTime);
            }

            velocity_old = _velocity;
        }
    }

    public class Strafing
    {
        PlayerController playerController;
        Player player;

        string inputPath;

        public Strafing(PlayerController pc, string inputPath)
        {
            playerController = pc;
            this.inputPath = inputPath;
            player = playerController.player;
        }

        public void Update()
        {
            if (player.GetAxis(inputPath) >= 0.5f
                && playerController.animationLock == false)
            {
                playerController.isStrafing = true;
            }
            else
            {
                playerController.isStrafing = false;
            }
        }
    }

    public class CustomGravity
    {
        PlayerController playerController;
        PlayerProfile playerProfile;
        Vector3 gravity;
        Vector3 _velocity;

        public CustomGravity(PlayerController pc)
        {
            playerController = pc;
            playerProfile = playerController.playerProfile;
            gravity = Physics.gravity;
        }

        public void Update(bool isGrounded)
        {
            if (isGrounded)
            {
                _velocity = gravity * playerProfile.gravity.gravityMultiplier;
            }
            else
            {
                _velocity += gravity * playerProfile.gravity.gravityMultiplier * Time.deltaTime;
            }
        }

        public Vector3 Velocity
        {
            get
            {
                return _velocity;
            }
        }
    }

    public class GroundedCheck
    {

        PlayerController playerController;
        CharacterController characterController;
        Transform transform;

        public GroundedCheck(PlayerController pc)
        {
            playerController = pc;
            characterController = playerController.characterController;
            transform = playerController.transform;
        }

        public bool IsGrounded
        {
            get
            {
                if (Physics.Raycast(transform.position, Vector3.down, 0.1f))
                {
                    return true;
                }
                else if (characterController.isGrounded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    public class EdgeCheck
    {
        PlayerController playerController;
        PlayerProfile playerProfile;
        Transform transform;

        bool _onEdge;

        public bool OnEdge
        {
            get
            {
                _onEdge = !Physics.Raycast(
                transform.position + transform.forward * playerProfile.edgeCheck.checkPointForward,
                Vector3.down,
                playerProfile.edgeCheck.checkDepth);

                return _onEdge;
            }
        }

        public EdgeCheck(PlayerController pc)
        {
            playerController = pc;
            playerProfile = playerController.playerProfile;
            transform = pc.transform;
        }
    }

    public class Jump
    {
        PlayerController playerController;
        Player player;
        PlayerProfile playerProfile;

        string inputPath;

        bool isGrounded;
        float jumpTimer;
        int currentJumps;

        Vector3 _velocity;

        public Vector3 Velocity
        {
            get
            {
                return _velocity;
            }
        }

        public Jump(PlayerController pc, string inputPath)
        {
            playerController = pc;
            this.inputPath = inputPath;
            player = playerController.player;
            playerProfile = playerController.playerProfile;
        }

        public void Update()
        {
            if (player.GetButtonDown(inputPath))
            {
                #region Start Jump

                isGrounded = playerController.isGrounded;

                if (isGrounded)
                {
                    if (playerController.isDashing)
                    {
                        if (playerProfile.jump.jumpDuringDash)
                        {
                            currentJumps = 0;
                            playerController.isGrounded = false;
                        }
                    }
                    else
                    {
                        currentJumps = 0;
                        isGrounded = false;
                        _velocity = Vector3.up * playerProfile.jump.jumpForce * playerProfile.gravity.gravityMultiplier;
                    }
                }

                if (currentJumps < playerProfile.jump.maxJumps)
                {
                    jumpTimer = 0;
                }

                currentJumps += 1;

                #endregion
            }
            else if (player.GetButton(inputPath))
            {
                #region Jumping

                isGrounded = playerController.isGrounded;

                if (currentJumps <= playerProfile.jump.maxJumps && jumpTimer < playerProfile.jump.jumpTimeForInput)
                {
                    _velocity = Vector3.up * playerProfile.jump.jumpForce * playerProfile.gravity.gravityMultiplier;
                    jumpTimer += Time.deltaTime;
                }
                else
                {
                    _velocity = Vector3.zero;
                }

                #endregion
            }
            else
            {
                _velocity = Vector3.zero;
            }
        }
    }

    public class Dash
    {
        PlayerController playerController;
        Player player;
        Transform transform;
        PlayerProfile playerProfile;

        string inputPath;
        InputLibrary inputLibrary = new InputLibrary();

        Vector3 horizontalInput;
        bool backwardDash;
        float dashTimer = 0;

        float dashSpeed;
        Vector3 velocity;

        public Vector3 Velocity
        {
            get
            {
                if (playerController.isDashing)
                {
                    return velocity;
                }
                else
                {
                    return Vector3.zero;
                }
            }
        }

        public Dash(PlayerController pc, string inputPath)
        {
            playerController = pc;
            this.inputPath = inputPath;
            player = playerController.player;
            transform = playerController.transform;
            playerProfile = playerController.playerProfile;
        }

        public void Update()
        {
            if (!playerController.isDashing)
            {
                #region Start Dash

                if (player.GetButtonDown(inputPath))
                {
                    if (!playerController.isDashing)
                    {
                        // Prevent Airborne dash unless permitted
                        if (!playerController.isGrounded && playerProfile.dash.airborneDash == false)
                        {
                            return;
                        }

                        horizontalInput = new Vector3(
                            player.GetAxis(inputLibrary.Left_Stick_X),
                            0,
                            player.GetAxis(inputLibrary.Left_Stick_Y));

                        horizontalInput.Normalize();

                        if (horizontalInput == Vector3.zero && playerProfile.dash.backwardDash == true)
                        {
                            backwardDash = true;
                        }
                        else
                        {
                            backwardDash = false;
                        }

                        playerController.isDashing = true;
                        dashTimer = 0;
                    }
                }

                #endregion
            }
            else
            {
                #region Dash

                if (playerController.isStrafing)
                {
                    dashSpeed = playerProfile.dash.maxSpeedWhileStrafing
                    * playerProfile.dash.dashSpeedCurve.Evaluate(dashTimer / playerProfile.dash.dashDuration);

                    if (backwardDash)
                    {
                        velocity = transform.forward * -dashSpeed;
                    }
                    else
                    {
                        velocity =
                            (transform.forward * horizontalInput.z + transform.right * horizontalInput.x)
                            * dashSpeed;
                    }
                }
                else
                {
                    dashSpeed = playerProfile.dash.dashMaxSpeed
                    * playerProfile.dash.dashSpeedCurve.Evaluate(dashTimer / playerProfile.dash.dashDuration);

                    if (backwardDash)
                    {
                        velocity = transform.forward * dashSpeed * -0.5f;
                    }
                    else
                    {
                        velocity = transform.forward * dashSpeed;
                    }
                }

                dashTimer += Time.deltaTime;

                if (dashTimer > playerProfile.dash.dashDuration)
                    playerController.isDashing = false;

                #endregion
            }
        }
    }

    public class Attack
    {
        /// <summary>
        /// 
        /// Pre = PreAttack, before Attack Frame
        /// Post = PostAttack, After Attack Frame
        /// 
        /// </summary>

        PlayerController playerController;
        P_Attack profile;
        Player player;
        Transform transform;
        string inputPath;

        public Vector3 Velocity
        {
            get
            {
                return _velocity;
            }
        }
        public bool Active
        {
            get
            {
                return _attacking;
            }
        }

        Vector3 _velocity;
        bool _attacking;

        bool pathBlocked;

        bool attacked;
        float attackTimer = 0;
        public float cdTimer = 0;

        public Attack(PlayerController pc, string inputPath, P_Attack attackProfile)
        {
            playerController = pc;
            profile = attackProfile;
            player = playerController.player;
            transform = playerController.transform;
            this.inputPath = inputPath;
        }

        public void Update()
        {
            if (player.GetButtonDown(inputPath) && cdTimer == 0)
            {
                _attacking = true;
                playerController.animationLock = true;
                pathBlocked = false;
                cdTimer = -1;
            }

            if (_attacking)
            {
                // Check if path is obstructed
                if (!pathBlocked && profile.movementPre.z > 0)
                {
                    pathBlocked = Physics.Raycast(transform.position + Vector3.up, transform.forward, profile.attackDistance * 0.5f);
                }

                if (!attacked)
                {
                    // Pre Attack

                    if (attackTimer > profile.moveDelay && !pathBlocked)
                    {
                        _velocity = (transform.forward * profile.movementPre.z + transform.right * profile.movementPre.x) / profile.moveTimePre;
                    }

                    if (attackTimer > profile.moveDelay + profile.moveTimePre)
                    {
                        AttackFrame();
                        attacked = true;
                        attackTimer = 0;
                    }
                }
                else
                {
                    // Post Attack

                    if (attackTimer < profile.moveTimePost)
                    {
                        _velocity = (transform.forward * profile.movementPost.z + transform.right * profile.movementPost.x) / profile.moveTimePost;
                    }
                    else if (attackTimer < profile.moveTimePost + profile.endDelay)
                    {
                        _velocity = Vector3.zero;
                    }
                    else
                    {
                        _attacking = false;
                        playerController.animationLock = false;
                        attacked = false;
                        attackTimer = 0;
                        cdTimer = profile.cooldown;
                    }
                }
                attackTimer += Time.deltaTime;
            }
            else
            {
                _velocity = Vector3.zero;
                cdTimer = Mathf.Max(0, cdTimer - Time.deltaTime);
            }
        }

        void AttackFrame()
        {
            // RaycastHit[] hit = Physics.RaycastAll(transform.position + Vector3.up, transform.forward, attackDistance, 13);
            Collider[] hit = Physics.OverlapCapsule(
                transform.position + Vector3.up,
                transform.position + Vector3.up + transform.forward * profile.attackDistance,
                profile.attackRadius);

            for (int i = 0; i < hit.Length; i++)
            {
                try
                {
                    if (hit[i].transform != this.transform)
                    {
                        hit[i].transform.GetComponent<Character>().Damage(profile.damage);
                    }
                }
                catch (NullReferenceException)
                {
                    Debug.Log(hit[i].transform.name + " was hit but cannot be damaged.");
                }
            }
        }
    }

    public class AnimationControl
    {
        PlayerController playerController;
        P_Animation profile;
        Animator animator;
        bool hasAnimator;

        Move move;
        int speedHash;

        public AnimationControl(PlayerController pc, Move move)
        {
            playerController = pc;
            profile = playerController.playerProfile.animation;

            LookForAnimator();

            this.move = move;
            speedHash = Animator.StringToHash(profile.SpeedName);
        }

        void LookForAnimator()
        {
            try
            {
                animator = playerController.transform.GetComponentInChildren<Animator>();
                hasAnimator = true;
            }
            catch (NullReferenceException)
            {
                Debug.Log("Player: Animator not found!");
                hasAnimator = false;
            }
        }

        public void Update()
        {
            if (hasAnimator)
            {
                animator.SetFloat(speedHash, move.Velocity.magnitude);
            }
        }
    }
}
