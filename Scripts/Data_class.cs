using System.Collections.Generic;

[System.Serializable]
public class Data_class
{
    public string lvlName = "";
    public bool isBeaten = false;
    public bool isUnlocked = false;
    public int HighestScore = 0;
    public string HighestInitial = "";
    public int SecondScore = 0;
    public string SecondInitial = "";
    public int ThirdtScore = 0;
    public string ThirdInitial = "";
}
[System.Serializable]
public class DataClassList
{
    public Data_class[] Data_Class = new Data_class[10];
}

public struct TableData
{
    public string lvlName;
    public bool isBeaten;
    public bool isUnlocked;
    public int HighestScore;
    public string HighestInitial;
    public int SecondScore;
    public string SecondInitial;
    public int ThirdtScore;
    public string ThirdInitial;
}
