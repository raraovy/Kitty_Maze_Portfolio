using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialUI;
    public TextMeshProUGUI tutorialText;
    public GameObject settingButton;

    //튜토리얼 가이드 화살표
    public GameObject guideArrow;


    //튜토리얼 문구
    private string text01 = "마우스 스크롤을 이용해\n줌 인/줌 아웃 해 보세요!";
    private string text02 = "WASD 키를 이용해\n상하좌우로 움직여 보세요!";
    private string text03 = "마우스 우클릭으로\n화면을 돌려 보세요!";
    private string text04 = "스페이스 바를 눌러\n처음 시점으로 돌아올 수 있어요!";
    private string text05 = "이제 타일을 밀어\n아기 고양이에게 길을 만들어 주세요!";

    private void Start()
    {
        settingButton.SetActive(false);
        StartCoroutine(TutorialText());
    }

    private void Update()
    {
        ShowGuideArrow();
    }


    IEnumerator ShowTutorial(string text)
    {
        //타이핑 효과
        string text00 = "";

        for (int i = 0; i < text.Length; i++)
        {
            text00 += text[i];

            //타이핑 속도
            yield return new WaitForSeconds(0.05f);

            //텍스트 초기화
            tutorialText.text = text00;
            yield return null;
        }

        //클릭하면 다음으로 넘어가기
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                break;
            }
            yield return null;
        }
    }

    //튜토리얼 텍스트 차례대로
    IEnumerator TutorialText()
    {
        GameManager.doTutorial = false;
        GameManager.isStart = false;

        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(ShowTutorial(text01));
        yield return StartCoroutine(ShowTutorial(text02));
        yield return StartCoroutine(ShowTutorial(text03));
        yield return StartCoroutine(ShowTutorial(text04));
        yield return StartCoroutine(ShowTutorial(text05));

        GameManager.doTutorial = true;
        GameManager.isStart = true;

        tutorialUI.SetActive(false);

        yield break;
    }

    //튜토리얼 스킵
    public void Skip()
    {
        GameManager.doTutorial = true;
        GameManager.isStart = true;

        settingButton.SetActive(true);
        tutorialUI.SetActive(false);

    }

    //가이드 화살표
    void ShowGuideArrow()
    {
        if (GameManager.isStart == false) //게임 시작 전에는 비활성화
        {
            guideArrow.SetActive(false);
        }
        else
        {
            guideArrow.SetActive(true); //게임 시작 후 활성화

            if (Input.GetMouseButton(0)) //클릭 시 비활성화
            {
                guideArrow.SetActive(false);
            }
            else
                guideArrow.SetActive(true);
        }
        if (GameManager.canMove == true || GameManager.isClear == true)
        {
            guideArrow.SetActive(false);
        }
    }
}
