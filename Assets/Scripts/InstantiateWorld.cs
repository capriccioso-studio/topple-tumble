using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateWorld : MonoBehaviour
{
    public int stage = 0;
    void Start()
    {
        if(this.gameObject.transform.childCount == stage)
        {
            Instantiate(Global.environmentType[stage].worldPrefab,
                        Vector3.zero,
                        gameObject.transform.rotation,
                        gameObject.transform);
        }


    }
}
