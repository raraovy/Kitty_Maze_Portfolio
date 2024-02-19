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

    //Ʃ�丮�� ���̵� ȭ��ǥ
    public GameObject guideArrow;


    //Ʃ�丮�� ����
    private string text01 = "���콺 ��ũ���� �̿���\n�� ��/�� �ƿ� �� ������!";
    private string text02 = "WASD Ű�� �̿���\n�����¿�� ������ ������!";
    private string text03 = "���콺 ��Ŭ������\nȭ���� ���� ������!";
    private string text04 = "�����̽� �ٸ� ����\nó�� �������� ���ƿ� �� �־��!";
    private string text05 = "���� Ÿ���� �о�\n�Ʊ� ����̿��� ���� ����� �ּ���!";

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
        //Ÿ���� ȿ��
        string text00 = "";

        for (int i = 0; i < text.Length; i++)
        {
            text00 += text[i];

            //Ÿ���� �ӵ�
            yield return new WaitForSeconds(0.05f);

            //�ؽ�Ʈ �ʱ�ȭ
            tutorialText.text = text00;
            yield return null;
        }

        //Ŭ���ϸ� �������� �Ѿ��
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                break;
            }
            yield return null;
        }
    }

    //Ʃ�丮�� �ؽ�Ʈ ���ʴ��
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

    //Ʃ�丮�� ��ŵ
    public void Skip()
    {
        GameManager.doTutorial = true;
        GameManager.isStart = true;

        settingButton.SetActive(true);
        tutorialUI.SetActive(false);

    }

    //���̵� ȭ��ǥ
    void ShowGuideArrow()
    {
        if (GameManager.isStart == false) //���� ���� ������ ��Ȱ��ȭ
        {
            guideArrow.SetActive(false);
        }
        else
        {
            guideArrow.SetActive(true); //���� ���� �� Ȱ��ȭ

            if (Input.GetMouseButton(0)) //Ŭ�� �� ��Ȱ��ȭ
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
