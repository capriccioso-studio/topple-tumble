using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {
    
    private Vector3 velocity = Vector3.zero;
    public Transform target = null;
    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private float minX = -10f;
    [SerializeField] private float maxX = 10f;
    [SerializeField] private float offset = 0;

    public bool start = false;

    // Update is called once per frame
    void FixedUpdate () 
    {
        if(start){
            start = false;
            StartCoroutine(Shake(.15f, .4f));
        }
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
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX) + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 orignalPosition = transform.position;
        float elapsed = 0.0f;

        //shake
        while(elapsed < duration)
        {
            float x = Random.Range(-10f, 10f) * magnitude;
            offset = x;

            // transform.position = new Vector3(x, orignalPosition.y, -10f);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        offset = 0;
        //reset camera
        // transform.position = new Vector3(orignalPosition.x, orignalPosition.y, -10);
    }
}