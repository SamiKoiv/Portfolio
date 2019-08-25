using UnityEngine;
using Rewired;

public class Input_Tester : MonoBehaviour
{

    #region Variables

    public PlayerInput inputEvents;
    public InputLibrary inputLibrary;

    [Header("PLAYER ID FROM PLAYABLE CHARACTER")]

    public int PlayerId;

    public Player player;

    [Header("INPUT EVENTS FEED")]

    [Header("Axes:")]

    public float Left_Stick_X;
    public float Left_Stick_Y;
    public float Right_Stick_X;
    public float Right_Stick_Y;
    public float Left_Trigger;
    public float Right_Trigger;

    [Header("Buttons:")]

    [Space(10)]

    public bool A;
    public bool B;
    public bool X;
    public bool Y;
    public bool Left_Shoulder;
    public bool Right_Shoulder;
    public bool View;
    public bool Menu;
    public bool Guide;
    public bool Left_Stick_Button;
    public bool Right_Stick_Button;
    public bool Dpad_Up;
    public bool Dpad_Right;
    public bool Dpad_Down;
    public bool Dpad_Left;

    #endregion

    private void OnEnable()
    {
        player = ReInput.players.GetPlayer(0);

        PlayerId = 0;

        SubscribeInputs();

    }

    private void OnDisable()
    {
        UnsubscribeInputs();
    }

    private void Update()
    {
        Left_Stick_X = inputEvents.Left_Stick_X;
        Left_Stick_Y = inputEvents.Left_Stick_Y;
        Right_Stick_X = inputEvents.Right_Stick_X;
        Right_Stick_Y = inputEvents.Right_Stick_Y;
        Left_Trigger = inputEvents.Left_Trigger;
        Right_Trigger = inputEvents.Right_Trigger;
    }

    void SubscribeInputs()
    {
        inputEvents.A_Down.Subscribe(A_Down);
        inputEvents.A_Up.Subscribe(A_Up);

        inputEvents.B_Down.Subscribe(B_Down);
        inputEvents.B_Up.Subscribe(B_Up);

        inputEvents.X_Down.Subscribe(X_Down);
        inputEvents.X_Up.Subscribe(X_Up);

        inputEvents.Y_Down.Subscribe(Y_Down);
        inputEvents.Y_Up.Subscribe(Y_Up);

        inputEvents.Left_Shoulder_Down.Subscribe(Left_Shoulder_Down);
        inputEvents.Left_Shoulder_Up.Subscribe(Left_Shoulder_Up);

        inputEvents.Right_Shoulder_Down.Subscribe(Right_Shoulder_Down);
        inputEvents.Right_Shoulder_Up.Subscribe(Right_Shoulder_Up);

        inputEvents.View_Down.Subscribe(View_Down);
        inputEvents.View_Up.Subscribe(View_Up);

        inputEvents.Menu_Down.Subscribe(Menu_Down);
        inputEvents.Menu_Up.Subscribe(Menu_Up);

        inputEvents.Guide_Down.Subscribe(Guide_Down);
        inputEvents.Guide_Up.Subscribe(Guide_Up);

        inputEvents.Left_Stick_Button_Down.Subscribe(Left_Stick_Button_Down);
        inputEvents.Left_Stick_Button_Up.Subscribe(Left_Stick_Button_Up);

        inputEvents.Right_Stick_Button_Down.Subscribe(Right_Stick_Button_Down);
        inputEvents.Right_Stick_Button_Up.Subscribe(Right_Stick_Button_Up);

        inputEvents.Dpad_Up_Down.Subscribe(Dpad_Up_Down);
        inputEvents.Dpad_Up_Up.Subscribe(Dpad_Up_Up);

        inputEvents.Dpad_Right_Down.Subscribe(Dpad_Right_Down);
        inputEvents.Dpad_Right_Up.Subscribe(Dpad_Right_Up);

        inputEvents.Dpad_Down_Down.Subscribe(Dpad_Down_Down);
        inputEvents.Dpad_Down_Up.Subscribe(Dpad_Down_Up);

        inputEvents.Dpad_Left_Down.Subscribe(Dpad_Left_Down);
        inputEvents.Dpad_Left_Up.Subscribe(Dpad_Left_Up);
    }

