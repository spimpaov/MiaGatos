using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapManager : MonoBehaviour {

	public enum Score {
		PERFECT, GOOD, BAD
	};
	
	public float intervaloPerfect;
	public float intervaloGood;
	public GameObject[] notas;

	private GameObject currNota; //nota mais a direita
	private GameObject HitFrame;
	private int count = 0;

	// --BAD--|--20--GOOD--|--10--PERFECT--|HitFrame|--10--PERFECT--|--20--GOOD--|--BAD--

	void Start() {
		HitFrame = GameObject.FindGameObjectWithTag("HitFrame");
		//currNota = notas[0];
	}

	public void atualizaNota() {
		currNota = notas[++count];
	}

	public void _checkNota(GameObject gato) {
		if (gato.GetComponent<IACat>().cor == currNota.GetComponent<Nota>().cor) {
			Score score = calculaScore(gato);
			Debug.Log("Score: " + score);
		} else {
			Debug.Log("cor errada");
		}
	}

	Score calculaScore(GameObject gato) {
		float dist = Mathf.Abs(gato.transform.position.x - HitFrame.transform.position.x);
		if (dist <= intervaloPerfect) {
			return Score.PERFECT;
		} else if (dist <= intervaloGood)  {
			return Score.GOOD;
		} else return Score.BAD;
	}


}
