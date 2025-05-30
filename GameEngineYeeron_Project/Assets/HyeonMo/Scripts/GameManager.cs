using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject player;
    public Collider playerCollider;
    public Rigidbody playerRigidbody;

    void Awake(){
        player = GameObject.FindWithTag("Player");
        playerCollider = player.GetComponent<Collider>();
        playerRigidbody = player.GetComponent<Rigidbody>();
    }

}
