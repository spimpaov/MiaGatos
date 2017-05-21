using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    [SerializeField]
    private Image healthBarL, healthBarR;
    private float healthMax = 100, health = 100;
    [SerializeField]
    private int wrongDmg, missDmg, perfHeal, gdHeal;
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
        healthBarL.fillAmount = Mathf.Lerp(healthBarL.fillAmount, (float)health / healthMax, .2f);
        healthBarR.fillAmount = Mathf.Lerp(healthBarR.fillAmount, (float)health / healthMax, .2f);
    }

    void healthColor() {
        if (healthBarL.fillAmount >= .5f) { healthBarL.color = colorHigh; healthBarR.color = colorHigh; }
        else if (healthBarL.fillAmount >= .2f) { healthBarL.color = colorMed; healthBarR.color = colorMed; }
        else { healthBarL.color = colorLow; healthBarR.color = colorLow; }
    }
    
    public void wrongTapDamage() {
        if (health > 0) health -= wrongDmg;
        else health = 0;
    }
    public void missNoteDamage() {
        if (health > 0) health -= missDmg;
        else health = 0;
    }
    public void perfectHeal() {
        if (health < healthMax) health += perfHeal;
        else health = healthMax;
    }
    public void goodHeal() {
        if (health < healthMax) health += gdHeal;
        else health = healthMax;
    }

    //Debug de dano
    void debugDamage() {
        if (Input.GetKey(KeyCode.Z) && health > 0) health--;
        if (Input.GetKey(KeyCode.X) && health < 100) health++;
    }
}
