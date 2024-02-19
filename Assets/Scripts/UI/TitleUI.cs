using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    //페이드 효과
    public SceneFader fader;

    //UI
    private OptionUI optionUI;
    public GameObject creditUI;


    //이동할 씬 이름
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
        //고양이 효과음
        SoundManager.instance.PlayMeow();
        //페이드 후 씬 이동
        fader.FadeTo(start);
    }

    //설정 버튼
    public void OptionButton()
    {
        //고양이 효과음
        SoundManager.instance.PlayMeow();
        //UI
        optionUI.OpenOptionUI();

    }

    //크레딧 버튼
    public void CreditButton()
    {
        //고양이 효과음
        SoundManager.instance.PlayMeow();
        //UI
        creditUI.SetActive(true);
    }

    public void CloseCreditUI()
    {   
        //고양이 효과음
        SoundManager.instance.PlayMeow();
        //UI
        creditUI.SetActive(false);
    }

    //나가기 버튼
    public void ExitButton()
    {
        //고양이 효과음
        SoundManager.instance.PlayMeow();
        //게임 종료
        Application.Quit();
    }
}
