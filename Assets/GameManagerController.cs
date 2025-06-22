using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    public static GameManagerController Instance;

    public GameObject[] gameManagers; // GameManager1-4
    public GameObject[] cubes; // Cube1-4

    private int currentActiveManager = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Aktifkan hanya GameManager1 di awal
        SetActiveGameManager(0);
    }

    public void SetActiveGameManager(int index)
    {
        // Nonaktifkan semua GameManager
        foreach (var manager in gameManagers)
        {
            manager.SetActive(false);
        }

        // Aktifkan GameManager yang dipilih
        if (index >= 0 && index < gameManagers.Length)
        {
            gameManagers[index].SetActive(true);
            currentActiveManager = index;
        }
    }

    public void PlayerSteppedOnCube(int cubeIndex)
    {
        // Jika player menginjak Cube1, aktifkan GameManager2, dst
        if (cubeIndex >= 0 && cubeIndex < cubes.Length)
        {
            int nextManagerIndex = cubeIndex + 1;
            if (nextManagerIndex < gameManagers.Length)
            {
                SetActiveGameManager(nextManagerIndex);
            }
        }
    }
}