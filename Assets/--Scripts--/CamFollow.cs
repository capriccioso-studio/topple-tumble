using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {
    
    private Vector3 velocity = Vector3.zero;
    public Transform target = null;
    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private float minX = -10f;
    [SerializeField] private float maxX = 10f;
    public float offset = 0;

    public static CamFollow instance;
    
    // Update is called once per frame

    private void Awake()
    {
        if(instance != null)
            Destroy(this);
        instance = this;
    }
    void FixedUpdate() 
    {
        target = (Global.guiState != GUISTATE.game)? null : Global.platform.transform;
        
        if (target != null)
        {
            Vector3 targetPosition = target.position;
            targetPosition.z = transform.position.z;
            targetPosition.y = target.transform.position.y + 3;
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX) + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    public void ResetCamera()
    {
        Camera.main.transform.position = new Vector3(0, 1.5f, -10);
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0.0f;

        //shake
        while(elapsed < duration)
        {
            float x = Random.Range(-5f, 5f) * magnitude;
            offset = x;

            elapsed += Time.deltaTime;
            yield return 0;
        }
        offset = 0;
    }
}