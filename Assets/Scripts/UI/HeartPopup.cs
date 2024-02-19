using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeartPopup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;

    [SerializeField]
    private string stageNumber;

    //저장된 하트 수 팝업
    public GameObject heartPopup;
    private int heartCount;

    public Image[] hearts; //클리어 UI 하트 이미지
    public Sprite heartSprite; //하트 이미지

    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //저장된 하트 수 가지고 오기
        heartCount = PlayerPrefs.GetInt(stageNumber, 0);

        Debug.Log("마우스 올라감");
        Debug.Log(heartCount);
        Debug.Log(stageNumber);

        //버튼이 활성화되어 있을 때에만(=클리어된 레벨 버튼)
        if (button.interactable == true && heartCount != 0)
        {
            heartPopup.SetActive(true);

            //저장된 하트 수에 따라 표시
            if (heartCount == 1)
            {
                hearts[0].sprite = heartSprite;
            }
            else if (heartCount == 2)
            {
                hearts[0].sprite = heartSprite;
                hearts[1].sprite = heartSprite;
            }
            else if (heartCount == 3)
            {
                hearts[0].sprite = heartSprite;
                hearts[1].sprite = heartSprite;
                hearts[2].sprite = heartSprite;
            }
            heartPopup.transform.position = new Vector2(transform.position.x, transform.position.y + 50.0f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("마우스 나감");
        heartPopup.SetActive(false);
    }
}
