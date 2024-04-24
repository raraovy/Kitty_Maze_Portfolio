using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class KittyMove : MonoBehaviour
{
    //애니메이션
    private Animator animator;

    //목적지
    [SerializeField]
    private Vector3 targetPosition;

    //내비메시
    private NavMeshAgent agent;
    [SerializeField]
    private NavMeshSurface surface;
    private NavMeshPath path;

    //타이머
    [SerializeField]
    private float countdown = 0f;
    private float moveTimer = 15.0f;

    //고양이 말풍선
    [SerializeField]
    private GameObject kittyBubble;


    private void Start()
    {
        kittyBubble = GameObject.Find("KittyBubble").transform.GetChild(0).gameObject;
        surface = GameObject.FindWithTag("Navmesh").GetComponent<NavMeshSurface>();
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        animator = GetComponentInChildren<Animator>();

        //목적지 설정하기
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
        if (countdown >= moveTimer) //15초 동안 타일을 움직이지 않을 경우
        {
            //스트레칭 애니메이션
            animator.SetTrigger("Sterch");
            //고양이 효과음
            SoundManager.instance.PlayMeow();
            //고양이 말풍선
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

            //경로를 찾을 수 없을 때
            if (path.status == NavMeshPathStatus.PathPartial || path.status == NavMeshPathStatus.PathInvalid)
            {
                GameManager.canMove = false;
                agent.ResetPath();
                yield return null;
            }
            //적합한 경로가 있을 때
            else if (path.status == NavMeshPathStatus.PathComplete && GameManager.isClicking == false)
            {
                GameManager.canMove = true;
                yield return new WaitForSeconds(1.0f);
                agent.ResetPath();
                agent.CalculatePath(targetPosition, path);
                if (path.status == NavMeshPathStatus.PathComplete)
                {
                    //목적지로 고양이 이동
                    agent.SetDestination(targetPosition);
                    //애니메이션
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
