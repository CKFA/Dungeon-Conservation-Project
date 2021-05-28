using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;

public class SerializationManager : MonoBehaviour
{
    public static bool Save(string saveName, object saveData)
    {
        BinaryFormatter formatter = GetBinaryFormatter();
        if (!Directory.Exists(Application.persistentDataPath + "/FYP/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/FYP/saves");
        }

        string path = Application.persistentDataPath + "/saves/" + saveName + ".save";

        FileStream file = File.Create(path);
        formatter.Serialize(file, saveData);

        file.Close();
        return true;
    }

    public static object Load(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }
        BinaryFormatter formatter = GetBinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogErrorFormat("Failed to Load file at {0}", path);
            file.Close();
            return null;
        }
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        SurrogateSelector selector = new SurrogateSelector();

        Vector3SerialisationSurrogate vector3Surrogate = new Vector3SerialisationSurrogate();
        QuaternionSerialisationSurrogate quaternionSurrogate = new QuaternionSerialisationSurrogate();

        selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3Surrogate);
        selector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), quaternionSurrogate);

        formatter.SurrogateSelector = selector;

        return formatter;
    }
}
