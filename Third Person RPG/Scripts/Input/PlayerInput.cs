using UnityEngine;
using Rewired;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance = null;

    void EstablishSingleton()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    // Controls categorised using Xbox controller as a reference.
    // PC equivalents placed next to Xbox Inputs.

    #region Variables

    Player player;
    public int PlayerId = 0;

    InputLibrary inputLibrary = new InputLibrary();

    float _mouseX;
    float _mouseY;
    float _leftStick_X;
    float _leftStick_Y;
    float _rightStick_X;
    float _rightStick_Y;
    float _leftTrigger;
    float _rightTrigger;
    
    public float Mouse_X
    {
        get
        {
            return _mouseX;
        }
    }
    public float Mouse_Y
    {
        get
        {
            return _mouseY;
        }
    }
    public float Left_Stick_X
    {
        get
        {
            return _leftStick_X;
        }
    }
    public float Left_Stick_Y
    {
        get
        {
            return _leftStick_Y;
        }
    }
    public float Right_Stick_X
    {
        get
        {
            return _rightStick_X;
        }
    }
    public float Right_Stick_Y
    {
        get
        {
            return _rightStick_Y;
        }
    }
    public float Left_Trigger
    {
        get
        {
            return _leftTrigger;
        }
    }
    public float Right_Trigger
    {
        get
        {
            return _rightTrigger;
        }
    }

    #endregion

    #region Awake, Update

    void Awake()
    {
        player = ReInput.players.GetPlayer(PlayerId);
        EstablishSingleton();
    }

    void Update()
    {
        ListenForInput();
    }

    #endregion

    //---------------------------------------------------------------

    #region InputListener

    void ListenForInput()
    {
        Controller_Axes();
        Controller_Buttons();
    }

    void Controller_Axes()
    {
        // LEFT STICK

        _mouseX = player.GetAxis(inputLibrary.Mouse_X);
        _mouseY = player.GetAxis(inputLibrary.Mouse_Y);


        // LEFT STICK

        _leftStick_X = player.GetAxis(inputLibrary.Left_Stick_X);
        _leftStick_Y = player.GetAxis(inputLibrary.Left_Stick_Y);


        // RIGHT STICK

        _rightStick_X = player.GetAxis(inputLibrary.Right_Stick_X);
        _rightStick_Y = player.GetAxis(inputLibrary.Right_Stick_Y);


        // TRIGGERS

        _leftTrigger = player.GetAxis(inputLibrary.Left_Trigger);
        _rightTrigger = player.GetAxis(inputLibrary.Right_Trigger);

    }

    void Controller_Buttons()
    {


        // A
        if (player.GetButton(inputLibrary.A))
        { A.Invoke(); }
        if (player.GetButtonDown(inputLibrary.A))
        { A_Down.Invoke(); }
        if (player.GetButtonUp(inputLibrary.A))
        { A_Up.Invoke(); }

        // B
        if (player.GetButton(inputLibrary.B))
        { B.Invoke(); }
        if (player.GetButtonDown(inputLibrary.B))
        { B_Down.Invoke(); }
        if (player.GetButtonUp(inputLibrary.B))
        { B_Up.Invoke(); }

        // X
        if (player.GetButton(inputLibrary.X))
        { X.Invoke(); }
        if (player.GetButtonDown(inputLibrary.X))
        { X_Down.Invoke(); }
        if (player.GetButtonUp(inputLibrary.X))
        { X_Up.Invoke(); }

        // Y
        if (player.GetButton(inputLibrary.Y))
        { Y.Invoke(); }
        if (player.GetButtonDown(inputLibrary.Y))
        { Y_Down.Invoke(); }
        if (player.GetButtonUp(inputLibrary.Y))
        { Y_Up.Invoke(); }

        //---------------------------------------------------------------

        // Left Shoulder
        if (player.GetButton(inputLibrary.Left_Shoulder))
        { Left_Shoulder.Invoke(); }
        if (player.GetButtonDown(inputLibrary.Left_Shoulder))
        { Left_Shoulder_Down.Invoke(); }
        if (player.GetButtonUp(inputLibrary.Left_Shoulder))
        { Left_Shoulder_Up.Invoke(); }

        // Right Shoulder
        if (player.GetButton(inputLibrary.Right_Shoulder))
        { Right_Shoulder.Invoke(); }
        if (player.GetButtonDown(inputLibrary.Right_Shoulder))
        { Right_Shoulder_Down.Invoke(); }
        if (player.GetButtonUp(inputLibrary.Right_Shoulder))
        { Right_Shoulder_Up.Invoke(); }

        //---------------------------------------------------------------

        // View (Select)
        if (player.GetButton(inputLibrary.View))
        { View.Invoke(); }
        if (player.GetButtonDown(inputLibrary.View))
        { View_Down.Invoke(); }
        if (player.GetButtonUp(inputLibrary.View))
        { View_Up.Invoke(); }


        // Menu (Start)
        if (player.GetButton(inputLibrary.Menu))
        { Menu.Invoke(); }
        if (player.GetButtonDown(inputLibrary.Menu))
        { Menu_Down.Invoke(); }
        if (player.GetButtonUp(inputLibrary.Menu))
        { Menu_Down.Invoke(); }


        // Guide (Device Button, Xbox button, PS4 button etc.)
        if (player.GetButton(inputLibrary.Guide))
        { Guide.Invoke(); }
        if (player.GetButtonDown(inputLibrary.Guide))
        { Guide_Down.Invoke(); }
        if (player.GetButtonUp(inputLibrary.Guide))
        { Guide_Up.Invoke(); }

        //---------------------------------------------------------------

        // Left Stick

        if (player.GetButton(inputLibrary.Left_Stick_Button))
        { Left_Stick_Button.Invoke(); }
        if (player.GetButtonDown(inputLibrary.Left_Stick_Button))
        { Left_Stick_Button_Down.Invoke(); }
        if (player.GetButtonUp(inputLibrary.Left_Stick_Button))
        { Left_Stick_Button_Up.Invoke(); }

        // Right Stick

        if (player.GetButton(inputLibrary.Right_Stick_Button))
        { Right_Stick_Button.Invoke(); }
        if (player.GetButtonDown(inputLibrary.Right_Stick_Button))
        { Right_Stick_Button_Down.Invoke(); }
        if (player.GetButtonUp(inputLibrary.Right_Stick_Button))
        { Right_Stick_Button_Up.Invoke(); }

        //---------------------------------------------------------------

        // D-Pad


        // D-Pad Up

        if (player.GetButton(inputLibrary.Dpad_Up))
        { Dpad_Up.Invoke(); }
        if (player.GetButtonDown(inputLibrary.Dpad_Up))
        { Dpad_Up_Down.Invoke(); }
        if (player.GetButtonUp(inputLibrary.Dpad_Up))
        { Dpad_Up_Up.Invoke(); }

        // D-Pad Right

        if (player.GetButton(inputLibrary.Dpad_Right))
        { Dpad_Right.Invoke(); }
        if (player.GetButtonDown(inputLibrary.Dpad_Right))
        { Dpad_Right_Down.Invoke(); }
        if (player.GetButtonUp(inputLibrary.Dpad_Right))
        { Dpad_Right_Up.Invoke(); }

        // D-Pad Down

        if (player.GetButton(inputLibrary.Dpad_Down))
        { Dpad_Down.Invoke(); }
        if (player.GetButtonDown(inputLibrary.Dpad_Down))
        { Dpad_Down_Down.Invoke(); }
        if (player.GetButtonUp(inputLibrary.Dpad_Down))
        { Dpad_Down_Up.Invoke(); }

        // D-Pad Left

        if (player.GetButton(inputLibrary.Dpad_Left))
        { Dpad_Left.Invoke(); }
        if (player.GetButtonDown(inputLibrary.Dpad_Left))
        { Dpad_Left_Down.Invoke(); }
        if (player.GetButtonUp(inputLibrary.Dpad_Left))
        { Dpad_Left_Up.Invoke(); }

    }

    #endregion

    //---------------------------------------------------------------

    #region Events

    // LEFT ANALOG STICK Press Down

    public Event_Function Left_Stick_Button = new Event_Function();
    public Event_Function Left_Stick_Button_Down = new Event_Function();
    public Event_Function Left_Stick_Button_Up = new Event_Function();


    // RIGHT ANALOG STICK Press Down

    public Event_Function Right_Stick_Button = new Event_Function();
    public Event_Function Right_Stick_Button_Down = new Event_Function();
    public Event_Function Right_Stick_Button_Up = new Event_Function();


    //---------------------------------------------------------------


    // A
    public Event_Function A = new Event_Function();
    public Event_Function A_Down = new Event_Function();
    public Event_Function A_Up = new Event_Function();


    // B
    public Event_Function B = new Event_Function();
    public Event_Function B_Down = new Event_Function();
    public Event_Function B_Up = new Event_Function();


    // X
    public Event_Function X = new Event_Function();
    public Event_Function X_Down = new Event_Function();
    public Event_Function X_Up = new Event_Function();


    // Y
    public Event_Function Y = new Event_Function();
    public Event_Function Y_Down = new Event_Function();
    public Event_Function Y_Up = new Event_Function();


    //---------------------------------------------------------------


    // Left Shoulder
    public Event_Function Left_Shoulder = new Event_Function();
    public Event_Function Left_Shoulder_Down = new Event_Function();
    public Event_Function Left_Shoulder_Up = new Event_Function();


    // Right Shoulder
    public Event_Function Right_Shoulder = new Event_Function();
    public Event_Function Right_Shoulder_Down = new Event_Function();
    public Event_Function Right_Shoulder_Up = new Event_Function();


    //---------------------------------------------------------------


    // View
    public Event_Function View = new Event_Function();
    public Event_Function View_Down = new Event_Function();
    public Event_Function View_Up = new Event_Function();


    // Menu
    public Event_Function Menu = new Event_Function();
    public Event_Function Menu_Down = new Event_Function();
    public Event_Function Menu_Up = new Event_Function();


    // guide
    public Event_Function Guide = new Event_Function();
    public Event_Function Guide_Down = new Event_Function();
    public Event_Function Guide_Up = new Event_Function();

    // D-PAD

    public Event_Function Dpad_Up = new Event_Function();
    public Event_Function Dpad_Up_Down = new Event_Function();
    public Event_Function Dpad_Up_Up = new Event_Function();

    public Event_Function Dpad_Right = new Event_Function();
    public Event_Function Dpad_Right_Down = new Event_Function();
    public Event_Function Dpad_Right_Up = new Event_Function();

    public Event_Function Dpad_Down = new Event_Function();
    public Event_Function Dpad_Down_Down = new Event_Function();
    public Event_Function Dpad_Down_Up = new Event_Function();

    public Event_Function Dpad_Left = new Event_Function();
    public Event_Function Dpad_Left_Down = new Event_Function();
    public Event_Function Dpad_Left_Up = new Event_Function();


    //---------------------------------------------------------------

    public void Reset()
    {
        // ANALOG STICKS

        _leftStick_X = 0;
        _leftStick_Y = 0;
        _rightStick_X = 0;
        _rightStick_Y = 0;
        _mouseX = 0;
        _mouseY = 0;

        // Triggers

        _leftTrigger = 0;
        _rightTrigger = 0;

        // ABXY

        A.Clear();
        A_Down.Clear();
        A_Up.Clear();

        B.Clear();
        B_Down.Clear();
        B_Up.Clear();

        X.Clear();
        X_Down.Clear();
        X_Up.Clear();

        Y.Clear();
        Y_Down.Clear();
        Y_Up.Clear();

        // Shoulders

        Left_Shoulder.Clear();
        Left_Shoulder_Down.Clear();
        Left_Shoulder_Up.Clear();

        Right_Shoulder.Clear();
        Right_Shoulder_Down.Clear();
        Right_Shoulder_Up.Clear();

        // View, Menu, Guide

        View.Clear();
        View_Down.Clear();
        View_Up.Clear();

        Menu.Clear();
        Menu.Clear();
        Menu.Clear();

        Guide.Clear();
        Guide.Clear();
        Guide.Clear();

        // Stick Buttons

        Left_Stick_Button.Clear();
        Left_Stick_Button_Down.Clear();
        Left_Stick_Button_Up.Clear();

        Right_Stick_Button.Clear();
        Right_Stick_Button_Down.Clear();
        Right_Stick_Button_Up.Clear();

        // D-PAD

        Dpad_Up.Clear();
        Dpad_Up_Down.Clear();
        Dpad_Up_Up.Clear();

        Dpad_Right.Clear();
        Dpad_Right_Down.Clear();
        Dpad_Right_Up.Clear();

        Dpad_Down.Clear();
        Dpad_Down_Down.Clear();
        Dpad_Down_Up.Clear();

        Dpad_Left.Clear();
        Dpad_Left_Down.Clear();
        Dpad_Left_Up.Clear();
    }

    public void Ping()
    {
        Debug.Log(name + "PING!");
    }

    #endregion

    //---------------------------------------------------------------

}
