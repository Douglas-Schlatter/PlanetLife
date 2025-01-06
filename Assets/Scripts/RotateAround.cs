using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public float rotateSpeed;
    public float lightChange = 0.1f;
    public float duration = 1f;
    public float lightChangeSpeed = 0.1f;
    public GameObject pivotObject;
    public bool isRotating = false;
    public bool needsToRotate = false;
    public Vector3 target = new Vector3( 0,0,360);
    public Vector3 origPos;
    public Quaternion origRot;
    public Light sLight;
    // Start is called before the first frame update
    // void Start()
    // {
    //     origPos = transform.position;
    //     origRot = transform.rotation;
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     if (needsToRotate == true)
    //     {
    //         //Debug.Log("Entrou no if");
    //         // transform.RotateAround(pivotObject.transform.position, new Vector3(0, 0, 1), rotateSpeed * Time.deltaTime);
    //         StartCoroutine(rotateAround());
    //         needsToRotate = false;
    //     }
        
        
    // }

    public IEnumerator lightCycle() 
    {
        StartCoroutine(offIntensitiLights());
        yield return null;
    }

    public IEnumerator rotateAround()
    {

        float quantRotacionada = 0;
        StartCoroutine(offIntensitiLights());
       // Debug.Log("Entrou na rotateAround");
        while (quantRotacionada < 360)
        {

            quantRotacionada += rotateSpeed * Time.deltaTime;
            //Debug.Log("quantidadeRotacionada " + quantRotacionada.ToString());
            transform.RotateAround(pivotObject.transform.position, new Vector3(0, 0, 1), rotateSpeed * Time.deltaTime);
            yield return null;
        }
        transform.rotation = origRot;
        transform.position = origPos;
        yield return null;
    }

    public IEnumerator offIntensitiLights() 
    {
        float quantAlterada = 0;
        float t = 0.0f;
        lightChange = lightChangeSpeed;
        //while (quantAlterada < 2)
         while (t < duration)
        {
            t += Time.deltaTime;
            Debug.Log("IntensidadeAlterada" + quantAlterada.ToString());
            Debug.Log("Mathf.Lerp(2, 1, t / duration)" + (Mathf.Lerp(2, 1, t / duration)).ToString());

            Debug.Log("Mathf.Lerp(1, 2, t / duration)" + Mathf.Lerp(1, 2, t / duration).ToString());
           // if (quantAlterada <= 1.5)
           // {
          //      volta = true;
                //t = 0.0f;
          //  }

           // if (!volta)
          //  {
                quantAlterada =  Mathf.Lerp(2, 0.5f, t / duration);
                sLight.intensity = Mathf.Lerp(2, 0.5f, t / duration);
          //  }
          //  else
          //  {
          //      //quantAlterada += lightChange;
          //      sLight.intensity = Mathf.Lerp(1, 2, t / duration);
          //  }
            lightChange += lightChangeSpeed;
            yield return null;
        }
        StartCoroutine(onIntensitiLights());
        yield return null;
    }
    public IEnumerator onIntensitiLights()
    {
        float quantAlterada = 0;
        float t = 0.0f;
        lightChange = lightChangeSpeed;
        //while (quantAlterada < 2)
        while (t < duration)
        {
            t += Time.deltaTime;
            Debug.Log("IntensidadeAlterada" + quantAlterada.ToString());
            Debug.Log("Mathf.Lerp(2, 1, t / duration)" + (Mathf.Lerp(2, 1, t / duration)).ToString());

            Debug.Log("Mathf.Lerp(1, 2, t / duration)" + Mathf.Lerp(1, 2, t / duration).ToString());
            // if (quantAlterada <= 1.5)
            // {
            //      volta = true;
            //t = 0.0f;
            //  }

            // if (!volta)
            //  {
            //quantAlterada = Mathf.Lerp(2, 1, t / duration);
            //sLight.intensity = Mathf.Lerp(2, 1, t / duration);
  
                 quantAlterada += lightChange;
                 sLight.intensity = Mathf.Lerp(0.5f, 2, t / duration);

            lightChange += lightChangeSpeed;
            yield return null;
        }

        sLight.intensity = 2;
        yield return null;
    }

}
