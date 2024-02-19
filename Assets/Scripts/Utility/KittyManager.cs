using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KittyManager : MonoBehaviour
{

    [SerializeField]
    public GameObject kitty;

    private void OnEnable()
    {
        if (kitty == null)
        {
            SpawnKitty();
        }
    }

    void SpawnKitty()
    {
        //고양이 프리팹 가지고 오기
        kitty = Resources.Load<GameObject>($"Prefabs/{DataManager.Instance.KittyCharacterData.kitties[GameManager.currentKittyNumber - 1].prefabName}");
        //고양이 시작 위치 가지고 오기
        Vector3 startPos = DataManager.Instance.KittyPositionData.positions[SceneManager.GetActiveScene().buildIndex - 3].startPosition;
        Quaternion startRot = DataManager.Instance.KittyPositionData.positions[SceneManager.GetActiveScene().buildIndex - 3].startRotation;
        
        //고양이 스폰
        Instantiate(kitty, startPos, startRot);
        GameManager.Init();
    }
}

