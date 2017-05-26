using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapManager : MonoBehaviour {

	public enum Score {
		PERFECT, GOOD, BAD
	};
	
	public float intervaloPerfect;
	public float intervaloGood;

    [SerializeField]
    private GameObject explB, explW, explY;

    private SongManager sm;
    private HealthManager hm;
    private Nota currNota; //nota mais a direita
	private GameObject HitFrame;
	private int count = 0;

	// --BAD--|--20--GOOD--|--10--PERFECT--|HitFrame|--10--PERFECT--|--20--GOOD--|--BAD--

	void Start() {
		HitFrame = GameObject.FindGameObjectWithTag("HitFrame");
        sm = GameObject.Find("SongManager").GetComponent<SongManager>();
        hm = GameObject.Find("HealthManager").GetComponent<HealthManager>();
    }

	public void checkNota(int c) {
        float dist;
        Cor cor = (Cor) c;
        if (cor == Cor.BLACK) {
            if (sm.NotasPretas.Count == 0) { hm.wrongTapDamage(); return; } //nota errada
            sm.NotasPretas.RemoveAll(item => item == null);
            if (sm.NotasPretas.Count > 0) currNota = sm.NotasPretas[0];
        }
        else if (cor == Cor.WHITE) {
            if (sm.NotasBrancas.Count == 0) { hm.wrongTapDamage(); return; } //nota errada
            sm.NotasBrancas.RemoveAll(item => item == null);
            if (sm.NotasBrancas.Count > 0) currNota = sm.NotasBrancas[0];
        }
        else if (cor == Cor.YELLOW) {
            if (sm.NotasAmarelas.Count == 0) { hm.wrongTapDamage(); return; } //nota errada
            sm.NotasAmarelas.RemoveAll(item => item == null);
            if (sm.NotasAmarelas.Count > 0) currNota = sm.NotasAmarelas[0];
        }
        else return;

        if (currNota == null) { hm.wrongTapDamage(); return; } //nota errada

        dist = Mathf.Abs(currNota.transform.position.x - HitFrame.transform.position.x);

        if (dist <= intervaloPerfect * Screen.width / 100)
        {
            hm.perfectHeal();
            if (cor == Cor.BLACK) instantiateExplostion(explB, currNota.rect);
            else if (cor == Cor.WHITE) instantiateExplostion(explW, currNota.rect);
            else if (cor == Cor.YELLOW) instantiateExplostion(explY, currNota.rect);
            if (currNota.gameObject != null) Destroy(currNota.gameObject);
        }
        else if (dist <= intervaloGood * Screen.width / 100)
        {
            hm.goodHeal();
            if (cor == Cor.BLACK) instantiateExplostion(explB, currNota.rect);
            else if (cor == Cor.WHITE) instantiateExplostion(explW, currNota.rect);
            else if (cor == Cor.YELLOW) instantiateExplostion(explY, currNota.rect);
            if (currNota.gameObject != null) Destroy(currNota.gameObject);
        }
        else {
            hm.wrongTapDamage();
        }
	}

    void instantiateExplostion(GameObject explosion, RectTransform t)
    {
        GameObject go = Instantiate(explosion) as GameObject;
        RectTransform rt = go.GetComponent<RectTransform>();
        go.transform.SetParent(t.parent.transform, true);
        go.transform.localPosition = t.localPosition;
        go.transform.localScale = t.localScale;
        rt.anchoredPosition = t.anchoredPosition;
        rt.sizeDelta = t.sizeDelta;
    }
}
