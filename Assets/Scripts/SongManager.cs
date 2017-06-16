using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{

    enum musica
    {
        SONGOFTIME,
        INFINITE
    }

    private class notaTempo
    {
        public Cor cor;
        public float tempo;
        public notaTempo(Cor _cor, float _tempo)
        {
            cor = _cor;
            tempo = _tempo;
        }
    }

    [SerializeField]
    private musica song;

    private ScoreManager score;

    private GameObject notaLinha;
    [SerializeField]
    private GameObject notaB, notaW, notaY;
    private RectTransform notaPos;

    private static int count = 0;

    public List<Nota> NotasPretas;
    public List<Nota> NotasBrancas;
    public List<Nota> NotasAmarelas;

    private Queue<notaTempo> noteQueue;

    [SerializeField]
    private GameObject gameOverBalloon, victoryBalloon;

    void Start()
    {
        notaLinha = GameObject.Find("HUD-Linha");
        notaPos = GameObject.Find("NotaPos").GetComponent<RectTransform>();
        noteQueue = new Queue<notaTempo>();

        if (song == musica.INFINITE) //StartCoroutine(infiniteSong());
            loadRandomSong();
        else if (song == musica.SONGOFTIME)
            loadSongOfTime();
        StartCoroutine(playSong());

        score = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();

    }

    void loadSongOfTime() {
        noteQueue.Enqueue(new notaTempo(Cor.WHITE, .7f));
        noteQueue.Enqueue(new notaTempo(Cor.BLACK, 1f));
        noteQueue.Enqueue(new notaTempo(Cor.YELLOW, .55f));
        noteQueue.Enqueue(new notaTempo(Cor.WHITE, .55f));
        noteQueue.Enqueue(new notaTempo(Cor.BLACK, 1f));
        noteQueue.Enqueue(new notaTempo(Cor.YELLOW, .55f));
        noteQueue.Enqueue(new notaTempo(Cor.WHITE, .3f));
        noteQueue.Enqueue(new notaTempo(Cor.YELLOW, .3f));
        noteQueue.Enqueue(new notaTempo(Cor.WHITE, .7f));
        noteQueue.Enqueue(new notaTempo(Cor.BLACK, .7f));
        noteQueue.Enqueue(new notaTempo(Cor.YELLOW, .3f));
        noteQueue.Enqueue(new notaTempo(Cor.BLACK, .3f));
        noteQueue.Enqueue(new notaTempo(Cor.WHITE, .6f));
        noteQueue.Enqueue(new notaTempo(Cor.YELLOW, .6f));
        noteQueue.Enqueue(new notaTempo(Cor.BLACK, .4f));
        noteQueue.Enqueue(new notaTempo(Cor.WHITE, .4f));
        noteQueue.Enqueue(new notaTempo(Cor.BLACK, 0f));
    }

    void loadRandomSong() {
        for (int i = 0; i < 25; i++) {
            noteQueue.Enqueue(new notaTempo((Cor)Random.Range(0, 3), Random.Range(.3f, 1.1f)));
        }
        noteQueue.Enqueue(new notaTempo((Cor)Random.Range(0, 3), 0f));
    }

    IEnumerator playSong()
    {
        notaTempo nt;
        while (noteQueue.Count > 0)
        {
            nt = noteQueue.Dequeue();
            spawnNota(nt.cor);
            yield return new WaitForSeconds(nt.tempo);
        }

        while (NotasAmarelas.Count > 0 || NotasBrancas.Count > 0 || NotasPretas.Count > 0)
        {
            NotasPretas.RemoveAll(item => item == null);
            NotasBrancas.RemoveAll(item => item == null);
            NotasAmarelas.RemoveAll(item => item == null);
            yield return new WaitForSeconds(.3f);
        }
        StartCoroutine(victory());
    }

    IEnumerator infiniteSong()
    {
        while (true)
        {
            spawnNota((Cor)Random.Range(0, 3));
            yield return new WaitForSeconds(Random.Range(.5f, 1.5f));
        }
    }

    void spawnNota(Cor c)
    {
        GameObject go;
        if (c == Cor.BLACK) go = Instantiate(notaB) as GameObject;
        else if (c == Cor.WHITE) go = Instantiate(notaW) as GameObject;
        else go = Instantiate(notaY) as GameObject;
        RectTransform rt = go.GetComponent<RectTransform>();
        Nota n = go.GetComponent<Nota>();
        go.transform.SetParent(notaLinha.transform, true);
        go.transform.localPosition = notaPos.localPosition;
        go.transform.localScale = notaPos.localScale;
        rt.anchoredPosition = notaPos.anchoredPosition;
        rt.sizeDelta = notaPos.sizeDelta;
        n.cor = c;

        if (c == Cor.BLACK) NotasPretas.Add(n);
        else if (c == Cor.WHITE) NotasBrancas.Add(n);
        else if (c == Cor.YELLOW) NotasAmarelas.Add(n);

        count++;
    }

    public IEnumerator gameOver()
    {
        StopAllCoroutines();
        foreach (Nota n in NotasAmarelas)
        {
            if (n != null) n.rb.velocity = Vector2.zero;
        }
        foreach (Nota n in NotasPretas)
        {
            if (n != null) n.rb.velocity = Vector2.zero;
        }
        foreach (Nota n in NotasBrancas)
        {
            if (n != null) n.rb.velocity = Vector2.zero;
        }

        gameOverBalloon.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("InterScene").GetComponent<InterScene>().LoadScene("GameOverMenu");
    }

    IEnumerator victory()
    {
        //StopAllCoroutines();
        victoryBalloon.SetActive(true);
        GameObject.Find("HealthManager").GetComponent<HealthManager>().passiveDmg = 0;
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("InterScene").GetComponent<InterScene>().LoadScene("GameOverMenu");
    }
}
