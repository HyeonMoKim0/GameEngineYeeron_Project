using System.Collections;
using UnityEngine;

public class PlayerInfermation : MonoBehaviour {

    [Header("플레이어 기본 정보")]
    public int health = 3;         //나중에 static으로 바꿀 것
    public int attackPower = 10;   //나중에 static으로 바꿀 것

    float invicibleTime = 2.0f;

    public static bool isHit = true;

    GameObject player;
    //Rigidbody rigidbody;
    Collider collider;

    Material material;
    Color color;

    private void Awake() {
        player = GameObject.FindWithTag("Player");
        //rigidbody = player.GetComponent<Rigidbody>();
        collider = player.GetComponent<Collider>();

        material = GetComponent<Material>();
        //color = GetComponent<Color>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && isHit)   //에너미랑 충돌하면 health 감소
        {
            --health;
            if (health <= 0)
                Die();

            StartCoroutine(LongInvicible());
            Debug.Log("player 피 잘 깎였구여");
            
        }
    }

    void Die() {
        gameObject.SetActive(false);
    }

    IEnumerator LongInvicible()
    {
        collider.enabled = false;
        isHit = false;
        Sprite();
        Debug.Log("SPrite도 됨");
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