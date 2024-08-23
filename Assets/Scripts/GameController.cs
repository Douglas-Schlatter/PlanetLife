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
    //Fases and avatars of each group
    //public List<GameObject> bancoFases = new List<GameObject>();
    public List<GroupAsset> bancoObjs;
    public GameObject bancoAvatar;
    public List<GroupAsset> govObjs;
    public List<GameObject> govAvatars;
    public List<GroupAsset> indusObjs;
    public GameObject indusAvatar;
    public List<GroupAsset> ambObjs;
    public GameObject ambAvatar;
    public List<GroupAsset> popObjs;
    public GameObject popAvatar;
    //Materials
    public Material planetMat;

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
        globalScore = (scoreByCat[0] + scoreByCat[1] + scoreByCat[2] + scoreByCat[3] + scoreByCat[4]) / 5;
        scoreValue.text = globalScore.ToString();
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
            bancoAvatar.GetComponent<Transform>().localScale = new Vector3(0.015f, 0.015f, 0.015f);
            Debug.Log("Distopico" +" Score banco: " + scoreByCat[0]+" step: "+ qc.gStep[0]);
            /*
            foreach (GroupAsset ga in bancoObjs)
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
            */
        }
        else if (qc.minGScore[0] + (1 * qc.gStep[0]) <= scoreByCat[0] && scoreByCat[0] < qc.minGScore[0] + (2 * qc.gStep[0]))// Negativo
        {
            Debug.Log("Estado Negativo" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            bancoAvatar.GetComponent<Transform>().localScale = new Vector3(0.02f, 0.02f, 0.02f);
            /*
            foreach (GroupAsset ga in bancoObjs)
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
            }
            */

        }
        else if (qc.minGScore[0] + (2 * qc.gStep[0]) <= scoreByCat[0] && scoreByCat[0] < qc.minGScore[0] + (3 * qc.gStep[0]))//Neutro
        {
            Debug.Log("Estado Neutro" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            bancoAvatar.GetComponent<Transform>().localScale = new Vector3(0.025f, 0.025f, 0.025f);

            /*
            foreach (GroupAsset ga in bancoObjs)
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
            */
        }
        else if (qc.minGScore[0] + (3 * qc.gStep[0]) <= scoreByCat[0] && scoreByCat[0] < qc.minGScore[0] + (4 * qc.gStep[0]))//  Positivo
        {
            Debug.Log("Estado Positivo" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);
            bancoAvatar.GetComponent<Transform>().localScale = new Vector3(0.03f, 0.03f, 0.03f);
            /*
            foreach (GroupAsset ga in bancoObjs)
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
            */

        }
        else // Utopia
        {
            Debug.Log("Estado Utopico" + " Score banco: " + scoreByCat[0] + " step: " + qc.gStep[0]);// Utopico
            bancoAvatar.GetComponent<Transform>().localScale = new Vector3(0.035f, 0.035f, 0.035f);
            foreach (GroupAsset ga in bancoObjs)
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

        /*
         Utopia:
        -As iniciais aumentam de tamanho
        -Aumenta a quantidade
        Neutro:
        -Pelo menos uma escola, Pelo menos um Hospital
        -Pelo menos Delegacia
        Distopico:
        -As iniciais diminuem de tamanho
        TODO:
        -> tentar ver se é disponível desenhar linhas como estradas
         
         */


        if (scoreByCat[1] < qc.minGScore[1] + (1 * qc.gStep[1]))// Distopia
        {
            Debug.Log("Distopico" + " Score Gov: " + scoreByCat[1] + " step: " + qc.gStep[1]);
            
            
            foreach (GameObject go in govAvatars)
            {
                go.GetComponent<Transform>().localScale = new Vector3(0.015f, 0.015f, 0.015f);
            }
            
        }
        else if (qc.minGScore[1] + (1 * qc.gStep[1]) <= scoreByCat[1] && scoreByCat[1] < qc.minGScore[1] + (2 * qc.gStep[1]))// Muito negativo
        {
            Debug.Log("Estado Negativo" + " Score Gov: " + scoreByCat[1] + " step: " + qc.gStep[1]);

            foreach (GameObject go in govAvatars)
            {
                go.GetComponent<Transform>().localScale = new Vector3(0.02f, 0.02f, 0.02f);
            }
            /*
            foreach (GroupAsset ga in govObjs)
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
            /*
            }
            */
        }
        else if (qc.minGScore[1] + (2 * qc.gStep[1]) <= scoreByCat[1] && scoreByCat[1] < qc.minGScore[1] + (3 * qc.gStep[1]))//Um pouco negativo
        {
            Debug.Log("Estado Neutro" + " Score Gov: " + scoreByCat[1] + " step: " + qc.gStep[1]);

            foreach (GameObject go in govAvatars)
            {
                go.GetComponent<Transform>().localScale = new Vector3(0.025f, 0.025f, 0.025f);
            }

            /*
            foreach (GroupAsset ga in govObjs)
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
            */
        }
        else if (qc.minGScore[1] + (3 * qc.gStep[1]) <= scoreByCat[1] && scoreByCat[1] < qc.minGScore[1] + (4 * qc.gStep[1]))// Um pouco Positivo
        {
            Debug.Log("Estado Positivo" + " Score Gov: " + scoreByCat[1] + " step: " + qc.gStep[1]);

            foreach (GameObject go in govAvatars)
            {
                go.GetComponent<Transform>().localScale = new Vector3(0.03f, 0.03f, 0.03f);
            }

            /*
            foreach (GroupAsset ga in govObjs)
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
            */
        }
        else // Utopia
        {
            Debug.Log("Estado Utopico" + " Score Gov: " + scoreByCat[1] + " step: " + qc.gStep[1]);

            foreach (GameObject go in govAvatars)
            {
                go.GetComponent<Transform>().localScale = new Vector3(0.035f, 0.035f, 0.035f);
            }

            /*
            foreach (GroupAsset ga in govObjs)
            {
                switch (ga.fase)
                {
                    default:
                        ga.go.SetActive(true);
                        break;
                }
            }
            */
        }
    }
    public void AttCityByIndustria()
    {


        //TODO REVISAR ISSO DPS
        /*
            Avatar:  Industria com chaminé
            Utopia:
            -Tamanho de um aranha-céu
            -Aumenta a quantidade de comercios
            Neutro:
            -Tamanho de uma casa
            -Alguns comércios
            Distopico:
            -Tamanho menor que uma casa
            -Desaparecem alguns
         
         */
        if (scoreByCat[2] < qc.minGScore[2] + (1 * qc.gStep[2]))// Distopia
        {
            Debug.Log("Distopico" + " Score Indus: " + scoreByCat[2] + " step: " + qc.gStep[2]);
            indusAvatar.GetComponent<Transform>().localScale = new Vector3(0.015f, 0.015f, 0.015f);

            foreach (GroupAsset ga in indusObjs)
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
        else if (qc.minGScore[2] + (1 * qc.gStep[2]) <= scoreByCat[2] && scoreByCat[2] < qc.minGScore[2] + (2 * qc.gStep[2]))// Muito negativo
        {
            Debug.Log("Estado Negativo" + " Score Indus: " + scoreByCat[2] + " step: " + qc.gStep[2]);
            indusAvatar.GetComponent<Transform>().localScale = new Vector3(0.02f, 0.02f, 0.02f);
            foreach (GroupAsset ga in indusObjs)
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
        else if (qc.minGScore[2] + (2 * qc.gStep[2]) <= scoreByCat[2] && scoreByCat[2] < qc.minGScore[2] + (3 * qc.gStep[2]))//Um pouco negativo
        {
            Debug.Log("Estado Neutro" + " Score Indus: " + scoreByCat[2] + " step: " + qc.gStep[2]);
            indusAvatar.GetComponent<Transform>().localScale = new Vector3(0.025f, 0.025f, 0.025f);
            foreach (GroupAsset ga in indusObjs)
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
        else if (qc.minGScore[2] + (3 * qc.gStep[2]) <= scoreByCat[2] && scoreByCat[2] < qc.minGScore[2] + (4 * qc.gStep[2]))// Um pouco Positivo
        {
            Debug.Log("Estado Positivo" + " Score Indus: " + scoreByCat[2] + " step: " + qc.gStep[2]);
            indusAvatar.GetComponent<Transform>().localScale = new Vector3(0.03f, 0.03f, 0.03f);
            foreach (GroupAsset ga in indusObjs)
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
            Debug.Log("Estado Utopico" + " Score Indus: " + scoreByCat[2] + " step: " + qc.gStep[2]);
            indusAvatar.GetComponent<Transform>().localScale = new Vector3(0.035f, 0.035f, 0.035f);
            foreach (GroupAsset ga in indusObjs)
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


        //TODO REVISAR ISSO DPS
        /*
            Avatar: Cor do planeta
            Utopia:
            -Cor Esverdeada
            -Aumenta a quantidade de arvores
            Neutro:
            -Sem alteração de cor
            -Algumas arvores
            Distopico:
            -Cor acinzentada
            -Desaparecem algumas arvores
         
         */
        Color customColor;
        //teste.SetColor("_Mid_Color", customColor);
        if (scoreByCat[3] < qc.minGScore[3] + (1 * qc.gStep[3]))// Distopia
        {
            Debug.Log("Distopico" + " Score Amb: " + scoreByCat[3] + " step: " + qc.gStep[3]);

            customColor = new Color(0.528f, 0.302f, 0.062f, 0.0f);
            planetMat.SetColor("_Bottom_Color", customColor);

            customColor = new Color(0.108f, 0.509f, 0.304f, 0.0f);
            planetMat.SetColor("_Mid_Color", customColor);
            /*
            foreach (GroupAsset ga in ambObjs)
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
            */
        }
        else if (qc.minGScore[3] + (1 * qc.gStep[3]) <= scoreByCat[3] && scoreByCat[3] < qc.minGScore[3] + (2 * qc.gStep[3]))// negativo
        {
            Debug.Log("Estado Negativo" + " Score Amb: " + scoreByCat[3] + " step: " + qc.gStep[3]);

            customColor = new Color(0f, 0.152f, 1.0f, 0.0f);
            planetMat.SetColor("_Bottom_Color", customColor);

            customColor = new Color(0.108f, 0.509f, 0.304f, 0.0f);
            planetMat.SetColor("_Mid_Color", customColor);
            /*
            foreach (GroupAsset ga in ambObjs)
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
            }
            */
        }
        else if (qc.minGScore[3] + (2 * qc.gStep[3]) <= scoreByCat[3] && scoreByCat[3] < qc.minGScore[3] + (3 * qc.gStep[3]))//neutro
        {
            Debug.Log("Estado Neutro" + " Score Amb: " + scoreByCat[3] + " step: " + qc.gStep[3]);

            customColor = new Color(0f, 0.152f, 1.0f, 0.0f);
            planetMat.SetColor("_Bottom_Color", customColor);

            customColor = new Color(0.0f, 1.0f, 0.490f, 0.0f);
            planetMat.SetColor("_Mid_Color", customColor);
            /*
            foreach (GroupAsset ga in ambObjs)
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
            */
        }
        else if (qc.minGScore[3] + (3 * qc.gStep[3]) <= scoreByCat[3] && scoreByCat[3] < qc.minGScore[3] + (4 * qc.gStep[3]))//  Positivo
        {
            Debug.Log("Estado Positivo" + " Score Amb: " + scoreByCat[3] + " step: " + qc.gStep[3]);
            /*
            foreach (GroupAsset ga in ambObjs)
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
            */
        }
        else // Utopia
        {
            Debug.Log("Estado Utopico" + " Score Amb: " + scoreByCat[3] + " step: " + qc.gStep[3]);//utopia
            /*
            foreach (GroupAsset ga in ambObjs)
            {
                switch (ga.fase)
                {
                    default:
                        ga.go.SetActive(true);
                        break;
                }
            }
            */
        }
    }
    public void AttCityByPop()
    {

        /*
        Avatar: ?
        Utopia:
        -Muitas pessoas circulando
        Neutro:
        -Algumas pessoas circulando
        Distopico:
        -Desaparecem algumas pessoas
         
         */


        if (scoreByCat[4] < qc.minGScore[4] + (1 * qc.gStep[4]))// Distopia
        {
            Debug.Log("Distopico" + " Score banco: " + scoreByCat[4] + " step: " + qc.gStep[4]);
            popAvatar.GetComponent<Transform>().localScale = new Vector3(0.015f, 0.015f, 0.015f);
            /*
            foreach (GroupAsset ga in popObjs)
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
            */
        }
        else if (qc.minGScore[4] + (1 * qc.gStep[4]) <= scoreByCat[4] && scoreByCat[4] < qc.minGScore[4] + (2 * qc.gStep[4]))// Muito negativo
        {
            Debug.Log("Estado Negativo" + " Score banco: " + scoreByCat[4] + " step: " + qc.gStep[4]);
            popAvatar.GetComponent<Transform>().localScale = new Vector3(0.02f, 0.02f, 0.02f);
            /*
            foreach (GroupAsset ga in popObjs)
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
            }
            */
        }
        else if (qc.minGScore[4] + (2 * qc.gStep[4]) <= scoreByCat[4] && scoreByCat[4] < qc.minGScore[4] + (3 * qc.gStep[4]))//Um pouco negativo
        {
            Debug.Log("Estado Neutro" + " Score banco: " + scoreByCat[4] + " step: " + qc.gStep[4]);
            popAvatar.GetComponent<Transform>().localScale = new Vector3(0.025f, 0.025f, 0.025f);
            /*
            foreach (GroupAsset ga in popObjs)
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
            */
        }
        else if (qc.minGScore[4] + (3 * qc.gStep[4]) <= scoreByCat[4] && scoreByCat[4] < qc.minGScore[4] + (4 * qc.gStep[4]))// Um pouco Positivo
        {
            Debug.Log("Estado Positivo" + " Score banco: " + scoreByCat[4] + " step: " + qc.gStep[4]);
            popAvatar.GetComponent<Transform>().localScale = new Vector3(0.03f, 0.03f, 0.03f);
            /*
            foreach (GroupAsset ga in popObjs)
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
            */
        }
        else // Utopia
        {
            Debug.Log("Estado Utopico" + " Score banco: " + scoreByCat[4] + " step: " + qc.gStep[4]);
            popAvatar.GetComponent<Transform>().localScale = new Vector3(0.035f, 0.035f, 0.035f);
            /*
            foreach (GroupAsset ga in popObjs)
            {
                switch (ga.fase)
                {
                    default:
                        ga.go.SetActive(true);
                        break;
                }
            }
            */
        }
    }

}
