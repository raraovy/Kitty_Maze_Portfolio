using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isStart; //���� ���� ����
    public static bool isPaused; //�Ͻ����� ����
    public static bool isClear; //���� Ŭ���� ����
    public static bool canMove; //����� �̵� ���� ����
    public static bool doTutorial; //Ʃ�丮�� �Ϸ� ����
    public static bool isClicking; //Ŭ�� ����

    public static int moveCount; //������ Ƚ��
    public static int starCount; //������������ ȹ���� �� ����

    public static int currentKittyNumber; //������ ����� ĳ���� ��ȣ


    private void Awake()
    {
        Init();

        Time.timeScale = 1.0f;

        //���� ���õ� ����� ��ȣ ��������
        currentKittyNumber = PlayerPrefs.GetInt("Kitty", 1);
    }

    // Update is called once per frame
    void Update()
    {
        PlayStart();

        /*
        //������ ���尪 �ʱ�ȭ
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            PlayerPrefs.DeleteAll();
            GameManager.starCount = 0;
        }
        */

        //1�� ����� ĳ���� �⺻������ ����
        if (!PlayerPrefs.HasKey("Kitty_1"))
        {
            PlayerPrefs.SetString("Kitty_1", "true");
        }
    }

    void PlayStart()
    {
        //���� ������ ������ �Ͻ� ���� ����
        if (isStart == true)
        {
            Pause();
        }
    }

    void Pause()
    {
        if (isPaused == true)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public static void Init()
    {
        doTutorial = true;
        isStart = true;
        isPaused = false;
        isClear = false;
        canMove = false;
        isClicking = false;
        moveCount = 0;
    }
}
