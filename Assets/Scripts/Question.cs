using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{
    // Start is called before the first frame update
    //Para implementar os multiplos scores dos diferentes tipos de jogador, seria bom trocar o inst score por um vetor, que de acordo
    //com a aposição seráo score afetado pela pergunta
    //depois é so intanciar pelo gamecontroller
    public int id;
    public string question = "";
    public string answ1 = "";
    public string answ2 = "";
    public string answ3 = "";
    public string answ4 = "";

    public float[][] answScores = new float[4][]{
    new float[] {0.0f, 0.0f, 0.0f, 0.0f, 0.0f},// answ1
    new float[] {0.0f, 0.0f, 0.0f, 0.0f, 0.0f},// answ2
    new float[] {0.0f, 0.0f, 0.0f, 0.0f, 0.0f},// answ3
    new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f }// answ4
    };
    public float[] minimumScore = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    public float[] maximumScore = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

   
    public Question() 
    { 
         
    }
    // tem um jeito melhor de fazer isso,sendo ou criando isso junto de um new ou com labda
    // a versão com new teria de re organizar de modo que todo new dessa classe ja teria de ser oferecido
    //juntos e logo depois fazer o maximumScore a partir desses argumentos
    //com labda poderia atualizar automatico dado uma fuinção, mas teria de pesquisar mais
    //acho que seria com logica de pipeline .net em c
    //essa forma não é tão robusta, mas funciona
    public void AttMaxMinScore() 
    {
        float maxMoment = 0;
        float minMoment = Mathf.Infinity;
        for (int i = 0; i < 5; i++)
        {
            foreach (float[] j in answScores)
            {
                if (maxMoment < j[i])
                {
                    maxMoment = j[i];
                }
            }
            maximumScore[i] = maxMoment;
            maxMoment = 0;
        }
        for (int i = 0; i < 5; i++)
        {
            foreach (float[] j in answScores)
            {
                if (minMoment > j[i])
                {
                    minMoment = j[i];
                }
            }
            minimumScore[i] = minMoment;
            minMoment = Mathf.Infinity;
        }
    }

}
