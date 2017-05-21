using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotaAnimation : MonoBehaviour {

    private float scaleX, scaleY;
    private Vector3 finalSize;
    private Image sprite;
    [HideInInspector]
    public Color color;

    private void Awake() {
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        finalSize = new Vector3(scaleX * 1.75f, scaleY * 1.75f, 1);
        sprite = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update () {
        transform.localScale = Vector3.Lerp(transform.localScale, finalSize, .2f);
        sprite.color = Color.Lerp(sprite.color, new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0), .2f);
        if (sprite.color.a < .05f) Destroy(gameObject);
    }

}
