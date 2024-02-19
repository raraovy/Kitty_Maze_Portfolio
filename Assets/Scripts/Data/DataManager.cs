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

    //����� ĳ���� ���� �ҷ�����
    public void LoadCharacterData()
    {
        //ĳ���� ����
        var jsonCharacterData = Resources.Load<TextAsset>("Json/KittyCharacterData");
        //json �����͸� ������ȭ�Ͽ� PositionWrapper�� �־� ��
        KittyCharacterData = JsonUtility.FromJson<DataWrapper>(jsonCharacterData.ToString());
    }

    //���������� ����� ��ġ ���� �ҷ�����
    public void LoadPositionData()
    {
        //��ġ ����
        var jsonPositionData = Resources.Load<TextAsset>("Json/KittyPositionData");
        //json �����͸� ������ȭ�Ͽ� PositionWrapper�� �־� ��
        KittyPositionData = JsonUtility.FromJson<PositionWrapper>(jsonPositionData.ToString());
    }
}

