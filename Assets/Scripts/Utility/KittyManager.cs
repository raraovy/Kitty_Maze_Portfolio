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
        //����� ������ ������ ����
        kitty = Resources.Load<GameObject>($"Prefabs/{DataManager.Instance.KittyCharacterData.kitties[GameManager.currentKittyNumber - 1].prefabName}");
        //����� ���� ��ġ ������ ����
        Vector3 startPos = DataManager.Instance.KittyPositionData.positions[SceneManager.GetActiveScene().buildIndex - 3].startPosition;
        Quaternion startRot = DataManager.Instance.KittyPositionData.positions[SceneManager.GetActiveScene().buildIndex - 3].startRotation;
        
        //����� ����
        Instantiate(kitty, startPos, startRot);
        GameManager.Init();
    }
}

