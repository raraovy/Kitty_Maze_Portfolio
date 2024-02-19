using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed = 2000.0f; //��ũ�� ����
    [SerializeField]
    private float moveSpeed = 20.0f; //�̵� �ӵ�
    [SerializeField]
    private float rotateSpeed = 300.0f; //ȸ�� �ӵ�

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float originalView;

    private void Start()
    {
        originalPosition = Camera.main.transform.position;
        originalRotation = Camera.main.transform.rotation;
        originalView = Camera.main.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        Zoom();
        Move();
        Rotate();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(Camera.main.transform.position);
            Initialize();
        }
    }


    void Zoom()
    {
        //ȭ�� ����, �ܾƿ�(���콺 ��ũ��)
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.fieldOfView -= scrollWheel * Time.deltaTime * scrollSpeed;
    }

    void Move()
    {
        //ī�޶� �����¿� �̵�(Ű���� WSAD)
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Camera.main.transform.Translate(moveX * Time.deltaTime * moveSpeed, 0, 0);
        Camera.main.transform.Translate(0, moveY * Time.deltaTime * moveSpeed, 0);
    }

    void Rotate()
    {
        //ī�޶� ȸ��(���콺 ��Ŭ��)
        if (Input.GetMouseButton(1))
        {
            Vector3 rot = Camera.main.transform.rotation.eulerAngles;
            rot.x -= Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;
            rot.y += Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;
            Quaternion qRot = Quaternion.Euler(rot);
            Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, qRot, 2f);
        }
    }

    void Initialize()
    {
        Camera.main.transform.position = originalPosition;
        Camera.main.transform.rotation = originalRotation;
        Camera.main.fieldOfView = originalView;
    }
}
