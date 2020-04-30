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
            return new LevelData();
        }

    }

    public static void SaveOptionsData(MenuController mc)
    {

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/options.returninghome";
        FileStream stream = new FileStream(path, FileMode.Create);

        OptionsData data = new OptionsData(mc);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static OptionsData LoadOptionsData()
    {

        string path = Application.persistentDataPath + "/options.returninghome";
        if (File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            OptionsData data = formatter.Deserialize(stream) as OptionsData;
            stream.Close();

            return data;

        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return new OptionsData();
        }

    }

    public static void SaveNameData(MenuController mc)
    {

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/playername.returninghome";
        FileStream stream = new FileStream(path, FileMode.Create);

        NameData data = new NameData(mc);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static NameData LoadNameData()
    {

        string path = Application.persistentDataPath + "/playername.returninghome";
        if (File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            NameData data = formatter.Deserialize(stream) as NameData;
            stream.Close();

            return data;

        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return new NameData();
        }

    }

}
