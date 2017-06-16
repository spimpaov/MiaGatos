using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapManager : MonoBehaviour
{

    public enum Score
    {
        PERFECT, GOOD, BAD
    };

    public float intervaloPerfect;
    public float intervaloGood;
    public GameObject perfectPopUp;
    public GameObject goodPopUp;
    public GameObject missPopUp;

    [SerializeField]
    private GameObject explB, explW, explY;

    private SongManager sm;
    private HealthManager hm;
    private Nota currNota; //nota mais a direita
    private GameObject HitFrame;
    private int count = 0;
    private ScoreManager score;

    private AudioGato audioGato;

    // --BAD--|--20--GOOD--|--10--PERFECT--|HitFrame|--10--PERFECT--|--20--GOOD--|--BAD--

    void Start()
    {
        HitFrame = GameObject.FindGameObjectWithTag("HitFrame");
        sm = GameObject.Find("SongManager").GetComponent<SongManager>();
        hm = GameObject.Find("HealthManager").GetComponent<HealthManager>();
        audioGato = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioGato>();
        score = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    public void checkNota(GameObject gato)
    {

        float dist;
        Cor cor = gato.GetComponent<IACat>().cor;
        if (cor == Cor.BLACK)
        {
            if (sm.NotasPretas.Count == 0)
            {
                hm.wrongTapDamage();
                audioGato.setSoundErrado();

                //POP UP MISS
                // StartCoroutine(POPUP(2));

                return; //nota errada
            }
            sm.NotasPretas.RemoveAll(item => item == null);
            if (sm.NotasPretas.Count > 0)
            {
                currNota = sm.NotasPretas[0];
            }
        }
        else if (cor == Cor.WHITE)
        {
            if (sm.NotasBrancas.Count == 0)
            {
                hm.wrongTapDamage();
                audioGato.setSoundErrado();

                //POP UP MISS
                //  StartCoroutine(POPUP(2));

                return; //nota errada
            }
            sm.NotasBrancas.RemoveAll(item => item == null);
            if (sm.NotasBrancas.Count > 0)
            {
                currNota = sm.NotasBrancas[0];
            }
        }
        else if (cor == Cor.YELLOW)
        {
            if (sm.NotasAmarelas.Count == 0)
            {
                hm.wrongTapDamage();
                audioGato.setSoundErrado();

                //POP UP MISS
                //   StartCoroutine(POPUP(2));

                return; //nota errada
            }
            sm.NotasAmarelas.RemoveAll(item => item == null);
            if (sm.NotasAmarelas.Count > 0)
            {
                currNota = sm.NotasAmarelas[0];
            }
        }
        // else return;

        if (currNota == null)
        {
            hm.wrongTapDamage();
            audioGato.setSoundErrado();

            //POP UP MISS
            //  StartCoroutine(POPUP(2));

            return;
        } //nota errada

        dist = Mathf.Abs(currNota.transform.position.x - HitFrame.transform.position.x);

        if (dist <= intervaloPerfect * Screen.width / 100)
        {
            audioGato.setSoundCerto(cor);
            hm.perfectHeal();

            //POP UP PERFECT
            StartCoroutine(POPUP(0));
            score.somaScore(0);

            if (cor == Cor.BLACK) instantiateExplostion(explB, currNota.rect);
            else if (cor == Cor.WHITE) instantiateExplostion(explW, currNota.rect);
            else if (cor == Cor.YELLOW) instantiateExplostion(explY, currNota.rect);
            if (currNota.gameObject != null) Destroy(currNota.gameObject);
        }
        else if (dist <= intervaloGood * Screen.width / 100)
        {
            hm.goodHeal();
            audioGato.setSoundCerto(cor);

            //POP UP GOOD
            StartCoroutine(POPUP(1));
            score.somaScore(1);

            if (cor == Cor.BLACK) instantiateExplostion(explB, currNota.rect);
            else if (cor == Cor.WHITE) instantiateExplostion(explW, currNota.rect);
            else if (cor == Cor.YELLOW) instantiateExplostion(explY, currNota.rect);
            if (currNota.gameObject != null) Destroy(currNota.gameObject);
        }
        else
        {
            hm.wrongTapDamage();
            audioGato.setSoundErrado();

            //POP UP MISS
            StartCoroutine(POPUP(2));
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

    public IEnumerator POPUP(int tipo)
    {
        GameObject go;
        if (tipo == 0) //perfect
        {
            //Debug.Log("perfect");

            perfectPopUp.SetActive(true);
            yield return new WaitForSeconds(0.1f);



        }
        else if (tipo == 1) //good
        {
            // Debug.Log("good");

            goodPopUp.SetActive(true);
            yield return new WaitForSeconds(0.1f);

        }
        else //miss
        {
            //Debug.Log("miss");
            missPopUp.SetActive(true);
            yield return new WaitForSeconds(0.1f);

        }
    }
}
