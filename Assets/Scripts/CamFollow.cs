using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {
    
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target = null;

    // Update is called once per frame
    void FixedUpdate () 
    {
        if(Global.guiState != GUISTATE.game)
        {
            target = null;
        }
        else
        {
            target = GameObject.Find("Platform").transform;
        }
        
        if (target != null)
        {
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
            Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.3f, 10f)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    
    }
}