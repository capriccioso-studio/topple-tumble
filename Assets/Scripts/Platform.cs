using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Seed seed;
    public PlatformScriptableObject platform;
    public GameObject lBooster, rBooster;
    public float lboostAmount = 0, rboostAmount = 0;
    public ParticleSystem rParticles, lParticles, wind;
    private Rigidbody2D rb2d;
    // public Transform camera = null;

    private bool isLeftBoosting, isRightBoosting;
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polycol2D;
    public AudioSource lBoostAudio, rBoostAudio;
    public Animator animator;
    public CamFollow cam;

    void Start()
    {
        Global.platform = gameObject;
        platform = Global.platformtype;
        spriteRenderer = GetComponent<SpriteRenderer>();


        spriteRenderer.sprite = platform.platformSprite;
        rb2d = this.gameObject.AddComponent<Rigidbody2D>();

        rb2d.sharedMaterial = platform.physMat;
        rb2d.mass = platform.mass * 0.25f;
        rb2d.drag = platform.drag * 0.25f;
        rb2d.angularDrag = platform.angularDrag + 5;
        rb2d.gravityScale = platform.gravityScale * 0.15f;
        animator = GetComponent<Animator>();
        cam = FindObjectOfType<CamFollow>();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(0);

            if(Input.touchCount > 1)
                touch1 = Input.GetTouch(1);

            if (touch.position.x < Screen.width/2 || touch1.position.x < Screen.width/2)
            {
                BoostLeft();
            }
            else
            {
                ReleaseLeft();
            }

            if (touch.position.x > Screen.width/2 || touch1.position.x > Screen.width/2)
            {
                BoostRight();
            }
            else
            {
                ReleaseRight();
            }
        }
        else
        {
            ReleaseRight();
            ReleaseLeft();
        }

        if(Input.GetKey(KeyCode.Return) || isRightBoosting)
        {
            if(!rBoostAudio.isPlaying)
                rBoostAudio.Play();
            if(rboostAmount < platform.boosterStrength)
            {
                rboostAmount += Time.fixedDeltaTime * (platform.acceleration); 
            }
            else
            {
                rboostAmount += Time.fixedDeltaTime * (platform.acceleration * 0.05f); 
            }
            rb2d.AddForceAtPosition(rBooster.transform.up * rboostAmount * Time.fixedDeltaTime * 100, rBooster.transform.position, ForceMode2D.Force);
            rParticles.gravityModifier = rboostAmount * 0.2f;
        
        }
        else
        {
            if(rboostAmount > 0)
            {
                rboostAmount -= Time.fixedDeltaTime; 
            }
            else
            {

            }
                rBoostAudio.Stop();

            rParticles.gravityModifier = 0;

        }

        animator.SetFloat("boost", (rboostAmount + lboostAmount) / 2);

        if(Input.GetKey(KeyCode.Space) || isLeftBoosting)
        {
            if(!lBoostAudio.isPlaying)
                lBoostAudio.Play();
            if(lboostAmount < platform.boosterStrength)
            {
                 lboostAmount += Time.fixedDeltaTime * (platform.acceleration); 
            }
            else
            {
                lboostAmount += Time.fixedDeltaTime * (platform.acceleration * 0.1f);
            }
            rb2d.AddForceAtPosition(lBooster.transform.up * lboostAmount * Time.fixedDeltaTime * 100, lBooster.transform.position, ForceMode2D.Force);
            lParticles.gravityModifier = lboostAmount * 0.2f;

        }
        else
        {
            if(lboostAmount > 0)
            {
                lboostAmount -= Time.fixedDeltaTime; 
            }
            else
            {
            }
            lBoostAudio.Stop();
            lParticles.gravityModifier = 0;

        }

        //rParticles.maxParticles = (int)Mathf.Ceil((rboostAmount/platform.boosterStrength) * 50);
        //lParticles.maxParticles = (int)Mathf.Ceil((lboostAmount/platform.boosterStrength) * 50);
        
        lParticles.maxParticles = (int)(lParticles.gravityModifier * 50);
        rParticles.maxParticles = (int)(rParticles.gravityModifier * 50);

    }


    public void BoostLeft()
    {
        isLeftBoosting = true;
        lBoostAudio.Play();
    }

    public void ReleaseLeft()
    {
        isLeftBoosting = false;

    }

    public void BoostRight()
    {
        isRightBoosting = true;
        rBoostAudio.Play();

    }

    public void ReleaseRight()
    {
        isRightBoosting = false;

    }

    public void Revive()
    {
        transform.position = new Vector3(0, Global.score-2, 0);
        rb2d.gravityScale = 0;
        transform.localEulerAngles = new Vector3(0,0,0);
        StartCoroutine(ReviveFloat(5.0f));
    }

    public IEnumerator ReviveFloat(float duration)
    {
        float normalizedTime = 0;
        while(rb2d.gravityScale < platform.gravityScale * 0.15f)
        {
            normalizedTime += Time.fixedDeltaTime / duration;
            rb2d.gravityScale += 0.005f;

            yield return null;
        }
        
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Sticky Mushroom")){
            StartCoroutine(SlowDownPlayer());
        }
        if(other.gameObject.CompareTag("Shooting Mushroom")){
            if(other.gameObject.transform.localScale.x > 0){
                Vector2 move = new Vector2(5, 7);
                rb2d.AddForce(move, ForceMode2D.Impulse);
                wind.Play();
                StartCoroutine(cam.Shake(1, 1));
                
            }else{
                Vector2 move = new Vector2(-5, 7);
                rb2d.AddForce(move, ForceMode2D.Impulse);
                wind.Play();
                // StartCoroutine(cam.Shake(.15f, .4f));
                StartCoroutine(cam.Shake(1, 1));
            }
        }
    }

    private IEnumerator SlowDownPlayer(){
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(2);
        rb2d.constraints = RigidbodyConstraints2D.None;
    }
}
