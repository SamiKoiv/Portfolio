using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera defaultCamera;
    [SerializeField] CinemachineVirtualCamera topCamera;

    public CameraMode Mode;
    CameraMode savedMode;
    CameraMode previousMode;

    CinemachineVirtualCamera[] cams;
    GameObject player;

    bool cameraLocked;
    GameObject lockingGO;

    void Awake()
    {
        cams = new CinemachineVirtualCamera[2];
        cams[0] = defaultCamera;
        cams[1] = topCamera;
    }

    private void OnEnable()
    {
        EventSystem.Objects.OnBroadcast_Player += GetPlayer;
        EventSystem.CameraEvents.OnSetMode += SetMode;
        EventSystem.CameraEvents.OnPreviousMode += PreviousMode;
        EventSystem.CameraEvents.OnLock += Lock;
        EventSystem.CameraEvents.OnUnlock += Unlock;
        EventSystem.CameraEvents.OnOverrideMode += OverrideMode;
    }

    private void OnDisable()
    {
        EventSystem.Objects.OnBroadcast_Player -= GetPlayer;
        EventSystem.CameraEvents.OnSetMode -= SetMode;
        EventSystem.CameraEvents.OnPreviousMode -= PreviousMode;
        EventSystem.CameraEvents.OnLock -= Lock;
        EventSystem.CameraEvents.OnUnlock -= Unlock;
        EventSystem.CameraEvents.OnOverrideMode -= OverrideMode;
    }

    void Start()
    {
        ProcessMode();
    }

    private void Update()
    {
        if (savedMode != Mode)
        {
            SetMode(Mode);
        }
    }

    void ProcessMode()
    {
        switch (Mode)
        {
            case CameraMode.Default:
                SwitchCamera(defaultCamera);
                break;
            case CameraMode.Top:
                SwitchCamera(topCamera);
                break;
        }
    }

    void SwitchCamera(CinemachineVirtualCamera cam)
    {
        for (int i = 0; i < cams.Length; i++)
        {
            if (cams[i] == cam)
            {
                cams[i].enabled = true;
            }

            else
            {
                cams[i].enabled = false;
            }
        }
    }

    

    void SetMode(CameraMode mode)
    {
        if (!cameraLocked)
        {
            previousMode = Mode;
            Mode = mode;
            savedMode = Mode;
            ProcessMode();
        }
    }

    void PreviousMode()
    {
        Debug.Log("Previous Mode Called");
        if (!cameraLocked)
        {
            Mode = previousMode;
            previousMode = savedMode;
            savedMode = Mode;
            ProcessMode();
        }
    }

    void Lock(GameObject lockingGO)
    {
        if (!cameraLocked)
        {
            cameraLocked = true;
            this.lockingGO = lockingGO;
        }
    }

    void Unlock(GameObject lockingGO)
    {
        if (cameraLocked && lockingGO == this.lockingGO)
        {
            cameraLocked = false;
            this.lockingGO = null;
        }

    }

    void OverrideMode(CameraMode mode, GameObject lockingGO)
    {
        cameraLocked = false;
        SetMode(mode);

        if (lockingGO != null)
        {
            this.lockingGO = lockingGO;
            cameraLocked = true;
        }
    }

    // GET REFERENCES --------------------------------------------------------------------------

    void GetPlayer(GameObject go)
    {
        player = go;

        defaultCamera.Follow = player.transform;
        topCamera.Follow = player.transform;
    }

}

public enum CameraMode
{
    Default,
    Top
}
