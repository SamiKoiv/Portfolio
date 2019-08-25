using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// TO DO: Highlight Color Lerp

public class UI_Juicer : ManagedBehaviour_Update, IPointerEnterHandler, IPointerExitHandler
{
    #region Public Variables

    [SerializeField] Main _Main = new Main { PlayOnEnable = true, LerpProgression = true };
    [SerializeField] OpenOptions _OnOpen = new OpenOptions() { StartScaleModifier = Vector3.one };
    [SerializeField] CloseOptions _OnClose = new CloseOptions() { ReversedOpen = true, EndScaleModifier = Vector3.one };
    [SerializeField] HighlightOptions _Highlight = new HighlightOptions() { ScaleModifier = Vector2.one, ImageColor = Color.magenta, ImageTintLerp = 1, TextColor = Color.magenta, TextTintLerp = 1 };
    [SerializeField] ButtonOptions _Button = new ButtonOptions();
    [SerializeField] References _References = new References();

    [System.Serializable]
    public struct Main
    {
        public GameObject JuicedObject;
        public bool PlayOnEnable;
        public bool LerpProgression;
        public EventBool OverrideIsOpen;

        public float LerpSpeed;
        public Float_ReadOnly OverrideLerpSpeed;

        [Header("Gizmos")]
        public bool Gizmos;
    }

    [System.Serializable]
    public struct OpenOptions
    {
        public bool MoveIn;
        public bool RotateIn;
        public bool ScaleIn;
        public bool FadeIn;

        [Space(10)]
        public Vector3 StartPosition;
        public Vector3 StartRotation;
        public Vector3 StartScaleModifier;
        public AnimationCurve OpenCurve;
        public AnimationCurve_ReadOnly OverrideOpenCurve;
        [Space(10)]
        public UnityEvent OnComplete;
    }

    [System.Serializable]
    public struct CloseOptions
    {
        public bool ReversedOpen;
        public bool MoveOut;
        public bool RotateOut;
        public bool ScaleOut;
        public bool FadeOut;

        [Space(10)]
        public Vector3 EndPosition;
        public Vector3 EndRotation;
        public Vector3 EndScaleModifier;
        public AnimationCurve CloseCurve;
        public AnimationCurve_ReadOnly OverrideCloseCurve;
        [Space(10)]
        public UnityEvent OnComplete;
    }

    [System.Serializable]
    public struct HighlightOptions
    {
        public bool LerpedHighlight;
        public bool HighlightImageColors;
        public bool HighlightTextColors;
        public bool HighlightScale;
        [Space(10)]
        public Color ImageColor;
        [Range(0, 1)] public float ImageTintLerp;
        public Color TextColor;
        [Range(0, 1)] public float TextTintLerp;
        [Space(10)]
        public Vector2 ScaleModifier;
    }

    [System.Serializable]
    public struct ButtonOptions
    {
        public Button JuicedButton;
        public Color PressedColor;
        public Color SelectionColor;
        public float FadeDuration;
        public Navigation ButtonNavigation;
    }

    [System.Serializable]
    public struct References
    {
        public Image[] JuicedImages;
        public Color[] DefaultImageColors;
        public TextMeshProUGUI[] JuicedTexts;
        public Color[] DefaultTextColors;
    }

    #endregion

    #region Private Variables

    int innerLoopBatchCount = 100;

    // Start variables
    Transform JuicedT;
    EventBool openingOverride;
    Vector3 defaultPosition;
    Vector3 startPosition;

    Quaternion defaultRotation;
    Quaternion startRotation;

    Color startColor;
    float prog;
    float curvedProg;

    // Highlight variables
    bool highlighted;
    float highlightProg;

    // Click variables
    bool clicked;

    Vector3 defaultScale;
    Vector3 currentScale;

    float deltaTime;
    State state;

    bool scaleUpdated;

    enum State
    {
        Opening,
        Idle,
        Closing
    }

    float LerpSpeed
    {
        get
        {
            if (_Main.OverrideLerpSpeed != null)
                return _Main.OverrideLerpSpeed.Value;
            else
                return _Main.LerpSpeed;
        }
    }

    AnimationCurve OpenCurve
    {
        get
        {
            if (_OnOpen.OverrideOpenCurve != null)
                return _OnOpen.OverrideOpenCurve.Curve;
            else
                return _OnOpen.OpenCurve;
        }
    }

