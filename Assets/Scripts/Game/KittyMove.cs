using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class KittyMove : MonoBehaviour
{
    //�ִϸ��̼�
    private Animator animator;

    //������
    [SerializeField]
    private Vector3 targetPosition;

    //����޽�
    private NavMeshAgent agent;
    [SerializeField]
    private NavMeshSurface surface;
    private NavMeshPath path;

    //Ÿ�̸�
    [SerializeField]
    private float countdown = 0f;
    private float moveTimer = 15.0f;

    //����� ��ǳ��
    [SerializeField]
    private GameObject kittyBubble;


    private void Start()
    {
        kittyBubble = GameObject.Find("KittyBubble").transform.GetChild(0).gameObject;
        surface = GameObject.FindWithTag("Navmesh").GetComponent<NavMeshSurface>();
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        animator = GetComponentInChildren<Animator>();

        //������ �����ϱ�
        for (int i = 0; i < DataManager.Instance.KittyPositionData.positions.Length; i++)
        {
            targetPosition = DataManager.Instance.KittyPositionData.positions[SceneManager.GetActiveScene().buildIndex - 3].endPosition;
        }

        StartCoroutine(SearchWay());
    }

    private void Update()
    {
        if (GameManager.isStart == true && GameManager.isClear == false)
        {
            NotMoveEffect();
        }
    }


    private void NotMoveEffect()
    {
        countdown += Time.deltaTime;

        if (GameManager.isClicking == true)
        {
            countdown = 0f;
        }
        if (countdown >= moveTimer) //15�� ���� Ÿ���� �������� ���� ���
        {
            //��Ʈ��Ī �ִϸ��̼�
            animator.SetTrigger("Sterch");
            //����� ȿ����
            SoundManager.instance.PlayMeow();
            //����� ��ǳ��
            StartCoroutine(Bubble());

            countdown = 0f;
        }
    }

    IEnumerator SearchWay()
    {
        while (GameManager.canMove == false && GameManager.isClear == false)
        {
            NotMoveEffect();

            surface.BuildNavMesh();
            agent.CalculatePath(targetPosition, path);

            //��θ� ã�� �� ���� ��
            if (path.status == NavMeshPathStatus.PathPartial || path.status == NavMeshPathStatus.PathInvalid)
            {
                GameManager.canMove = false;
                agent.ResetPath();
                yield return null;
            }
            //������ ��ΰ� ���� ��
            else if (path.status == NavMeshPathStatus.PathComplete && GameManager.isClicking == false)
            {
                GameManager.canMove = true;
                yield return new WaitForSeconds(1.0f);
                agent.ResetPath();
                agent.CalculatePath(targetPosition, path);
                if (path.status == NavMeshPathStatus.PathComplete)
                {
                    //�������� ����� �̵�
                    agent.SetDestination(targetPosition);
                    //�ִϸ��̼�
                    animator.SetBool("IsGo", true);
                    StopAllCoroutines();
                }
                else
                {
                    GameManager.canMove = false;
                }
            }
        }
    }

    IEnumerator Bubble()
    {
        kittyBubble.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        kittyBubble.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Destination")
        {
            agent.ResetPath();
            animator.SetBool("IsGo", false);
        }
    }
}