    void UnsubscribeInputs()
    {
        inputEvents.A_Down.Unsubscribe(A_Down);
        inputEvents.A_Up.Unsubscribe(A_Up);

        inputEvents.B_Down.Unsubscribe(B_Down);
        inputEvents.B_Up.Unsubscribe(B_Up);

        inputEvents.X_Down.Unsubscribe(X_Down);
        inputEvents.X_Up.Unsubscribe(X_Up);

        inputEvents.Y_Down.Unsubscribe(Y_Down);
        inputEvents.Y_Up.Unsubscribe(Y_Up);

        inputEvents.Left_Shoulder_Down.Unsubscribe(Left_Shoulder_Down);
        inputEvents.Left_Shoulder_Up.Unsubscribe(Left_Shoulder_Up);

        inputEvents.Right_Shoulder_Down.Unsubscribe(Right_Shoulder_Down);
        inputEvents.Right_Shoulder_Up.Unsubscribe(Right_Shoulder_Up);

        inputEvents.View_Down.Unsubscribe(View_Down);
        inputEvents.View_Up.Unsubscribe(View_Up);

        inputEvents.Menu_Down.Unsubscribe(Menu_Down);
        inputEvents.Menu_Up.Unsubscribe(Menu_Up);

        inputEvents.Guide_Down.Unsubscribe(Guide_Down);
        inputEvents.Guide_Up.Unsubscribe(Guide_Up);

        inputEvents.Left_Stick_Button_Down.Unsubscribe(Left_Stick_Button_Down);
        inputEvents.Left_Stick_Button_Up.Unsubscribe(Left_Stick_Button_Up);

        inputEvents.Right_Stick_Button_Down.Unsubscribe(Right_Stick_Button_Down);
        inputEvents.Right_Stick_Button_Up.Unsubscribe(Right_Stick_Button_Up);

        inputEvents.Dpad_Up_Down.Unsubscribe(Dpad_Up_Down);
        inputEvents.Dpad_Up_Up.Unsubscribe(Dpad_Up_Up);

        inputEvents.Dpad_Right_Down.Unsubscribe(Dpad_Right_Down);
        inputEvents.Dpad_Right_Up.Unsubscribe(Dpad_Right_Up);

        inputEvents.Dpad_Down_Down.Unsubscribe(Dpad_Down_Down);
        inputEvents.Dpad_Down_Up.Unsubscribe(Dpad_Down_Up);

        inputEvents.Dpad_Left_Down.Unsubscribe(Dpad_Left_Down);
        inputEvents.Dpad_Left_Up.Unsubscribe(Dpad_Left_Up);
    }

    void A_Down() { A = true; }
    void A_Up() { A = false; }

    void B_Down() { B = true; }
    void B_Up() { B = false; }

    void X_Down() { X = true; }
    void X_Up() { X = false; }

    void Y_Down() { Y = true; }
    void Y_Up() { Y = false; }

    void Left_Shoulder_Down() { Left_Shoulder = true; }
    void Left_Shoulder_Up() { Left_Shoulder = false; }

    void Right_Shoulder_Down() { Right_Shoulder = true; }
    void Right_Shoulder_Up() { Right_Shoulder = false; }

    void View_Down() { View = true; }
    void View_Up() { View = false; }

    void Menu_Down() { Menu = true; }
    void Menu_Up() { Menu = false; }

    void Guide_Down() { Guide = true; }
    void Guide_Up() { Guide = false; }

    void Left_Stick_Button_Down() { Left_Stick_Button = true; }
    void Left_Stick_Button_Up() { Left_Stick_Button = false; }

    void Right_Stick_Button_Down() { Right_Stick_Button = true; }
    void Right_Stick_Button_Up() { Right_Stick_Button = false; }

    void Dpad_Up_Down() { Dpad_Up = true; }
    void Dpad_Up_Up() { Dpad_Up = false; }

    void Dpad_Right_Down() { Dpad_Right = true; }
    void Dpad_Right_Up() { Dpad_Right = false; }

    void Dpad_Down_Down() { Dpad_Down = true; }
    void Dpad_Down_Up() { Dpad_Down = false; }

    void Dpad_Left_Down() { Dpad_Left = true; }
    void Dpad_Left_Up() { Dpad_Left = false; }
}
