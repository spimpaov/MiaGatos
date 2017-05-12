using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CatActions {
	LICK, WALK, RUN, EAT, PLAY
};


public class IACat : MonoBehaviour {

	public float timeBetweenActions;
	public float walkVelocity;
	public float runVelocity;
	public Brinquedo brinquedo;
	public Transform posPote;

	void Start () {
		
		StartCoroutine (chooseAction());
	}
		
	IEnumerator chooseAction() {

		while (true) {
			yield return new WaitForSeconds (timeBetweenActions);
			CatActions nextAction = (CatActions)Random.Range (0, System.Enum.GetValues (typeof(CatActions)).Length);
			doAction (nextAction);
		}
	
	}

	void doAction(CatActions chosenAction) {
		switch (chosenAction) {
		case CatActions.LICK:
			lickCat ();
			break;
		case CatActions.WALK:
			walkCat (walkVelocity);
			break;
		case CatActions.RUN:
			walkCat (runVelocity);
			break;
		case CatActions.PLAY:
			if (!brinquedo.ocupado) {
				brinquedo.ocupado = true; //pega o brinquedo
				playCat ();
				brinquedo.ocupado = false; //libera o brinquedo
			} else {
				doAction ((CatActions)Random.Range (0, System.Enum.GetValues(typeof(CatActions)).Length-1)); //PLAY éa ultima ação do enum
			}
			break;
		case CatActions.EAT:
			eatCat ();
			break;

		}
	}
	//circle colliders pras cabeças dos gatos (pra evitar que dois gatos fiquem parados quando se cruzam pelo caminho)
	//possivel problema: ainda podem ficar parados se seus centros estiverem alinhados, eu acho

	void lickCat() {
		Debug.Log ("LICK");
		//seta a animação de lick
	}

	void walkCat(float velocity) {
		Debug.Log ("WALK");

		Vector2 dir = new Vector2 (Random.Range(-1,1), Random.Range(-1,1)).normalized; //escolhe um sentido aleatório
		//faz o gato andar em determinada velocidade
		//seta a animação de walk (uma só ou uma pra walk e outra pra run? -> escolhe a animação de acordo com a velocidade)
	}

	void playCat() {
		Debug.Log ("PLAY");

		Transform posBrinquedo = brinquedo.transform;
		Vector2 dir = (posBrinquedo.position - this.transform.position).normalized; //direção entre o gato e o brinquedo
		//faz o gato andar na direção ate colidir com o brinquedo
		//quando o gato colide com o brinquedo, muda a animação

	}

	void eatCat() {
		Debug.Log ("EAT");
		Vector2 dir = (posPote.position - this.transform.position).normalized; //direção entre o gato e o pote
		//faz o gato andar na direção ate colidir com o pote de comida
		//seta a animação de eat


	}
}
