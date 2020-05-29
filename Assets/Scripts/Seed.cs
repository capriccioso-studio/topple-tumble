using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public SeedScriptableObject seed;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polycol2D;
    void Start()
    {
        seed = Global.seedtype;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = this.gameObject.AddComponent<Rigidbody2D>();


        spriteRenderer.sprite = seed.soloSprite;
        polycol2D = this.gameObject.AddComponent<PolygonCollider2D>();
        polycol2D.offset = new Vector2(0,0.17f);


        rb2d.mass = seed.mass * 0.1f;
        rb2d.gravityScale = seed.gravityScale * 0.14f;
        rb2d.sharedMaterial = seed.physMat;
        Global.seed = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
