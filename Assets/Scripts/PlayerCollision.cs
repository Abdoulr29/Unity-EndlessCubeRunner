using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement playerMovement;
    void OnCollisionEnter(Collision other)
    {

        if (other.collider.tag == "Obstacle")
        {
            Debug.Log("collision" + other.collider.name);
            playerMovement.enabled = false;
            FindObjectOfType<GameManager>().EndGame();        }
    }

}
