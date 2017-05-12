using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nota : MonoBehaviour {
    public enum Cor {
        BLACK, WHITE, YELLOW
    }

    public Cor cor;
    public float speed;
    private Rigidbody2D rb;
    private RectTransform rect;
    private Image img;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        rect = GetComponent<RectTransform>();
        rb.velocity = new Vector2(speed, 0);
        img = GetComponent<Image>();
    }

    void Update() {
        deleteOnExit();
        changeColor();
    }

    void changeColor() {
        if (cor == Cor.BLACK) img.color = Color.black;
        if (cor == Cor.WHITE) img.color = Color.white;
        if (cor == Cor.YELLOW) img.color = Color.yellow;

    }

    void deleteOnExit() {
        // O *1.5f eh gambiarra;
        // preciso saber quando o sprite inteiro está depois da tela, mas so sei a posicao do meio dele
        if (gameObject != null && rect.position.x > Screen.width * 1.5f) Destroy(gameObject);
    }
}
