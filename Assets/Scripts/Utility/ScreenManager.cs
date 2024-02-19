using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    //스크린 모드
    FullScreenMode fullScreenMode;
    //스크린 모드 토글
    public Toggle fullScreenToggle;

    //드롭다운
    public TMP_Dropdown resolutionDropdown;
    //드롭다운 리스트
    List<Resolution> resolutions = new List<Resolution>();

    [SerializeField]
    private int resolutionNum;

    // Start is called before the first frame update
    void Start()
    {
        SetScreenOption();
        fullScreenToggle.isOn = true;
    }

    //화면 해상도 값
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

        //창모드, 전체 화면 토글
        fullScreenToggle.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    //해상도 변경
    public void DropboxOptionChange(int x)
    {
        resolutionNum = x;
    }

    //창모드, 전체 화면
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
