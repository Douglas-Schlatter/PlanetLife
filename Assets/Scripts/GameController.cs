using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public int globalScore = 0;
    public int[] scoreByCat = { 0, 0, 0, 0, 0 };
    /*
     scoreByCat Esta separado em
    pos 0: Banco

    pos 1: Governo

    pos 2: Indústria Privada

    pos 3: Orgão do Meio ambiente

    pos 4: População
     */
    public QuestionsController qc;
    public bool acabouQuestions = false;
    public List<GameObject> cityFases = new List<GameObject>();
    [SerializeField] public TextMeshProUGUI scoreValue;
    public GameObject planet;
    public RotateAround planetRA;
    public Animator planetAnim;
    public Animator lightAnim;
    public bool respUltimaQuest = false;
    // Start is called before the first frame update
    void Start()
    {
        planetRA = planet.GetComponent<RotateAround>();
        AttCity();
       // scoreValue
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    public void clickButton1() 
    {

        if (!acabouQuestions && !respUltimaQuest)
        {
            int[] pointHolder = qc.getRespValueByIndex(1);
            for (int i = 0; i < 5; i++)
            {
                scoreByCat[i] += pointHolder[i];
            }
            PassDay();
            qc.nextQuestion();
        }
        else if (acabouQuestions && !respUltimaQuest)
        {
            respUltimaQuest = true;
            int[] pointHolder = qc.getRespValueByIndex(1);
            for (int i = 0; i < 5; i++)
            {
                scoreByCat[i] += pointHolder[i];
            }
            PassDay();
        }
        else 
        { 
        
        }

    }
    public void clickButton2()
    {
        if (!acabouQuestions)
        {
            //score += qc.getRespValueByIndex(2);
            int[] pointHolder = qc.getRespValueByIndex(2);
            for (int i = 0; i < 5; i++)
            {
                scoreByCat[i] += pointHolder[i];
            }
            
            PassDay();
            qc.nextQuestion();
        }
        else if (acabouQuestions && !respUltimaQuest)
        {
            respUltimaQuest = true;
            //score += qc.getRespValueByIndex(2);
            int[] pointHolder = qc.getRespValueByIndex(2);
            for (int i = 0; i < 5; i++)
            {
                scoreByCat[i] += pointHolder[i];
            }

            PassDay();
        }
        else
        {

        }

    }
    public void clickButton3()
    {
        if (!acabouQuestions)
        {
            //score += qc.getRespValueByIndex(3);
            int[] pointHolder = qc.getRespValueByIndex(3);
            for (int i = 0; i < 5; i++)
            {
                scoreByCat[i] += pointHolder[i];
            }
            PassDay();
            qc.nextQuestion();
        }
        else if (acabouQuestions && !respUltimaQuest)
        {
            respUltimaQuest = true;
            //score += qc.getRespValueByIndex(3);
            int[] pointHolder = qc.getRespValueByIndex(3);
            for (int i = 0; i < 5; i++)
            {
                scoreByCat[i] += pointHolder[i];
            }
            PassDay();
           
        }
        else
        {

        }

    }
    public void clickButton4()
    {
        if (!acabouQuestions)
        {
            //score += qc.getRespValueByIndex(4);
            int[] pointHolder = qc.getRespValueByIndex(4);
            for (int i = 0; i < 5; i++)
            {
                scoreByCat[i] += pointHolder[i];
            }
            PassDay();
            qc.nextQuestion();
        }
        else if (acabouQuestions && !respUltimaQuest)
        {
            respUltimaQuest = true;
            //score += qc.getRespValueByIndex(4);
            int[] pointHolder = qc.getRespValueByIndex(4);
            for (int i = 0; i < 5; i++)
            {
                scoreByCat[i] += pointHolder[i];
            }
            PassDay();
           
        }
        else
        {

        }
    }

    public void PassDay()
    {
        //StartCoroutine(planetRA.lightCycle());
        //planetRA.needsToRotate = true;
        planetAnim.SetTrigger("Rotate");
        lightAnim.SetTrigger("lightCycle");
        AttCity();

    }

    public void AttCity()
    {
        globalScore = (scoreByCat[0] + scoreByCat[1] + scoreByCat[2] + scoreByCat[3] + scoreByCat[4]) / 5;
        scoreValue.text = globalScore.ToString();
        if (globalScore < 0)
        {
            foreach (GameObject gm in cityFases)
            {
                gm.SetActive(false);
            }
            cityFases[0].SetActive(true);

   
        }
        else if (0 <= globalScore && globalScore < 100)
        {
            foreach (GameObject gm in cityFases)
            {
                gm.SetActive(false);
            }
            cityFases[1].SetActive(true);
        }
        else //score maior 100
        {
            foreach (GameObject gm in cityFases)
            {
                gm.SetActive(false);
            }
            cityFases[2].SetActive(true);
        }
    }
    /*
    scoreByCat Esta separado em
    pos 0: Banco

    pos 1: Governo

    pos 2: Indústria Privada

    pos 3: Orgão do Meio ambiente

    pos 4: População
    */
    public void AttCityByIndScore()
    {
        globalScore = (scoreByCat[0] + scoreByCat[1] + scoreByCat[2] + scoreByCat[3] + scoreByCat[4]) / 5;
        scoreValue.text = globalScore.ToString();

        
        if (globalScore < 0)
        {
            foreach (GameObject gm in cityFases)
            {
                gm.SetActive(false);
            }
            cityFases[0].SetActive(true);


        }
        else if (0 <= globalScore && globalScore < 100)
        {
            foreach (GameObject gm in cityFases)
            {
                gm.SetActive(false);
            }
            cityFases[1].SetActive(true);
        }
        else //score maior 100
        {
            foreach (GameObject gm in cityFases)
            {
                gm.SetActive(false);
            }
            cityFases[2].SetActive(true);
        }
    }
}
