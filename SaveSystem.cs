using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
   public static void SavePlayer(deathDisplay player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Path.Combine(Application.persistentDataPath, "player.fun");
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer(deathDisplay player)
    {
        string path = Path.Combine(Application.persistentDataPath, "player.fun");
        FileStream stream = new FileStream(path, FileMode.Open);
        if (File.Exists(path) && stream.Length > 0)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("File not found");
            BinaryFormatter formatter = new BinaryFormatter();
            PlayerData data = new PlayerData(player);
            formatter.Serialize(stream, data);
            stream.Close();
            return data;
        }
        
    }
}
