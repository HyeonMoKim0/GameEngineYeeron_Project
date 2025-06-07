using System.Collections;
using UnityEngine;

public class PlayerInfermation : MonoBehaviour {
    private float invicibleTime = 2.0f;

    public static int health = 3;
    public static int attackPower = 10;

    public static bool isHit = true;

    GameObject player;
    //Rigidbody rigidbody;
    Collider collider;

    Material material;
    Color color;

    private void Awake() {
        player = GameObject.FindWithTag("Player");
        //rigidbody = player.GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

        material = GetComponent<Material>();
        //color = GetComponent<Color>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && isHit)   //¿¡³Ê¹Ì¶û Ãæµ¹ÇÏ¸é health °¨¼Ò
        {
            --health;
            if (health <= 0)
                Die();

            StartCoroutine(LongInvicible());
        }
    }

    void Die() { gameObject.SetActive(false); }

    IEnumerator LongInvicible()
    {
        collider.enabled = false;
        isHit = false;
        Sprite();
        Debug.Log("SPriteµµ µÊ");
        yield return new WaitForSeconds(invicibleTime);

        collider.enabled = true;
        isHit = true;
        Debug.Log("isHit°¡ ²¨Áü");
    }

    IEnumerator Sprite() {
        while (true) {
            material.color = new Color(color.a, color.g, color.b, 0.5f);

            yield return new WaitForSeconds(0.5f);

            material.color = new Color(color.a, color.g, color.b, 1f);

            if (isHit == false)
                break;
        }
    }
}