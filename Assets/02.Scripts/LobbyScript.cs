using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyScript : MonoBehaviour
{
    public Text gameTitleText; // 게임 제목을 표시할 Text 컴포넌트
    public Button gameStartButton; // Game Start 버튼
    public Button exitButton; // Exit 버튼

    private void Start()
    {
        // 게임 제목 설정
        gameTitleText.text = "Divine Madness";

        // Game Start 버튼에 클릭 이벤트 리스너 등록
        gameStartButton.onClick.AddListener(StartGame);

        // Exit 버튼에 클릭 이벤트 리스너 등록
        exitButton.onClick.AddListener(ExitGame);

        Cursor.lockState = CursorLockMode.None; // 커서 잠금 해제
        Cursor.visible = true; // 커서 보이기
    }

    private void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked; // 커서 잠금 설정
        Cursor.visible = false; // 커서 숨기기
        // LobbyScene에서 GameScene으로 전환
        SceneManager.LoadScene("GameScene");
        
    }

    private void ExitGame()
    {
        // 게임 종료
        Application.Quit();
    }
}
