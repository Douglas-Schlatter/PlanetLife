using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class QuestionsController : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI perTxt;
    [SerializeField] public TextMeshProUGUI q1Text;
    [SerializeField] public TextMeshProUGUI q2Text;
    [SerializeField] public TextMeshProUGUI q3Text;
    [SerializeField] public TextMeshProUGUI q4Text;


    public GameController gm;
    //File Related
    private string filePath;
    private int currentLineIndex = 0;
    public string[] lines;
    private string[] sections;
    //Questions Related
    public int questIndex = 0;
    public List<Question> questions = new List<Question>();
    public int quantQuest;
    public GameController gc;
    public Question currentQ;
    public bool enPopulate = false;
    //Score related
    public float[] minGScore = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    public float[] maxGScore = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    //Step Related
    public float[] gStep = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    public int targetStep = 6;
   
    void Start()
    {
        //filePath = Application.dataPath + "/test.txt";  
        filePath = Application.streamingAssetsPath + "/20quest.txt";
        lines = File.ReadAllLines(filePath);
        // Question q1 = GetLineAtIndex(currentLineIndex, lines);
        //questions.Add(q1);
        populAllQuest();
        calcMaxMin();
        gStep = calcStep(targetStep);
        //inicialização do game Controller depois de inicializar o Questions Controller
        gm.setBaseScore();
        gm.AttCity();
        Question q1 = getRandomQuest();
        perTxt.text = q1.question;
        q1Text.text = q1.answ1;
        q2Text.text = q1.answ2;
        q3Text.text = q1.answ3;
        q4Text.text = q1.answ4;
        
    }
    public Question GetLineAtIndex(int index, string[] lines)
    {
        //string[] lines = File.ReadAllLines(filePath);
        Question qInter = new Question();
        bool complete = false;
        while (!complete & currentLineIndex < lines.Length)
        {

            //char test = lines[index].ToCharArray()[0];
            if (lines[currentLineIndex].ToCharArray()[0].Equals('P'))
            {
                qInter.id = questIndex;
                questIndex++;
                sections = lines[currentLineIndex].Split("/");
                qInter.question = sections[1];
                currentLineIndex++;
            }
            else if (lines[currentLineIndex].ToCharArray()[0].Equals('R') && lines[currentLineIndex].ToCharArray()[1].Equals('1'))//R1
            {
                sections = lines[currentLineIndex].Split("/");
                qInter.answ1 = sections[1];
                for (int i = 2; i < 7; i++)
                {
                    //qInter.answ1scores[i-2] = int.Parse(sections[i]);
                    qInter.answScores[0][ i-2] = float.Parse(sections[i]);
                    
                }
                //qInter.answ1Value = int.Parse(sections[2]); <- como era antes com somente uma questão
                currentLineIndex++;
            }
            else if (lines[currentLineIndex].ToCharArray()[0].Equals('R') && lines[currentLineIndex].ToCharArray()[1].Equals('2'))//R2
            {
                sections = lines[currentLineIndex].Split("/");
                qInter.answ2 = sections[1];
                for (int i = 2; i < 7; i++)
                {
                    qInter.answScores[1][ i - 2] = float.Parse(sections[i]);
                }
                //qInter.answ1Value = int.Parse(sections[2]); <- como era antes com somente uma questão
                currentLineIndex++;
            }
            else if (lines[currentLineIndex].ToCharArray()[0].Equals('R') && lines[currentLineIndex].ToCharArray()[1].Equals('3'))//R3
            {
                sections = lines[currentLineIndex].Split("/");
                qInter.answ3 = sections[1];
                for (int i = 2; i < 7; i++)
                {
                    qInter.answScores[2][ i - 2] = float.Parse(sections[i]);
                }
                //qInter.answ1Value = int.Parse(sections[2]); <- como era antes com somente uma questão
                currentLineIndex++;
            }
            else if (lines[currentLineIndex].ToCharArray()[0].Equals('R') && lines[currentLineIndex].ToCharArray()[1].Equals('4'))//R4
            {
                sections = lines[currentLineIndex].Split("/");

                qInter.answ4 = sections[1]; // aqui tem que retirar o identificardor (ex p r1 r2 r3 etc), antes de printar
                for (int i = 2; i < 7; i++)
                {
                    qInter.answScores[3][ i - 2] = float.Parse(sections[i]);
                }
                //qInter.answ1Value = int.Parse(sections[2]); <- como era antes com somente uma questão
                currentLineIndex++;
            }
            else if (lines[currentLineIndex].ToCharArray()[0].Equals('F') && lines[currentLineIndex].ToCharArray()[1].Equals('Q'))// acabou essa questão
            {
                quantQuest++;
                complete = true;
                currentLineIndex++;
                qInter.AttMaxMinScore();
            }
            else if (lines[currentLineIndex].ToCharArray()[0].Equals('F') && lines[currentLineIndex].ToCharArray()[1].Equals('A'))// essa questão acabou e todas acabaram
            {
                quantQuest++;
                qInter.AttMaxMinScore();
                enPopulate = true;
                complete = true;
                currentLineIndex++;
            }
        }
        return qInter;

    }
    public void populAllQuest()
    {
        while (!enPopulate)
        {
            Question q1 = GetLineAtIndex(currentLineIndex, lines);
            questions.Add(q1);
        }
        
    }
    public Question getRandomQuest()
    {
        int randomIndex = Random.Range(0, questions.Count);
        Question rq = questions[randomIndex];
        questions.RemoveAt(randomIndex);
        currentQ = rq;
        if (questions.Count == 0)
        {
            gc.acabouQuestions = true;
        }
        return rq;
    }
    public void calcMaxMin()
    {
       
        foreach (Question qi in questions)
        {
            for (int i = 0; i < 5; i++)
            {
                //Debug.Log(maxGScore[0]);
                maxGScore[i] += qi.maximumScore[i];

            }
            for (int i = 0; i < 5; i++)
            {
                minGScore[i] += qi.minimumScore[i];

            }
        }
    }

    public float[] calcStep(int np)
    {
        float[] range = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
        float[] step = { 0.0f, 0.0f, 0.0f, 0.0f , 0.0f };
        for (int i = 0; i < 5; i++)
        {
            range[i] = maxGScore[i] + Mathf.Abs(minGScore[i]);
            step[i] = (float)range[i] / np;
        }
        return step;
    }


    public void nextQuestion()
    {
        // Question qn = GetLineAtIndex(currentLineIndex, lines);
        //questions.Add(qn);
        Question qn = getRandomQuest();
        perTxt.text = qn.question;
        q1Text.text = qn.answ1;
        q2Text.text = qn.answ2;
        q3Text.text = qn.answ3;
        q4Text.text = qn.answ4;
    }
    public float[] getRespValueByIndex( int question)
    {
        Question qi = currentQ;
        if (question == 1)
        {
            return qi.answScores[0];
        }
        else if (question == 2)
        {
            return qi.answScores[1];
        }
        else if (question == 3)
        {
            return qi.answScores[2];
        }
        else if (question == 4)
        {
            return qi.answScores[3];
        }
        else 
        {
            Debug.Log("Não foi achado um valor para essa resposta");
            return null;
        }

    }

    public void NextLine()
    {
        
        //myText.text = GetLineAtIndex(currentLineIndex);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
