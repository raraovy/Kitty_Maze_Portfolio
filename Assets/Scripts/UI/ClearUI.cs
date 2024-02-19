using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearUI : MonoBehaviour
{
    public GameObject settingButton; //���� ��ư
    public SceneFader fader; //���̵� ȿ��
    public TextMeshProUGUI clearText; //Ŭ���� �ؽ�Ʈ
    [SerializeField]
    private string clear; //Ŭ���� �ؽ�Ʈ ����

    public Image[] stars; //Ŭ���� UI ��Ʈ �̹���
    public Sprite starSprite; //��Ʈ �̹���

    //Ÿ�� ������ Ƚ���� ���� �� ����
    [SerializeField]
    private int twoStar;
    [SerializeField]
    private int threeStar;
    //ȹ���� �� ����
    private int getStarCount;

    //��������
    [SerializeField]
    private string stageName;

    //���� ����
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

        //������ �����̸� ���� ���� ������ �̵�
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

        //�ش� ������������ ����� ���� ���� ���
        if (PlayerPrefs.HasKey(stageName))
        {
            //����� ���� ���� ������ ����
            int beforeCount = PlayerPrefs.GetInt(stageName, 0);
            int totalStar = PlayerPrefs.GetInt("star", 0);

            //������ ȹ���� ������ ������ ���ų� �� ���� ��
            if (beforeCount <= getStarCount)
            {
                //������ ȹ���� �� ���� ����
                totalStar -= beforeCount;
                //�̹��� ȹ���� �� ���� ����
                PlayerPrefs.SetInt(stageName, getStarCount);
                //�� ���� ���� ����
                totalStar += getStarCount;
                PlayerPrefs.SetInt("star", totalStar);

                Debug.Log($"������ �� ����: {beforeCount}");
                Debug.Log($"�߰��� �� ����: {getStarCount}");
            }
            //������ ȹ���� ���� ���ݺ��� �� ���� ��
            else
            {
                //�״�� ����
                PlayerPrefs.SetInt("star", totalStar);
            }
            //����
            GameManager.starCount = totalStar;
            Debug.Log($"���� ����: {GameManager.starCount}");
        }
        else //ó�� Ŭ������ ���������� ���
        {
            int totalStar = PlayerPrefs.GetInt("star", 0);
            totalStar += getStarCount;

            //���� ����
            GameManager.starCount = totalStar;
            PlayerPrefs.SetInt("star", totalStar);
            PlayerPrefs.SetInt(stageName, getStarCount);

            Debug.Log($"�߰��� �� ����: {getStarCount}");
            Debug.Log($"���� ����: {GameManager.starCount}");
        }

        //���� ����� ������ �����´�
        int nowLevel = PlayerPrefs.GetInt("NLevel", 1);

        if (nextLevel > nowLevel)
        {
            PlayerPrefs.SetInt("NLevel", nextLevel);
        }
    }

    private void PrintStar()
    {
        if (GameManager.moveCount > twoStar && GameManager.moveCount > threeStar) //�� �� �� ����
        {
            stars[0].sprite = starSprite;
            getStarCount = 1;
        }
        else if (GameManager.moveCount <= twoStar && GameManager.moveCount > threeStar) //�� �� �� ����
        {
            stars[0].sprite = starSprite;
            stars[1].sprite = starSprite;
            getStarCount = 2;
        }
        else if (GameManager.moveCount < twoStar && GameManager.moveCount <= threeStar) //�� �� �� ����
        {
            stars[0].sprite = starSprite;
            stars[1].sprite = starSprite;
            stars[2].sprite = starSprite;
            getStarCount = 3;
        }
    }
}
