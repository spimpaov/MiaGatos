using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour {

    enum musica {
        SONGOFTIME,
        INFINITE
    }

    [SerializeField]
    private musica song;

    private GameObject notaLinha;
    [SerializeField]
    private GameObject notaB, notaW, notaY;
    private RectTransform notaPos;

    private static int count = 0;

    public List<Nota> NotasPretas;
    public List<Nota> NotasBrancas;
    public List<Nota> NotasAmarelas;

    [SerializeField]
    private GameObject gameOverBalloon, victoryBalloon;

    void Start () {
        notaLinha = GameObject.Find("HUD-Linha");
        notaPos = GameObject.Find("NotaPos").GetComponent<RectTransform>();

        if (song == musica.INFINITE) StartCoroutine(infiniteSong());
        else if (song == musica.SONGOFTIME) StartCoroutine(songOfTime());
    }

    IEnumerator songOfTime()
    {
        spawnNota(Cor.WHITE);
        yield return new WaitForSeconds(.7f);
        spawnNota(Cor.BLACK);
        yield return new WaitForSeconds(1f);
        spawnNota(Cor.YELLOW);
        yield return new WaitForSeconds(.55f);
        spawnNota(Cor.WHITE);
        yield return new WaitForSeconds(.55f);
        spawnNota(Cor.BLACK);
        yield return new WaitForSeconds(1f);
        spawnNota(Cor.YELLOW);
        yield return new WaitForSeconds(.55f);
        spawnNota(Cor.WHITE);
        yield return new WaitForSeconds(.3f);
        spawnNota(Cor.YELLOW);
        yield return new WaitForSeconds(.3f);
        spawnNota(Cor.WHITE);
        yield return new WaitForSeconds(.7f);
        spawnNota(Cor.BLACK);
        yield return new WaitForSeconds(.7f);
        spawnNota(Cor.YELLOW);
        yield return new WaitForSeconds(.3f);
        spawnNota(Cor.BLACK);
        yield return new WaitForSeconds(.3f);
        spawnNota(Cor.WHITE);
        yield return new WaitForSeconds(.6f);
        spawnNota(Cor.YELLOW);
        yield return new WaitForSeconds(.6f);
        spawnNota(Cor.BLACK);
        yield return new WaitForSeconds(.4f);
        spawnNota(Cor.WHITE);
        yield return new WaitForSeconds(.4f);
        spawnNota(Cor.BLACK);


        while (NotasAmarelas.Count > 0 || NotasBrancas.Count > 0 || NotasPretas.Count > 0)
        {
            NotasPretas.RemoveAll(item => item == null);
            NotasBrancas.RemoveAll(item => item == null);
            NotasAmarelas.RemoveAll(item => item == null);
            yield return new WaitForSeconds(.3f);
        }
        StartCoroutine(victory());
    }

    IEnumerator infiniteSong() {
        while (true) {
            spawnNota((Cor)Random.Range(0, 3));
            yield return new WaitForSeconds(Random.Range(.5f, 1.5f));
        }
    }

    void spawnNota(Cor c) {
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

    public IEnumerator gameOver() {
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
