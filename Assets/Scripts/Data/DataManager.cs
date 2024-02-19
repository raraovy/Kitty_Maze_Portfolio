using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

[System.Serializable]
public class DataWrapper
{
    public KittyCharacterData[] kitties;
}

[System.Serializable]
public class PositionWrapper
{
    public KittyPositionData[] positions;
}

public class DataManager : MonoBehaviour
{
    #region singleton

    private static DataManager instance;

    public static DataManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion singleton

    public DataWrapper KittyCharacterData;
    public PositionWrapper KittyPositionData;


    private void Start()
    {
        LoadCharacterData();
        LoadPositionData();
    }

    //고양이 캐릭터 정보 불러오기
    public void LoadCharacterData()
    {
        //캐릭터 정보
        var jsonCharacterData = Resources.Load<TextAsset>("Json/KittyCharacterData");
        //json 데이터를 역직렬화하여 PositionWrapper에 넣어 줌
        KittyCharacterData = JsonUtility.FromJson<DataWrapper>(jsonCharacterData.ToString());
    }

    //스테이지별 고양이 위치 정보 불러오기
    public void LoadPositionData()
    {
        //위치 정보
        var jsonPositionData = Resources.Load<TextAsset>("Json/KittyPositionData");
        //json 데이터를 역직렬화하여 PositionWrapper에 넣어 줌
        KittyPositionData = JsonUtility.FromJson<PositionWrapper>(jsonPositionData.ToString());
    }
}

