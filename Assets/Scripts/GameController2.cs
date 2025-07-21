using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController2 : MonoBehaviour
{
    public int governo = 50;
    public int sociedade = 50;
    public int empresas = 50;

    public int score_final;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(governo < 0)
        {
            governo = 0;
        }
        if(governo > 100)
        {
            governo = 100;
        }

        if(sociedade < 0)
        {
            sociedade = 0;
        }
        if(sociedade > 100)
        {
            sociedade = 100;
        }

        if(empresas < 0)
        {
            empresas = 0;
        }
        if(empresas > 100)
        {
            empresas = 100;
        }
        
    }

    public void ReiniciarJogo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SairJogo()
    {
        Application.Quit();
    }

    public void VoltarMenu()
    {
        SceneManager.LoadScene("Menu");
    }


    public float calcular_score(float poluicao_agua, float poluicao_terra, float poluicao_ar)
    {
        float poluicao_media = (poluicao_agua + poluicao_terra + poluicao_ar);

        if(poluicao_media != 0)
        {
            poluicao_media /= 3;
        }

        float sustentabilidade = 100 - poluicao_media;

        float equilibrio = (100 - Mathf.Abs(100 - governo)) + (100 - Mathf.Abs(100 - sociedade)) + (100 - Mathf.Abs(100 - empresas));

        if (equilibrio != 0)
        {
            equilibrio /= 3;
        }


        // Combinação de sustentabilidade (70%) e equilíbrio (30%)
        float score = (sustentabilidade * 7 + equilibrio * 3) / 10;

        return Mathf.Clamp(score, 0, 100); // Garante que fique entre 0 e 100
    }

    public float Calcular_sustentabilidade(float poluicao_agua, float poluicao_terra, float poluicao_ar)
    {
        float poluicao_media = (poluicao_agua + poluicao_terra + poluicao_ar);

        if(poluicao_media != 0)
        {
            poluicao_media /= 3;
        }

        float sustentabilidade = 100 - poluicao_media;

        return sustentabilidade;
    }

    public float Calcular_equilibrio()
    {
        float equilibrio = (100 - Mathf.Abs(100 - governo)) + (100 - Mathf.Abs(100 - sociedade)) + (100 - Mathf.Abs(100 - empresas));

        if (equilibrio != 0)
        {
            equilibrio /= 3;
        }

        return equilibrio;
    }

}
