using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TwitterPosts : MonoBehaviour
{
    public List<string> personNames = new List<string>();
    public GameObject twitterPostPrefab;
    public Transform content;
    public float postHeight = 668f; // Altura de cada post
    public float maxContentHeight = 2000f; // Altura máxima do Content

    public GameObject twitterPanel;

    void Awake()
    {
        LoadNames();

        // pega o prefab "X - Post"
        GameObject postPrefab = Resources.Load<GameObject>("X - Post");

    }


    // Start is called before the first frame update
    void Start()
    {
        CreatePost();
        
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
        }
    }

    // função para criar um post no twitter
    public void CreatePost()
    {
        // Pega o prefab "X - Post"
        GameObject postPrefab = Resources.Load<GameObject>("X - Post");

        // Instancia o prefab
        GameObject post = Instantiate(postPrefab, content);

        // Pega o texto do post de nome "Text (TMP) Name"
        TextMeshProUGUI postText = post.transform.Find("Text (TMP) Name").GetComponent<TextMeshProUGUI>();

        // Pega um nome aleatório da lista de nomes
        string randomName = personNames[Random.Range(0, personNames.Count)];

        // Pega o texto do post e substitui o nome do jogador pelo nome aleatório
        postText.text = postText.text.Replace("PlayerName", randomName);

        // Ajusta o tamanho do Content
        RectTransform contentRect = content.GetComponent<RectTransform>();
        float newHeight = contentRect.sizeDelta.y + postHeight;
        if (newHeight <= maxContentHeight)
        {
            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, newHeight);
        }
        else
        {
            Debug.LogWarning("Content height limit reached.");
        }
    }
}
