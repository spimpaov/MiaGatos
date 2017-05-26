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


    public void setSoundCerto()
    {
        audioSource.PlayOneShot(miado_amarelo_sucesso);
    }

    public void setSoundErrado()
    {
        audioSource.PlayOneShot(miado_falha);
    }

}
