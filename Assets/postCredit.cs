using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class postCredit : MonoBehaviour
{
    public Material currentSkybox;
    public Material newSkyBox;

    private int count = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(count==0)
            {
                RenderSettings.skybox = newSkyBox;
                count++;
                StartCoroutine(Wait());
            }
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        RenderSettings.skybox = currentSkybox;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
