using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    //��ũ�� ���
    FullScreenMode fullScreenMode;
    //��ũ�� ��� ���
    public Toggle fullScreenToggle;

    //��Ӵٿ�
    public TMP_Dropdown resolutionDropdown;
    //��Ӵٿ� ����Ʈ
    List<Resolution> resolutions = new List<Resolution>();

    [SerializeField]
    private int resolutionNum;

    // Start is called before the first frame update
    void Start()
    {
        SetScreenOption();
        fullScreenToggle.isOn = true;
    }

    //ȭ�� �ػ� ��
    void SetScreenOption()
    {
        resolutions.AddRange(Screen.resolutions);
        resolutionDropdown.options.Clear();
        
        int optionNum = 0;
        foreach (Resolution item in resolutions)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = item.width + " X " + item.height;
            resolutionDropdown.options.Add(option);

            if (item.width == Screen.width && item.height == Screen.height)
            {
                resolutionDropdown.value = optionNum;
            }
            optionNum++;
        }

        resolutionDropdown.RefreshShownValue();

        //â���, ��ü ȭ�� ���
        fullScreenToggle.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    //�ػ� ����
    public void DropboxOptionChange(int x)
    {
        resolutionNum = x;
    }

    //â���, ��ü ȭ��
    public void SetFullScreen(bool isFull)
    {
        fullScreenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        ChangeScreenOption();
    }
    public void ChangeScreenOption()
    {
        Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, fullScreenMode);
    }
}
