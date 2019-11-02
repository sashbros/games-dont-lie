using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameHandler : MonoBehaviour
{
    public int waitSeconds;

    void Start()
    {
        StartCoroutine(WaitFor());
    }

    IEnumerator WaitFor()
    {
        yield return new WaitForSeconds(waitSeconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
