using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragTile : MonoBehaviour
{
    //바닥 empty 타일 배열
    private GameObject[] emptyTile;

    //타일의 시작 위치
    private Vector3 startPosition;

    //타일의 목표 위치
    private Vector3 targetPosition;

    //타일의 현재 위치
    private Vector3 currentPosition; //자리잡은 위치
    private Vector3 nowPosition; //실시간 위치

    //드래그 보정
    private Vector3 dragPos;
    private Vector3 dist;

    //드래그 레이캐스팅
    private RaycastHit hit; //클릭 시

    //드래그 축 제한 거리
    private float moveLimit = 5.0f;

    //드래그할 때 타일 올리기
    private float offset = 1.0f;

    //위치 가능 여부 확인
    private bool isSuccess = false;

    //타일 머터리얼
    public Material[] materials;


    private void Awake()
    {       
        emptyTile = GameObject.FindGameObjectsWithTag("EmptyTile");
        //타일 원래 색상
        gameObject.GetComponent<Renderer>().material = materials[0];
        //
        GameManager.isClicking = false;
        dist = Vector3.zero;

        //시작 위치 저장
        startPosition = transform.position;
        currentPosition = startPosition;
        nowPosition = startPosition;
    }

    private void Update()
    {
        nowPosition = transform.position;
    }

    //드래그 시작
    private void OnMouseDown()
    {
        GameManager.isClicking = true;

        //UI 위에서 클릭 막기
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("UI 위에서 클릭");
            return;
        }

        if (GameManager.isStart == false || GameManager.canMove == true || GameManager.isPaused == true)
        {
            Debug.Log($"isStart : {GameManager.isStart}, canMove : {GameManager.canMove}, isPaused : {GameManager.isPaused}");
            return;
        }

        isSuccess = true;

        //타일 색상과 높이 변경
        gameObject.GetComponent<Renderer>().material = materials[1];
        transform.position = new Vector3(currentPosition.x, currentPosition.y + offset, currentPosition.z);
    }

    //드래그 중
    private void OnMouseDrag()
    {
        //UI 위에서 클릭 막기
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (GameManager.isStart == false || GameManager.canMove == true || GameManager.isPaused == true)
            return;

        GameManager.isClicking = true;

        //마우스 좌표가 Floor에 ray hit이 된 경우 오브젝트의 좌표를 이동
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //마우스 좌표 ray
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Floor")))
        {

            //보정값 조정으로 좀 더 부드러운 드래그
            if (dist == Vector3.zero)
            {
                dist = hit.point - transform.position;
            }
            dragPos = hit.point - dist;


            //타일의 축 이동 제한
            dragPos.y = currentPosition.y + offset;

            //움직이는 축의 값을 절대값으로 변환
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


            //앞뒤좌우 한 칸씩만 움직일 수 있도록 제한
            if (dragPos.x > currentPosition.x + moveLimit || dragPos.x < currentPosition.x - moveLimit || dragPos.z > currentPosition.z + moveLimit || dragPos.z < currentPosition.z - moveLimit)
                return;

            //일정 거리만큼 가까워지면 자동으로 타일이 자리잡게 함
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


    //드래그 마침
    private void OnMouseUp()
    {
        //UI 위에서 클릭 막기
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (GameManager.isStart == false || GameManager.canMove == true || GameManager.isPaused == true)
            return;

        GameManager.isClicking = false;

        //타일 색상과 높이 원래대로
        gameObject.GetComponent<Renderer>().material = materials[0];
        transform.position = new Vector3(nowPosition.x, nowPosition.y - offset, nowPosition.z);


        if (isSuccess == true)
        {
            if (GameManager.canMove == false && currentPosition != transform.position)
            {
                GameManager.moveCount += 1; //타일 위치 변경 횟수
            }

            currentPosition = transform.position;
            startPosition = transform.position;
        }
        else
        {           
            transform.position = currentPosition;
            startPosition = transform.position;
            //고양이 효과음
            SoundManager.instance.PlayMeow();
        }
    }


    //타일의 이동 가능 여부 체크
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Tile" || other.tag == "MovingTile" || other.tag == "StartTile" || other.tag == "EndTile") && startPosition != nowPosition)
        {
            isSuccess = false;
            Debug.Log("충돌");
        }
        else
        {
            isSuccess = true;
        }
    }
}
