using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    private Rigidbody2D rb2d;
    // public ParticleSystem pickupParticle;
    public bool isPicked = false;
    
    // Start is called before the first frame update
    void Start()
    {
    }

     /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {

    }

    IEnumerator Pickup(float delay = 0.5f)
    {

        yield return new WaitForSeconds(delay);
        // var iExplodeParticle = Instantiate(pickupParticle, transform.position, Quaternion.identity);
        // iExplodeParticle.Play();
        Destroy(gameObject);

    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(!isPicked)
            {
                Global.orb++;
                isPicked = true;
                StartCoroutine(Pickup(0.1f));
            }
            
        }
    }
}
