using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "Score", menuName = "lvlScores", order = 1)]
public class lvlScores : ScriptableObject
{
    public string lvlName;
    public bool isUnlocked;
    public int HighestScore;
    public string HighestInitial;
    public int SecondScore;
    public string SecondInitial;
    public int ThirdtScore;
    public string ThirdInitial;
}
