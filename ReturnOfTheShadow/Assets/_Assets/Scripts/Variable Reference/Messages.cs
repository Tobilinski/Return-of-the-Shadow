using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "VariableReference", menuName = "VariableReference/Messages")]
public class Messages : SerializedScriptableObject
{
    public HashSet<String> messages;
}
