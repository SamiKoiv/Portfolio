using UnityEngine;

public class InputSystem : ManagedBehaviour_Update
{
    static InputSystem instance;

    #region Paths

    class PathLibrary
    {
        [Header("Mouse")]
        public string MouseLeft = "MouseLeft";

        [Header("Keyboard")]
        public string MoveX = "Horizontal";
        public string MoveY = "Vertical";
        public string Interact = "Interact";
        public string Inventory = "Inventory";
    }

    #endregion

    #region Variables & Events

    static PathLibrary paths = new PathLibrary();

    static bool inDialogue;
    static bool inPlacementTool;

    public delegate void VoidEvent();
    public delegate void FloatEvent(float value);
    public delegate void BoolEvent(bool value);
    public delegate void Vector2Event(Vector2 value);

    public static event FloatEvent UI_OnMoveX;
    public static event FloatEvent UI_OnMoveY;
    public static event VoidEvent UI_OnSubmit;
    public static event VoidEvent UI_OnCancel;

    public static event Vector2Event OnMove;
    public static event FloatEvent OnMoveX;
    public static event FloatEvent OnMoveY;
    public static event VoidEvent OnInteractDown;
    public static event VoidEvent OnInteractUp;

    public static event VoidEvent OnDialogueContinue;

    public static event VoidEvent OnPlacementButton;
    public static event VoidEvent OnPlacementButtonDown;
    public static event VoidEvent OnPlacementButtonUp;
    public static event VoidEvent OnPlacementCancel;

    #endregion

    #region Monobehaviour

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Duplicate InputSystem removed from the scene.");
            Destroy(this);
        }
    }

    private void OnEnable()
    {
        Subscribe_Update();
    }

    private void OnDisable()
    {
        Unsubscribe_Update();
    }

    #endregion

    #region Locked States


    public static void StartDialogue()
    {
        OnMove(Vector2.zero);
        inDialogue = true;
    }

    public static void EndDialogue()
    {
        inDialogue = false;
    }

    public static void StartPlacementTool()
    {
        inPlacementTool = true;
    }

    public static void StopPlacementTool()
    {
        inPlacementTool = false;
    }

    #endregion

    #region Update

    public override void M_Update()
    {
        if (inDialogue)
        {
            if (OnDialogueContinue == null)
                return;

            if (Input.GetMouseButtonDown(0))
                OnDialogueContinue();

            if (Input.GetButtonDown("Interact"))
                OnDialogueContinue();

            return;
        }

        else if (inPlacementTool)
        {
            if (OnPlacementButton != null)
            {
                if (Input.GetMouseButton(0))
                    OnPlacementButton();
            }

            if (OnPlacementButtonDown != null)
            {
                if (Input.GetMouseButtonDown(0))
                    OnPlacementButtonDown();
            }

            if (OnPlacementButtonUp != null)
            {
                if (Input.GetMouseButtonUp(0))
                    OnPlacementButtonUp();
            }

            if (OnPlacementCancel != null)
            {
                if (Input.GetButtonDown("Cancel"))
                    OnPlacementCancel();
            }

            return;
        }

        CharacterMovement();
        CharacterActions();

        // UI ------------------------------------------------------------------

        if (UI_OnMoveX != null)
            UI_OnMoveX(Input.GetAxis("Horizontal"));

        if (UI_OnMoveY != null)
            UI_OnMoveY(Input.GetAxis("Vertical"));

        if (UI_OnSubmit != null && Input.GetButtonDown("Submit"))
            UI_OnSubmit();

        if (UI_OnCancel != null && Input.GetButtonDown("Cancel"))
            UI_OnCancel();

        // ------------------------------------------------------------------

        if (Input.GetButtonDown("Inventory"))
            UISystem.Instance.Inventory_Toggle();
    }

    static void CharacterMovement()
    {
        if (OnMove != null)
            OnMove(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }

    static void CharacterActions()
    {
        if (OnInteractDown != null && Input.GetButtonDown("Interact"))
            OnInteractDown();

        if (OnInteractUp != null && Input.GetButtonUp("Interact"))
            OnInteractUp();
    }

    #endregion
}
