using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public GameObject optionUI;

    //��ũ�� ��� ���
    public Toggle fullScreenToggle;

    //����� �ͼ�
    [SerializeField] private AudioMixer audioMixer;

    //���� ���� �����̵�
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider FXSlider;

    //���� ������
    public GameObject BGMIcon;
    public GameObject FXIcon;

    //������ �迭
    public Sprite[] BGMSprite;
    public Sprite[] FXSprite;


    private void Start()
    {
        //���� ����
        BGMSlider.onValueChanged.AddListener(SetBGMVolume);
        FXSlider.onValueChanged.AddListener(SetFXVolume);

        //���� ������ ����
        PlayerPrefs.SetFloat("BGMVol", 1f);
        PlayerPrefs.SetFloat("FXVol", 1f);

        //���� ������ �ҷ�����
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVol");
        FXSlider.value = PlayerPrefs.GetFloat("FXVol");
    }

    // Update is called once per frame
    void Update()
    {
        //ESC Ű�� ����â �ݱ�
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseOptionUI();
        }

        ChangeBGMIcon();
        ChangeFXIcon();
    }

    //����â ����
    public void OpenOptionUI()
    {
        optionUI.SetActive(true);
    }

    //����â �ݱ�
    public void CloseOptionUI()
    {
        SoundManager.instance.PlayMeow();
        optionUI.SetActive(false);
    }

    //BGM ����
    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVol", volume);
    }

    //����� ȿ���� ����
    public void SetFXVolume(float volume)
    {
        audioMixer.SetFloat("FX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("FXVol", volume);
    }

    //���Ұ� ������ �� ������ ����
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

    //���Ұ� ������ �� ������ ����
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

    //BGM ���Ұ� ��ȯ
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

    //ȿ���� ���Ұ� ��ȯ
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