    AnimationCurve CloseCurve
    {
        get
        {
            if (_OnClose.OverrideCloseCurve != null)
                return _OnClose.OverrideCloseCurve.Curve;
            else
                return _OnClose.CloseCurve;
        }
    }

    #endregion

    #region Monobehaviour

    private void Awake()
    {
        if (_Main.JuicedObject == null)
        {
            Debug.Log("UI Juicer on " + gameObject + " has no JuicedObject set");
            return;
        }

        _Main.JuicedObject.SetActive(false);
        JuicedT = _Main.JuicedObject.transform;

        state = State.Idle;

        defaultPosition = JuicedT.position;
        startPosition = defaultPosition + _OnOpen.StartPosition;

        defaultRotation = JuicedT.rotation;
        startRotation = Quaternion.Euler(defaultRotation.eulerAngles + _OnOpen.StartRotation);

        defaultScale = JuicedT.localScale;
        currentScale = JuicedT.localScale;

        openingOverride = _Main.OverrideIsOpen;

        Initialize();
    }

    private void OnEnable()
    {
        Subscribe_Update();

        if (openingOverride != null)
        {
            openingOverride.OnChange += OverrideSwitch;
        }
        else if (_Main.PlayOnEnable)
        {
            Open();
        }
    }

    private void OnDisable()
    {
        Unsubscribe_Update();

        if (openingOverride != null)
        {
            openingOverride.OnChange -= OverrideSwitch;
        }
        else if (_Main.PlayOnEnable)
        {
            Stop();
        }

        Initialize();
    }

    private void OnDrawGizmos()
    {
        if (!_Main.Gizmos || _Main.JuicedObject == null)
            return;

        JuicedT = _Main.JuicedObject.transform;

        if (_OnOpen.MoveIn)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(JuicedT.position, JuicedT.position + _OnOpen.StartPosition);
        }

