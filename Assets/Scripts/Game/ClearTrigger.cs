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
            //클리어 판정
            GameManager.isClear = true;
            GameManager.canMove = false;

            clearEffect.SetActive(true);

            //클리어 사운드
            SoundManager.instance.ClearSound();

            //다음 스테이지로 넘어가는 팝업 박스 UI
            clearUI.SetActive(true);
            Destroy(gameObject);
        }
    }
}
