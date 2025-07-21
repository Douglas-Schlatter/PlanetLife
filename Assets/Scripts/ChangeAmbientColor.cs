using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;

public class ChangeAmbientColor : MonoBehaviour
{
    public Material planetMaterial;

    [Header("Hex Colors")]
    public string endWaterColorHex = "#482D24";
    public string endGroundColorHex = "#84741F";

    [Header("Material Color Properties")]
    public string bottomColor = "_Bottom_Color";
    public string midColor = "_Mid_Color";
    public string topColor = "_TopColor";

    [Header("Ground Transition")]
    [Range(0, 100)] public float transitionValueGround = 0;
    public float tGround;
    public Color startGroundColor;
    public Color endGroundColor;

    [Header("Water Transition")]
    [Range(0, 100)] public float transitionValueWater = 0;
    public float tWater;
    public Color startWaterColor;
    public Color endWaterColor;

    private void Awake()
    {
        planetMaterial = GetComponent<Renderer>().material;
        Debug.Log("Material name: " + planetMaterial.name);

        // Salvar cores iniciais
        startGroundColor = planetMaterial.GetColor(midColor);
        startWaterColor = planetMaterial.GetColor(bottomColor);

        // Converter hex para Color
        endGroundColor = HexToColor(endGroundColorHex);
        endWaterColor = HexToColor(endWaterColorHex);
    }

    private void Update()
    {
        // Normalizar de 0 a 1
        tGround = transitionValueGround / 100f;
        tWater = transitionValueWater / 100f;

        // Interpola cores
        ChangeColorT(startGroundColor, endGroundColor, tGround, midColor);
        ChangeColorT(startWaterColor, endWaterColor, tWater, bottomColor);
    }

    public Color HexToColor(string hex)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString(hex, out color);
        color.a = 1f;
        return color;
    }

    public void ChangeColorT(Color fromColor, Color toColor, float t, string property)
    {
        Color currentColor = Color.Lerp(fromColor, toColor, t);
        planetMaterial.SetColor(property, currentColor);
    }

}
