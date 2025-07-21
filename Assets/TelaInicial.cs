using UnityEngine;
using UnityEngine.UI;
using TMPro; // Se estiver usando TextMeshPro

public class TelaInicial : MonoBehaviour {
    public TMP_InputField inputNome;
    public GameObject painel; // para esconder após início
    public GameObject painelJogo;

    void Start()
    {
        painel.SetActive(true); // mostra painel de nome
        painelJogo.SetActive(false); // oculta painel do jogo
    }

    public void ConfirmarNome() {
        string nome = inputNome.text.Trim();

        if (!string.IsNullOrEmpty(nome)) {
            GameDataCollector.instance.DefinirNomeJogador(nome);
            painelJogo.SetActive(true); // mostra painel do jogo
            painel.SetActive(false); // oculta painel de nome
            Debug.Log("Nome definido: " + nome);

        } else {
            Debug.LogWarning("Nome inválido.");
        }
    }
}