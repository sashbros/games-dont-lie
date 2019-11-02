using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class deathDisplay : MonoBehaviour
{
    [HideInInspector]
    public int deathDisplayer;
    [HideInInspector]
    public string levelContName;

    // Start is called before the first frame update
    void Start()
    {
        LoadPlayer();
        deathDisplayer = playerMovement.totalDeathCount;
        if(GetComponent<Text>())
            GetComponent<Text>().text = "X " + deathDisplayer.ToString();
        /*if(levelContName=="")
        {
            GameObject.Find("Restart Button").GetComponentInChildren<Text>().color = Color.gray;
            GameObject.Find("Restart Button").GetComponent<Button>().enabled = false;
        }
        else
        {
            GameObject.Find("Restart Button").GetComponentInChildren<Text>().color = Color.white;
            GameObject.Find("Restart Button").GetComponent<Button>().enabled = true;
        }*/
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {        
        PlayerData data = SaveSystem.LoadPlayer(this);
        playerMovement.totalDeathCount = data.deathCountTotal;
        levelContName = data.levelName;
    }
    public void Continue()
    {
        SceneManager.LoadScene(levelContName);
    }
}