        if (!_OnClose.ReversedOpen && _OnClose.MoveOut)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(JuicedT.position, JuicedT.position + _OnClose.EndPosition);
        }
    }

    #endregion

    #region Managed Update

    public override void M_Update()
    {
        deltaTime = Time.deltaTime;

        switch (state)
        {
            case State.Opening:
                if (!_OnOpen.MoveIn && !_OnOpen.RotateIn && !_OnOpen.ScaleIn && !_OnOpen.FadeIn)
                {
                    prog = 1;
                }
                else
                {
                    if (_Main.LerpProgression)
                        prog = Mathf.Lerp(prog, 1, LerpSpeed * deltaTime);
                    else
                        prog = Mathf.Min(prog + (LerpSpeed * deltaTime), 1);
                }

                ProcessOpen();
                ProcessHighlight();
                CheckForIdle();
                break;

            case State.Idle:
                ProcessHighlight();
                break;

            case State.Closing:
                if (!_OnClose.ReversedOpen && !_OnClose.MoveOut && !_OnClose.RotateOut && !_OnClose.ScaleOut && !_OnClose.FadeOut)
                    prog = 0;
                else
                {
                    if (_Main.LerpProgression)
                        prog = Mathf.Lerp(prog, 0, LerpSpeed * deltaTime);
                    else
                        prog = Mathf.Max(prog - LerpSpeed * deltaTime, 0);
                }

                if (_OnClose.ReversedOpen)
                    ProcessOpen();
                else
                    ProcessClose();

                CheckForIdle();

                if (state == State.Idle)
                    _Main.JuicedObject.SetActive(false);
                break;
        }

        if (scaleUpdated)
        {
            JuicedT.localScale = currentScale;
            scaleUpdated = false;
        }
    }

    #endregion

    #region States

    void Initialize()
    {
        DoMoveIn(0);
        DoRotateIn(0);
        DoScaleIn(0);
        DoFadeIn(0);
    }

    void OverrideSwitch(bool open)
    {
        if (open)
        {
            Open();
        }
        else
        {
            state = State.Closing;
        }
    }

    public void Open()
    {
        _Main.JuicedObject.SetActive(true);
        state = State.Opening;
    }

    public void Close()
    {
        state = State.Closing;
    }

    void Stop()
    {
        if (JuicedT == null)
            return;

        JuicedT.position = defaultPosition;
        prog = 0;

        Initialize();
    }

    void CheckForIdle()
    {
        if (state == State.Opening && prog >= 0.9999f)
        {
            state = State.Idle;

            if (_OnOpen.OnComplete != null)
                _OnOpen.OnComplete.Invoke();
        }
        else if (state == State.Closing && prog <= 0.0001f)
        {
            state = State.Idle;

            if (_OnClose.OnComplete != null)
                _OnClose.OnComplete.Invoke();
        }
    }

    #endregion

    #region Start & Close

    void ProcessOpen()
    {
        if (_Main.LerpProgression)
            curvedProg = prog;
        else
            curvedProg = OpenCurve.Evaluate(prog);

        if (OpenCurve.length != 0)
        {
            DoMoveIn(curvedProg);
            DoRotateIn(curvedProg);
            DoScaleIn(curvedProg);
            DoFadeIn(curvedProg);
        }
        else
        {
            DoMoveIn(prog);
            DoRotateIn(prog);
            DoScaleIn(prog);
            DoFadeIn(prog);
        }
    }

    void ProcessClose()
    {
        curvedProg = CloseCurve.Evaluate(prog);

        if (CloseCurve.length != 0)
        {
            DoMoveIn(curvedProg);
            DoRotateIn(curvedProg);
            DoScaleIn(curvedProg);
            DoFadeIn(curvedProg);
        }
        else
        {
            DoMoveIn(prog);
            DoRotateIn(prog);
            DoScaleIn(prog);
            DoFadeIn(prog);
        }
    }

    void DoMoveIn(float prog)
    {
        if (_OnOpen.MoveIn)
            JuicedT.position = Vector3.Lerp(startPosition, defaultPosition, prog);
    }

    void DoRotateIn(float prog)
    {
        if (_OnOpen.RotateIn)
            JuicedT.rotation = Quaternion.Lerp(startRotation, defaultRotation, prog);
    }

    void DoScaleIn(float prog)
    {
        if (_OnOpen.ScaleIn)
        {
            currentScale = Vector3.Lerp(Vector3.zero, defaultScale, prog);
            scaleUpdated = true;
        }
    }

    void DoFadeIn(float prog)
    {
        if (!_OnOpen.FadeIn)
            return;

        foreach (Image i in _References.JuicedImages)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, prog);
        }

        foreach (TextMeshProUGUI t in _References.JuicedTexts)
        {
            t.color = new Color(t.color.r, t.color.g, t.color.b, prog);
        }
    }

    #endregion

    #region Highlight

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlighted = true;

        if (!_Highlight.LerpedHighlight)
        {
            highlightProg = 1;
            DoHighlightColor();
            DoHighlightScale();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlighted = false;

        if (!_Highlight.LerpedHighlight)
        {
            highlightProg = 0;
            DoHighlightColor();
            DoHighlightScale();
        }
    }

    void ProcessHighlight()
    {
        if (!_Highlight.LerpedHighlight)
            return;

        if (highlighted)
            highlightProg = Mathf.Lerp(highlightProg, 1, LerpSpeed * deltaTime);
        else
            highlightProg = Mathf.Lerp(highlightProg, 0, LerpSpeed * deltaTime);

        DoHighlightColor();
        DoHighlightScale();
    }

    void DoHighlightColor()
    {
        if (_Highlight.HighlightImageColors)
        {
            if (highlighted)
            {
                for (int i = 0; i < _References.JuicedImages.Length; i++)
                {
                    _References.JuicedImages[i].color = Color.Lerp(_References.DefaultImageColors[i], _Highlight.ImageColor, highlightProg * _Highlight.ImageTintLerp);
                }
            }
            else
            {
                for (int i = 0; i < _References.JuicedImages.Length; i++)
                {
                    _References.JuicedImages[i].color = Color.Lerp(_References.DefaultImageColors[i], _References.JuicedImages[i].color, highlightProg * _Highlight.ImageTintLerp);
                }
            }
        }

        if (_Highlight.HighlightTextColors)
        {
            if (highlighted)
            {
                for (int i = 0; i < _References.JuicedTexts.Length; i++)
                {
                    _References.JuicedTexts[i].color = Color.Lerp(_References.DefaultTextColors[i], _Highlight.TextColor, highlightProg * _Highlight.TextTintLerp);
                }
            }
            else
            {
                for (int i = 0; i < _References.JuicedTexts.Length; i++)
                {
                    _References.JuicedTexts[i].color = Color.Lerp(_References.DefaultTextColors[i], _References.JuicedTexts[i].color, highlightProg * _Highlight.TextTintLerp);
                }
            }
        }

    }

    void DoHighlightScale()
    {
        if (!_Highlight.HighlightScale)
            return;

        if (highlighted)
        {
            float x = defaultScale.x * _Highlight.ScaleModifier.x;
            float y = defaultScale.y * _Highlight.ScaleModifier.y;
            float z = defaultScale.z;

            Vector3 targetScale = new Vector3(x, y, z);
            currentScale = Vector3.Lerp(JuicedT.localScale, targetScale, highlightProg);
        }
        else
        {
            Vector3 targetScale = defaultScale;
            currentScale = Vector3.Lerp(targetScale, JuicedT.localScale, highlightProg);
        }

        scaleUpdated = true;
    }

    #endregion

    #region Click



    #endregion

    #region Context Menu Functions

    [ContextMenu("Restart")]
    public void Restart()
    {
        OnDisable();
        prog = 0;
        OnEnable();
    }

    [ContextMenu("Get All References")]
    public void GetReferences()
    {
        _References.JuicedImages = GetComponentsInChildren<Image>();
        _References.JuicedTexts = GetComponentsInChildren<TextMeshProUGUI>();

        GetReference_ColorsOnly();

        // Check if there is a Button and set colors.

        if (_Button.JuicedButton != null)
        {
            Image image = _Button.JuicedButton.GetComponent<Image>();
            TextMeshProUGUI text = _Button.JuicedButton.GetComponent<TextMeshProUGUI>();

            if (image != null)
            {
                ColorBlock colors = new ColorBlock
                {
                    normalColor = image.color,
                    highlightedColor = Color.Lerp(image.color, _Highlight.ImageColor, _Highlight.ImageTintLerp),
                    selectedColor = _Button.SelectionColor,
                    pressedColor = _Button.PressedColor,
                    colorMultiplier = 1,
                    fadeDuration = _Button.FadeDuration
                };

                _Button.JuicedButton.colors = colors;
            }

            if (text != null)
            {
                ColorBlock colors = new ColorBlock
                {
                    normalColor = text.color,
                    highlightedColor = Color.Lerp(text.color, _Highlight.TextColor, _Highlight.TextTintLerp),
                    selectedColor = _Button.SelectionColor,
                    pressedColor = _Button.PressedColor,
                    colorMultiplier = 1,
                    fadeDuration = _Button.FadeDuration
                };

                _Button.JuicedButton.colors = colors;
            }

        }

    }

    [ContextMenu("Get Colors Only")]
    public void GetReference_ColorsOnly()
    {
        _References.DefaultImageColors = new Color[_References.JuicedImages.Length];
        for (int i = 0; i < _References.JuicedImages.Length; i++)
        {
            _References.DefaultImageColors[i] = _References.JuicedImages[i].color;
        }

        _References.DefaultTextColors = new Color[_References.JuicedTexts.Length];
        for (int i = 0; i < _References.JuicedTexts.Length; i++)
        {
            _References.DefaultTextColors[i] = _References.JuicedTexts[i].color;
        }

        // Setup Button Colors
        SetupButtonColors();
    }

    void SetupButtonColors()
    {
        if (_Button.JuicedButton != null)
        {
            Image image = _Button.JuicedButton.GetComponent<Image>();
            TextMeshProUGUI text = _Button.JuicedButton.GetComponent<TextMeshProUGUI>();

            if (image != null)
            {
                ColorBlock colors = new ColorBlock
                {
                    normalColor = image.color,
                    highlightedColor = Color.Lerp(image.color, _Highlight.ImageColor, _Highlight.ImageTintLerp),
                    selectedColor = _Button.SelectionColor,
                    pressedColor = _Button.PressedColor,
                    colorMultiplier = 1,
                    fadeDuration = _Button.FadeDuration
                };

                _Button.JuicedButton.colors = colors;
            }

            if (text != null)
            {
                ColorBlock colors = new ColorBlock
                {
                    normalColor = text.color,
                    highlightedColor = Color.Lerp(text.color, _Highlight.TextColor, _Highlight.TextTintLerp),
                    selectedColor = _Button.SelectionColor,
                    pressedColor = _Button.PressedColor,
                    colorMultiplier = 1,
                    fadeDuration = _Button.FadeDuration
                };

                _Button.JuicedButton.colors = colors;
            }

        }
    }

    #endregion
}
