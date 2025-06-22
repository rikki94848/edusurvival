using UnityEngine;
using System.Collections;

public class GlassDisappear : MonoBehaviour
{
    [Header("Glass Settings")]
    [SerializeField] private GameObject brokenGlass; // Parent pecahan kaca
    [SerializeField] private AudioClip glassBreakSound;
    [SerializeField] private AudioClip playerScreamSound;

    private Vector3[] initialPositions;
    private Quaternion[] initialRotations;

    private void Start()
    {
        if (brokenGlass != null)
        {
            brokenGlass.SetActive(false);

            // Simpan posisi awal pecahan kaca
            Rigidbody[] pieces = brokenGlass.GetComponentsInChildren<Rigidbody>();
            initialPositions = new Vector3[pieces.Length];
            initialRotations = new Quaternion[pieces.Length];
            for (int i = 0; i < pieces.Length; i++)
            {
                initialPositions[i] = pieces[i].transform.localPosition;
                initialRotations[i] = pieces[i].transform.localRotation;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BreakGlass();
            PlayScreamSound(other.transform.position);
            CoroutineHelper.RunCoroutine(ResetGlassAfterDelay(3f)); // Gunakan helper
        }
    }

    public bool IsBroken()
    {
        return brokenGlass != null && brokenGlass.activeSelf;
    }

    private void BreakGlass()
    {
        gameObject.SetActive(false);

        if (brokenGlass != null)
        {
            brokenGlass.SetActive(true);
            foreach (Rigidbody piece in brokenGlass.GetComponentsInChildren<Rigidbody>())
            {
                piece.velocity = Vector3.zero;
                piece.angularVelocity = Vector3.zero;
                piece.AddExplosionForce(3f, transform.position, 2f);
            }
        }

        if (glassBreakSound != null)
            AudioSource.PlayClipAtPoint(glassBreakSound, transform.position);
    }

    private void PlayScreamSound(Vector3 position)
    {
        if (playerScreamSound != null)
            AudioSource.PlayClipAtPoint(playerScreamSound, position);
    }

    private IEnumerator ResetGlassAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (brokenGlass != null)
        {
            Rigidbody[] pieces = brokenGlass.GetComponentsInChildren<Rigidbody>();
            for (int i = 0; i < pieces.Length; i++)
            {
                pieces[i].velocity = Vector3.zero;
                pieces[i].angularVelocity = Vector3.zero;
                pieces[i].transform.localPosition = initialPositions[i];
                pieces[i].transform.localRotation = initialRotations[i];
            }

            brokenGlass.SetActive(false);
        }

        gameObject.SetActive(true);
    }
}
