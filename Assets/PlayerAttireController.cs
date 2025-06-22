using UnityEngine;

public class PlayerAttireController : MonoBehaviour
{
    [Header("Attire References")]
    public GameObject bag;
    public GameObject hat;
    public GameObject vest;
    // Tambahkan referensi lainnya sesuai kebutuhan

    public void ToggleBag(bool isActive)
    {
        if (bag != null) bag.SetActive(isActive);
    }

    public void ToggleHat(bool isActive)
    {
        if (hat != null) hat.SetActive(isActive);
    }

    public void ToggleVest(bool isActive)
    {
        if (vest != null) vest.SetActive(isActive);
    }
}