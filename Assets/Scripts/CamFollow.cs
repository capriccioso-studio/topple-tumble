using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {
    
    private Vector3 velocity = Vector3.zero;
    public Transform target = null;
    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private float minX = -10f;
    [SerializeField] private float maxX = 10f;

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
            Vector3 targetPosition = target.position;
            targetPosition.z = transform.position.z;
            targetPosition.y = target.transform.position.y +3;
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    
    }
}