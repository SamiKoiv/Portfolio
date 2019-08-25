using Unity.Burst;
using Unity.Jobs;
using UnityEngine;

public class ManagedUpdate : MonoBehaviour
{
    static ManagedUpdate instance;
    static ManagedBehaviour_Update[] updates = new ManagedBehaviour_Update[100000];
    static ManagedBehaviour_LateUpdate[] lateUpdates = new ManagedBehaviour_LateUpdate[10000];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Debug.Log("Multiple instances of ManagedUpdate in the scene. Destroying duplicates.");
            Destroy(this);
        }
    }

    [BurstCompile]
    struct UpdateJob : IJobParallelFor
    {
        public int First;
        public int Last;
        public ManagedBehaviour_Update[] Updates;

        public void Execute(int index)
        {
            for (int i = First + index; i <= Last; i++)
            {
                if (Updates[i] != null)
                    Updates[i].M_Update();
            }
        }
    }

    void Update()
    {

        for (int i = 0; i < updates.Length; i++)
        {
            if (updates[i] != null)
                updates[i].M_Update();
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < lateUpdates.Length; i++)
        {
            if (lateUpdates[i] != null)
                lateUpdates[i].M_LateUpdate();
        }
    }

    // Update ---------------------------------------------------------------------------------------

    public static void Subscribe(ManagedBehaviour_Update behaviour)
    {
        for (int i = 0; i < updates.Length; i++)
        {
            if (updates[i] == null)
            {
                updates[i] = behaviour;
                return;
            }

        }

        Debug.Log("ManagedUpdate: Update buffer capacity exceeded.");
    }

    public static void Unsubscribe(ManagedBehaviour_Update behaviour)
    {
        for (int i = 0; i < updates.Length; i++)
        {
            if (updates[i] == behaviour)
            {
                updates[i] = null;
                return;
            }
        }
    }

    // LateUpdate ---------------------------------------------------------------------------------------

    public static void Subscribe(ManagedBehaviour_LateUpdate behaviour)
    {
        for (int i = 0; i < lateUpdates.Length; i++)
        {
            if (lateUpdates[i] == null)
            {
                lateUpdates[i] = behaviour;
                return;
            }
        }

        Debug.Log("ManagedUpdate: LateUpdate buffer capacity exceeded.");
    }

    public static void Unsubscribe(ManagedBehaviour_LateUpdate behaviour)
    {
        for (int i = 0; i < lateUpdates.Length; i++)
        {
            if (lateUpdates[i] == behaviour)
            {
                lateUpdates[i] = null;
                return;
            }
        }
    }
}
