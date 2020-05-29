using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public PlatformScriptableObject platform;
    public GameObject lBooster, rBooster;
    public float lboostAmount = 0, rboostAmount = 0;
    private Rigidbody2D rb2d;

    private bool isLeftBoosting, isRightBoosting;
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polycol2D;

    void Start()
    {

        platform = Global.platformtype;
        spriteRenderer = GetComponent<SpriteRenderer>();


        spriteRenderer.sprite = platform.platformSprite;
        polycol2D = this.gameObject.AddComponent<PolygonCollider2D>();
        rb2d = this.gameObject.AddComponent<Rigidbody2D>();


        rb2d.sharedMaterial = platform.physMat;
        rb2d.mass = platform.mass * 0.25f;
        rb2d.drag = platform.drag * 0.25f;
        rb2d.angularDrag = platform.angularDrag + 5;
        rb2d.gravityScale = platform.gravityScale * 0.15f;
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Return) || isRightBoosting)
        {
            if(rboostAmount < platform.boosterStrength)
            {
                rboostAmount += Time.deltaTime * (platform.acceleration); 
            }
            rb2d.AddForceAtPosition(rBooster.transform.up * rboostAmount * Time.deltaTime * 100, rBooster.transform.position, ForceMode2D.Force);
        }
        else
        {
            if(rboostAmount > 0)
            {
                rboostAmount -= Time.deltaTime; 
            }

        }

        if(Input.GetKey(KeyCode.Space) || isLeftBoosting)
        {
            if(lboostAmount < platform.boosterStrength)
            {
                lboostAmount += Time.deltaTime * (platform.acceleration); 
            }
            rb2d.AddForceAtPosition(lBooster.transform.up * lboostAmount * Time.deltaTime * 100, lBooster.transform.position, ForceMode2D.Force);

        }
        else
        {
            if(lboostAmount > 0)
            {
                lboostAmount -= Time.deltaTime; 
            }
        }
    }

    public void BoostLeft()
    {
        isLeftBoosting = true;
    }

    public void ReleaseLeft()
    {
        isLeftBoosting = false;

    }

    public void BoostRight()
    {
        isRightBoosting = true;

    }

    public void ReleaseRight()
    {
        isRightBoosting = false;

    }
}
