using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour, IInteractable
{
    [SerializeField] private float jumpForce;
    public void Interact(GameObject player)
    {
        player.GetComponent<PhysicsController>().AddForce(0, jumpForce);
    }
}
