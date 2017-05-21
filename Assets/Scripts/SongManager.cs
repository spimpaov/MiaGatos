using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour {

    private GameObject notaLinha;
    [SerializeField]
    private GameObject nota;
    private RectTransform notaPos;

    private static int count = 0;

    public List<Nota> NotasPretas;
    public List<Nota> NotasBrancas;
    public List<Nota> NotasAmarelas;


    void Start () {
        notaLinha = GameObject.Find("HUD-Linha");
        notaPos = GameObject.Find("NotaPos").GetComponent<RectTransform>();
        StartCoroutine(spawnNotas());
    }

    IEnumerator spawnNotas() {
        while (true) {
            spawnNota((Cor) Random.Range(0, 3));
            yield return new WaitForSeconds(2f);
        }
    }

    void spawnNota(Cor c) {
        var go = Instantiate(nota) as GameObject;
        RectTransform rt = go.GetComponent<RectTransform>();
        Nota n = go.GetComponent<Nota>();
        go.transform.SetParent(notaLinha.transform, true);
        go.transform.localPosition = notaPos.localPosition;
        go.transform.localScale = notaPos.localScale;
        rt.anchoredPosition = notaPos.anchoredPosition;
        rt.sizeDelta = notaPos.sizeDelta;
        n.cor = c;
        n.setID(count);

        if (c == Cor.BLACK) NotasPretas.Add(n);
        else if (c == Cor.WHITE) NotasBrancas.Add(n);
        else if (c == Cor.YELLOW) NotasAmarelas.Add(n);

        count++;
    }
}
