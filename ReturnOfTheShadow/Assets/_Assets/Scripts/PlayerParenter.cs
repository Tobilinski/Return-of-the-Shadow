using System;
using UnityEngine;

public class PlayerParenter : MonoBehaviour
{
    private ColliderEventTrigger trigger;

    private void OnEnable()
    {
        trigger = GetComponent<ColliderEventTrigger>();
        trigger.onCollisionEnterPlayer.AddListener(Parenter);
        trigger.onCollisionExitPlayer.AddListener(DeParenter);
    }

    private void OnDisable()
    {
        trigger.onCollisionEnterPlayer.RemoveListener(Parenter);
        trigger.onCollisionExitPlayer.RemoveListener(Parenter);
    }
    public void Parenter(PlayerController player)
    {
        player.transform.parent = transform;
    }
    public void DeParenter(PlayerController player)
    {
        player.transform.parent = null;
    }
}
