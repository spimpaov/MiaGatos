using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour {

    public GameObject notaLinha;
    public GameObject nota;
    public RectTransform notaPos;

    IEnumerator spawnNotas() {
        while (true) {
            spawnNota((Nota.Cor) Random.Range(0, 3));
            yield return new WaitForSeconds(1f);
        }
    }

    void spawnNota(Nota.Cor c) {
        var go = Instantiate(nota) as GameObject;
        RectTransform rt = go.GetComponent<RectTransform>();
        go.transform.SetParent(notaLinha.transform, true);
        go.transform.localPosition = notaPos.localPosition;
        go.transform.localScale = notaPos.localScale;
        rt.anchoredPosition = notaPos.anchoredPosition;
        rt.sizeDelta = notaPos.sizeDelta;
        go.GetComponent<Nota>().cor = c;

    }

	// Use this for initialization
	void Start () {
        StartCoroutine(spawnNotas());

    }
	
	// Update is called once per frame
	void Update ()
    {
    }
}
