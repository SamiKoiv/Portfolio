using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent (typeof(GraphicRaycaster))]
public class MouseDebugger : MonoBehaviour
{
    public bool RaycastUI;
    Camera mainCamera;
    GraphicRaycaster graphicRaycaster;
    public UnityEngine.EventSystems.EventSystem eventSystem;

    PointerEventData pointerEventData;

    void Start()
    {
        Debug.Log("Mouse Debugger in the Scene");
        mainCamera = Camera.main;

        if (RaycastUI)
        {
            graphicRaycaster = GetComponent<GraphicRaycaster>();

            if (graphicRaycaster == null)
            {
                Debug.Log("Mouse Debugger cannot find GraphicRaycaster. Please place MouseDebugger in Canvas.");
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RayCast();
        }
    }

    void RayCast()
    {
        if (RaycastUI)
        {
            pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            graphicRaycaster.Raycast(pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                Debug.Log("Hit " + result.gameObject.name);
            }
        }
        else
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);

            if (hit.transform != null)
            {
                Debug.Log(hit.transform.name);
            }
        }
    }
}
