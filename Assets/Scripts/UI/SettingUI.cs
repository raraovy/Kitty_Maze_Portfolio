using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingUI : MonoBehaviour
{
    //���� �޴�
    public GameObject settingUI;
    //��Ϲ��� ������
    public GameObject settingButton;
    //Ŭ���� UI
    public GameObject clearUI;
    //���̵� ȿ��
    public SceneFader fader;

    private void Update()
    {
        //ESC Ű�� ����â ���� �ݱ�
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.doTutorial == true)
            {
                Toggle();
            }
        }
    }
    
    //����â ����
    public void OpenSetting()
    {
        settingButton.SetActive(false);
        settingUI.SetActive(true);
        GameManager.isPaused = true;

        Debug.Log("Setting");
    }

    //����â �ݱ�
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

        if (settingUI.activeSelf == true) //���� â�� ������ ��
        {
            settingButton.SetActive(false);
            GameManager.isPaused = true;
            Debug.Log("PAUSE");
        }
        else //â�� ������ ��
        {
            settingButton.SetActive(true);
            GameManager.isPaused = false;
            Debug.Log("RESUME");
        }
    }

    //�����
    public void Retry()
    {
        Debug.Log("Retry");
        Scene scene = SceneManager.GetActiveScene();
        fader.FadeTo(scene.name);
        GameManager.isPaused = false;
    }

    //����
    public void Exit()
    {
        fader.FadeTo("LevelSelect");
        GameManager.isPaused = false;
    }
}
