using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereChanger : MonoBehaviour
{
    private string atmosphereColorPolluted = "#FFD700";
    private string atmosphereColorClean = "#000000";
    public Material atmosphereMaterial;

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


}
