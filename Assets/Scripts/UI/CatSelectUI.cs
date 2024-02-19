using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class CatSelectUI : MonoBehaviour
{
    //캐릭터 오브젝트
    [SerializeField]
    private GameObject[] kitties;

    //보유한 별 개수 텍스트
    public TextMeshProUGUI starText;

    //가격
    public GameObject price;
    public TextMeshProUGUI priceText;

    //보유 중 텍스트
    public GameObject haveText;

    //지금 보여 주고 있는 고양이 번호
    private int showKittyNumber;

    //팝업 UI
    public GameObject popupUI;
    public TextMeshProUGUI popupText;

    //적용 버튼
    public GameObject applyButton;
    //구매 버튼
    public GameObject purchaseButton;

    //파티클 이펙트
    public GameObject particle;

    //페이드 효과
    public SceneFader fader;


    // Start is called before the first frame update
    void Start()
    {
        //kitties = GameObject.FindGameObjectsWithTag("Kitty");

        showKittyNumber = GameManager.currentKittyNumber;

        CurrentKitty();
    }

    // Update is called once per frame
    void Update()
    {
        //별 개수 실시간 업데이트
        int star = PlayerPrefs.GetInt("star", 0);
        starText.text = star.ToString();

        //현재 보여지고 있는 고양이가 선택한 고양이일 경우 파티클 이펙트 ON
        if (GameManager.currentKittyNumber == showKittyNumber)
        {
            particle.SetActive(true);
        }
        else
        {
            particle.SetActive(false);
        }
    }

    public void LeftButton()
    {
        //1번 고양이에서 왼쪽 버튼 누르면 10번 고양이로
        if (showKittyNumber == 1)
        {
            showKittyNumber = 11;
        }
        showKittyNumber--;
        CurrentKitty();
        SoundManager.instance.PlayMeow();
    }

    public void RightButton()
    {
        //10번 고양이에서 오른쪽 버튼 누르면 1번 고양이로
        if (showKittyNumber == 10)
        {
            showKittyNumber = 0;
        }
        showKittyNumber++;
        CurrentKitty();
        SoundManager.instance.PlayMeow();
    }

    private void CurrentKitty()
    {
        //고양이 보여 주기
        for (int i = 0; i < kitties.Length; i++)
        {
            kitties[i].SetActive(false);
            kitties[showKittyNumber - 1].SetActive(true);

            CheckHaveKitty();
        }
    }


    private void CheckHaveKitty()
    {

        //보유 중인 고양이가 아닐 때
        if (!PlayerPrefs.HasKey($"Kitty_{showKittyNumber}"))
        {
            haveText.SetActive(false);
            price.SetActive(true);
            purchaseButton.SetActive(true);
            applyButton.SetActive(false);
            priceText.text = DataManager.Instance.KittyCharacterData.kitties[showKittyNumber - 1].kittyPrice.ToString();
        }
        //보유 중인 고양이일 때
        else
        {
            price.SetActive(false);
            haveText.SetActive(true);
            purchaseButton.SetActive(false);
            applyButton.SetActive(true);
        }
    }


    //캐릭터 구매
    public void BuyKitty()
    {
        SoundManager.instance.PlayMeow();
        GameManager.starCount = PlayerPrefs.GetInt("star", 0);

        //보유한 별이 고양이의 가격보다 같거나 많은 경우
        if (GameManager.starCount >= DataManager.Instance.KittyCharacterData.kitties[showKittyNumber - 1].kittyPrice)
        {
            //별 지불
            GameManager.starCount -= DataManager.Instance.KittyCharacterData.kitties[showKittyNumber - 1].kittyPrice;
            PlayerPrefs.SetInt("star", GameManager.starCount);

            StartCoroutine(ShowPopupUI("고양이를 구매하였습니다!"));

            //데이터값 저장
            PlayerPrefs.SetString($"Kitty_{showKittyNumber}", "true");

            CurrentKitty();
        }
        else
        {
            StartCoroutine(ShowPopupUI("별이 부족합니다!"));
        }
    }


    public void Back()
    {
        SoundManager.instance.PlayMeow();
        fader.FadeTo("LevelSelect");
    }

    public void Apply()
    {
        SoundManager.instance.PlayMeow();

        //보유 중인 고양이가 아닐 때 return
        if (!PlayerPrefs.HasKey($"Kitty_{showKittyNumber}"))
            return;

        if (GameManager.currentKittyNumber == showKittyNumber)
        {
            StartCoroutine(ShowPopupUI("이미 선택된 고양이입니다!"));
            return;
        }

        GameManager.currentKittyNumber = showKittyNumber;
        StartCoroutine(ShowPopupUI("고양이가 선택되었습니다!"));

        //현재 선택된 고양이에 저장
        PlayerPrefs.SetInt("Kitty", GameManager.currentKittyNumber);
    }


    //자동으로 창 닫기
    IEnumerator ShowPopupUI(string text)
    {
        popupUI.SetActive(true);
        popupText.text = text;
        yield return new WaitForSeconds(1.0f);
        popupUI.SetActive(false);
    }
}
