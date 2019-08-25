using InventorySystem;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    static EventSystem instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Debug.Log("Multiple instances of EventHandler in the scene. Destroying duplicates.");
            Destroy(gameObject);
        }
    }

    public static EH_GameEvents GameEvents = new EH_GameEvents();
    public static EH_CameraEvents CameraEvents = new EH_CameraEvents();
    public static EH_ObjectEvents Objects = new EH_ObjectEvents();
    public static EH_InteractionEvents InteractionEvents = new EH_InteractionEvents();
    public static EH_InventoryEvents InventoryEvents = new EH_InventoryEvents();

    // ----------------------------------------------------------------------

    public class EH_CameraEvents
    {
        public delegate void VoidEvent();
        public delegate void CameraModeEvent(CameraMode value);
        public delegate void GameObjectEvent(GameObject value);
        public delegate void OverrideModeEvent(CameraMode mode, GameObject lockingGO);

        public event CameraModeEvent OnSetMode;
        public event VoidEvent OnPreviousMode;
        public event GameObjectEvent OnLock;
        public event GameObjectEvent OnUnlock;

        public event OverrideModeEvent OnOverrideMode;

        public void SetMode(CameraMode mode)
        {
            if (OnSetMode != null)
                OnSetMode(mode);
        }

        public void PreviousMode()
        {
            if (OnPreviousMode != null)
                OnPreviousMode();
        }

        public void Lock(GameObject go)
        {
            if (OnLock != null)
                OnLock(go);
        }

        public void Unlock(GameObject go)
        {
            if (OnUnlock != null)
                OnUnlock(go);
        }

        public void OverrideMode(CameraMode mode, GameObject lockingObject)
        {
            if (OnOverrideMode != null)
                OnOverrideMode(mode, lockingObject);
        }
    }

    public class EH_GameEvents
    {
        public delegate void VoidEvent();
        public delegate void ObjectEvent(GameObject value);
        public delegate void EntryPlacementEvent(InventoryEntry value, int quantity);

        public event VoidEvent OnPlacing_Start;
        public event VoidEvent OnPlacing_Stop;
        public event VoidEvent OnPlacing_ItemPlaced;
        public event ObjectEvent OnPlacing_PlaceGO;
        public event EntryPlacementEvent OnPlacing_PlaceEntry;

        public void Placing_Start()
        {
            if (OnPlacing_Start != null)
                OnPlacing_Start();
        }

        public void Placing_Stop()
        {
            if (OnPlacing_Stop != null)
                OnPlacing_Stop();
        }

        public void Placing_ItemPlaced()
        {
            if (OnPlacing_ItemPlaced != null)
                OnPlacing_ItemPlaced();
        }

        public void Placing_PlaceGO(GameObject go)
        {
            if (OnPlacing_PlaceGO != null)
            {
                OnPlacing_PlaceGO(go);
            }
        }

        public void Placing_PlaceEntry(InventoryEntry entry, int quantity)
        {
            if (OnPlacing_PlaceEntry != null)
                OnPlacing_PlaceEntry(entry, quantity);
        }
    }

    public class EH_ObjectEvents
    {
        public delegate void GameObjectEvent(GameObject value);

        // PLAYER ------------------------------------------------------------------------

        public event GameObjectEvent OnBroadcast_Player;

        GameObject _player;
        public GameObject Player
        {
            get { return _player; }
        }

        public void Broadcast_Player(GameObject player)
        {
            _player = player;

            if (OnBroadcast_Player != null)
                OnBroadcast_Player(_player);
        }

        // MAIN CAMERA ------------------------------------------------------------------------

        public event GameObjectEvent OnBroadcast_MainCamera;

        static GameObject _mainCamera;
        public static GameObject MainCamera
        {
            get { return _mainCamera; }
        }

        public void Broadcast_MainCamera(GameObject mainCamera)
        {
            _mainCamera = mainCamera;

            if (OnBroadcast_MainCamera != null)
                OnBroadcast_MainCamera(_mainCamera);
        }

        // ------------------------------------------------------------------------

    }

    public class EH_InteractionEvents
    {
        // Broadcast destroyed InteractableObjects, so that they are removed from all listening lists.
        public delegate void InteractableEvent(IInteractable interactable);
        public event InteractableEvent OnInteractableDestroyed;

        public void InteractableDestroyed(IInteractable interactable)
        {
            OnInteractableDestroyed(interactable);
        }

        // Broadcast destroyed CombinableObjects, so that they are removed from all listening lists.
        public delegate void CombinableEvent(ICombinable combinable);
        public event CombinableEvent OnCombinableDestroyed;

        public void CombinableDestroyed(ICombinable combinable)
        {
            OnCombinableDestroyed(combinable);
        }

        public delegate void HoldableObjectEvent(IHoldable ho, Rigidbody rb);
        public event HoldableObjectEvent OnTakenHold;

        public void TakenHold(IHoldable holdable, Rigidbody rb)
        {
            OnTakenHold(holdable, rb);
        }

        public delegate void InteractionPopupcallEvent(bool isActive, Transform target, Vector3 offset, string message);
        public event InteractionPopupcallEvent OnSetInteractionPopup;

        public void SetInteractionPopup(bool isActive, Transform target, Vector3 offset, string message)
        {
            OnSetInteractionPopup(isActive, target, offset, message);
        }
    }

    public class EH_InventoryEvents
    {
        public delegate void ItemEvent(Item item);

        public event ItemEvent OnInventoryFull;

        public void InventoryFull(Item item)
        {
            if (OnInventoryFull != null)
                OnInventoryFull(item);
        }
    }
}
