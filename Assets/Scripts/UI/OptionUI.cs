using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public GameObject optionUI;

    //스크린 모드 토글
    public Toggle fullScreenToggle;

    //오디오 믹서
    [SerializeField] private AudioMixer audioMixer;

    //볼륨 조절 슬라이드
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider FXSlider;

    //볼륨 아이콘
    public GameObject BGMIcon;
    public GameObject FXIcon;

    //아이콘 배열
    public Sprite[] BGMSprite;
    public Sprite[] FXSprite;


    private void Start()
    {
        //볼륨 조절
        BGMSlider.onValueChanged.AddListener(SetBGMVolume);
        FXSlider.onValueChanged.AddListener(SetFXVolume);

        //볼륨 조절값 저장
        PlayerPrefs.SetFloat("BGMVol", 1f);
        PlayerPrefs.SetFloat("FXVol", 1f);

        //볼륨 조절값 불러오기
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVol");
        FXSlider.value = PlayerPrefs.GetFloat("FXVol");
    }

    // Update is called once per frame
    void Update()
    {
        //ESC 키로 설정창 닫기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseOptionUI();
        }

        ChangeBGMIcon();
        ChangeFXIcon();
    }

    //설정창 열기
    public void OpenOptionUI()
    {
        optionUI.SetActive(true);
    }

    //설정창 닫기
    public void CloseOptionUI()
    {
        SoundManager.instance.PlayMeow();
        optionUI.SetActive(false);
    }

    //BGM 볼륨
    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVol", volume);
    }

    //고양이 효과음 볼륨
    public void SetFXVolume(float volume)
    {
        audioMixer.SetFloat("FX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("FXVol", volume);
    }

    //음소거 상태일 때 아이콘 변경
    public void ChangeBGMIcon()
    {
        if (BGMSlider.value == 0.001f)
        {
            BGMIcon.GetComponent<Image>().sprite = BGMSprite[0];
        }
        else
        {
            BGMIcon.GetComponent<Image>().sprite = BGMSprite[1];
        }
    }

    //음소거 상태일 때 아이콘 변경
    public void ChangeFXIcon()
    {
        if (FXSlider.value == 0.001f)
        {
            FXIcon.GetComponent<Image>().sprite = FXSprite[0];
        }
        else
        {
            FXIcon.GetComponent<Image>().sprite = FXSprite[1];
        }
    }

    //BGM 음소거 전환
    public void ToggleBGMMute()
    {
        if (BGMSlider.value == 0.001f)
        {
            BGMSlider.value = 1.0f;
        }
        else if (BGMSlider.value > 0.001f)
        {
            BGMSlider.value = 0.001f;
        }
    }

    //효과음 음소거 전환
    public void ToggleFXMute()
    {
        if (FXSlider.value == 0.001f)
        {
            FXSlider.value = 1.0f;
        }
        else if (FXSlider.value > 0.001f)
        {
            FXSlider.value = 0.001f;
        }
    }

    public void ChangeScreenMode()
    {
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("changed screen mode");
    }
}
