using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreList", menuName = "ScoresList", order = 1)]
public class scoreList : ScriptableObject
{
    public List<lvlScores> wholeList;
}
