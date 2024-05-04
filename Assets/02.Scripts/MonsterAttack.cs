using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public Animator monsterAnimator;
    public Animator playerAnimator;
    public GameObject deathUI;
    public Button returnButton;

    private bool isGameoverScene = false;

    private void Start()
    {
        // 현재 씬이 GameOverScene인지 확인
        if (SceneManager.GetActiveScene().name == "GameoverScene")
        {
            isGameoverScene = true;
            Attack();
        }
    }

    void Update()
    {
        if (isGameoverScene)
            return;

        // 다른 입력 로직이 있다면 여기에 추가 가능
    }

    private void Attack()
    {
        monsterAnimator.SetTrigger("Attack");

        Invoke("PlayDieAnimation", 2f); // 2초 후에 플레이어 죽음 애니메이션 시작
    }

    private void PlayDieAnimation()
    {
        playerAnimator.SetTrigger("Die"); // 플레이어 Die 트리거 설정

        Invoke("ShowDeathUI", 0f); // 2초 후에 DeathUI 활성화
    }

    private void ShowDeathUI()
    {
        Time.timeScale = 0f; // 게임 멈춤
        deathUI.SetActive(true); // 사망 메시지 UI 활성화
        Cursor.lockState = CursorLockMode.None; // 커서 잠금 해제
        Cursor.visible = true; // 커서 보이기
        returnButton.onClick.AddListener(ReturnToLobby); // Lobby로 돌아가는 버튼에 클릭 이벤트 추가
    }

    private void ReturnToLobby()
    {
        deathUI.SetActive(false); // 사망 메시지 UI 비활성화
        Cursor.lockState = CursorLockMode.Locked; // 커서 잠금 설정
        Cursor.visible = false; // 커서 숨기기
        SceneManager.LoadScene("LobbyScene"); // Lobby로 씬 전환
    }
}
