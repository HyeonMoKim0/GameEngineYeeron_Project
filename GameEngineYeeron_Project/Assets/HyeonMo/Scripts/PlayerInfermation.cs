using System.Collections;
using UnityEngine;

public class PlayerInfermation : MonoBehaviour {
    [Header("�÷��̾� �⺻ ����")]
    [SerializeField] public static int health = 3;
    [SerializeField] public static int attackPower = 10;

    float invicibleTime = 2.0f;

    GameObject player;
    //Rigidbody rigidbody;
    Collider collider;

    void Start() {
        player = GameObject.FindWithTag("Player");
        //rigidbody = player.GetComponent<Rigidbody>();
        collider = player.GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            --health;
            if (health <= 0)
                Die();

            StartCoroutine(LongInvicible());
            Debug.Log("player �� �� �𿴱���");
        }
    }

    void Die() {
        gameObject.SetActive(false);
    }

    IEnumerator LongInvicible()
    {
        collider.enabled = false;
        yield return new WaitForSeconds(invicibleTime);

        collider.enabled = true;
    }
}