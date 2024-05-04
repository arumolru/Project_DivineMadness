using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// ������̼� ����� ����ϱ� ���� �߰��ؾ� �ϴ� ���ӽ����̽�
using UnityEngine.AI;


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// ������̼� ����� ����ϱ� ���� �߰��ؾ� �ϴ� ���ӽ����̽�
using UnityEngine.AI;


public class MonsterCtrl : MonoBehaviour
{
    // ������ ���� ����
    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }


    // ������ ���� ����
    public State state = State.IDLE;
    // ���� �����Ÿ�
    public float traceDist = 10.0f;
    // ���� �����Ÿ�
    public float attackDist = 2.0f;
    // ������ ��� ����
    public bool isDie = false;


    // ������Ʈ�� ĳ�ø� ó���� ����
    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    private Animator anim;


    // Animator �Ķ������ �ؽð� ����
    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashPlayerDie = Animator.StringToHash("PlayerDie");
    private readonly int hashGameOver = Animator.StringToHash("GameOver");

    // ������ �̵� �ӵ��� �����ϴ� ����
    public float moveSpeed = 1.0f;

    // 심장 박동 소리를 조절할 오디오 소스
    public AudioSource heartBeatAudioSource;
    public AudioClip heartBeatSound;
    public float minDistance = 5.0f;
    public float maxDistance = 30.0f;
    public float minPitch = 1.0f;
    public float maxPitch = 2.0f;

    private Transform monsterTrasnform;

    // GameOver 관련 변수
    private bool isGameOver = false;

    void Start()
    {
        // ������ Transform �Ҵ�
        monsterTr = GetComponent<Transform>();


        // ���� ����� Player�� Transform �Ҵ�
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();


        // NavMeshAgent ������Ʈ �Ҵ�
        agent = GetComponent<NavMeshAgent>();


        // Animator 컴포넌트 할당
        anim = GetComponent<Animator>();

        // AudioSource 컴포넌트와 심장 박동 사운드 할당
        heartBeatAudioSource = gameObject.AddComponent<AudioSource>();
        heartBeatAudioSource.clip = heartBeatSound;
        heartBeatAudioSource.loop = true;
        heartBeatAudioSource.playOnAwake = false;



        // ������ ���¸� üũ�ϴ� �ڷ�ƾ �Լ� ȣ��
        StartCoroutine(CheckMonsterState());
        // ���¿� ���� ������ �ൿ�� �����ϴ� �ڷ�ƾ �Լ� ȣ��
        StartCoroutine(MonsterAction());
    }

    void Update()
    {
        // 몬스터의 이동 속도가 4 이상일 때 "walk" 애니메이션으로 전환
        if (moveSpeed >= 4.0f)
        {
            anim.SetBool("IsOverLord", true);
        }

        // 몬스터와의 거리에 따라 심박도 크기 조절
        if (!isGameOver && playerTr != null)
        {
            float distance = Vector3.Distance(playerTr.position, transform.position);

            if (distance <= maxDistance)
            {
                if (!heartBeatAudioSource.isPlaying)
                {
                    heartBeatAudioSource.Play();
                }
            }
            else
            {
                heartBeatAudioSource.Stop();
            }

            float pitchInterval = 5.0f;
            float calculatedPitch = Mathf.Lerp(minPitch, maxPitch, Mathf.InverseLerp(minDistance, maxDistance, distance));
            float distanceToNearestInterval = Mathf.Round(distance / pitchInterval) * pitchInterval;
            float adjustedPitch = Mathf.Lerp(maxPitch, minPitch, Mathf.InverseLerp(minDistance, maxDistance, distanceToNearestInterval));
            heartBeatAudioSource.pitch = adjustedPitch;
        }
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
        // NavMeshAgent�� �̵� �ӵ��� �Բ� ������Ʈ�մϴ�.
        if (agent != null)
        {
            agent.speed = moveSpeed;
        }
    }

    // ������ �̵� �ӵ��� ������Ű�� �޼���
    public void IncreaseSpeed(float multiplier)
    {
        moveSpeed *= multiplier;
        // NavMeshAgent�� �̵� �ӵ��� �Բ� ������Ʈ�մϴ�.
        if (agent != null)
        {
            agent.speed = moveSpeed;
        }
    }


    // ������ �������� ������ �ൿ ���¸� üũ
    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            // 0.3�� ���� ����(���)�ϴ� ���� ������� �޽��� ������ �纸
            yield return new WaitForSeconds(5.0f);


            // ���Ϳ� ���ΰ� ĳ���� ������ �Ÿ� ����
            float distance = Vector3.Distance(playerTr.position, monsterTr.position);


            // ���� �����Ÿ� ������ ���Դ��� Ȯ��
            if (distance <= attackDist)
            {
                state = State.ATTACK;
            }
            // ���� �����Ÿ� ������ ���Դ��� Ȯ��
            else if (distance <= traceDist)
            {
                state = State.TRACE;
            }
            else
            {
                state = State.IDLE;
            }
        }
    }


    // ������ ���¿� ���� ������ ������ ����
    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                // IDLE 상태
                case State.IDLE:
                    // 이동 정지
                    agent.isStopped = true;
                    break;

                // 추적 상태
                case State.TRACE:
                    // 추적 대상의 좌표로 이동 시작
                    agent.SetDestination(playerTr.position);
                    agent.isStopped = false;
                    // Animator의 IsTrace변수를 true로 설정
                    anim.SetBool("IsTrace", true);
                    break;

                // 공격 상태
                case State.ATTACK:
                    break;

                // 죽음 상태
                case State.DIE:
                    break;
            }
            yield return new WaitForSeconds(5.0f);
        }
    }


    void OnDrawGizmos()
    {
        // ���� �����Ÿ� ǥ��
        if (state == State.TRACE)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, traceDist);
        }
        // ���� �����Ÿ� ǥ��
        if (state == State.ATTACK)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDist);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PLAYER"))
        {
            // 플레이어와 충돌한 경우 heartbeat 사운드 정지
            heartBeatAudioSource.Stop();
        }
    }

    // 게임 종료시 HeartBeat 사운드 중지를 처리할 함수 추가
    public void GameOver()
    {
        isGameOver = true;
        heartBeatAudioSource.Stop();
    }

    void OnEnable()
    {
        monsterTrasnform = monsterTrasnform; // 현재 몬스터 오브젝트의 Transform 할당
    }
}
