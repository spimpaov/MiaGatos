using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGato : MonoBehaviour
{

    public AudioClip miado_amarelo_sucesso;
    public AudioClip miado_preto_sucesso;
    public AudioClip miado_cinza_sucesso;

    public AudioClip miado_falha;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioSource>();
    }


    public void setSoundCerto(Cor c)
    {
        switch (c)
        {
            case Cor.BLACK:
                audioSource.pitch = .7f;
                audioSource.PlayOneShot(miado_preto_sucesso);
                break;
            case Cor.YELLOW:
                audioSource.pitch = 1f;
                audioSource.PlayOneShot(miado_amarelo_sucesso);
                break;
            case Cor.WHITE:
                audioSource.pitch = 1.3f;
                audioSource.PlayOneShot(miado_cinza_sucesso);
                break;
            default:
                break;
        }

    }

    public void setSoundErrado()
    {
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(miado_falha);
    }

}
