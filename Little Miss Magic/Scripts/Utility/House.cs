using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public EventBool PlayerInside;

    [SerializeField] List<Renderer> exterior = new List<Renderer>();
    [SerializeField] List<Renderer> interior = new List<Renderer>();

    void OnEnable()
    {
        PlayerInside.OnChange += SetView;
    }

    void OnDisable()
    {
        PlayerInside.OnChange -= SetView;
    }

    private void Start()
    {
        SetView(PlayerInside.Value);
    }

    void SetView(bool state)
    {
        if (PlayerInside.Value == true)
            InteriorView();
        else
            ExteriorView();
    }

    [ContextMenu("Interior View")]
    public void InteriorView()
    {
        foreach (Renderer r in exterior)
        {
            r.enabled = false;
        }

        foreach (Renderer r in interior)
        {
            r.enabled = true;
        }
    }

    [ContextMenu("Exterior View")]
    public void ExteriorView()
    {
        foreach (Renderer r in exterior)
        {
            r.enabled = true;
        }

        foreach (Renderer r in interior)
        {
            r.enabled = false;
        }
    }


}
