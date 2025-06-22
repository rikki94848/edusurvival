using UnityEngine;
using System.Collections;

public class CoroutineHelper : MonoBehaviour
{
    private static CoroutineHelper instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void RunCoroutine(IEnumerator coroutine)
    {
        if (instance == null)
        {
            GameObject obj = new GameObject("CoroutineHelper");
            instance = obj.AddComponent<CoroutineHelper>();
            DontDestroyOnLoad(obj);
        }

        instance.StartCoroutine(coroutine);
    }
}
