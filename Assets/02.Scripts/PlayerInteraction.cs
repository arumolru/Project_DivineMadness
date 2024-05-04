using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 2f; // 상호작용 범위
    public Transform attachPoint; // 횃불을 연결할 AttachPoint 오브젝트

    public GameObject greenTorchPrefab; // Green Torch 프리팹
    public GameObject blueTorchPrefab; // Blue Torch 프리팹
    public GameObject orangeTorchPrefab; // Orange Torch 프리팹
    public GameObject redTorchPrefab; // Red Torch 프리팹

    public GameObject greenCorePrefab; // Green Core 프리팹
    public GameObject blueCorePrefab; // Blue Core 프리팹
    public GameObject orangeCorePrefab; // Orange Core 프리팹
    public GameObject redCorePrefab; // Red Core 프리팹

    public GameObject monsterPrefab; // Monster 프리팹
    public GameObject monsterSpawnZone; // Monster 생성 위치
    public GameObject portalPrefab; // Portal 프리팹

    public Transform portalSpawnZone; // Portal 생성 위치

    public AudioClip footstepSound; // 발소리 사운드
    public AudioClip burningSound; // 불타는 소리 사운드
    public AudioClip GameStartSound; // 문이 닫히는 소리
    public AudioClip monsterRoarSound; // 몬스터 생성 소리
    public AudioClip firecoreSound; // core 상호작용 소리

    private List<MonsterCtrl> monsters = new List<MonsterCtrl>();

    private GameObject currentCore; // 현재 상호작용 중인 Core오브젝트
    private GameObject currentTorch; // 현재 상호작용 중인 횃불 오브젝트
    private bool isInteracting = false; // 상호작용 중인지 여부를 나타내는 변수

    private int monsterCount = 0; // 생성된 Monster의 수를 세는 변수
    private bool isCleared = false; // 클리어 여부를 나타내는 변수
    private bool isOriginalMonsterSpeedChanged = false; // 오리지널 MONSTER의 속도가 변경되었는지 확인하는 변수
    private bool isMonsterSpawned = false;

    private AudioSource footstepAudioSource; // 발소리 사운드
    private AudioSource burningAudioSource; // 불타는 소리 사운드
    private AudioSource GameStartAudioSource; // 문이 닫히는 소리
    private AudioSource monsterRoarAudioSource; // 몬스터 생성 소리
    private AudioSource firecoreAudioSource; // core 상호작용 소리

    private bool hasCollidedWithRound2 = false; // round2 오브젝트와 충돌한 여부를 나타내는 변수
    private bool hasInteractedWithCore = false;


    void Start()
    {
        // AudioSource 컴포넌트를 가져옴
        AudioSource[] audioSources = GetComponents<AudioSource>();
        footstepAudioSource = audioSources[0];
        burningAudioSource = audioSources[1];
        GameStartAudioSource = audioSources[2];
        monsterRoarAudioSource = audioSources[3];
        firecoreAudioSource = audioSources[4];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isInteracting)
            {
                CheckInteraction();
            }
        }
    }

    void CheckInteraction()
    {
        // 주변에 있는 모든 Collider 가져오기
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange); // 주변에 있는 모든 Collider 가져오기

        bool hasInteracted = false; // 상호작용 여부를 나타내는 변수

        foreach (Collider collider in colliders)
        {
            // 플레이어와 상호작용 가능한 태그를 가진 오브젝트 찾기
            if (collider.CompareTag("GREENTORCH"))
            {
                InteractWithGreenTorch(collider.gameObject);
                hasInteracted = true;
                break;
            }
            else if (collider.CompareTag("BLUETORCH"))
            {
                InteractWithBlueTorch(collider.gameObject);
                hasInteracted = true;
                break;
            }
            else if (collider.CompareTag("ORANGETORCH"))
            {
                InteractWithOrangeTorch(collider.gameObject);
                hasInteracted = true;
                break;
            }
            else if (collider.CompareTag("REDTORCH"))
            {
                InteractWithRedTorch(collider.gameObject);
                hasInteracted = true;
                break;
            }
        }
    }

    void InteractWithGreenTorch(GameObject greenTorch)
    {
        MoveTorchToAttachPoint(greenTorch);
        isInteracting = true;
        PlayBurningSound(); // 불타는 소리 재생
        Debug.Log("플레이어가 GreenTorch와 상호작용했습니다.");
    }

    void InteractWithBlueTorch(GameObject blueTorch)
    {
        MoveTorchToAttachPoint(blueTorch);
        isInteracting = true;
        PlayBurningSound(); // 불타는 소리 재생
        Debug.Log("플레이어가 BlueTorch와 상호작용했습니다.");
    }

    void InteractWithOrangeTorch(GameObject orangeTorch)
    {
        MoveTorchToAttachPoint(orangeTorch);
        isInteracting = true;
        PlayBurningSound(); // 불타는 소리 재생
        Debug.Log("플레이어가 OrangeTorch와 상호작용했습니다.");
    }

    void InteractWithRedTorch(GameObject redTorch)
    {
        MoveTorchToAttachPoint(redTorch);
        isInteracting = true;
        PlayBurningSound(); // 불타는 소리 재생
        Debug.Log("플레이어가 redTorch와 상호작용했습니다.");
    }

    void MoveTorchToAttachPoint(GameObject torch)
    {
        torch.transform.SetParent(attachPoint.transform);
        torch.transform.localPosition = Vector3.zero;
        torch.transform.localRotation = Quaternion.identity;

        currentTorch = torch;
    }

    void OnTriggerEnter(Collider other)
    {
        if (isInteracting && currentTorch.CompareTag("GREENTORCH") && other.CompareTag("GREENCORE"))
        {
            CreateMagicFire(greenCorePrefab, other.transform.position);
            CreateMonster();
        }
        else if (isInteracting && currentTorch.CompareTag("BLUETORCH") && other.CompareTag("BLUECORE"))
        {
            CreateMagicFire(blueCorePrefab, other.transform.position);
            CreateMonster();
        }
        else if (isInteracting && currentTorch.CompareTag("ORANGETORCH") && other.CompareTag("ORANGECORE"))
        {
            CreateMagicFire(orangeCorePrefab, other.transform.position);
            CreateMonster();
        }
        else if (isInteracting && currentTorch.CompareTag("REDTORCH") && other.CompareTag("REDCORE"))
        {
            CreateMagicFire(redCorePrefab, other.transform.position);
            CreateMonster();
        }

        else if (other.CompareTag("ROUND2") && !hasCollidedWithRound2)
        {
            CreateMonster();

            // DELWALL 태그를 가진 모든 오브젝트를 찾아서 삭제
            GameObject[] delWallObjects = GameObject.FindGameObjectsWithTag("DELWALL");
            foreach (GameObject delWall in delWallObjects)
            {
                Destroy(delWall);
            }

            // GameStart 사운드 재생
            if (footstepAudioSource != null && GameStartSound != null)
            {
                footstepAudioSource.PlayOneShot(GameStartSound);

                // 2초 뒤에 Roar 사운드를 재생
                Invoke("PlayRoarSound", 2.0f);
            }

            // round2 충돌 여부를 true로 설정하여 더 이상 재생하지 않도록 함
            hasCollidedWithRound2 = true;
        }

        else if (other.CompareTag("PORTAL"))
        {
            // 충돌한 오브젝트가 PORTAL 태그를 가진 오브젝트라면 Clear 씬으로 전환
            SceneManager.LoadScene("Stage2_Clear");
        }

        else if (other.CompareTag("MONSTER"))
        {
            // 현재 플레이어 위치를 저장하고 게임 오버 씬으로 전환
            PlayerPrefs.SetString("GameOverPosition", transform.position.ToString());
            SceneManager.LoadScene("Stage2_GameOver");
        }
    }

    void PlayFootstepSound()
    {
        if (footstepSound != null)
        {
            footstepAudioSource.PlayOneShot(footstepSound);
        }
    }

    void PlayBurningSound()
    {
        if (burningSound != null)
        {
            burningAudioSource.PlayOneShot(burningSound);
        }
    }

    void CreateMagicFire(GameObject corePrefab, Vector3 position)
    {
        // AttachPoint에 위치한 torch 오브젝트 삭제
        Destroy(currentTorch);

        // Core 위치에 Magic fire 생성
        GameObject magicFire = Instantiate(corePrefab, position, Quaternion.identity);
        Debug.Log("불이 생성되었습니다.");

        hasInteractedWithCore = true;
        firecoreAudioSource.Play();

        isInteracting = false;
    }

    void CreateMonster()
    {
        if (monsterPrefab != null) // 몬스터 프리팹이 할당되어 있는지 확인
        {
            // 5초 뒤에 몬스터를 생성하는 함수를 호출
            Invoke("SpawnMonster", 5.0f);
        }
        else
        {
            Debug.LogWarning("Monster prefab is not assigned!");
        }
    }

    // 5초 후에 실행될 함수
    void SpawnMonster()
    {
        GameObject newMonster = Instantiate(monsterPrefab, monsterSpawnZone.transform.position, monsterSpawnZone.transform.rotation);

        // MonsterCtrl 스크립트를 가져옴
        MonsterCtrl monsterCtrl = newMonster.GetComponent<MonsterCtrl>();

        monsterCount++;

        if (monsterCtrl != null)
        {
            // 생성된 Monster를 리스트에 추가
            monsters.Add(monsterCtrl);

            // 생성된 Monster의 수가 4개일 때 모든 몬스터의 속도를 4로 고정
            if (monsters.Count == 5 && !isOriginalMonsterSpeedChanged)
            {
                SetAllMonsterSpeedToFour();
                isOriginalMonsterSpeedChanged = true;
            }
        }

        // 4마리의 Monster가 생성되면 클리어 로그 출력
        if (monsterCount >= 5)
        {
            Debug.Log("클리어했습니다");

            // 5초 뒤에 Portal 오브젝트 생성 함수를 호출
            Invoke("CreatePortal", 5.0f);
        }
        else
        {
            Debug.Log("Monster가 생성되었습니다. (남은 수: " + (5 - monsterCount) + ")");
        }

        isMonsterSpawned = true; // 몬스터가 생성되었음을 표시
    }

    // MONSTER 태그를 가진 모든 오브젝트의 이동속도를 4로 고정시키는 함수
    void SetAllMonsterSpeedToFour()
    {
        foreach (MonsterCtrl monster in monsters)
        {
            monster.moveSpeed = 4f;
        }
    }

    void CreatePortal()
    {
        // Portal 오브젝트를 PortalSpawnZone에 생성
        Instantiate(portalPrefab, portalSpawnZone.position, portalSpawnZone.rotation);

        isCleared = true; // 클리어 상태로 변경
        Debug.Log("Portal이 생성되었습니다.");
    }
}



