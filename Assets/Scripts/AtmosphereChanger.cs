using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereChanger : MonoBehaviour
{
    public string atmosphereColorPolluted = "#FFD700";
    public string atmosphereColorClean = "#000000";
    public Material atmosphereMaterial;
    [Range(0, 100)] // Slider no editor para testar
    public float transitionValue = 0;
    public float t;

    void Awake()
    {
        // muda a cor do material atmosphereMaterial
        atmosphereMaterial = GetComponent<Renderer>().material;

        Debug.Log("Material name: " + atmosphereMaterial.name);
        
        //ChangeAtmosphereColor(atmosphereColor);

        //FromColorToAnother(atmosphereColorClean, atmosphereColorPolluted, 20);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Normalizar o valor de 0 a 100 para 0 a 1
        t = transitionValue / 100f;

        // Muda a cor do material atmosphereMaterial
        ChangeColorT(atmosphereColorClean, atmosphereColorPolluted, t);
    }

    // função para mudar a cor do basemap do material atmosphereMaterial
    public void ChangeAtmosphereColor(string newColor)
    {
        //muda a cor do material atmosphereMaterial
        if (atmosphereMaterial.HasProperty("_Color"))
        {
            Debug.Log("Tem");

            // imprime a cor atual do material atmosphereMaterial
            Debug.Log("Current color: " + atmosphereMaterial.GetColor("_Color"));

            Color color = HexToColor(newColor);

            // muda a cor do material atmosphereMaterial
            atmosphereMaterial.color = color;
            
            

            // imprime a cor do material atmosphereMaterial após a mudança
            Debug.Log("New color: " + atmosphereMaterial.GetColor("_Color"));

        }
        else
        {
            Debug.LogWarning("The material does not have a color property named " + "_Color");
        }
    }

    // função para converter hex para RGBA
    public Color HexToColor(string hex)
    {
        // muda o alfa da cor para 1
        //hex += "FF";

        // converte a string hexadecimal para Color
        Color color = new Color();
        UnityEngine.ColorUtility.TryParseHtmlString(hex, out color);

        // alfa = 0
        color.a = 0;

        return color;
    }

    public void FromColorToAnother(string fromColor, string toColor, float duration)
    {
        StartCoroutine(ChangeColor(fromColor, toColor, duration));
    }

    private IEnumerator ChangeColor(string fromColor, string toColor, float duration)
    {
        float timeElapsed = 0;
        Color colorFrom = HexToColor(fromColor);
        Color colorTo = HexToColor(toColor);

        while (timeElapsed < duration)
        {
            atmosphereMaterial.color = Color.Lerp(colorFrom, colorTo, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        atmosphereMaterial.color = colorTo;
       
    }

    public void ChangeColorT(string fromColor, string toColor, float t)
    {
        Color colorFrom = HexToColor(fromColor);
        Color colorTo = HexToColor(toColor);

        // Calcular a cor intermediária
        Color currentColor = Color.Lerp(colorFrom, colorTo, t);

        atmosphereMaterial.color = currentColor;
    }


}
