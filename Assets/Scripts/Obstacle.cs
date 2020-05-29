using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private HingeJoint2D hj2d;
    public float mass = 1, jointStrength = 10;
    private bool selfDestruct = false;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        hj2d = GetComponent<HingeJoint2D>();

        rb2d.mass = mass * 0.25f;
        hj2d.breakForce = jointStrength;
        hj2d.breakTorque = jointStrength;
    }

     /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if(!hj2d)
        {
            selfDestruct = true;
        }
    }

    private void OnBecameInvisible() {
        if(selfDestruct && this.gameObject && this.gameObject.activeSelf)
            Invoke("SelfDestruct", 10);
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }

}
