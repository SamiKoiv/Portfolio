using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : ManagedBehaviour_Update
{
    // PUBLIC
    public PlayerAssets m_PlayerAssets;
    public PlayerInteractableSensor m_InteractableSensor;
    public float m_MovementSpeed;
    public float m_SprintModifier;

    public bool m_HoldingObject;
    public Rigidbody m_HeldRigidbody;
    public Transform m_ItemHoldPosition;
    public float m_ThrowStrenght;

    // PRIVATE
    Transform mainCamera;

    Rigidbody m_rb;

    Vector2 m_moveInput;
    bool m_isSprinting;

    IInteractable closestInteractable;
    IHoldable heldObject;
    bool interactionDown;
    float interactionTimer;

    [Header("Interaction")]
    public float interactionRadius;
    public float interactionDistance;
    public float interactionLongPressTime;

    void OnEnable()
    {
        Subscribe_Update();
        EventSystem.InteractionEvents.OnTakenHold += GetHeldObject;
        InputSystem.OnMove += GetMove;
        InputSystem.OnInteractDown += InteractDown;
        InputSystem.OnInteractUp += InteractUp;
    }

    void OnDisable()
    {
        Unsubscribe_Update();
        EventSystem.InteractionEvents.OnTakenHold -= GetHeldObject;
        InputSystem.OnMove -= GetMove;
        InputSystem.OnInteractDown -= InteractDown;
        InputSystem.OnInteractUp -= InteractUp;
    }

    void Start()
    {
        mainCamera = Core.Instance.m_MainCamera.transform;
        m_rb = GetComponent<Rigidbody>();

        EventSystem.Objects.Broadcast_Player(gameObject);
    }

    public override void M_Update()
    {
        if (interactionDown)
        {
            if (interactionTimer < interactionLongPressTime)
            {
                interactionTimer += Time.deltaTime;
            }
            else
            {
                InteractLong();
                interactionDown = false;
            }
        }

        // Holding Object
    }

    void FixedUpdate()
    {
        m_rb.angularVelocity = Vector3.zero;

        if (m_moveInput.magnitude != 0)
        {
            Vector3 rotation = Quaternion.LookRotation(new Vector3(m_moveInput.x, 0, m_moveInput.y)).eulerAngles + mainCamera.eulerAngles.y * Vector3.up;
            m_rb.MoveRotation(Quaternion.Euler(rotation));

            if (m_isSprinting)
            {
                m_rb.MovePosition(transform.position + transform.forward * Mathf.Min(m_moveInput.magnitude, 1) * m_MovementSpeed * m_SprintModifier * Time.deltaTime);
            }
            else
            {
                m_rb.MovePosition(transform.position + transform.forward * Mathf.Min(m_moveInput.magnitude, 1) * m_MovementSpeed * Time.deltaTime);
            }
        }

        if (m_HoldingObject)
        {
            m_HeldRigidbody.position = m_ItemHoldPosition.position;
            m_HeldRigidbody.rotation = m_ItemHoldPosition.rotation;
        }
    }

    void GetMove(Vector2 moveInput)
    {
        m_moveInput = moveInput;
    }

    void CastSpell()
    {
        if (m_HoldingObject)
        {
            ThrowHeldObject();
        }
        else
        {
            ISpell spell = GetComponent<ISpell>();
            spell.Cast();
        }
    }

    void UncastSpell()
    {
        ISpell spell = GetComponent<ISpell>();
        spell.Uncast();
    }

    void InteractDown()
    {
        closestInteractable = m_InteractableSensor.m_ClosestInteractable;

        interactionDown = true;
        interactionTimer = 0;
    }

    void InteractUp()
    {
        if (interactionDown && m_InteractableSensor.m_ClosestInteractable == closestInteractable)
        {
            if (interactionTimer < interactionLongPressTime)
                Interact();
        }
        interactionDown = false;
    }

    void Interact()
    {
        if (m_HoldingObject)
        {
            ICombinable combinable = m_InteractableSensor.m_ClosestCombinable;

            if (combinable != null)
            {
                combinable.Combine(m_HeldRigidbody);
                DropHeldObject();
            }
        }
        else
        {
            IInteractable interactable = m_InteractableSensor.m_ClosestInteractable;

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    void InteractLong()
    {
        Drop();

        {
            IInteractable interactable = m_InteractableSensor.m_ClosestInteractable;

            if (interactable != null)
            {
                interactable.InteractLong();
            }
        }
    }

    void GetHeldObject(IHoldable holdable, Rigidbody rb)
    {
        this.heldObject = holdable;
        m_HeldRigidbody = rb;
        m_HoldingObject = true;

        m_InteractableSensor.SetHoldingObject(true);
        m_InteractableSensor.ResetClosestInteractable();
    }

    void ThrowHeldObject()
    {
        m_HoldingObject = false;
        heldObject.Throw(transform.forward * m_ThrowStrenght);
        m_HeldRigidbody = null;
        heldObject = null;

        m_InteractableSensor.SetHoldingObject(false);
    }

    void Drop()
    {
        if (m_HoldingObject)
        {
            DropHeldObject();
        }
    }

    void DropHeldObject()
    {
        m_HoldingObject = false;
        heldObject.Drop();
        m_HeldRigidbody = null;
        heldObject = null;

        m_InteractableSensor.SetHoldingObject(false);
    }

    //-------------------------------------------------------------------------------------------------

    void OnDrawGizmos()
    {

    }
}
