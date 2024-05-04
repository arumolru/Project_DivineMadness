using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject optionUI;
    public Slider brightnessSlider;
    public Slider soundSlider;
    public Button closeButton; // Close 버튼 추가

    private bool isPaused = false;
    private bool isOptionOpen = false;

    void Start()
    {
        

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                if (isOptionOpen)
                {
                    CloseOptions();
                }
                else
                {
                    ResumeGame();
                }
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;
    }

    public void PauseGame()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;
    }

    public void GoToLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void OpenOptions()
    {
        optionUI.SetActive(true);
        isOptionOpen = true;
    }

    public void CloseOptions()
    {
        if (optionUI != null) // OptionUI가 파괴되지 않았는지 확인
        {
            optionUI.SetActive(false);
            isOptionOpen = false;
        }
    }

    public void AdjustBrightness()
    {
        float brightnessValue = brightnessSlider.value;
        // 밝기 조절 로직 구현
    }

    public void AdjustSound()
    {
        float soundValue = soundSlider.value;
        // 사운드 조절 로직 구현
    }

    private void OnDisable()
    {
        if (isOptionOpen)
        {
            CloseOptions();
        }
    }
}
