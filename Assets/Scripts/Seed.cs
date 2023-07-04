using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public CamFollow cam;
    public SeedScriptableObject seed;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    public PolygonCollider2D polycol2D;
    public ParticleSystem explodeParticle;
    public Animator animator;
    public AudioSource audioSource;
    public bool isTouchingPlatform = false, isTouchingGround = false, isDead = false, hasDied = false;
    private float deathCount = 0;
    void Start()
    {
        seed = Global.seedtype;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = this.gameObject.AddComponent<Rigidbody2D>();


        spriteRenderer.sprite = seed.soloSprite;
        // polycol2D = this.gameObject.AddComponent<PolygonCollider2D>();


        rb2d.mass = seed.mass * 0.1f;
        rb2d.gravityScale = seed.gravityScale * 0.15f;
        rb2d.sharedMaterial = seed.physMat;
        Global.seed = this.gameObject;
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.CompareTag("Platform") && isDead == false)
        {
            deathCount = 0;
            Global.score = ((int)Mathf.Floor(transform.position.y) < 0)? 0 : (int)Mathf.Floor(transform.position.y);
            isTouchingPlatform = true;
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        float zangle = this.transform.rotation.eulerAngles.z;
        // animator.SetFloat("zangle", (zangle > 180) ? zangle - 360 : zangle);

        deathCount = (!isTouchingPlatform || isTouchingGround)? deathCount + Time.deltaTime : 0;
        
        if(deathCount > 3 && !isDead)
        {
            isDead = true;
            Global.gameManager.Die();
            var iExplodeParticle = Instantiate(explodeParticle, transform.position, Quaternion.identity);
            iExplodeParticle.Play();
            StartCoroutine(cam.Shake(.15f, .4f));
            audioSource.clip = seed.deathSound;
            audioSource.Play();
            gameObject.SetActive(false);
        }


    }
    public IEnumerator DelayedDisable(float duration)
    {
        yield return new WaitForSeconds(duration);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(!other.gameObject.CompareTag("Platform"))
        {
            audioSource.clip = seed.hitSound;
            audioSource.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Ground")){
            isTouchingGround = true;
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

    
    public void Revive()
    {
        gameObject.SetActive(true);
        transform.localEulerAngles = new Vector3(0,0,0);
        transform.position = new Vector3(0, Global.score, 0);
        hasDied = true;
        isDead = false;
        deathCount = 0;
        rb2d.gravityScale = 0;
        StartCoroutine(ReviveFloat(5));
    }

    public IEnumerator ReviveFloat(float duration)
    {
        float normalizedTime = 0;
        while(rb2d.gravityScale < seed.gravityScale * 0.15f)
        {
            normalizedTime += Time.deltaTime / duration;
            rb2d.gravityScale += 0.005f;
            yield return null;
        }
        
    }


}
