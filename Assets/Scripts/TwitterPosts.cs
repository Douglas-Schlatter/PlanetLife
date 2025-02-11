using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

public class TwitterPosts : MonoBehaviour
{
    public List<string> personNames = new List<string>();
    public GameObject twitterPostPrefab;
 
    public float postHeight = 668f; // Altura de cada post
    public float maxContentHeight = 2000f; // Altura máxima do Content

    public GameObject twitterPanel;
    public Transform content;
    public GameObject imageNewMessage;
    public TextMeshProUGUI newMessageText;
    List<TwitterPost> posts;

    void Awake()
    {
        //LoadNames();

        // Pega o arquivo json e guarda suas informações na classe TwitterPost
        string jsonPath = Application.dataPath + "/pessoas.json";
        string jsonData = System.IO.File.ReadAllText(jsonPath);
        posts = JsonConvert.DeserializeObject<List<TwitterPost>>(jsonData);

        // imprime o nome de cada pessoa
        foreach (TwitterPost post in posts)
        {
            Debug.Log(post.nome);
            Debug.Log(post.username);
            Debug.Log(post.foto);
        }


        // pega o prefab "X - Post"
        GameObject postPrefab = Resources.Load<GameObject>("X - Post");

    }


    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // função que pega o arquivo de texto names.txt e adiciona os nomes na lista personNames
    public void LoadNames()
    {
        TextAsset names = Resources.Load<TextAsset>("names");
        string[] namesArray = names.text.Split('\n');
        foreach (string name in namesArray)
        {
            personNames.Add(name);
        }
    }

    public void CloseTwitter()
    {
        twitterPanel.SetActive(false);
    }

    public void OpenTwitter()
    {
        // se o painel de twitter já estiver ativo, desativa ele
        if (twitterPanel.activeSelf)
        {
            twitterPanel.SetActive(false);
            return;
        }
        else
        {
            twitterPanel.SetActive(true);
            imageNewMessage.SetActive(false);
            newMessageText.text = "0";
        }
    }

    // função para criar um post no twitter
    public void CreatePost(string tweet)
    {
        // Pega o prefab "X - Post"
        GameObject postPrefab = Resources.Load<GameObject>("X - Post");

        // Instancia o prefab
        GameObject post = Instantiate(postPrefab, content);

        // Mover o post para o topo da lista
        post.transform.SetSiblingIndex(0);

        // Pega o texto do post de nome "Text (TMP) Name"
        TextMeshProUGUI postText = post.transform.Find("Text (TMP) Name").GetComponent<TextMeshProUGUI>();

        // Pega um nome aleatório da lista de nomes
        //string randomName = personNames[Random.Range(0, personNames.Count)];
        int randomIndex = Random.Range(0, posts.Count);
        string randomName = posts[randomIndex].nome;



        // Adiciona o nome ao post
        postText.text = randomName;

        // Pega o texto do post de nome "Text (TMP) PostText"
        TextMeshProUGUI postTextText = post.transform.Find("Text (TMP) PostText").GetComponent<TextMeshProUGUI>();

        postTextText.text = tweet;

        // Adiciona hashtags ao texto do post
        postTextText.text = HighlightHashtags(postTextText.text);

        // verifica se o texto de postText tem unicode e substitui pela imagem correspondente
        postTextText.text = ReplaceUnicodeWithImage(postTextText.text);


        // pega a imagem do post de nome "Image Photo"
        Image postImage = post.transform.Find("Image Photo").GetComponent<Image>();

        // pega a imagem correspondente ao nome
        Sprite image = Resources.Load<Sprite>(posts[randomIndex].foto);

        // Adiciona a imagem ao post
        postImage.sprite = image;

        // pega o texto do post de nome "Text (TMP) Nickname"
        TextMeshProUGUI postNickname = post.transform.Find("Text (TMP) Nickname").GetComponent<TextMeshProUGUI>();

        // Adiciona o nome de usuário ao post
        postNickname.text = "@" + posts[randomIndex].username;


        

        // verifica se o twitterPanel está ativo
        if (!twitterPanel.activeSelf)
        {
            if(!imageNewMessage.activeSelf)
            {
                imageNewMessage.SetActive(true);
            }

            // pega o texto em newMessageText, convertendo para int e incrementa
            int newMessage = int.Parse(newMessageText.text) + 1;
            // converte o int para string e adiciona ao newMessageText
            newMessageText.text = newMessage.ToString();
        }
        
    }

    string HighlightHashtags(string inputText)
    {
        // Expressão regular para encontrar palavras que começam com '#'
        string pattern = @"#\S+";  // Encontra todas as palavras que começam com '#'

        // Substituir a palavra encontrada com a tag <color>
        string highlightedText = Regex.Replace(inputText, pattern, match => 
        {
            return $"<color=blue>{match.Value}</color>"; // Colore de azul o trecho que começa com '#'
        });

        return highlightedText;
    }

    // função para trocar unicode por imagens
    string ReplaceUnicodeWithImage(string inputText)
    {
        // Expressão regular para encontrar unicode
        string pattern = @"\\u\w{4}";  // Encontra todos os unicode

        // Substituir o unicode encontrado pela imagem correspondente
        string replacedText = Regex.Replace(inputText, pattern, match => 
        {
            // Pega o unicode e converte para string
            string unicode = match.Value;
            // Pega o código unicode e converte para int
            int code = int.Parse(unicode.Substring(2), System.Globalization.NumberStyles.HexNumber);
            // Converte o int para char
            char character = (char)code;
            // Pega o nome do char
            string charName = character.ToString();
    
            // Pega a imagem correspondente ao nome
            Sprite image = Resources.Load<Sprite>(charName);
            // Retorna a imagem em formato de texto
            return $"<sprite name={charName}>";
        });

        return replacedText;
    }

    

    
}

[System.Serializable]
public class TwitterPost
{
    public string nome;
    public string username;
    public string foto;
}