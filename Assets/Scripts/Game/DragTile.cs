using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragTile : MonoBehaviour
{
    //�ٴ� empty Ÿ�� �迭
    private GameObject[] emptyTile;

    //Ÿ���� ���� ��ġ
    private Vector3 startPosition;

    //Ÿ���� ��ǥ ��ġ
    private Vector3 targetPosition;

    //Ÿ���� ���� ��ġ
    private Vector3 currentPosition; //�ڸ����� ��ġ
    private Vector3 nowPosition; //�ǽð� ��ġ

    //�巡�� ����
    private Vector3 dragPos;
    private Vector3 dist;

    //�巡�� ����ĳ����
    private RaycastHit hit; //Ŭ�� ��

    //�巡�� �� ���� �Ÿ�
    private float moveLimit = 5.0f;

    //�巡���� �� Ÿ�� �ø���
    private float offset = 1.0f;

    //��ġ ���� ���� Ȯ��
    private bool isSuccess = false;

    //Ÿ�� ���͸���
    public Material[] materials;


    private void Awake()
    {       
        emptyTile = GameObject.FindGameObjectsWithTag("EmptyTile");
        //Ÿ�� ���� ����
        gameObject.GetComponent<Renderer>().material = materials[0];
        //
        GameManager.isClicking = false;
        dist = Vector3.zero;

        //���� ��ġ ����
        startPosition = transform.position;
        currentPosition = startPosition;
        nowPosition = startPosition;
    }

    private void Update()
    {
        nowPosition = transform.position;
    }

    //�巡�� ����
    private void OnMouseDown()
    {
        GameManager.isClicking = true;

        //UI ������ Ŭ�� ����
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("UI ������ Ŭ��");
            return;
        }

        if (GameManager.isStart == false || GameManager.canMove == true || GameManager.isPaused == true)
        {
            Debug.Log($"isStart : {GameManager.isStart}, canMove : {GameManager.canMove}, isPaused : {GameManager.isPaused}");
            return;
        }

        isSuccess = true;

        //Ÿ�� ����� ���� ����
        gameObject.GetComponent<Renderer>().material = materials[1];
        transform.position = new Vector3(currentPosition.x, currentPosition.y + offset, currentPosition.z);
    }

    //�巡�� ��
    private void OnMouseDrag()
    {
        //UI ������ Ŭ�� ����
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (GameManager.isStart == false || GameManager.canMove == true || GameManager.isPaused == true)
            return;

        GameManager.isClicking = true;

        //���콺 ��ǥ�� Floor�� ray hit�� �� ��� ������Ʈ�� ��ǥ�� �̵�
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //���콺 ��ǥ ray
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Floor")))
        {

            //������ �������� �� �� �ε巯�� �巡��
            if (dist == Vector3.zero)
            {
                dist = hit.point - transform.position;
            }
            dragPos = hit.point - dist;


            //Ÿ���� �� �̵� ����
            dragPos.y = currentPosition.y + offset;

            //�����̴� ���� ���� ���밪���� ��ȯ
            float dragPosNumX = Mathf.Abs(currentPosition.x - dragPos.x);
            float dragPosNumZ = Mathf.Abs(currentPosition.z - dragPos.z);

            if (dragPosNumX >= dragPosNumZ)
            {
                dragPos.z = currentPosition.z;
            }
            if (dragPosNumZ > dragPosNumX)
            {
                dragPos.x = currentPosition.x;
            }


            //�յ��¿� �� ĭ���� ������ �� �ֵ��� ����
            if (dragPos.x > currentPosition.x + moveLimit || dragPos.x < currentPosition.x - moveLimit || dragPos.z > currentPosition.z + moveLimit || dragPos.z < currentPosition.z - moveLimit)
                return;

            //���� �Ÿ���ŭ ��������� �ڵ����� Ÿ���� �ڸ���� ��
            for (int i = 0; i < emptyTile.Length; i++)
            {
                float distance = Vector3.Distance(dragPos, emptyTile[i].transform.position);

                if (distance < 3.0f)
                {
                    targetPosition = new Vector3(emptyTile[i].transform.position.x, dragPos.y, emptyTile[i].transform.position.z);
                    transform.position = targetPosition;
                }
            }
        }
    }


    //�巡�� ��ħ
    private void OnMouseUp()
    {
        //UI ������ Ŭ�� ����
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (GameManager.isStart == false || GameManager.canMove == true || GameManager.isPaused == true)
            return;

        GameManager.isClicking = false;

        //Ÿ�� ����� ���� �������
        gameObject.GetComponent<Renderer>().material = materials[0];
        transform.position = new Vector3(nowPosition.x, nowPosition.y - offset, nowPosition.z);


        if (isSuccess == true)
        {
            if (GameManager.canMove == false && currentPosition != transform.position)
            {
                GameManager.moveCount += 1; //Ÿ�� ��ġ ���� Ƚ��
            }

            currentPosition = transform.position;
            startPosition = transform.position;
        }
        else
        {           
            transform.position = currentPosition;
            startPosition = transform.position;
            //����� ȿ����
            SoundManager.instance.PlayMeow();
        }
    }


    //Ÿ���� �̵� ���� ���� üũ
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Tile" || other.tag == "MovingTile" || other.tag == "StartTile" || other.tag == "EndTile") && startPosition != nowPosition)
        {
            isSuccess = false;
            Debug.Log("�浹");
        }
        else
        {
            isSuccess = true;
        }
    }
}
