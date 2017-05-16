using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CatActions {
	LICK, WALK, RUN, EAT, PLAY
};


public class IACat : MonoBehaviour {
    
	public float walkVelocity;
	public float runVelocity;

    private GameObject brinquedo;
    private GameObject poteDeComida;
    private bool colidiuBrinquedo;
    private bool colidiuPote;
    private bool colidiuWall;
    private bool raycastPote;
    private bool raycastBrinquedo;
    private bool raycastWall;
    private bool raycastCat;

    void Start () {
        brinquedo = GameObject.FindGameObjectWithTag("Brinquedo");
        poteDeComida = GameObject.FindGameObjectWithTag("PoteDeComida");
		StartCoroutine (chooseAction());
	}
		
	IEnumerator chooseAction() {
		while (true) {
            CatActions currAction = (CatActions)Random.Range (0, System.Enum.GetValues (typeof(CatActions)).Length);
            yield return doAction (currAction);
		}
	}

	IEnumerator doAction(CatActions chosenAction) {
		switch (chosenAction) {
		case CatActions.LICK:
            yield return lickCat();
            break;
		case CatActions.WALK:
            yield return walkCat(walkVelocity);
            break;
		case CatActions.RUN:
			yield return walkCat(runVelocity);
			break;
		case CatActions.PLAY:
            if (!brinquedo.GetComponent<Brinquedo>().ocupado) {
                brinquedo.GetComponent<Brinquedo>().ocupado = true; //pega o brinquedo
				yield return playCat(walkVelocity);
                brinquedo.GetComponent<Brinquedo>().ocupado = false; //libera o brinquedo
			} else {
				doAction ((CatActions)Random.Range (0, System.Enum.GetValues(typeof(CatActions)).Length-1)); //PLAY é a ultima ação do enum
			}
			break;
		case CatActions.EAT:
			yield return eatCat(walkVelocity);
			break;
		}
    }
	//circle colliders pras cabeças dos gatos (pra evitar que dois gatos fiquem parados quando se cruzam pelo caminho)
	//possivel problema: ainda podem ficar parados se seus centros estiverem alinhados, eu acho

	IEnumerator lickCat() {
		//Debug.Log ("LICK");
        //seta a animação de lick
        yield return new WaitForSeconds(3f);
	}
	IEnumerator walkCat(float velocity) {
		Vector3 aux = new Vector3 (Random.Range(-1,2), Random.Range(-1,2), 0).normalized; //escolhe um sentido aleatório
        Vector3 target = transform.position + aux*5;
		//Debug.Log ("WALK/RUN, dir: " + aux);
        //seta a animação de walk ou run (walk se velocity == walkVelocity e run se velocity == runVelocity)
        while (!colidiuWall) {
            raycast(aux, Color.red);
            if (this.transform.position == target || raycastBrinquedo || raycastPote || raycastWall || raycastCat) yield break;
            transform.position = Vector3.MoveTowards(transform.position, target, velocity * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
    void raycast(Vector3 vetor, Color cor)
    {
        Vector2 sightStart = new Vector2(this.transform.position.x, this.transform.position.y);
        Vector2 sightEnd = sightStart + new Vector2(vetor.x, vetor.y);
        Debug.DrawLine(sightStart, sightEnd, cor);
        raycastPote = Physics2D.Linecast(sightStart, sightEnd, 1 << LayerMask.NameToLayer("PoteDeComida"));
        raycastBrinquedo = Physics2D.Linecast(sightStart, sightEnd, 1 << LayerMask.NameToLayer("Brinquedo"));
        raycastWall = Physics2D.Linecast(sightStart, sightEnd, 1 << LayerMask.NameToLayer("Wall"));
        raycastCat = Physics2D.Linecast(sightStart, sightEnd, 1 << LayerMask.NameToLayer("Gato"));
    }
    IEnumerator playCat(float velocity) {
		//Debug.Log ("PLAY");
		Vector3 target = brinquedo.GetComponent<Transform>().position; //direção entre o gato e o brinquedo
        while (!colidiuBrinquedo) {
            if (raycastBrinquedo || raycastPote || raycastWall || raycastCat) yield break;
            raycast(target.normalized, Color.blue);
            transform.position = Vector3.MoveTowards(transform.position, target, velocity * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        //seta a animação de play
    }
    IEnumerator eatCat(float velocity) {
		//Debug.Log ("EAT");
		Vector3 target = poteDeComida.GetComponent<Transform>().position; //direção entre o gato e o pote
        while (!colidiuPote) {
            if (raycastBrinquedo || raycastPote || raycastWall || raycastCat) yield break;
            raycast(target.normalized, Color.green);
            transform.position = Vector3.MoveTowards(transform.position, target, velocity * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        //seta a animação de eat
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "PoteDeComida") {
            colidiuPote = true;
        } if (coll.gameObject.tag == "Brinquedo") {
            colidiuBrinquedo = true;
        } if (coll.gameObject.tag == "Wall") {
            colidiuWall = true;
        }
    }
    void OnCollisionExit2D(Collision2D coll) {
        if (coll.gameObject.tag == "PoteDeComida") {
            colidiuPote = false;
        } if (coll.gameObject.tag == "Brinquedo") {
            colidiuBrinquedo = false;
        } if (coll.gameObject.tag == "Wall") {
            colidiuWall = false;
        }
    }

}