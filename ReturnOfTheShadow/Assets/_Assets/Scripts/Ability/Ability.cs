using UnityEngine;
using System.Collections;
[System.Serializable]
public abstract class Ability
{
    public string abilityName;
    
    public abstract void Activate(PlayerController player, Vector2 target);
}

[System.Serializable]
public class TeleportAbility : Ability
{
    public float delay = 0.5f;
    public override void Activate(PlayerController player, Vector2 target)
    {
        player.transform.position = target;
    }
}
