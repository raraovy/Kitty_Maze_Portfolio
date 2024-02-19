using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectUI : MonoBehaviour
{
    //레벨 선택
    private Button[] levelButtons;
    public Transform contents;

    //3별 레벨 아이콘
    public Sprite threeStarImage;
    //1~2별 레벨 아이콘
    public Sprite halfStarImage;

    //별 개수 텍스트
    public TextMeshProUGUI starText;

    //업데이트 예정 UI
    public GameObject updateUI;

    //페이드 효과
    public SceneFader fader;

    // Start is called before the first frame update
    void Start()
    {
        int nowLevel = PlayerPrefs.GetInt("NLevel", 1);
        Debug.Log($"가져온 nowLevel: {nowLevel}");

        //levelButtons 배열 선언
        levelButtons = new Button[contents.childCount];

        //레벨 버튼 세팅
        for (int i = 0; i < levelButtons.Length; i++)
        {
            Transform child = contents.GetChild(i);
            levelButtons[i] = child.GetComponent<Button>();

            if (i >= nowLevel)
            {
                levelButtons[i].interactable = false; //레벨 lock & unlock
            }
            //획득한 별 개수에 따라 레벨 아이콘 바꾸기
            if (PlayerPrefs.GetInt($"Stage{i}", 0) == 3)
            {
                levelButtons[i - 1].image.sprite = threeStarImage;
            }
            else if (PlayerPrefs.GetInt($"Stage{i}", 0) == 1 || PlayerPrefs.GetInt($"Stage{i}", 0) == 2)
            {
                levelButtons[i - 1].image.sprite = halfStarImage;
            }
        }

        //지금까지 모은 별 개수 표시
        GameManager.starCount = PlayerPrefs.GetInt("star", 0);
        starText.text = GameManager.starCount.ToString();
    }

    //레벨 선택 시 호출되는 함수
    public void SelectLevel(string sceneName)
    {
        SoundManager.instance.PlayMeow();
        fader.FadeTo(sceneName);
    }

    //뒤로 가기
    public void Back()
    {
        SoundManager.instance.PlayMeow();
        fader.FadeTo("Title");
    }

    //별 상점
    public void GoStarShop()
    {
        SoundManager.instance.PlayMeow();
        fader.FadeTo("KittySelect");

    }

    //마지막 레벨
    public void LastLevel()
    {
        SoundManager.instance.PlayMeow();
        StartCoroutine(UpdateUIPopup());
    }

    //자동으로 창 닫기
    IEnumerator UpdateUIPopup()
    {
        updateUI.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        updateUI.SetActive(false);
    }


}
