using UnityEngine;

public class CubeTrigger : MonoBehaviour
{
    public int cubeIndex; // Set di Inspector (0 untuk Cube1, 1 untuk Cube2, dst)

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player menyentuh cube index: " + cubeIndex);
            GameManagerController.Instance.PlayerSteppedOnCube(cubeIndex);
        }
    }
}