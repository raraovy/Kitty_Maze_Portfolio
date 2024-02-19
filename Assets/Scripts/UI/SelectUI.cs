using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectUI : MonoBehaviour
{
    //���� ����
    private Button[] levelButtons;
    public Transform contents;

    //3�� ���� ������
    public Sprite threeStarImage;
    //1~2�� ���� ������
    public Sprite halfStarImage;

    //�� ���� �ؽ�Ʈ
    public TextMeshProUGUI starText;

    //������Ʈ ���� UI
    public GameObject updateUI;

    //���̵� ȿ��
    public SceneFader fader;

    // Start is called before the first frame update
    void Start()
    {
        int nowLevel = PlayerPrefs.GetInt("NLevel", 1);
        Debug.Log($"������ nowLevel: {nowLevel}");

        //levelButtons �迭 ����
        levelButtons = new Button[contents.childCount];

        //���� ��ư ����
        for (int i = 0; i < levelButtons.Length; i++)
        {
            Transform child = contents.GetChild(i);
            levelButtons[i] = child.GetComponent<Button>();

            if (i >= nowLevel)
            {
                levelButtons[i].interactable = false; //���� lock & unlock
            }
            //ȹ���� �� ������ ���� ���� ������ �ٲٱ�
            if (PlayerPrefs.GetInt($"Stage{i}", 0) == 3)
            {
                levelButtons[i - 1].image.sprite = threeStarImage;
            }
            else if (PlayerPrefs.GetInt($"Stage{i}", 0) == 1 || PlayerPrefs.GetInt($"Stage{i}", 0) == 2)
            {
                levelButtons[i - 1].image.sprite = halfStarImage;
            }
        }

        //���ݱ��� ���� �� ���� ǥ��
        GameManager.starCount = PlayerPrefs.GetInt("star", 0);
        starText.text = GameManager.starCount.ToString();
    }

    //���� ���� �� ȣ��Ǵ� �Լ�
    public void SelectLevel(string sceneName)
    {
        SoundManager.instance.PlayMeow();
        fader.FadeTo(sceneName);
    }

    //�ڷ� ����
    public void Back()
    {
        SoundManager.instance.PlayMeow();
        fader.FadeTo("Title");
    }

    //�� ����
    public void GoStarShop()
    {
        SoundManager.instance.PlayMeow();
        fader.FadeTo("KittySelect");

    }

    //������ ����
    public void LastLevel()
    {
        SoundManager.instance.PlayMeow();
        StartCoroutine(UpdateUIPopup());
    }

    //�ڵ����� â �ݱ�
    IEnumerator UpdateUIPopup()
    {
        updateUI.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        updateUI.SetActive(false);
    }


}
