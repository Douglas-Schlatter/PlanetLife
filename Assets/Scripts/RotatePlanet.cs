using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanet : MonoBehaviour
{
    public float rotationSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotaciona o objeto ao redor do eixo Y (para a direita)
        Vector3 rotation = transform.eulerAngles;
        rotation.y -= rotationSpeed * Time.deltaTime;
        transform.eulerAngles = rotation;
    }
}
