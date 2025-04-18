using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class MotdLoader : MonoBehaviour
{
    [SerializeField] private Motd motd;
    [SerializeField] private Messages messageList;
    void Start()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("motd");
        if (jsonFile != null)
        {
            motd = JsonUtility.FromJson<Motd>(jsonFile.text);
            messageList.messages = motd.Messages;
        }
    }
}

[Serializable]
public class Motd
{
    [OdinSerialize] private HashSet<string> messages;

    public HashSet<string> Messages
    {
        get => messages;
        set => messages = value;
    }
}