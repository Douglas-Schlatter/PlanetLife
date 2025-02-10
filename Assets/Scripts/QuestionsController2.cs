using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;


public class QuestionsController2 : MonoBehaviour
{
    private Dictionary<string, Pergunta> perguntas;
    private List<string> perguntasIds;

    [SerializeField] public TextMeshProUGUI perTxt;
    [SerializeField] public TextMeshProUGUI q1Text;
    [SerializeField] public TextMeshProUGUI q2Text;
    [SerializeField] public TextMeshProUGUI q3Text;
    [SerializeField] public TextMeshProUGUI q4Text;

    public TwitterPosts twitterPosts;
    public AtmosphereChanger atmosphereChanger;

    public GameObject[] answerButtons;

    void Start()
    {
        string jsonPath = Application.dataPath + "/perguntas.json";
        string jsonData = System.IO.File.ReadAllText(jsonPath);
        perguntas = JsonConvert.DeserializeObject<Dictionary<string, Pergunta>>(jsonData);

        // Inicializa a lista de perguntas
        perguntasIds = new List<string>(perguntas.Keys);
        // Embaralha a lista de perguntas
        Shuffle(perguntasIds);

        foreach (string id in perguntasIds)
        {
            Pergunta pergunta = perguntas[id];
            Debug.Log(pergunta.pergunta);

            foreach (string alternativaId in pergunta.alternativas.Keys)
            {
                Alternativa alternativa = pergunta.alternativas[alternativaId];
                Debug.Log(alternativa.texto);

                // foreach (string tweet in alternativa.tweets)
                // {
                //     Debug.Log(tweet);
                // }
            }
        }

        // Exibe a primeira pergunta
        ShowNextQuestion();
    }

    public void ShowNextQuestion()
    {
        if (perguntasIds.Count == 0)
        {
            Debug.Log("Fim das perguntas");
            return;
        }

        string id = perguntasIds[0];
        Pergunta pergunta = perguntas[id];

        perTxt.text = pergunta.pergunta;
        q1Text.text = pergunta.alternativas["A"].texto;
        q2Text.text = pergunta.alternativas["B"].texto;
        q3Text.text = pergunta.alternativas["C"].texto;
        q4Text.text = pergunta.alternativas["D"].texto;

        
        StartCoroutine(WaitTimeButtons(5f));
        
        //perguntasIds.RemoveAt(0);
    }

    public void AnswerButtonClicked(string alternativaId)
    {
        // pega a alternativa selecionada
        Alternativa alternativa = perguntas[perguntasIds[0]].alternativas[alternativaId];
        //pega os tweets da alternativa
        List<string> tweets = alternativa.tweets;

        
        if (tweets.Count > 0)
        {
            foreach (string tweet in tweets)
            {
                twitterPosts.CreatePost(tweet);
            }
        }


        // pega o valor de impacto ambiental da alternativa
        int[] ambiental = alternativa.ambiental;

        // altera o valor da atmosfera
        atmosphereChanger.transitionValue += ambiental[0];
        float t = atmosphereChanger.transitionValue / 100f;

        atmosphereChanger.ChangeColorT(atmosphereChanger.atmosphereColorClean, atmosphereChanger.atmosphereColorPolluted, t);


    

        //remove a pergunta atual da lista
        perguntasIds.RemoveAt(0);

        if(perguntasIds.Count > 0)
        {
            ShowNextQuestion();
        }
        else
        {
            Debug.Log("Fim das perguntas");

            //endgame
        }
        
        
    }

    private void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            string temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public Pergunta GetPergunta(string id)
    {
        return perguntas.ContainsKey(id) ? perguntas[id] : null;
    }

    // Função que deixa os botões inativos por waitTime tempo
    public IEnumerator WaitTimeButtons(float waitTime)
    {
        // desativa o interactable dos botões de resposta
        foreach (GameObject button in answerButtons)
        {
            button.GetComponent<Button>().interactable = false;
        }

        yield return new WaitForSeconds(waitTime);

        // ativa o interactable dos botões de resposta
        foreach (GameObject button in answerButtons)
        {
            button.GetComponent<Button>().interactable = true;
        }

    }
    
}

[System.Serializable]
public class Pergunta
{
    public string pergunta;
    public Dictionary<string, Alternativa> alternativas;
}

[System.Serializable]
public class Alternativa
{
    public string texto;
    public int[] impacto;
    public int[] ambiental;
    public List<string> tweets;
}