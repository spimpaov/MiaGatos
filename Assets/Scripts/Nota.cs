using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Cor
{
    BLACK, WHITE, YELLOW
}

public class Nota : MonoBehaviour {
    public Cor cor;
    private GameObject aux;
    private AudioGato audioGato;
    private TapManager tap;
    public float speed;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public RectTransform rect;
    private Image img;
    private float hitFramePos;

    void Start()
    {

        audioGato = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioGato>();
        aux = GameObject.FindGameObjectWithTag("HitFrame");
        hitFramePos = aux.transform.position.x;
        tap = GameObject.FindGameObjectWithTag("TapManager").GetComponent<TapManager>();

        rb = GetComponent<Rigidbody2D>();
        rect = GetComponent<RectTransform>();
        rb.velocity = new Vector2(speed * Screen.width / 10, 0);

        //Pegar a parte que deve ser colorida dentre os Images dos filhos
        Image[] children = GetComponentsInChildren<Image>();
        for (int i = 0; i < children.Length; i++)
            if (children[i].gameObject.name.Equals("Color"))
            {
                img = children[i];
                break;
            }
    }

    void Update()
    {
        deleteOnExit();
        //Debug.Log(Mathf.Abs(transform.position.x - GameObject.FindGameObjectWithTag("HitFrame").transform.position.x));
        // ---- So pra saber onde ele ta
    }

    void deleteOnExit()
    {
        // O 1.07f eh gambiarra;
        // preciso saber quando o sprite inteiro está depois da tela, mas so sei a posicao do meio dele
        Debug.DrawLine(new Vector3(aux.transform.position.x + 100, aux.transform.position.y + 200, 0),
                        new Vector3(aux.transform.position.x + 100, aux.transform.position.y - 200, 0), Color.red);
        if (gameObject != null && rect.position.x > (hitFramePos +130)) {
            GameObject.Find("HealthManager").GetComponent<HealthManager>().missNoteDamage();
            audioGato.setSoundErrado();
            StartCoroutine(tap.POPUP(2));
            Destroy(gameObject);
        }
    }
}
