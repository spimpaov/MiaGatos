using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum color {
    BLACK, WHITE, YELLOW
}

public class Nota : MonoBehaviour {
    
    public color color;
    public float speed;
    private Rigidbody2D rb;
    private RectTransform rect;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        rect = GetComponent<RectTransform>();
        rb.velocity = new Vector2(speed, 0);
	}

    void Update() {
        deleteOnExit();
    }

    void deleteOnExit() {
        // O *1.5f eh gambiarra;
        // preciso saber quando o sprite inteiro está depois da tela, mas so sei a posicao do meio dele
        if (gameObject != null && rect.position.x > Screen.width * 1.5f) Destroy(gameObject);
    }
}
