using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Linq;
using System.IO;



public class QuestionsController2 : MonoBehaviour
{
    public Dictionary<string, Pergunta> perguntas;
    private List<string> perguntasIds;

    [SerializeField] public TextMeshProUGUI perTxt;
    [SerializeField] public TextMeshProUGUI q1Text;
    [SerializeField] public TextMeshProUGUI q2Text;
    [SerializeField] public TextMeshProUGUI q3Text;
    [SerializeField] public TextMeshProUGUI q4Text;

    public TwitterPosts twitterPosts;
    public AtmosphereChanger atmosphereChanger;
    public ChangeAmbientColor changeAmbientColor;

    public GameController2 gameController;

    public GameObject[] answerButtons;

    public Image newspaper;
    public GameObject earth;

    public int questionIndex = 0;

    List<GameObject> botoes = new List<GameObject>();

    Vector3[] posicoesOriginais;

    public GameObject planet;
    public GameObject canvas;
    public GameObject gameOverScreen;
    public TextMeshProUGUI FinalScoreText;

    public FinalScoreSlider finalScoreSlider;
    
    public GameObject twitterLogoButton;
    public GameObject twitterPanel;
    

    void Start()
    {
        // desativa o jornal
        newspaper.gameObject.SetActive(false);
        // desativa o botão de logo do twitter
        twitterLogoButton.SetActive(false);
        // desativa o painel do twitter
        twitterPanel.SetActive(false);

        // Cria uma lista de botões na ordem original
        botoes = new List<GameObject>
        {
            answerButtons[0], // Botão A
            answerButtons[1], // Botão B
            answerButtons[2], // Botão C
            answerButtons[3]  // Botão D
        };

        // Salva as posições originais dos botões
        posicoesOriginais = new Vector3[botoes.Count];
        for (int i = 0; i < botoes.Count; i++)
        {
            posicoesOriginais[i] = botoes[i].transform.position;
        }


        //string jsonPath = Application.dataPath + "/perguntas.json";
        //string jsonData = System.IO.File.ReadAllText(jsonPath);

        //perguntas = JsonConvert.DeserializeObject<Dictionary<string, Pergunta>>(jsonData);

        string caminho = Path.Combine(Application.streamingAssetsPath, "perguntas.json");

        
        string jsonData = File.ReadAllText(caminho);
        perguntas = JsonConvert.DeserializeObject<Dictionary<string, Pergunta>>(jsonData);
        //Debug.Log("Perguntas carregadas: " + perguntas.Count);
        
        
        
       

        // Inicializa a lista de perguntas
        //perguntasIds = new List<string>(perguntas.Keys);

        // cria uma lista com perguntas específicas, por exemplo P1, P7, P10
        perguntasIds = new List<string> { "P2", "P6", "P9", "P19"};

        // Embaralha a lista de perguntas
        //Shuffle(perguntasIds);

        // Embaralha a lista de perguntas manualmente
        for (int i = 0; i < perguntasIds.Count; i++)
        {
            int randomIndex = Random.Range(i, perguntasIds.Count);
            string temp = perguntasIds[i];
            perguntasIds[i] = perguntasIds[randomIndex];
            perguntasIds[randomIndex] = temp;
        }
        

        foreach (string id in perguntasIds)
        {
            Pergunta pergunta = perguntas[id];
            //Debug.Log(pergunta.pergunta);

            foreach (string alternativaId in pergunta.alternativas.Keys)
            {
                Alternativa alternativa = pergunta.alternativas[alternativaId];
                //Debug.Log(alternativa.texto);

                // foreach (string tweet in alternativa.tweets)
                // {
                //     Debug.Log(tweet);
                // }
            }
        }

        // Exibe a primeira pergunta
        ShowNextQuestion();
    }

    private void Shuffle2<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
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
        // Remove o número inicial e o traço da pergunta (exemplo: "7- ")
        string perguntaLimpa = Regex.Replace(pergunta.pergunta, @"^\d+\s*-\s*", "");

