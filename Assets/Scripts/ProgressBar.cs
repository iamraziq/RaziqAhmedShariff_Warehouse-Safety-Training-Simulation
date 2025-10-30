using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text percentText;

    public void SetProgress(float value)
    {
        slider.value = Mathf.Clamp01(value);
        if (percentText) percentText.text = Mathf.RoundToInt(value * 100f) + "%";
    }
}
