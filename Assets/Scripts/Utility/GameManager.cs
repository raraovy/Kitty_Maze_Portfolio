using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isStart; //게임 시작 여부
    public static bool isPaused; //일시정지 여부
    public static bool isClear; //게임 클리어 여부
    public static bool canMove; //고양이 이동 가능 여부
    public static bool doTutorial; //튜토리얼 완료 여부
    public static bool isClicking; //클릭 여부

    public static int moveCount; //움직임 횟수
    public static int starCount; //스테이지에서 획득한 별 개수

    public static int currentKittyNumber; //선택한 고양이 캐릭터 번호


    private void Awake()
    {
        Init();

        Time.timeScale = 1.0f;

        //현재 선택된 고양이 번호 가져오기
        currentKittyNumber = PlayerPrefs.GetInt("Kitty", 1);
    }

    // Update is called once per frame
    void Update()
    {
        PlayStart();

        /*
        //데이터 저장값 초기화
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            PlayerPrefs.DeleteAll();
            GameManager.starCount = 0;
        }
        */

        //1번 고양이 캐릭터 기본적으로 보유
        if (!PlayerPrefs.HasKey("Kitty_1"))
        {
            PlayerPrefs.SetString("Kitty_1", "true");
        }
    }

    void PlayStart()
    {
        //시작 상태일 때에만 일시 정지 가능
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
