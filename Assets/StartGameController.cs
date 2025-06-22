using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGameController : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject quizPanel;

    public static bool IsMainMenu = true;

    private void Start()
    {
        // Pastikan panel aktif saat game dimulai
        AudioManager.Instance.PlayMainMenuBGM();
        IsMainMenu = true;
        startPanel.SetActive(true);
        quizPanel.SetActive(false);

        // Setup button listener
        startButton.onClick.AddListener(StartGame);

        // Jeda game saat di menu start
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        // Nonaktifkan panel
        startPanel.SetActive(false);
        quizPanel.SetActive(true);

        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayGameplayBGM();

        // Lanjutkan waktu game
        Time.timeScale = 1f;

        // Jika perlu pindah scene:
        // SceneManager.LoadScene("NamaSceneGameplay");
    }
}