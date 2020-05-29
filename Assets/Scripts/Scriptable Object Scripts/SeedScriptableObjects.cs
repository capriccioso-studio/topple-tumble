using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Seed", order = 1)]
public class SeedScriptableObject : ScriptableObject
{
    [Header("Seed Demographics")]

    public SEEDTYPE seedtype;
    public string seedName = "same";
    public float mass = 1;
    public float gravityScale = 1;
    public PhysicsMaterial2D physMat;

    [Header("Seed Sprites")]

    public Sprite soloSprite;

    [Header("Seed Animations")]
    public Animator seedAnimator;


}