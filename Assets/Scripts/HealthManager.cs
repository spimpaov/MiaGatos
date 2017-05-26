using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    [SerializeField]
    private Image healthBarL, healthBarR;
    private float healthMax = 100, health = 100;
    public int passiveDmg;
    [SerializeField]
    private int wrongDmg, missDmg, perfHeal, gdHeal;
    [SerializeField]
    private Color colorHigh, colorMed, colorLow;

    private SongManager sm;
    private bool gameisover = false;

	// Use this for initialization
	void Start () {
        sm = GameObject.Find("SongManager").GetComponent<SongManager>();
	}
	
	// Update is called once per frame
	void Update () {
        debugDamage();
        healthColor();
        healthSize();
        health -= passiveDmg*Time.deltaTime;
    }

    void healthSize() {
        healthBarL.fillAmount = Mathf.Lerp(healthBarL.fillAmount, (float)health / healthMax, .2f);
        healthBarR.fillAmount = Mathf.Lerp(healthBarR.fillAmount, (float)health / healthMax, .2f);
        if (!gameisover && healthBarL.fillAmount == 0) { gameisover = true; StartCoroutine(sm.gameOver()); }
    }

    void healthColor() {
        if (healthBarL.fillAmount >= .5f) { healthBarL.color = colorHigh; healthBarR.color = colorHigh; }
        else if (healthBarL.fillAmount >= .2f) { healthBarL.color = colorMed; healthBarR.color = colorMed; }
        else { healthBarL.color = colorLow; healthBarR.color = colorLow; }
    }

    public void wrongTapDamage() {
        if (health > wrongDmg) health -= wrongDmg;
        else health = 0;
    }
    public void missNoteDamage() {
        if (health > missDmg) health -= missDmg;
        else health = 0;
    }
    public void perfectHeal() {
        if (health < healthMax-perfHeal) health += perfHeal;
        else health = healthMax;
    }
    public void goodHeal() {
        if (health < healthMax-gdHeal) health += gdHeal;
        else health = healthMax;
    }

    //Debug de dano
    void debugDamage() {
        if (Input.GetKey(KeyCode.Z) && health > 0) health--;
        if (Input.GetKey(KeyCode.X) && health < 100) health++;
    }
}
