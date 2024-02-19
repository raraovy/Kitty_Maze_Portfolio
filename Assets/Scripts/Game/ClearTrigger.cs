using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTrigger : MonoBehaviour
{
    public GameObject clearEffect;
    public GameObject clearUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Kitty")
        {
            //Ŭ���� ����
            GameManager.isClear = true;
            GameManager.canMove = false;

            clearEffect.SetActive(true);

            //Ŭ���� ����
            SoundManager.instance.ClearSound();

            //���� ���������� �Ѿ�� �˾� �ڽ� UI
            clearUI.SetActive(true);
            Destroy(gameObject);
        }
    }
}
