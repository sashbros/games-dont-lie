using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int deathCountTotal;
    public string levelName;

    public PlayerData(deathDisplay player)
    {
        deathCountTotal = playerMovement.totalDeathCount;
        levelName = playerMovement.activeScene;
    }
}
