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
    public float speed;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public RectTransform rect;
    private Image img;

    void Start()
    {
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
        if (gameObject != null && rect.position.x > Screen.width * 1.07f) {
            GameObject.Find("HealthManager").GetComponent<HealthManager>().missNoteDamage();
            Destroy(gameObject);
        }
    }
}
