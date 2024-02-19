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

    //����� ��Ʈ �� �˾�
    public GameObject heartPopup;
    private int heartCount;

    public Image[] hearts; //Ŭ���� UI ��Ʈ �̹���
    public Sprite heartSprite; //��Ʈ �̹���

    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //����� ��Ʈ �� ������ ����
        heartCount = PlayerPrefs.GetInt(stageNumber, 0);

        Debug.Log("���콺 �ö�");
        Debug.Log(heartCount);
        Debug.Log(stageNumber);

        //��ư�� Ȱ��ȭ�Ǿ� ���� ������(=Ŭ����� ���� ��ư)
        if (button.interactable == true && heartCount != 0)
        {
            heartPopup.SetActive(true);

            //����� ��Ʈ ���� ���� ǥ��
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
        Debug.Log("���콺 ����");
        heartPopup.SetActive(false);
    }
}
