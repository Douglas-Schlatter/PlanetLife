using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PerguntaRespondida {
    public string enunciado;
    public List<string> alternativas;
    public string respostaSelecionada;
}

[System.Serializable]
public class ResultadoFinal {
    public string nomeJogador;
    public List<PerguntaRespondida> perguntas = new List<PerguntaRespondida>();
    public float impactoAgua;
    public float impactoSolo;
    public float impactoAr;
    public float impactoGoverno;
    public float impactoEmpresas;
    public float impactoSociedade;
    public int pontuacaoFinal;
    public string dataHora;
}

public class GameDataCollector : MonoBehaviour {
    public static GameDataCollector instance;

    private ResultadoFinal resultado = new ResultadoFinal();

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mantém entre cenas
        } else {
            Destroy(gameObject);
        }
    }

    public void DefinirNomeJogador(string nome) {
        resultado.nomeJogador = nome;
    }

    public void AdicionarPergunta(string enunciado, List<string> alternativas, string respostaSelecionada) {
        PerguntaRespondida p = new PerguntaRespondida {
            enunciado = enunciado,
            alternativas = alternativas,
            respostaSelecionada = respostaSelecionada
        };
        resultado.perguntas.Add(p);
    }

    public void RegistrarPergunta(string enunciado, List<string> alternativas) 
    {
        PerguntaRespondida p = new PerguntaRespondida {
            enunciado = enunciado,
            alternativas = alternativas,
            respostaSelecionada = ""
        };
        resultado.perguntas.Add(p);
    }

    public void RegistrarResposta(string respostaSelecionada) 
    {
        if (resultado.perguntas.Count == 0) {
            Debug.LogWarning("Nenhuma pergunta registrada para adicionar resposta.");
            return;
        }

        resultado.perguntas[resultado.perguntas.Count - 1].respostaSelecionada = respostaSelecionada;
    }



    public void SomarImpactos(float agua, float solo, float ar, float governo, float empresas, float sociedade) {
        resultado.impactoAgua += agua;
        resultado.impactoSolo += solo;
        resultado.impactoAr += ar;
        resultado.impactoGoverno += governo;
        resultado.impactoEmpresas += empresas;
        resultado.impactoSociedade += sociedade;
    }

    public void SomarImpactoAgua(float valor) {
    resultado.impactoAgua += valor;
    }

    public void SomarImpactoSolo(float valor) {
        resultado.impactoSolo += valor;
    }

    public void SomarImpactoAr(float valor) {
        resultado.impactoAr += valor;
    }

    public void SomarImpactoGoverno(float valor) {
        resultado.impactoGoverno += valor;
    }

    public void SomarImpactoEmpresas(float valor) {
        resultado.impactoEmpresas += valor;
    }

    public void SomarImpactoSociedade(float valor) {
        resultado.impactoSociedade += valor;
    }

    public void DefinirPontuacaoFinal(int pontuacao) {
        resultado.pontuacaoFinal = pontuacao;
    }

    public void SalvarDados() {
    resultado.dataHora = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

    string json = JsonUtility.ToJson(resultado, true);
    string path = Application.persistentDataPath + "/resultado_" + resultado.nomeJogador + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".json";
    File.WriteAllText(path, json);

    Debug.Log("Dados salvos em: " + path);

    // Reseta os dados para a próxima jogada
    resultado = new ResultadoFinal();
}
}
