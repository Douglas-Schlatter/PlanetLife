using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class GrabRotation : MonoBehaviour
{

    //Mouse drag Related
    float rotationSpeed = 1f;
    public Vector2 turn;
    public Animator planetAnim;
    //Raycast related
    public Camera cam;
    public LayerMask mask;
    public bool isRotating = false;
    //TODO
    /*
     Se estiver passando o dia não pode segurar pra rotacionar, darum jeito de atualizar por animação 
    algo como set rotation objective pro animator do  planeta, ou de não dar lock na rotação por animação
     
     */

    void OnMouseDrag()
    {
       // turn.x += Input.GetAxis("Mouse X") * rotationSpeed;
       // turn.y += Input.GetAxis("Mouse Y") * rotationSpeed;

        //transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {

    }
    // Update is called once per frame
    void LateUpdate()
    {

        if (isRotating)
        {
            turn.x += Input.GetAxis("Mouse X") * rotationSpeed;
            turn.y += Input.GetAxis("Mouse Y") * rotationSpeed;
            transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
        }
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position, Color.blue);
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, mask))
            {
                Debug.Log(hit.transform.name);
                if (Input.GetKey("mouse 0"))
                {


                    Cursor.lockState = CursorLockMode.Locked;
                    // planetAnim.enabled = false;
                    isRotating = true;


                }

            }
        }
        if (Input.GetKeyUp("mouse 0"))
        {
            Cursor.lockState = CursorLockMode.None;
            isRotating = false;
            //turn.x = 0;
           // turn.y = 0;
            //planetAnim.enabled = true;
        }

    }
}
