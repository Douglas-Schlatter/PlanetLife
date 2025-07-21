using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Obtém o componente AudioSource anexado ao GameObject
        audioSource = GetComponent<AudioSource>();

        // Certifica-se de que o som será reproduzido em loop
        if (audioSource != null)
        {
            audioSource.loop = true;
            audioSource.Play(); // Inicia a reprodução do som
        }
        else
        {
            Debug.LogError("Nenhum AudioSource encontrado no GameObject.");
        }
    }
}