        // Cria uma lista de alternativas
        List<KeyValuePair<string, string>> alternativasComIds = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("A", pergunta.alternativas["A"].texto),
            new KeyValuePair<string, string>("B", pergunta.alternativas["B"].texto),
            new KeyValuePair<string, string>("C", pergunta.alternativas["C"].texto),
            new KeyValuePair<string, string>("D", pergunta.alternativas["D"].texto)
        };

        //GameDataCollector.instance.AdicionarPergunta(perguntaLimpa, alternativasComIds.Select(x => x.Value).ToList(), "");
        GameDataCollector.instance.RegistrarPergunta(perguntaLimpa, alternativasComIds.Select(x => x.Value).ToList());

        // Embaralha as alternativas
        Shuffle2(alternativasComIds);

        
        

        
        // Remove os prefixos
        for (int i = 0; i < alternativasComIds.Count; i++)
        {
            alternativasComIds[i] = new KeyValuePair<string, string>(alternativasComIds[i].Key, alternativasComIds[i].Value.Replace("A) ", "").Replace("B) ", "").Replace("C) ", "").Replace("D) ", ""));
        }

        // Cria uma nova lista para armazenar os botões reorganizados
        List<GameObject> botoesReordenados = new List<GameObject>();

        // Reorganiza os botões na UI com base na ordem embaralhada
        for (int i = 0; i < alternativasComIds.Count; i++)
        {
            switch (alternativasComIds[i].Key)
            {
                case "A":
                    botoes[0].transform.position = posicoesOriginais[i];
                    botoes[0].GetComponentInChildren<TextMeshProUGUI>().text = alternativasComIds[i].Value;
                    botoesReordenados.Add(botoes[0]);
                    break;
                case "B":
                    botoes[1].transform.position = posicoesOriginais[i];
                    botoes[1].GetComponentInChildren<TextMeshProUGUI>().text = alternativasComIds[i].Value;
                    botoesReordenados.Add(botoes[1]);
                    break;
                case "C":
                    botoes[2].transform.position = posicoesOriginais[i];
                    botoes[2].GetComponentInChildren<TextMeshProUGUI>().text = alternativasComIds[i].Value;
                    botoesReordenados.Add(botoes[2]);
                    break;
                case "D":
                    botoes[3].transform.position = posicoesOriginais[i];
                    botoes[3].GetComponentInChildren<TextMeshProUGUI>().text = alternativasComIds[i].Value;
                    botoesReordenados.Add(botoes[3]);
                    break;
            }
        }

        // Atualiza os textos dos botões com as alternativas embaralhadas
        for (int i = 0; i < botoesReordenados.Count; i++)
        {
            string letter;
            if (i == 0)
                letter = "A) ";
            else if (i == 1)
                letter = "B) ";
            else if (i == 2)
                letter = "C) ";
            else
                letter = "D) ";

            botoesReordenados[i].GetComponentInChildren<TextMeshProUGUI>().text = letter + botoesReordenados[i].GetComponentInChildren<TextMeshProUGUI>().text;
        }

        // após usar os botões, reorganizar eles na posição original



        // adiciona o Número da pergunta
        questionIndex++;
        perTxt.text = questionIndex + ": " + perguntaLimpa;

        //perTxt.text = pergunta.pergunta;
        // Define os textos embaralhados nos campos TextMeshProUGUI
        // q1Text.text = alternativas[0];
        // q2Text.text = alternativas[1];
        // q3Text.text = alternativas[2];
        // q4Text.text = alternativas[3];

        
        StartCoroutine(WaitTimeButtons(15f));
        
        //perguntasIds.RemoveAt(0);
    }

    public void AnswerButtonClicked(string alternativaId)
    {
        twitterPosts.ClearPosts();
        // desativa o botão de logo do twitter
        twitterLogoButton.SetActive(false);
        // desativa o botão de logo do twitter
        twitterPosts.imageNewMessage.SetActive(false);
        twitterPosts.imageNewMessage.GetComponentInChildren<TextMeshProUGUI>().text = "0";

        // desativa o painel do twitter
        twitterPanel.SetActive(false);
        // desativa o jornal
        newspaper.gameObject.SetActive(false);
        

        // volte os botões para a posição original
        for (int i = 0; i < botoes.Count; i++)
        {
            botoes[i].transform.position = posicoesOriginais[i];
        }

        // pega a alternativa selecionada
        Alternativa alternativa = perguntas[perguntasIds[0]].alternativas[alternativaId];


        //pega os tweets da alternativa
        if (alternativa.tweets != null)
        {
            List<string> tweets = alternativa.tweets;

            if (tweets.Count > 0)
            {
                foreach (string tweet in tweets)
                {
                    twitterPosts.CreatePost2(tweet);
                }
            }

            // mostra a logo do twitter
            twitterLogoButton.SetActive(true);

            twitterPosts.imageNewMessage.SetActive(true);
        }

        int governo = alternativa.governo;
        int empresas = alternativa.empresas;
        int sociedade = alternativa.sociedade;

        // altera os valores do governo, empresas e sociedade
        gameController.governo += governo;
        gameController.empresas += empresas;
        gameController.sociedade += sociedade;

  
        //GameDataCollector.instance.SomarImpactos(0, 0, 0, governo, empresas, sociedade);
        GameDataCollector.instance.SomarImpactoGoverno(governo);
        GameDataCollector.instance.SomarImpactoEmpresas(empresas);
        GameDataCollector.instance.SomarImpactoSociedade(sociedade);



        // pega o valor de impacto ambiental da alternativa
        //int[] ambiental = alternativa.ambiental;
        int impactoAtmosferico = alternativa.impactoAtmosferico;
        int impactoSolo = alternativa.impactoSolo;
        int impactoHidrico = alternativa.impactoHidrico;
        


        // altera o valor da atmosfera
        atmosphereChanger.transitionValue += impactoAtmosferico;
        if(atmosphereChanger.transitionValue > 100)
        {
            atmosphereChanger.transitionValue = 100;
        }
        else if(atmosphereChanger.transitionValue < 0)
        {
            atmosphereChanger.transitionValue = 0;
        }

        GameDataCollector.instance.SomarImpactoAr(impactoAtmosferico);
        
        float t = atmosphereChanger.transitionValue / 100f;

        atmosphereChanger.ChangeColorT(atmosphereChanger.atmosphereColorClean, atmosphereChanger.atmosphereColorPolluted, t);

        // altera o valor do solo
        changeAmbientColor.transitionValueGround += impactoSolo;
        if(changeAmbientColor.transitionValueGround > 100)
        {
            changeAmbientColor.transitionValueGround = 100;
        }
        else if(changeAmbientColor.transitionValueGround < 0)
        {
            changeAmbientColor.transitionValueGround = 0;
        }

        GameDataCollector.instance.SomarImpactoSolo(impactoSolo);

        t = changeAmbientColor.transitionValueGround / 100f;

        changeAmbientColor.ChangeColorT(changeAmbientColor.startGroundColor, changeAmbientColor.endGroundColor, t, changeAmbientColor.midColor);

        // altera o valor da água
        changeAmbientColor.transitionValueWater += impactoHidrico;

        if(changeAmbientColor.transitionValueWater > 100)
        {
            changeAmbientColor.transitionValueWater = 100;
        }
        else if(changeAmbientColor.transitionValueWater < 0)
        {
            changeAmbientColor.transitionValueWater = 0;
        }

        GameDataCollector.instance.SomarImpactoAgua(impactoHidrico);

        t = changeAmbientColor.transitionValueWater / 100f;

        changeAmbientColor.ChangeColorT(changeAmbientColor.startWaterColor, changeAmbientColor.endWaterColor, t, changeAmbientColor.bottomColor);




        // mostra o jornal
        // if(alternativa.news != null)
        // {
        //     newspaper.GetComponentInChildren<TextMeshProUGUI>().text = alternativa.news;
        //     MoveEarthAndNewspaper();
        // }

        // guarda apenas a resposta selecionada
        GameDataCollector.instance.RegistrarResposta(alternativaId);

        //remove a pergunta atual da lista
        perguntasIds.RemoveAt(0);

        if(perguntasIds.Count > 0)
        {

            if (questionIndex != 4)
            {
                if(alternativa.news != null)
                {
                    newspaper.gameObject.SetActive(true);
                    newspaper.GetComponentInChildren<TextMeshProUGUI>().text = alternativa.news;
                    MoveEarthAndNewspaper();
                }

                ShowNextQuestion();
            }
            else
            {
                Debug.Log("Fim das perguntas");

                // faz o cálculo do score
                float score = gameController.calcular_score(atmosphereChanger.transitionValue, changeAmbientColor.transitionValueGround, changeAmbientColor.transitionValueWater);
                Debug.Log("Score: " + score);

                GameDataCollector.instance.DefinirPontuacaoFinal((int)score);
                GameDataCollector.instance.SalvarDados();

                //FinalScoreText.text = "Score Final: " + score.ToString();

                //endgame
                // desativa o planeta
                planet.SetActive(false);
                // desativa o canvas
                canvas.SetActive(false);
                // ativa o canvas do game over
                gameOverScreen.SetActive(true);

                //finalScoreSlider.SetFinalScore(score);
                float sustentabilidade = gameController.Calcular_sustentabilidade(atmosphereChanger.transitionValue, changeAmbientColor.transitionValueGround, changeAmbientColor.transitionValueWater);
                float equilibrio = gameController.Calcular_equilibrio();

                finalScoreSlider.SetFinalStats(score, sustentabilidade, equilibrio);
            }
            
        }
        else
        {

            Debug.Log("Fim das perguntas");

            // faz o cálculo do score
            float score = gameController.calcular_score(atmosphereChanger.transitionValue, changeAmbientColor.transitionValueGround, changeAmbientColor.transitionValueWater);
            Debug.Log("Score: " + score);

            GameDataCollector.instance.DefinirPontuacaoFinal((int)score);
            GameDataCollector.instance.SalvarDados();

            //FinalScoreText.text = "Score Final: " + score.ToString();

            //endgame
            // desativa o planeta
            planet.SetActive(false);
            // desativa o canvas
            canvas.SetActive(false);
            // ativa o canvas do game over
            gameOverScreen.SetActive(true);

            //finalScoreSlider.SetFinalScore(score);
            float sustentabilidade = gameController.Calcular_sustentabilidade(atmosphereChanger.transitionValue, changeAmbientColor.transitionValueGround, changeAmbientColor.transitionValueWater);
            float equilibrio = gameController.Calcular_equilibrio();

            finalScoreSlider.SetFinalStats(score, sustentabilidade, equilibrio);

        }
        
        
    }

    // Função para mostrar o jornal
    public void MoveEarthAndNewspaper()
    {
        StartCoroutine(MoveObjects());
    }

    private IEnumerator MoveObjects()
    {
        Vector3 originalEarthPosition = earth.transform.position;
        Vector3 originalNewspaperPosition = newspaper.transform.position;

        Vector3 targetEarthPosition = originalEarthPosition + new Vector3(0, -2.8f, 0); // Ajuste o valor conforme necessário
        Vector3 targetNewspaperPosition = originalNewspaperPosition + new Vector3(0, -8, 0); // Ajuste o valor conforme necessário

        float duration = 1f; // Duração do movimento
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            earth.transform.position = Vector3.Lerp(originalEarthPosition, targetEarthPosition, elapsedTime / duration);
            newspaper.transform.position = Vector3.Lerp(originalNewspaperPosition, targetNewspaperPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        earth.transform.position = targetEarthPosition;
        newspaper.transform.position = targetNewspaperPosition;

        yield return new WaitForSeconds(10f); // Espera 10 segundos

        elapsedTime = 0;
        while (elapsedTime < duration)
        {
            earth.transform.position = Vector3.Lerp(targetEarthPosition, originalEarthPosition, elapsedTime / duration);
            newspaper.transform.position = Vector3.Lerp(targetNewspaperPosition, originalNewspaperPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        earth.transform.position = originalEarthPosition;
        newspaper.transform.position = originalNewspaperPosition;

        newspaper.gameObject.SetActive(false); // Desativa o jornal após o movimento
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
    public int impactoAtmosferico;
    public int impactoSolo;
    public int impactoHidrico;
    public int governo;
    public int empresas;
    public int sociedade;
    public List<string> tweets;
    public List<string> instas;
    public string news;
}