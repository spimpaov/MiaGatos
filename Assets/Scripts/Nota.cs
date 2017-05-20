using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nota : MonoBehaviour
{
    public enum Cor
    {
        BLACK, WHITE, YELLOW
    }

    public Cor cor;
    public float speed;
    private Rigidbody2D rb;
    private RectTransform rect;
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
        changeColor();
    }

    void changeColor()
    {
        if (cor == Cor.BLACK) img.color = new Color(0.196f, 0.196f, 0.196f, 1f);
        if (cor == Cor.WHITE) img.color = Color.white;
        if (cor == Cor.YELLOW) img.color = new Color(0.906f, 0.788f, 0.306f, 1f);

    }

    void deleteOnExit()
    {
        // O *1.5f eh gambiarra;
        // preciso saber quando o sprite inteiro está depois da tela, mas so sei a posicao do meio dele
        if (gameObject != null && rect.position.x > Screen.width * 1.5f) Destroy(gameObject);
    }
}
