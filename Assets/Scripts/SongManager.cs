using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour {

    public GameObject notaLinha;
    public GameObject nota;
    public RectTransform notaPos;

    private static int count = 0;

	void Start () {
        StartCoroutine(spawnNotas());
    }

    IEnumerator spawnNotas() {
        while (true) {
            spawnNota((Cor) Random.Range(0, 3));
            yield return new WaitForSeconds(3f);
        }
    }

    void spawnNota(Cor c) {
        var go = Instantiate(nota) as GameObject;
        RectTransform rt = go.GetComponent<RectTransform>();
        go.transform.SetParent(notaLinha.transform, true);
        go.transform.localPosition = notaPos.localPosition;
        go.transform.localScale = notaPos.localScale;
        rt.anchoredPosition = notaPos.anchoredPosition;
        rt.sizeDelta = notaPos.sizeDelta;
        go.GetComponent<Nota>().cor = c;
        go.GetComponent<Nota>().setID(count);
        count++;
    }
}
