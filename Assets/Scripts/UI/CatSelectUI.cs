using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class CatSelectUI : MonoBehaviour
{
    //ĳ���� ������Ʈ
    [SerializeField]
    private GameObject[] kitties;

    //������ �� ���� �ؽ�Ʈ
    public TextMeshProUGUI starText;

    //����
    public GameObject price;
    public TextMeshProUGUI priceText;

    //���� �� �ؽ�Ʈ
    public GameObject haveText;

    //���� ���� �ְ� �ִ� ����� ��ȣ
    private int showKittyNumber;

    //�˾� UI
    public GameObject popupUI;
    public TextMeshProUGUI popupText;

    //���� ��ư
    public GameObject applyButton;
    //���� ��ư
    public GameObject purchaseButton;

    //��ƼŬ ����Ʈ
    public GameObject particle;

    //���̵� ȿ��
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
        //�� ���� �ǽð� ������Ʈ
        int star = PlayerPrefs.GetInt("star", 0);
        starText.text = star.ToString();

        //���� �������� �ִ� ����̰� ������ ������� ��� ��ƼŬ ����Ʈ ON
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
        //1�� ����̿��� ���� ��ư ������ 10�� ����̷�
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
        //10�� ����̿��� ������ ��ư ������ 1�� ����̷�
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
        //����� ���� �ֱ�
        for (int i = 0; i < kitties.Length; i++)
        {
            kitties[i].SetActive(false);
            kitties[showKittyNumber - 1].SetActive(true);

            CheckHaveKitty();
        }
    }


    private void CheckHaveKitty()
    {

        //���� ���� ����̰� �ƴ� ��
        if (!PlayerPrefs.HasKey($"Kitty_{showKittyNumber}"))
        {
            haveText.SetActive(false);
            price.SetActive(true);
            purchaseButton.SetActive(true);
            applyButton.SetActive(false);
            priceText.text = DataManager.Instance.KittyCharacterData.kitties[showKittyNumber - 1].kittyPrice.ToString();
        }
        //���� ���� ������� ��
        else
        {
            price.SetActive(false);
            haveText.SetActive(true);
            purchaseButton.SetActive(false);
            applyButton.SetActive(true);
        }
    }


    //ĳ���� ����
    public void BuyKitty()
    {
        SoundManager.instance.PlayMeow();
        GameManager.starCount = PlayerPrefs.GetInt("star", 0);

        //������ ���� ������� ���ݺ��� ���ų� ���� ���
        if (GameManager.starCount >= DataManager.Instance.KittyCharacterData.kitties[showKittyNumber - 1].kittyPrice)
        {
            //�� ����
            GameManager.starCount -= DataManager.Instance.KittyCharacterData.kitties[showKittyNumber - 1].kittyPrice;
            PlayerPrefs.SetInt("star", GameManager.starCount);

            StartCoroutine(ShowPopupUI("����̸� �����Ͽ����ϴ�!"));

            //�����Ͱ� ����
            PlayerPrefs.SetString($"Kitty_{showKittyNumber}", "true");

            CurrentKitty();
        }
        else
        {
            StartCoroutine(ShowPopupUI("���� �����մϴ�!"));
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

        //���� ���� ����̰� �ƴ� �� return
        if (!PlayerPrefs.HasKey($"Kitty_{showKittyNumber}"))
            return;

        if (GameManager.currentKittyNumber == showKittyNumber)
        {
            StartCoroutine(ShowPopupUI("�̹� ���õ� ������Դϴ�!"));
            return;
        }

        GameManager.currentKittyNumber = showKittyNumber;
        StartCoroutine(ShowPopupUI("����̰� ���õǾ����ϴ�!"));

        //���� ���õ� ����̿� ����
        PlayerPrefs.SetInt("Kitty", GameManager.currentKittyNumber);
    }


    //�ڵ����� â �ݱ�
    IEnumerator ShowPopupUI(string text)
    {
        popupUI.SetActive(true);
        popupText.text = text;
        yield return new WaitForSeconds(1.0f);
        popupUI.SetActive(false);
    }
}
