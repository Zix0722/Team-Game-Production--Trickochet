using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class saveLoadSystem : MonoBehaviour
{
    public Data_class[] saveLvlScores; //edit this then write score;
    public static GameObject theSave;
    public Setting_class setting;
    private void Awake()
    {
        if (theSave)
        {

        }
        else
        {
            theSave = gameObject;
            GameObject.DontDestroyOnLoad(gameObject);
        }
        ReadDataLevel();
        WriteScore(saveLvlScores); // initialize file
        Debug.Log(saveLvlScores[1].lvlName);
        setting = ReadDataSetting();
        //Debug.Log(setting.MusicOnOff);
        if (setting == null)
        {
            WriteSetting(true, true);
            setting = ReadDataSetting();
        }
    }
    void Start()
    {
        
        
    }

    public void updateData<T> (string lvl_name, dataType type, T val )
    {
        foreach(var data in saveLvlScores)
        {
            if(data.lvlName == lvl_name)
            {
                Debug.Log("Find it");
                switch(type)
                {
                    case dataType.lvlName:
                        data.lvlName = Convert.ToString(val);
                        break;
                    case dataType.isBeaten:
                        data.isBeaten= Convert.ToBoolean(val);
                        break;
                    case dataType.isUnlocked:
                        data.isUnlocked =  Convert.ToBoolean(val);
                        break;
                    case dataType.HighestScore:
                        data.HighestScore = Convert.ToInt32(val);
                        break;
                    case dataType.HighestInitial:
                        data.HighestInitial = Convert.ToString(val);
                        break;
                    case dataType.SecondScore:
                        data.SecondScore = Convert.ToInt32(val);
                        break;
                    case dataType.SecondInitial:
                        data.SecondInitial = Convert.ToString(val);
                        break;
                    case dataType.ThirdtScore:
                        data.ThirdtScore = Convert.ToInt32(val);
                        break;
                    case dataType.ThirdInitial:
                        data.ThirdInitial = Convert.ToString(val);
                        break;
                }
            }
           
        }
            WriteScore(saveLvlScores);
    }
    public void ReadDataLevel()
    {

        //string filePath =  Application.persistentDataPath + "/Save/lvlscores.json";
        string filePath = Path.Combine(Application.persistentDataPath, "lvlscores.json");
        if (!File.Exists(filePath))
        {
            return;
        }

        //string readData = "";


        //StreamReader str = File.OpenText(filePath); 
       
        string loadData = File.ReadAllText(filePath);
        //readData = str.ReadToEnd();
        //str.Close();
        DataClassList scoreList = JsonUtility.FromJson<DataClassList>(loadData);
        //return JsonUtility.FromJson<DataClassList>(loadData);
        saveLvlScores = scoreList.Data_Class;
        
    }

    public Setting_class ReadDataSetting()
    {
        //string filePath =  Application.persistentDataPath + "/Save/settings.json";
        string filePath = Path.Combine(Application.persistentDataPath, "settings.json");
        //string readData = "";
        if (!File.Exists(filePath))
        {
            return null;
        }
        string loadData = File.ReadAllText(filePath);
        //StreamReader str = File.OpenText(filePath);

        //readData = str.ReadToEnd();
        //str.Close();

        SettingClassList Class = JsonUtility.FromJson<SettingClassList>(loadData);

        return Class.Setting_Class;
    }

    public void WriteSetting(bool music, bool soundEffect)
    {
        Setting_class m_setting = new Setting_class();
        if(music)
        {
            m_setting.MusicOnOff = true;
        }
        else
        {
            m_setting.MusicOnOff = false;
        }

        if (soundEffect)
        {
            m_setting.SoundEffectOnOff = true;
        }
        else
        {
            m_setting.SoundEffectOnOff = false;
        }

        SettingClassList list = new SettingClassList();

        list.Setting_Class = m_setting;

       // string js = JsonUtility.ToJson(list);
        string jsonData = JsonUtility.ToJson(list);
        //string fileUrl = Application.persistentDataPath + "/Save/settings.json";
        string fileUrl = Path.Combine(Application.persistentDataPath, "settings.json");
        if (!File.Exists(fileUrl))
        {

        }
        File.WriteAllText(fileUrl, jsonData);
        //StreamWriter sw = new StreamWriter(fileUrl);
        //sw.WriteLine(js);
       // sw.Close();
       
    }

    public void WriteScore(Data_class[] datas)
    {
        Data_class m_data = new Data_class();


        DataClassList list = new DataClassList();
        Data_class[] temp = datas;
        list.Data_Class = temp;

        //string js = JsonUtility.ToJson(list);
        string jsonData = JsonUtility.ToJson(list);
        //string fileUrl = Application.persistentDataPath + "/Save/lvlscores.json";
        string fileUrl = Path.Combine(Application.persistentDataPath, "lvlscores.json");
        if (!File.Exists(fileUrl))
        {
            Data_class emptyOne = new Data_class();
            emptyOne.lvlName = "none";
            emptyOne.isBeaten = false;
            emptyOne.isUnlocked = false;
            emptyOne.HighestScore = 0;
            emptyOne.HighestInitial = "Mr.someone";
            emptyOne.SecondInitial = "Mr.someone";
            emptyOne.SecondScore = 0;
            emptyOne.ThirdInitial = "Mr.someone";
            emptyOne.ThirdtScore = 0;
            Debug.Log(emptyOne.HighestInitial);
            Data_class[] createNew = new Data_class[20] {emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne };
            DataClassList newList = new DataClassList();
            newList.Data_Class = createNew;
            string json = JsonUtility.ToJson(newList);
            File.WriteAllText(fileUrl, json);
            //StreamWriter SW = new StreamWriter(fileUrl);
            //SW.WriteLine(json);
            
            //SW.Close();
            ReadDataLevel();
            return;
        }
        File.WriteAllText(fileUrl, jsonData);
        ReadDataLevel();
        //StreamWriter sw = new StreamWriter(fileUrl);
        //sw.WriteLine(js);
        //sw.Close();
    }
    public enum dataType
    {
        lvlName,
        isBeaten,
        isUnlocked,
        HighestScore,
        HighestInitial,
        SecondScore,
        SecondInitial,
        ThirdtScore,
        ThirdInitial
    }

    public bool GetLevelIsUnlocked(int levelIndex)
    {
        return saveLvlScores[levelIndex].isUnlocked;
    }

  

    public bool GetLevelIsBeanten(int levelIndex)
    {
        return saveLvlScores[levelIndex].isBeaten;
    }

    public void SetLevelName(int levelIndex, string name)
    {
        saveLvlScores[levelIndex].lvlName = name;
        
    }

    public void WriteData()
    {
        WriteScore(saveLvlScores);
    }
    
    public void clearData()
    {
        Data_class m_data = new Data_class();


        DataClassList list = new DataClassList();
        Data_class[] temp = saveLvlScores;
        list.Data_Class = temp;

        //string js = JsonUtility.ToJson(list);
        string jsonData = JsonUtility.ToJson(list);
        //string fileUrl = Application.persistentDataPath + "/Save/lvlscores.json";
        string fileUrl = Path.Combine(Application.persistentDataPath, "lvlscores.json");
        if (File.Exists(fileUrl))
        {
            Data_class emptyOne = new Data_class();
            emptyOne.lvlName = "none";
            emptyOne.isBeaten = false;
            emptyOne.isUnlocked = false;
            emptyOne.HighestScore = 0;
            emptyOne.HighestInitial = "Mr.someone";
            emptyOne.SecondInitial = "Mr.someone";
            emptyOne.SecondScore = 0;
            emptyOne.ThirdInitial = "Mr.someone";
            emptyOne.ThirdtScore = 0;
            Debug.Log(emptyOne.HighestInitial);
            Data_class[] createNew = new Data_class[20] { emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne, emptyOne };
            DataClassList newList = new DataClassList();
            newList.Data_Class = createNew;
            string json = JsonUtility.ToJson(newList);
            File.WriteAllText(fileUrl, json);
            //StreamWriter SW = new StreamWriter(fileUrl);
            //SW.WriteLine(json);

            //SW.Close();
            ReadDataLevel();
            return;
        }
    }
}