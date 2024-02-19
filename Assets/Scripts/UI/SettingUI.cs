using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingUI : MonoBehaviour
{
    //설정 메뉴
    public GameObject settingUI;
    //톱니바퀴 아이콘
    public GameObject settingButton;
    //클리어 UI
    public GameObject clearUI;
    //페이드 효과
    public SceneFader fader;

    private void Update()
    {
        //ESC 키로 설정창 열고 닫기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.doTutorial == true)
            {
                Toggle();
            }
        }
    }
    
    //설정창 열기
    public void OpenSetting()
    {
        settingButton.SetActive(false);
        settingUI.SetActive(true);
        GameManager.isPaused = true;

        Debug.Log("Setting");
    }

    //설정창 닫기
    public void CloseSetting()
    {
        settingButton.SetActive(true);
        settingUI.SetActive(false);
        GameManager.isPaused = false;

        Debug.Log("Close");
    }

    public void Toggle()
    {
        settingUI.SetActive(!settingUI.activeSelf);

        if (settingUI.activeSelf == true) //설정 창이 열렸을 때
        {
            settingButton.SetActive(false);
            GameManager.isPaused = true;
            Debug.Log("PAUSE");
        }
        else //창이 닫혔을 때
        {
            settingButton.SetActive(true);
            GameManager.isPaused = false;
            Debug.Log("RESUME");
        }
    }

    //재시작
    public void Retry()
    {
        Debug.Log("Retry");
        Scene scene = SceneManager.GetActiveScene();
        fader.FadeTo(scene.name);
        GameManager.isPaused = false;
    }

    //종료
    public void Exit()
    {
        fader.FadeTo("LevelSelect");
        GameManager.isPaused = false;
    }
}
