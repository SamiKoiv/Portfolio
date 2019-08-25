using UnityEngine;
using UnityEngine.UI;

public class UI_ChargeMeter : MonoBehaviour
{

    UI_Events ui_events;

    Text chargeText;
    Slider slider;

    int fontSize;

    public Color mainColor;
    public Color critColor;

    void Awake()
    {
        ui_events = Core.instance.UIEvents;
        chargeText = GetComponent<Text>();
        fontSize = chargeText.fontSize;
        slider = transform.Find("Slider").GetComponent<Slider>();
    }

    void OnEnable()
    {
        ui_events.Charge_Changed.Subscribe(OnChargeChange);
    }

    void OnDisable()
    {
        ui_events.Charge_Changed.Unsubscribe(OnChargeChange);
    }

    public void OnChargeChange()
    {
        TextSlider(ui_events.Charge_Get());
        Critical(ui_events.Critical_Get());
    }

    void TextSlider(float value)
    {
        chargeText.text = "" + (int) (value * 100);
        slider.value = value;
    }

    void Critical(bool critical)
    {
        if (critical)
        {
            chargeText.fontSize = fontSize + 20;
            chargeText.color = critColor;
        }
        else
        {
            chargeText.fontSize = fontSize;
            chargeText.color = mainColor;
        }
    }
}
