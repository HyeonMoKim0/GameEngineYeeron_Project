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

    void OnTriggerEnter(Collider other) { //자식이랑 부모가 각각 rigidbody가 있어야 따로 충돌을 인식함. 비효율적이나, 설계를 처음부터 잘 했어야 했음
        if (other.gameObject.tag == "Enemy" && isHit) //에너미랑 충돌하면 health 감소
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
        //Sprite();
        //Debug.Log("SPrite도 됨");
        yield return new WaitForSeconds(invicibleTime);

        collider.enabled = true;
        isHit = true;
        Debug.Log("isHit가 꺼짐");
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