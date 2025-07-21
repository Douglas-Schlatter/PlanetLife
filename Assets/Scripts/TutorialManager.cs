using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;          // Painel geral do tutorial
    public GameObject gameplayPanel;          // Painel de gameplay
    public GameObject earth;
    public GameObject[] slides;               // Array com todos os slides
    private int currentSlide = 0;

    void Start()
    {
        gameplayPanel.SetActive(false);
        earth.SetActive(false);


        ShowSlide(0); // Mostra o primeiro slide ao iniciar
    }

    public void NextSlide()
    {
        slides[currentSlide].SetActive(false);
        currentSlide++;

        if (currentSlide < slides.Length)
        {
            slides[currentSlide].SetActive(true);
        }
        else
        {
            CloseTutorial();
        }
    }

    void ShowSlide(int index)
    {
        // Desativa todos os slides antes de ativar o primeiro
        for (int i = 0; i < slides.Length; i++)
        {
            slides[i].SetActive(i == index);
        }
    }

    void CloseTutorial()
    {
        gameplayPanel.SetActive(true); // Ativa o painel de gameplay
        earth.SetActive(true); 
        tutorialPanel.SetActive(false); // Esconde o painel todo
    }
}