using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    
    public static void SaveLevelData(PlayerConnectionObject pco)
    {

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/save.returninghome";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(pco);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static LevelData LoadLevelData()
    {

        string path = Application.persistentDataPath + "/save.returninghome";
        if (File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();

            return data;

        } else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }

    }

}
