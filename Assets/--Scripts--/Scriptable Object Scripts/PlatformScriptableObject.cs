using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Platform", order = 1)]
public class PlatformScriptableObject : ScriptableObject
{
    [Header("Platform Demographics")]

    public PLATFORMTYPE platformtype;
    public string environmentName = "same";
    public float boosterStrength = 5;
    public float mass = 5;
    public float drag = 5;
    public float angularDrag = 5;
    public float gravityScale = 5;
    public float acceleration = 1;

    public PhysicsMaterial2D physMat;

    [Header("Platform Sprites")]

    public Sprite platformSprite;

    [Header("Platform FX")]
    public ParticleSystem leftBooster;
    public ParticleSystem rightBooster;


}