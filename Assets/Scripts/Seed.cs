using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public SeedScriptableObject seed;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polycol2D;
    private bool isTouchingPlatform = false, isDead = false;
    private float deathCount = 0;
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

    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.CompareTag("Platform"))
        {
            Global.score = (int)Mathf.Floor(transform.position.y);
            isTouchingPlatform = true;
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if(!isTouchingPlatform)
        {
            deathCount += Time.deltaTime;
        }
        else 
        {
            deathCount = 0;
        }
        
        if(deathCount > 3 && !isDead)
        {
            isDead = true;
            Global.gameManager.Die();

        }
    }

    /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Platform"))
        {
            isTouchingPlatform = false;
        }
    }


}
