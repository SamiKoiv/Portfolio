using UnityEngine;
using PlayerProfileNS;

public class Camera_Controller : MonoBehaviour
{
    // CameraController
    new Transform transform;

    public PlayerProfile playerProfile;
    P_Camera profile;
    public CameraMode cameraMode = CameraMode.FollowPosRot;

    PlayerInput input;

    Transform player;
    new Camera camera;
    Transform cameraTransform;
    Transform cameraPosition;

    internal Transform Target;

    void Awake()
    {
        transform = gameObject.transform;
        player = Core.instance.GetPlayer();
        cameraPosition = transform.Find("CameraPosition");
        profile = playerProfile.camera;
    }

    void Start()
    {
        GetReferences();
        player = Core.instance.GetPlayer();
        camera = Core.instance.MainCamera;
        cameraTransform = camera.transform;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position;
        }

        if (profile.ZoomedIn)
        {
            cameraPosition.localPosition = profile.zoomedPositionOffset;
            cameraPosition.localEulerAngles = profile.zoomedRotationOffset;
        }
        else
        {
            cameraPosition.localPosition = profile.defaultPositionOffset;
            cameraPosition.localEulerAngles = profile.defaultRotationOffset;
        }

        if (Target != null)
        {
            FollowTarget();
        }
        else
        {
            if (!profile.StopRotation)
            {
                TurnInput();
            }
        }

        CameraCollision();

        if (Target != null)
        {
            cameraPosition.LookAt(Target);
        }

        //camera.position = cameraPosition.position;
        //camera.rotation = cameraPosition.rotation;
        ProcessCamera();
    }

    void GetReferences()
    {
        input = PlayerInput.instance;
    }

    #region CameraController

    void TurnInput()
    {
        Quaternion rotation = transform.rotation;

        float input_X =
            input.Right_Stick_X * profile.StickSensitivity
            + input.Mouse_X * profile.MouseSensitivity;

        float input_Y =
            input.Right_Stick_Y * profile.StickSensitivity
            + input.Mouse_Y * profile.MouseSensitivity;

        float x = rotation.eulerAngles.x - input_Y;

        // EulerAngles always return value between 0 and 360
        //Here we convert the camera to work between -180 and 180 degrees.

        if (x > 180)
        {
            x -= 360;
        }

        x = Mathf.Clamp(x, profile.VerticalRotationMin, profile.VerticalRotationMax);

        float y = rotation.eulerAngles.y + input_X;

        rotation.eulerAngles = new Vector3(x, y, 0.0f);

        transform.rotation = rotation;

    }

    void FollowTarget()
    {
        Quaternion rotation = Quaternion.LookRotation(Target.position - transform.position);
        transform.eulerAngles = new Vector3(0, rotation.eulerAngles.y, 0);
    }

    void CameraCollision()
    {
        RaycastHit hit;

        if (Physics.Linecast(transform.position + Vector3.up, cameraPosition.position, out hit))
        {
            if (hit.transform != Core.instance.GetPlayer())
                cameraPosition.position = hit.point + (transform.position - cameraPosition.position).normalized;
        }
    }

    #endregion

    #region Camera

    // Camera
    Vector3 startingPosition;
    Quaternion startingRotation;
    Vector3 targetPosition;
    Quaternion targetRotation;
    float taxiTimer = 0;
    float taxiDuration = 1;

    void ProcessCamera()
    {
        switch (cameraMode)
        {
            case CameraMode.Static:
                Process_Static();
                break;
            case CameraMode.FollowPos:
                Process_FollowPos();
                break;
            case CameraMode.FollowRot:
                Process_FollowRot();
                break;
            case CameraMode.FollowPosRot:
                Process_FollowPosRot();
                break;
        }
    }

    #region Switch Camera Mode

    public void Switch_Static()
    {
        cameraMode = CameraMode.Static;
    }

    public void Switch_FollowPos()
    {
        cameraMode = CameraMode.FollowPos;
    }

    public void Switch_FollowRot()
    {
        cameraMode = CameraMode.FollowRot;
    }

    public void Switch_FollowPosRot()
    {
        cameraMode = CameraMode.FollowPosRot;
    }

    #endregion

    #region Process Camera Mode

    void Process_Static()
    {

    }

    void Process_FollowPos()
    {
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraPosition.position, Time.deltaTime * profile.followPosSpeed);
    }

    void Process_FollowRot()
    {
        cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, cameraPosition.rotation, Time.deltaTime * profile.followRotSpeed);
    }

    void Process_FollowPosRot()
    {
        Process_FollowPos();
        Process_FollowRot();
    }

    #endregion

    #endregion
}
