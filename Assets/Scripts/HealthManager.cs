using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    [SerializeField]
    private Image healthBarL, healthBarR;
    private float healthMax = 100, health = 100;
    [SerializeField]
    private Color colorHigh, colorMed, colorLow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        debugDamage();
        healthColor();
        healthSize();
    }

    void healthSize() {
        healthBarL.fillAmount = (float)health / 100;
        healthBarR.fillAmount = (float)health / 100;
    }

    void healthColor() {
        if (health >= 50) { healthBarL.color = colorHigh; healthBarR.color = colorHigh; }
        else if (health >= 20) { healthBarL.color = colorMed; healthBarR.color = colorMed; }
        else { healthBarL.color = colorLow; healthBarR.color = colorLow; }
    }

    //Debug de dano
    void debugDamage() {
        if (Input.GetKey(KeyCode.Z) && health > 0) health--;
        if (Input.GetKey(KeyCode.X) && health < 100) health++;
    }
}
