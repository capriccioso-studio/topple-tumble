using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    //12.78
    public GameObject layer1, layer0, layerNeg1, layerNeg2, layerNeg3, layerNeg4, ground, pickups;
    private Transform playerTransform;
    public GameObject[] easyObs, normalObs, hardObs, hellObs; 

    public GameObject blueOrb;
    public int startingBase = 0;
    // Start is called before the first frame update
    void Start()
    {

        playerTransform = GameObject.FindWithTag("Player").transform;

        SpawnNextObstacle(12.78f);
    }

    // Update is called once per frame
     /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        
        if(playerTransform.position.y > (layerNeg4.transform.GetChild(layerNeg4.transform.childCount - 1).transform.position.y ) - (layerNeg4.transform.localScale.z * 12.78) * 0.25)
            DuplicateBackground(layerNeg4);
        if(playerTransform.position.y > (layerNeg3.transform.GetChild(layerNeg3.transform.childCount - 1).transform.position.y ) - (layerNeg3.transform.localScale.z * 12.78) * 0.25)
            DuplicateBackground(layerNeg3);
        if(playerTransform.position.y > (layerNeg2.transform.GetChild(layerNeg2.transform.childCount - 1).transform.position.y ) - (layerNeg2.transform.localScale.z * 12.78) * 0.25)
            DuplicateBackground(layerNeg2);
        if(playerTransform.position.y > (layerNeg1.transform.GetChild(layerNeg1.transform.childCount - 1).transform.position.y ) - (layerNeg1.transform.localScale.z * 12.78) * 0.25)
            DuplicateBackground(layerNeg1);
        if(playerTransform.position.y > (layer1.transform.GetChild(layer1.transform.childCount - 1).transform.position.y ) - (layer1.transform.localScale.z * 12.78) * 0.5)
            DuplicateBackground(layer1);

        if(playerTransform.position.y > (layer0.transform.GetChild(layer0.transform.childCount - 1).transform.position.y ) - (layer0.transform.localScale.z * 12.78) * 0.5)
        {
            var y= layer0.transform.GetChild(layer0.transform.childCount - 1).position.y +  (layer0.transform.localScale.z * 12.78f);
            if(Random.Range(0, 100) < 50)
            {
                SpawnNextOrb(y);
            }

            SpawnNextObstacle(y);
        }
        
            



    }

    public void DuplicateBackground(GameObject layer)
    {
        Transform lastChild = layer.transform.GetChild(layer.transform.childCount - 1);
            Instantiate(lastChild.gameObject, 
                        new Vector3(lastChild.position.x, lastChild.position.y + (layer.transform.localScale.z * 11f) ,lastChild.position.z),
                        // new Vector3(lastChild.position.x, lastChild.position.y + (layer.transform.localScale.z * 12.78f) ,lastChild.position.z),
                        lastChild.rotation,
                        layer.transform);
    }

    public void SpawnNextOrb(float yPos)
    {
        float xRand = Random.Range(-3.36f, 3.36f);
        Instantiate(
                blueOrb,
                new Vector3(xRand, yPos, 0),
                this.transform.rotation,
                pickups.transform
        );
    }

    public void SpawnNextObstacle(float yPos)
    {
        int lvlRand = Random.Range(0, 100);

        if(playerTransform.position.y < 50 - startingBase)
        {
            //InstantiateObs(easyObs, yPos);
            InstantiateObs(normalObs, yPos);
        }   
        else if(playerTransform.position.y < 150  - startingBase)
        {
            if(lvlRand < 20)
                InstantiateObs(easyObs, yPos);
            else
                InstantiateObs(normalObs, yPos);

        }
        else if(playerTransform.position.y < 250  - startingBase)
        {
            if(lvlRand < 20)
            {
                if(lvlRand < 5)
                    InstantiateObs(easyObs, yPos);
                else
                    InstantiateObs(normalObs, yPos);
            }
            else
                InstantiateObs(hardObs, yPos); 
        }
        else if(playerTransform.position.y < 500  - startingBase)
        {
            if(lvlRand < 20)
            {
                if(lvlRand < 2)
                    InstantiateObs(easyObs, yPos);
                else if(lvlRand < 5)
                    InstantiateObs(normalObs, yPos);
                else
                    InstantiateObs(hardObs, yPos);

            }   
            else
                InstantiateObs(hellObs, yPos); 
        }
        else
        {

        }
    }

    public void InstantiateObs(GameObject[] obs, float yPos)
    {
        Instantiate(
            obs[Random.Range(0, obs.Length)],
                new Vector3(0, yPos, 0),
                this.transform.rotation,
                layer0.transform
        );

    }

}
