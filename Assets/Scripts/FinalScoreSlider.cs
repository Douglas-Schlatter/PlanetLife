using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class FinalScoreSlider : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public float animationDuration = 5f;

    public TextMeshProUGUI sustentabilidadeText;
    public TextMeshProUGUI equilibrioText;

    private float targetValue = 0f;

    private float sustentabilidadeValue;
    private float equilibrioValue;
    public Image planetState;
    public Sprite[] planetStates;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI conscienciaText;
    public string conscienciaGood = "Ótima Consciência Ambiental!";
    public string conscienciaBad = "Baixa Consciência Ambiental!";
    public string conscienciaNeutral = "Consciência Ambiental Neutra!";

    void Start()
    {
        slider.value = 0f;
    }

    public void SetFinalStats(float score, float sustentabilidade, float equilibrio)
    {
        targetValue = Mathf.Clamp01(score / 100f);

        sustentabilidadeValue = sustentabilidade;
        equilibrioValue = equilibrio;

        StartCoroutine(AnimateAll());
    }

    private IEnumerator AnimateAll()
    {
        yield return StartCoroutine(AnimateText(equilibrioText, equilibrioValue, "Equilíbrio: "));
        yield return StartCoroutine(AnimateText(sustentabilidadeText, sustentabilidadeValue, "Sustentabilidade: "));
        yield return StartCoroutine(AnimateSlider());
    }

    private IEnumerator AnimateText(TextMeshProUGUI text, float target, string prefix)
    {
        float duration = 1f;
        float elapsed = 0f;
        float current = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / duration);
            current = Mathf.Lerp(0, target, progress);
            text.text = prefix + Mathf.RoundToInt(current) + "%";
            yield return null;
        }

        text.text = prefix + Mathf.RoundToInt(target) + "%";
    }

    private IEnumerator AnimateSlider()
    {
        float elapsedTime = 0f;
        float startValue = slider.value;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / animationDuration);
            slider.value = Mathf.Lerp(startValue, targetValue, progress);
            // atualiza o texto do score
            scoreText.text = Mathf.RoundToInt(slider.value * 100) + "%";
            UpdateColor(slider.value);
            yield return null;
        }

        slider.value = targetValue;
        UpdateColor(slider.value);

        // Atualiza o texto de consciência ambiental
        if (slider.value < 0.33f)
        {
            conscienciaText.text = conscienciaBad;
        }
        else if (slider.value < 0.66f)
        {
            conscienciaText.text = conscienciaNeutral;
        }
        else
        {
            conscienciaText.text = conscienciaGood;
        }

        if (slider.value < 0.33f)
        {
            planetState.sprite = planetStates[0];
            // define o alfa para o máximo
            Color color = planetState.color;
            color.a = 1f;
            planetState.color = color;
        }
        else
        {
            planetState.sprite = planetStates[1];
            // define o alfa para o mínimo
            Color color = planetState.color;
            color.a = 1;
            planetState.color = color;
        }
        
    }

    private void UpdateColor(float value)
    {
        if (value < 0.33f)
            fill.color = new Color(0.8f, 0.2f, 0.2f);

        else if (value < 0.66f)
            fill.color = new Color(1f, 0.85f, 0.2f);
        else
            fill.color = new Color(0.3f, 0.8f, 0.3f);


        
    }
}