using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject finishCanvas;
    [SerializeField] private Button playButton;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button exitButton;

    [Header("Player Customization")]
    [SerializeField] private Toggle hatToggle;
    [SerializeField] private Toggle vestToggle;
    [SerializeField] private Toggle backpackToggle;
    [SerializeField] private GameObject hatObject;
    [SerializeField] private GameObject vestObject;
    [SerializeField] private GameObject backpackObject;

    [Header("Glass Monitoring")]
    [SerializeField] private GlassDisappear[] glassObjects;

    private bool isGameStarted = false;
    private bool isGameOver = false;
    private bool isFinished = false;
    private bool canCheckGlass = false;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    [SerializeField] private GameObject quizPanel;



    private void Start()
    {
        player.SetActive(false);
        mainMenuCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        finishCanvas.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        playButton.onClick.AddListener(StartGame);

        playAgainButton.onClick.RemoveAllListeners();
        playAgainButton.onClick.AddListener(RestartGame);

        exitButton.onClick.RemoveAllListeners();
        exitButton.onClick.AddListener(ReturnToMainMenu);

        // Set default toggle states
        hatToggle.isOn = true;
        vestToggle.isOn = true;
        backpackToggle.isOn = true;
    }

    public void StartGame()
    {
        initialPosition = player.transform.position;
        initialRotation = player.transform.rotation;
        isGameStarted = true;
        mainMenuCanvas.SetActive(false);
        player.SetActive(true);

        // Apply player customization
        hatObject.SetActive(hatToggle.isOn);
        vestObject.SetActive(vestToggle.isOn);
        backpackObject.SetActive(backpackToggle.isOn);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        StartCoroutine(EnableGlassCheckWithDelay());
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    private IEnumerator EnableGlassCheckWithDelay()
    {
        yield return new WaitForSeconds(0.5f); // Delay sebelum mulai cek kaca
        canCheckGlass = true;
    }

    private void Update()
    {
        if (isGameStarted && !isGameOver && !isFinished && canCheckGlass)
        {
            foreach (var glass in glassObjects)
            {
                if (glass != null && glass.IsBroken())
                {
                    TriggerGameOver();
                    break;
                }
            }
        }
    }

    private void TriggerGameOver()
    {
        isGameOver = true;
        StartCoroutine(DelayedGameOver());
    }

    private IEnumerator DelayedGameOver()
    {
        yield return new WaitForSeconds(3f);

        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayGameOverBGM();

        if (quizPanel != null)
            quizPanel.SetActive(false);

        gameOverCanvas.SetActive(true);
        player.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void TriggerFinish()
    {
        Debug.Log("Finish Triggered");

        if (isFinished || isGameOver) return;

        isFinished = true;
        StartCoroutine(DelayedFinish());
    }
    private IEnumerator DelayedFinish()
    {
        yield return new WaitForSeconds(0.6f); // Bisa ubah delay sesuai kebutuhan
        finishCanvas.SetActive(true);
        player.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void RestartGame()
    {
        // Reset status game seperti awal
        isGameStarted = true;
        isGameOver = false;
        isFinished = false;
        canCheckGlass = false;

        // Reset posisi dan status player
        player.SetActive(true);

        // Aktifkan ulang kostum
        hatObject.SetActive(hatToggle.isOn);
        vestObject.SetActive(vestToggle.isOn);
        backpackObject.SetActive(backpackToggle.isOn);

        // Reset UI
        gameOverCanvas.SetActive(false);
        finishCanvas.SetActive(false);
        mainMenuCanvas.SetActive(false);

        // Reset Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Mulai ulang pengecekan kaca
        StartCoroutine(EnableGlassCheckWithDelay());

        // (Opsional) reset posisi player jika perlu
        player.transform.position = initialPosition;
        player.transform.rotation = initialRotation;


    }

    public void ExitGame()
    {
        Application.Quit();
        AudioManager.Instance.StopBGM();
        Debug.Log("Quit Game"); 
    }
}
