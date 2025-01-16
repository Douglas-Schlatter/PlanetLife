using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeAmbientColor : MonoBehaviour
{
    public Material planetMaterial;
    public Color testColor = Color.red;
    private string bottomColor = "_Bottom_Color";
    private string midColor = "_Mid_Color";
    private string topColor = "_TopColor";
    
    private string endWaterColor = "#482D24";
    private string endGroundColor = "#84741F";

    void Awake()
    {
        planetMaterial = GetComponent<Renderer>().material;
        Debug.Log("Material name: " + planetMaterial.name);
    }

    void Start()
    {
        //ChangeWaterColor(testColor);

        Color colorWater = planetMaterial.GetColor(bottomColor);
        // Convert hex to color
        Color colorWaterEnd = new Color();
        UnityEngine.ColorUtility.TryParseHtmlString(endWaterColor, out colorWaterEnd);

        FromColorToAnother(colorWater, colorWaterEnd, 20, bottomColor);

        Color colorGround = planetMaterial.GetColor(midColor);
        // Convert hex to color
        Color colorGroundEnd = new Color();
        UnityEngine.ColorUtility.TryParseHtmlString(endGroundColor, out colorGroundEnd);

        FromColorToAnother(colorGround, colorGroundEnd, 20, midColor);
    }

    public void ChangeWaterColor(Color newColor)
    {
        if (planetMaterial.HasProperty(bottomColor))
        {
            planetMaterial.SetColor(bottomColor, newColor);
            Debug.Log("TEM");
        }
        else
        {
            Debug.LogWarning("The material does not have a color property named " + "Bottom Color");
        }
    }

    public void FromColorToAnother(Color fromColor, Color toColor, float duration, string field)
    {
        StartCoroutine(ChangeColor(fromColor, toColor, duration, field));
    }

    private IEnumerator ChangeColor(Color fromColor, Color toColor, float duration, string field)
    {
        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            planetMaterial.SetColor(field, Color.Lerp(fromColor, toColor, timeElapsed / duration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        planetMaterial.SetColor(field, toColor);
    }

}
