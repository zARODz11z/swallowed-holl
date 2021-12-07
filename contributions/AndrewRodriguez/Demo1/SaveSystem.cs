using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
//Made by Andrew Rodriguez with the help of https://www.youtube.com/watch?v=XOjd_qU2Ido&ab_channel=Brackeys
public static class SaveSystem
{
    /*
     * This function is static so it can be called any time without an instance of a class and it takes in a PlayerStats object.
     * It utilizes unitys BinaryFormatter to write the PlayerStats to a persistent data path in binary which has a small footprint.
     */
    public static void SavePlayer(PlayerStats player)
    {
        Debug.Log("save player called");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.txt";
        FileStream stream = new FileStream(path, FileMode.Create);
        
        PlayerData data = new PlayerData(player);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    /*
    * This function is static so it can be called any time without an instance of a class.
    * It utilizes unitys BinaryFormatter to deserailize or convert the PlayerStats that is stored in a persistent file from binary
    * back to the data that the PlayerData class expects.
    */
    public static PlayerData LoadPlayerStats()
    {
        Debug.Log("load player called");
        string path = Application.persistentDataPath + "/player.txt";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}