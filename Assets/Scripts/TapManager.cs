using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapManager : MonoBehaviour {

	public enum Score {
		PERFECT, GOOD, BAD
	};
	
	public float intervaloPerfect;
	public float intervaloGood;
	//public GameObject[] notas;

    private SongManager sm;
	private Nota currNota; //nota mais a direita
	private GameObject HitFrame;
	private int count = 0;

	// --BAD--|--20--GOOD--|--10--PERFECT--|HitFrame|--10--PERFECT--|--20--GOOD--|--BAD--

	void Start() {
		HitFrame = GameObject.FindGameObjectWithTag("HitFrame");
        sm = GameObject.Find("SongManager").GetComponent<SongManager>();
		//currNota = notas[0];
	}

	public void atualizaNota() {
		//currNota = notas[++count];
	}

	public void checkNota(int c) {
        float dist;
        Cor cor = (Cor) c;
        if (cor == Cor.BLACK) {
            if (sm.NotasPretas.Count == 0) { Debug.Log("errou!"); return; } //nota errada
            sm.NotasPretas.RemoveAll(item => item == null);
            currNota = sm.NotasPretas[sm.NotasPretas.Count - 1];
        }
        else if (cor == Cor.WHITE) {
            if (sm.NotasBrancas.Count == 0) { Debug.Log("errou!"); return; } //nota errada
            sm.NotasBrancas.RemoveAll(item => item == null);
            currNota = sm.NotasBrancas[sm.NotasBrancas.Count - 1];
        }
        else if (cor == Cor.YELLOW) {
            if (sm.NotasAmarelas.Count == 0) { Debug.Log("errou!"); return; } //nota errada
            sm.NotasAmarelas.RemoveAll(item => item == null);
            currNota = sm.NotasAmarelas[sm.NotasAmarelas.Count - 1];
        }
        else return;

        if (currNota == null) { Debug.Log("errou!"); return; } //nota errada

        dist = Mathf.Abs(currNota.transform.position.x - HitFrame.transform.position.x);

        if (dist <= intervaloPerfect)
        {
            Debug.Log("Score: " + Score.PERFECT);
            if (currNota.gameObject != null) Destroy(currNota.gameObject);
        }
        else if (dist <= intervaloGood)
        {
            Debug.Log("Score: " + Score.GOOD);
            if (currNota.gameObject != null) Destroy(currNota.gameObject);
        }
        else {
            Debug.Log("errou!"); //nota errada
        }

        /*
        if (cor == currNota.GetComponent<Nota>().cor) {
			Score score = calculaScore(gato);
			Debug.Log("Score: " + score);
		} else {
			Debug.Log("cor errada");
		}*/
	}

	Score calculaScore(Cor c, GameObject gato) {

        if (c == Cor.BLACK)
        {
            sm.NotasPretas.RemoveAll(item => item == null);
            if (sm.NotasPretas.Count == 0) Debug.Log("errou!"); //nota errada
            else
            {

            }


        }
        else if (c == Cor.WHITE)
        { }
        else if (c == Cor.YELLOW)
        { }
            float dist = Mathf.Abs(gato.transform.position.x - HitFrame.transform.position.x);
		if (dist <= intervaloPerfect) {
			return Score.PERFECT;
		} else if (dist <= intervaloGood)  {
			return Score.GOOD;
		} else return Score.BAD;
	}


}
