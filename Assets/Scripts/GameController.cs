using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameController : MonoBehaviour
{
    public float globalScore = 0;
    public float[] scoreByCat = { 0, 0, 0, 0, 0 };
    /*
     scoreByCat Esta separado em
    pos 0: Banco

    pos 1: Governo

    pos 2: Ind�stria Privada

    pos 3: Org�o do Meio ambiente

    pos 4: Popula��o
     */
    public QuestionsController qc;
    public bool acabouQuestions = false;

    [SerializeField] public TextMeshProUGUI scoreValue;
    public GameObject planet;
    public RotateAround planetRA;
    public Animator planetAnim;
    public Animator lightAnim;
    public bool respUltimaQuest = false;
    //Fases of each group
    public List<GameObject> bancoFases = new List<GameObject>();
    public List<GroupAsset> bancoTeste;
 
    // Start is called before the first frame update
    void Start()
    {
        planetRA = planet.GetComponent<RotateAround>();
        //AttCity();
        // scoreValue
    }
    //Declare a custom struct.  The [Serializable] attribute ensures
    //that its content is included when the 'stats' field is serialized.
    [Serializable]
    public struct GroupAsset
    {
        public int fase;
        public GameObject go;
    }
    // Update is called once per frame
    void Update()
    {



    }

    public void setBaseScore() 
    {
        for (int i = 0; i < 5; i++)
        {
            scoreByCat[i] = (qc.maxGScore[i] + qc.minGScore[i])/2;
        }
    }
    public void clickButton1()
    {

        if (!acabouQuestions && !respUltimaQuest)
        {
            float[] pointHolder = qc.getRespValueByIndex(1);
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
            float[] pointHolder = qc.getRespValueByIndex(1);
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
            float[] pointHolder = qc.getRespValueByIndex(2);
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
            float[] pointHolder = qc.getRespValueByIndex(2);
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
            float[] pointHolder = qc.getRespValueByIndex(3);
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
            float[] pointHolder = qc.getRespValueByIndex(3);
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
            float[] pointHolder = qc.getRespValueByIndex(4);
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
            float[] pointHolder = qc.getRespValueByIndex(4);
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
        AttCityByBanco();
        AttCityByGoverno();
        AttCityByIndustria();
        AttCityByAmbiente();
        AttCityByPop();
    }
    /*
    scoreByCat Esta separado em
    pos 0: Banco qc.gStep[0]

    pos 1: Governo qc.gStep[1]

    pos 2: Ind�stria Privada qc.gStep[2]

    pos 3: Org�o do Meio ambiente qc.gStep[3]

    pos 4: Popula��o qc.gStep[0]
    */
    public void AttCityByBanco()
    {

        globalScore = (scoreByCat[0] + scoreByCat[1] + scoreByCat[2] + scoreByCat[3] + scoreByCat[4]) / 5;
        scoreValue.text = globalScore.ToString();
        //TODO REVISAR ISSO DPS
        /*
         Banco qc.gStep[0]
         Atualizações do banco envolvem
         Prédio do banco fica maior
         Surgem edificações maiores
         
         
         Aqui fiz para 5 passos o que resulta em 6 ranges de valores
         
         */
        if (scoreByCat[0] < qc.minGScore[0]+( 1 * qc.gStep[0]))// Distopia
        {
            Debug.Log("Distopico" +" Score banco: " + scoreByCat[0]+" step: "+ qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {
                switch (ga.fase)
                {
                    case -3:
                        ga.go.SetActive(true);
                        break;
                    default:
                        ga.go.SetActive(false);
                        break;
                }
            }
        }
        else if (qc.minGScore[0] + (1 * qc.gStep[0]) <= scoreByCat[0] && scoreByCat[0] < qc.minGScore[0] + (2 * qc.gStep[0]))// Muito negativo
        {
            Debug.Log("Estado Negativo" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {

                switch (ga.fase)
                {
                    case -3:
                        ga.go.SetActive(true);
                        break;
                    case -2:
                        ga.go.SetActive(true);
                        break;
                    default:
                        ga.go.SetActive(false);
                        break;
                }
                /*
                if (ga.fase == 1)
                {
                    ga.go.SetActive(false);
                }
                if (ga.fase == 2)
                {
                    ga.go.SetActive(true);
                }
                */
            }
        }
        else if (qc.minGScore[0] + (2 * qc.gStep[0]) <= scoreByCat[0] && scoreByCat[0] < qc.minGScore[0] + (3 * qc.gStep[0]))//Um pouco negativo
        {
            Debug.Log("Estado Neutro" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {
                switch (ga.fase)
                {
                    case -3:
                        ga.go.SetActive(true);
                        break;
                    case -2:
                        ga.go.SetActive(true);
                        break;
                    case -1:
                        ga.go.SetActive(true);
                        break;
                    default:
                        ga.go.SetActive(false);
                        break;
                }
            }
        }
        else if (qc.minGScore[0] + (3 * qc.gStep[0]) <= scoreByCat[0] && scoreByCat[0] < qc.minGScore[0] + (4 * qc.gStep[0]))// Um pouco Positivo
        {
            Debug.Log("Estado Positivo" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {
                switch (ga.fase)
                {
                    case 3:
                        ga.go.SetActive(false);
                        break;
                    case 2:
                        ga.go.SetActive(false);
                        break;
                    default:
                        ga.go.SetActive(true);
                        break;
                }
            }
        }
        else // Utopia
        {
            Debug.Log("Estado Utopico" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {
                switch (ga.fase)
                {
                    default:
                        ga.go.SetActive(true);
                        break;
                }
            }
        }
    }
    public void AttCityByGoverno()
    {

        globalScore = (scoreByCat[0] + scoreByCat[1] + scoreByCat[2] + scoreByCat[3] + scoreByCat[4]) / 5;
        scoreValue.text = globalScore.ToString();
        //TODO REVISAR ISSO DPS
        /*
         Banco qc.gStep[0]
         Atualizações do banco envolvem
         Prédio do banco fica maior
         Surgem edificações maiores
         
         
         Aqui fiz para 5 passos o que resulta em 6 ranges de valores
         
         */
        if (scoreByCat[0] < qc.minGScore[0] + (1 * qc.gStep[0]))// Distopia
        {
            Debug.Log("Distopico" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {
                switch (ga.fase)
                {
                    case -3:
                        ga.go.SetActive(true);
                        break;
                    default:
                        ga.go.SetActive(false);
                        break;
                }
            }
        }
        else if (qc.minGScore[0] + (1 * qc.gStep[0]) <= scoreByCat[0] && scoreByCat[0] < qc.minGScore[0] + (2 * qc.gStep[0]))// Muito negativo
        {
            Debug.Log("Estado Negativo" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {

                switch (ga.fase)
                {
                    case -3:
                        ga.go.SetActive(true);
                        break;
                    case -2:
                        ga.go.SetActive(true);
                        break;
                    default:
                        ga.go.SetActive(false);
                        break;
                }
                /*
                if (ga.fase == 1)
                {
                    ga.go.SetActive(false);
                }
                if (ga.fase == 2)
                {
                    ga.go.SetActive(true);
                }
                */
            }
        }
        else if (qc.minGScore[0] + (2 * qc.gStep[0]) <= scoreByCat[0] && scoreByCat[0] < qc.minGScore[0] + (3 * qc.gStep[0]))//Um pouco negativo
        {
            Debug.Log("Estado Neutro" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {
                switch (ga.fase)
                {
                    case -3:
                        ga.go.SetActive(true);
                        break;
                    case -2:
                        ga.go.SetActive(true);
                        break;
                    case -1:
                        ga.go.SetActive(true);
                        break;
                    default:
                        ga.go.SetActive(false);
                        break;
                }
            }
        }
        else if (qc.minGScore[0] + (3 * qc.gStep[0]) <= scoreByCat[0] && scoreByCat[0] < qc.minGScore[0] + (4 * qc.gStep[0]))// Um pouco Positivo
        {
            Debug.Log("Estado Positivo" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {
                switch (ga.fase)
                {
                    case 3:
                        ga.go.SetActive(false);
                        break;
                    case 2:
                        ga.go.SetActive(false);
                        break;
                    default:
                        ga.go.SetActive(true);
                        break;
                }
            }
        }
        else // Utopia
        {
            Debug.Log("Estado Utopico" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {
                switch (ga.fase)
                {
                    default:
                        ga.go.SetActive(true);
                        break;
                }
            }
        }
    }
    public void AttCityByIndustria()
    {

        globalScore = (scoreByCat[0] + scoreByCat[1] + scoreByCat[2] + scoreByCat[3] + scoreByCat[4]) / 5;
        scoreValue.text = globalScore.ToString();
        //TODO REVISAR ISSO DPS
        /*
         Banco qc.gStep[0]
         Atualizações do banco envolvem
         Prédio do banco fica maior
         Surgem edificações maiores
         
         
         Aqui fiz para 5 passos o que resulta em 6 ranges de valores
         
         */
        if (scoreByCat[0] < qc.minGScore[0] + (1 * qc.gStep[0]))// Distopia
        {
            Debug.Log("Distopico" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {
                switch (ga.fase)
                {
                    case -3:
                        ga.go.SetActive(true);
                        break;
                    default:
                        ga.go.SetActive(false);
                        break;
                }
            }
        }
        else if (qc.minGScore[0] + (1 * qc.gStep[0]) <= scoreByCat[0] && scoreByCat[0] < qc.minGScore[0] + (2 * qc.gStep[0]))// Muito negativo
        {
            Debug.Log("Estado Negativo" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {

                switch (ga.fase)
                {
                    case -3:
                        ga.go.SetActive(true);
                        break;
                    case -2:
                        ga.go.SetActive(true);
                        break;
                    default:
                        ga.go.SetActive(false);
                        break;
                }
                /*
                if (ga.fase == 1)
                {
                    ga.go.SetActive(false);
                }
                if (ga.fase == 2)
                {
                    ga.go.SetActive(true);
                }
                */
            }
        }
        else if (qc.minGScore[0] + (2 * qc.gStep[0]) <= scoreByCat[0] && scoreByCat[0] < qc.minGScore[0] + (3 * qc.gStep[0]))//Um pouco negativo
        {
            Debug.Log("Estado Neutro" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {
                switch (ga.fase)
                {
                    case -3:
                        ga.go.SetActive(true);
                        break;
                    case -2:
                        ga.go.SetActive(true);
                        break;
                    case -1:
                        ga.go.SetActive(true);
                        break;
                    default:
                        ga.go.SetActive(false);
                        break;
                }
            }
        }
        else if (qc.minGScore[0] + (3 * qc.gStep[0]) <= scoreByCat[0] && scoreByCat[0] < qc.minGScore[0] + (4 * qc.gStep[0]))// Um pouco Positivo
        {
            Debug.Log("Estado Positivo" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {
                switch (ga.fase)
                {
                    case 3:
                        ga.go.SetActive(false);
                        break;
                    case 2:
                        ga.go.SetActive(false);
                        break;
                    default:
                        ga.go.SetActive(true);
                        break;
                }
            }
        }
        else // Utopia
        {
            Debug.Log("Estado Utopico" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {
                switch (ga.fase)
                {
                    default:
                        ga.go.SetActive(true);
                        break;
                }
            }
        }
    }
    public void AttCityByAmbiente()
    {

        globalScore = (scoreByCat[0] + scoreByCat[1] + scoreByCat[2] + scoreByCat[3] + scoreByCat[4]) / 5;
        scoreValue.text = globalScore.ToString();
        //TODO REVISAR ISSO DPS
        /*
         Banco qc.gStep[0]
         Atualizações do banco envolvem
         Prédio do banco fica maior
         Surgem edificações maiores
         
         
         Aqui fiz para 5 passos o que resulta em 6 ranges de valores
         
         */
        if (scoreByCat[0] < qc.minGScore[0] + (1 * qc.gStep[0]))// Distopia
        {
            Debug.Log("Distopico" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {
                switch (ga.fase)
                {
                    case -3:
                        ga.go.SetActive(true);
                        break;
                    default:
                        ga.go.SetActive(false);
                        break;
                }
            }
        }
        else if (qc.minGScore[0] + (1 * qc.gStep[0]) <= scoreByCat[0] && scoreByCat[0] < qc.minGScore[0] + (2 * qc.gStep[0]))// Muito negativo
        {
            Debug.Log("Estado Negativo" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {

                switch (ga.fase)
                {
                    case -3:
                        ga.go.SetActive(true);
                        break;
                    case -2:
                        ga.go.SetActive(true);
                        break;
                    default:
                        ga.go.SetActive(false);
                        break;
                }
                /*
                if (ga.fase == 1)
                {
                    ga.go.SetActive(false);
                }
                if (ga.fase == 2)
                {
                    ga.go.SetActive(true);
                }
                */
            }
        }
        else if (qc.minGScore[0] + (2 * qc.gStep[0]) <= scoreByCat[0] && scoreByCat[0] < qc.minGScore[0] + (3 * qc.gStep[0]))//Um pouco negativo
        {
            Debug.Log("Estado Neutro" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {
                switch (ga.fase)
                {
                    case -3:
                        ga.go.SetActive(true);
                        break;
                    case -2:
                        ga.go.SetActive(true);
                        break;
                    case -1:
                        ga.go.SetActive(true);
                        break;
                    default:
                        ga.go.SetActive(false);
                        break;
                }
            }
        }
        else if (qc.minGScore[0] + (3 * qc.gStep[0]) <= scoreByCat[0] && scoreByCat[0] < qc.minGScore[0] + (4 * qc.gStep[0]))// Um pouco Positivo
        {
            Debug.Log("Estado Positivo" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {
                switch (ga.fase)
                {
                    case 3:
                        ga.go.SetActive(false);
                        break;
                    case 2:
                        ga.go.SetActive(false);
                        break;
                    default:
                        ga.go.SetActive(true);
                        break;
                }
            }
        }
        else // Utopia
        {
            Debug.Log("Estado Utopico" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {
                switch (ga.fase)
                {
                    default:
                        ga.go.SetActive(true);
                        break;
                }
            }
        }
    }
    public void AttCityByPop()
    {

        globalScore = (scoreByCat[0] + scoreByCat[1] + scoreByCat[2] + scoreByCat[3] + scoreByCat[4]) / 5;
        scoreValue.text = globalScore.ToString();
        //TODO REVISAR ISSO DPS
        /*
         Banco qc.gStep[0]
         Atualizações do banco envolvem
         Prédio do banco fica maior
         Surgem edificações maiores
         
         
         Aqui fiz para 5 passos o que resulta em 6 ranges de valores
         
         */
        if (scoreByCat[0] < qc.minGScore[0] + (1 * qc.gStep[0]))// Distopia
        {
            Debug.Log("Distopico" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {
                switch (ga.fase)
                {
                    case -3:
                        ga.go.SetActive(true);
                        break;
                    default:
                        ga.go.SetActive(false);
                        break;
                }
            }
        }
        else if (qc.minGScore[0] + (1 * qc.gStep[0]) <= scoreByCat[0] && scoreByCat[0] < qc.minGScore[0] + (2 * qc.gStep[0]))// Muito negativo
        {
            Debug.Log("Estado Negativo" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {

                switch (ga.fase)
                {
                    case -3:
                        ga.go.SetActive(true);
                        break;
                    case -2:
                        ga.go.SetActive(true);
                        break;
                    default:
                        ga.go.SetActive(false);
                        break;
                }
                /*
                if (ga.fase == 1)
                {
                    ga.go.SetActive(false);
                }
                if (ga.fase == 2)
                {
                    ga.go.SetActive(true);
                }
                */
            }
        }
        else if (qc.minGScore[0] + (2 * qc.gStep[0]) <= scoreByCat[0] && scoreByCat[0] < qc.minGScore[0] + (3 * qc.gStep[0]))//Um pouco negativo
        {
            Debug.Log("Estado Neutro" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {
                switch (ga.fase)
                {
                    case -3:
                        ga.go.SetActive(true);
                        break;
                    case -2:
                        ga.go.SetActive(true);
                        break;
                    case -1:
                        ga.go.SetActive(true);
                        break;
                    default:
                        ga.go.SetActive(false);
                        break;
                }
            }
        }
        else if (qc.minGScore[0] + (3 * qc.gStep[0]) <= scoreByCat[0] && scoreByCat[0] < qc.minGScore[0] + (4 * qc.gStep[0]))// Um pouco Positivo
        {
            Debug.Log("Estado Positivo" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {
                switch (ga.fase)
                {
                    case 3:
                        ga.go.SetActive(false);
                        break;
                    case 2:
                        ga.go.SetActive(false);
                        break;
                    default:
                        ga.go.SetActive(true);
                        break;
                }
            }
        }
        else // Utopia
        {
            Debug.Log("Estado Utopico" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            foreach (GroupAsset ga in bancoTeste)
            {
                switch (ga.fase)
                {
                    default:
                        ga.go.SetActive(true);
                        break;
                }
            }
        }
    }

}
