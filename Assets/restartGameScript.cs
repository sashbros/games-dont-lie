using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class restartGameScript : MonoBehaviour
{
    [SerializeField]
    public GameObject blinkImage;
    public GameObject playerPos;

    public void RestartGame()
    {
        AdManager.instance.ShowRewardedAd();
        SceneManager.LoadScene("Level00");
    }
    public void Quit()
    {
        AdManager.instance.ShowRewardedAd();
        Application.Quit();
    }
    
    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void HomeButton()
    {
        AdManager.instance.ShowAd();
        SceneManager.LoadScene("Level15");
    }
    public void ScreenshotButton()
    {
        
        StartCoroutine("Capture");
        AdManager.instance.ShowAd();
    }
    IEnumerator Capture()
    {
        string timesStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string fileName = "Screenshot" + timesStamp + ".png";
        string pathToSave = fileName;
        ScreenCapture.CaptureScreenshot(pathToSave);
        yield return new WaitForEndOfFrame();
        GameObject blinkShot = Instantiate(blinkImage, playerPos.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.25f);
        Destroy(blinkShot);
    }
}
