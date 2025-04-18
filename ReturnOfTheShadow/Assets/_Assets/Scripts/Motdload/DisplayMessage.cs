using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class DisplayMessage : MonoBehaviour
{
    [SerializeField] private Messages message;
    private TextMeshProUGUI Text;

    private List<String> MOfTheDaySet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();
        MOfTheDaySet = message.messages.ToList();
        int index = Random.Range(0, message.messages.Count);
        Text.text = MOfTheDaySet[index]; 
    }
}
