using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    //���̵� ȿ��
    public SceneFader fader;

    //UI
    private OptionUI optionUI;
    public GameObject creditUI;


    //�̵��� �� �̸�
    private string start = "LevelSelect";


    public void Start()
    {
        optionUI = GetComponent<OptionUI>();       
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseCreditUI();
        }
    }

    public void StartButton()
    {
        //����� ȿ����
        SoundManager.instance.PlayMeow();
        //���̵� �� �� �̵�
        fader.FadeTo(start);
    }

    //���� ��ư
    public void OptionButton()
    {
        //����� ȿ����
        SoundManager.instance.PlayMeow();
        //UI
        optionUI.OpenOptionUI();

    }

    //ũ���� ��ư
    public void CreditButton()
    {
        //����� ȿ����
        SoundManager.instance.PlayMeow();
        //UI
        creditUI.SetActive(true);
    }

    public void CloseCreditUI()
    {   
        //����� ȿ����
        SoundManager.instance.PlayMeow();
        //UI
        creditUI.SetActive(false);
    }

    //������ ��ư
    public void ExitButton()
    {
        //����� ȿ����
        SoundManager.instance.PlayMeow();
        //���� ����
        Application.Quit();
    }
}
