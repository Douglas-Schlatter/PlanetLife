using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataGraphics : MonoBehaviour
{
    // lista com os valores de poluição da atmosfera
    public List<float> atmospherePollutionValues = new List<float>();

    // objeto no qual os gráficos serão desenhados
    public GameObject graphicsObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // função para desenhar gráficos
    public void DrawGraphics()
    {
        // limpa os gráficos anteriores
        foreach (Transform child in graphicsObject.transform)
        {
            Destroy(child.gameObject);
        }

        // desenha os gráficos
        for (int i = 0; i < atmospherePollutionValues.Count; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(i, atmospherePollutionValues[i], 0);
            cube.transform.localScale = new Vector3(1, atmospherePollutionValues[i], 1);
            cube.transform.parent = graphicsObject.transform;
        }
    }
}
