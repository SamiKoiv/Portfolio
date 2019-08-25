using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Proto_Movement_1 : MonoBehaviour
{
    public Mode mode;

    Rigidbody rb;
    CharacterController characterController;

    float physicsMovement = 0;
    float transformMovement = 0;

    public float transformSpeed;
    public float physicsVelocity;

    public enum Mode
    {
        TransformTranslate,
        RigidBodyVelocity,
        CharacterController
    };

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        CheckInput();

        if (mode == Mode.TransformTranslate)
        {
            transform.Translate(Vector3.right * transformMovement * Time.deltaTime);
        }

        if (mode == Mode.CharacterController)
        {
            characterController.SimpleMove(Vector3.right * transformMovement);
        }
    }

    void FixedUpdate()
    {
        if (mode == Mode.RigidBodyVelocity)
        {
            rb.velocity = Vector3.right * physicsMovement;
        }
    }

    void CheckInput()
    {
        physicsMovement = 0;
        transformMovement = 0;

        if (Input.GetKey(KeyCode.A))
        {
            physicsMovement -= physicsVelocity;
            transformMovement -= transformSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            physicsMovement += physicsVelocity;
            transformMovement += transformSpeed;
        }
    }
}
