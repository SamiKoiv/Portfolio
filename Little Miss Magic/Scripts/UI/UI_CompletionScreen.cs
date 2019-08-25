using TMPro;
using UnityEngine;

public class UI_CompletionScreen : ManagedBehaviour_Update
{
    [SerializeField] TextMeshProUGUI textField;
    [SerializeField] string completionText;
    [SerializeField] float fadeinTime;
    [SerializeField] float screenDuration;
    [SerializeField] float fadeoutTime;

    float screenTimer;
    float fadeProg;

    void Awake()
    {
        textField.text = completionText;

        textField.color = new Color(
                textField.color.r,
                textField.color.g,
                textField.color.b,
                0);
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    void StartCompletionScreen(Quest quest)
    {
        textField.enabled = true;
        Subscribe_Update();

        screenTimer = 0;
    }

    public override void M_Update()
    {
        if (screenTimer < fadeinTime)
        {
            screenTimer += Time.deltaTime;

            fadeProg = Mathf.Min((screenTimer) / fadeinTime, 1);
            textField.color = new Color(
                textField.color.r,
                textField.color.g,
                textField.color.b,
                fadeProg);

            return;
        }
        else if (screenTimer >= screenDuration)
        {
            screenTimer += Time.deltaTime;

            fadeProg = 1 - Mathf.Min((screenTimer - screenDuration) / fadeoutTime, 1);
            textField.color = new Color(
                textField.color.r,
                textField.color.g,
                textField.color.b,
                fadeProg);

            return;
        }

        screenTimer += Time.deltaTime;

        if (screenTimer >= fadeinTime + screenDuration + fadeoutTime)
        {
            textField.enabled = false;
            Unsubscribe_Update();
        }
    }
}
