using UnityEngine;

public static class Global
{
  public static GUISTATE guiState;
  public static EnvironmentScriptableObject[] environmentType; 
  public static PlatformScriptableObject platformtype;
  public static GameManager gameManager;
  public static SeedScriptableObject seedtype;
  public static GameObject seed = null, platform = null; 
  public static int score = 0;
  public static ADREWARDTYPE adRewardType;
  public static AdManager adManager;
}