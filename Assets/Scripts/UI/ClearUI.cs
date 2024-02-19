using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearUI : MonoBehaviour
{
    public GameObject settingButton; //설정 버튼
    public SceneFader fader; //페이드 효과
    public TextMeshProUGUI clearText; //클리어 텍스트
    [SerializeField]
    private string clear; //클리어 텍스트 문자

    public Image[] stars; //클리어 UI 하트 이미지
    public Sprite starSprite; //하트 이미지

    //타일 움직인 횟수에 따른 별 개수
    [SerializeField]
    private int twoStar;
    [SerializeField]
    private int threeStar;
    //획득한 별 개수
    private int getStarCount;

    //스테이지
    [SerializeField]
    private string stageName;

    //다음 레벨
    [SerializeField]
    private int nextLevel = 2;

    private void OnEnable()
    {
        settingButton.SetActive(false);

        getStarCount = 0;
        LevelClear();
    }

    public void Menu()
    {
        Debug.Log("Menu");
        fader.FadeTo("LevelSelect");
    }

    public void NextLevel()
    {
        Debug.Log("NextLevel");

        //마지막 레벨이면 레벨 선택 씬으로 이동
        if (SceneManager.GetActiveScene().name == "Stage8")
        {
            fader.FadeTo("LevelSelect");
            return;
        }
        fader.FadeTo($"Stage{nextLevel}");
    }

    public void Retry()
    {
        Debug.Log("Retry");
        string sceneName = SceneManager.GetActiveScene().name;

        fader.FadeTo(sceneName);
    }

    public void LevelClear()
    {
        clearText.text = clear;
        PrintStar();

        //해당 스테이지에서 저장된 별이 있을 경우
        if (PlayerPrefs.HasKey(stageName))
        {
            //저장된 별의 개수 가지고 오기
            int beforeCount = PlayerPrefs.GetInt(stageName, 0);
            int totalStar = PlayerPrefs.GetInt("star", 0);

            //이전에 획득한 별보다 지금이 같거나 더 많을 때
            if (beforeCount <= getStarCount)
            {
                //이전에 획득한 별 개수 삭제
                totalStar -= beforeCount;
                //이번에 획득한 별 개수 저장
                PlayerPrefs.SetInt(stageName, getStarCount);
                //총 별의 개수 저장
                totalStar += getStarCount;
                PlayerPrefs.SetInt("star", totalStar);

                Debug.Log($"지워진 별 개수: {beforeCount}");
                Debug.Log($"추가한 별 개수: {getStarCount}");
            }
            //이전에 획득한 별이 지금보다 더 많을 때
            else
            {
                //그대로 저장
                PlayerPrefs.SetInt("star", totalStar);
            }
            //저장
            GameManager.starCount = totalStar;
            Debug.Log($"별의 개수: {GameManager.starCount}");
        }
        else //처음 클리어한 스테이지일 경우
        {
            int totalStar = PlayerPrefs.GetInt("star", 0);
            totalStar += getStarCount;

            //새로 저장
            GameManager.starCount = totalStar;
            PlayerPrefs.SetInt("star", totalStar);
            PlayerPrefs.SetInt(stageName, getStarCount);

            Debug.Log($"추가한 별 개수: {getStarCount}");
            Debug.Log($"별의 개수: {GameManager.starCount}");
        }

        //현재 저장된 레벨을 가져온다
        int nowLevel = PlayerPrefs.GetInt("NLevel", 1);

        if (nextLevel > nowLevel)
        {
            PlayerPrefs.SetInt("NLevel", nextLevel);
        }
    }

    private void PrintStar()
    {
        if (GameManager.moveCount > twoStar && GameManager.moveCount > threeStar) //별 한 개 기준
        {
            stars[0].sprite = starSprite;
            getStarCount = 1;
        }
        else if (GameManager.moveCount <= twoStar && GameManager.moveCount > threeStar) //별 두 개 기준
        {
            stars[0].sprite = starSprite;
            stars[1].sprite = starSprite;
            getStarCount = 2;
        }
        else if (GameManager.moveCount < twoStar && GameManager.moveCount <= threeStar) //별 세 개 기준
        {
            stars[0].sprite = starSprite;
            stars[1].sprite = starSprite;
            stars[2].sprite = starSprite;
            getStarCount = 3;
        }
    }
}
