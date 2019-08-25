using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractableSensor : ManagedBehaviour_Update
{
    // PUBLIC --------------------------------------------------------------------------

    public IInteractable m_ClosestInteractable
    {
        get
        {
            return m_closestInteractable;
        }
    }

    public ICombinable m_ClosestCombinable
    {
        get
        {
            return m_closestCombinable;
        }
    }

    public void RemoveInteractableObject(IInteractable interactable)
    {
        m_interactables.Remove(interactable);
        ResetClosestInteractable();
    }

    public void RemoveCombinableObject(ICombinable combinable)
    {
        m_combinables.Remove(combinable);
    }

    // PRIVATE --------------------------------------------------------------------------

    IInteractable m_closestInteractable;
    List<IInteractable> m_interactables = new List<IInteractable>();

    ICombinable m_closestCombinable;
    List<ICombinable> m_combinables = new List<ICombinable>();

    float m_radius;
    public bool m_isHolding;

    void OnEnable()
    {
        Subscribe_Update();
        EventSystem.InteractionEvents.OnInteractableDestroyed += RemoveInteractableObject;
        EventSystem.InteractionEvents.OnCombinableDestroyed += RemoveCombinableObject;
    }
    void OnDisable()
    {
        Unsubscribe_Update();
        EventSystem.InteractionEvents.OnInteractableDestroyed -= RemoveInteractableObject;
        EventSystem.InteractionEvents.OnCombinableDestroyed -= RemoveCombinableObject;
    }

    void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (interactable != null)
        {
            m_interactables.Add(interactable);
        }

        ICombinable combinable = other.GetComponent<ICombinable>();

        if (combinable != null)
        {
            m_combinables.Add(combinable);
        }
    }
    void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (interactable != null && m_interactables.Contains(interactable))
        {
            m_interactables.Remove(interactable);
        }

        ICombinable combinable = other.GetComponent<ICombinable>();

        if (combinable != null)
        {
            m_combinables.Remove(combinable);
        }
    }

    void Start()
    {
        m_radius = GetComponent<SphereCollider>().radius;
    }

    public override void M_Update()
    {
        if (m_isHolding)
        {
            LookForCombinables();
        }
        else
        {
            LookForInteractables();
        }
    }

    void LookForInteractables()
    {
        if (m_interactables.Count != 0)
        {
            m_closestInteractable = null;
            float distance = 0;

            foreach (IInteractable interactable in m_interactables)
            {
                if (m_closestInteractable == null)
                {
                    m_closestInteractable = interactable;
                    distance = Vector3.Distance(transform.position, m_closestInteractable.GetTransform().position);
                }
                else if (Vector3.Distance(transform.position, interactable.GetTransform().position) < distance)
                {
                    m_closestInteractable = interactable;
                    distance = Vector3.Distance(transform.position, m_closestInteractable.GetTransform().position);
                }
            }

            EventSystem.InteractionEvents.SetInteractionPopup(true, m_closestInteractable.GetTransform(), m_ClosestInteractable.GetPopupOffset(), m_closestInteractable.GetInteractionMessage());

            if (Vector3.Distance(transform.position, m_closestInteractable.GetTransform().position) > m_radius)
            {
                EventSystem.InteractionEvents.SetInteractionPopup(false, null, Vector3.zero, string.Empty);
                m_closestInteractable = null;
            }
        }
        else
        {
            m_closestInteractable = null;
        }
    }

    void LookForCombinables()
    {
        if (m_combinables.Count != 0)
        {
            m_closestCombinable = null;
            float distance = 0;

            foreach (ICombinable combinable in m_combinables)
            {
                if (m_closestCombinable == null)
                {
                    m_closestCombinable = combinable;
                    distance = Vector3.Distance(transform.position, m_closestCombinable.GetTransform().position);
                }
                else if (Vector3.Distance(transform.position, combinable.GetTransform().position) < distance)
                {
                    m_closestCombinable = combinable;
                    distance = Vector3.Distance(transform.position, m_closestCombinable.GetTransform().position);
                }
            }

            EventSystem.InteractionEvents.SetInteractionPopup(true, m_closestCombinable.GetTransform(), m_ClosestCombinable.GetPopupOffset(), m_closestCombinable.GetCombinationMessage());

            if (Vector3.Distance(transform.position, m_closestCombinable.GetTransform().position) > m_radius)
            {
                EventSystem.InteractionEvents.SetInteractionPopup(false, null, Vector3.zero, string.Empty);
                m_closestCombinable = null;
            }
        }
        else
        {
            m_closestCombinable = null;
        }
    }

    public void ResetClosestInteractable()
    {
        EventSystem.InteractionEvents.SetInteractionPopup(false, null, Vector3.zero, string.Empty);
        m_closestInteractable = null;
    }

    public void SetHoldingObject(bool isHolding)
    {
        m_isHolding = isHolding;
        EventSystem.InteractionEvents.SetInteractionPopup(false, null, Vector3.zero, string.Empty);
    }

    
}
