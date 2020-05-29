using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Environment", order = 1)]
public class EnvironmentScriptableObject : ScriptableObject
{
    [Header("Environment Demographics")]

    public ENVIRONMENT environment;
    public string environmentName = "same";
    public GameObject worldPrefab;
    public GameObject[] easy;
    public GameObject[] normal;
    public GameObject[] hard;
    public GameObject[] hell; 

}