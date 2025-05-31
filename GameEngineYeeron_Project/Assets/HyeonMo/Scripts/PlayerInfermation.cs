using System.Collections;
using UnityEngine;

public class PlayerInfermation : MonoBehaviour {

    [Header("�÷��̾� �⺻ ����")]
    public int health = 3;         //���߿� static���� �ٲ� ��
    public int attackPower = 10;   //���߿� static���� �ٲ� ��

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
        if (other.gameObject.tag == "Enemy" && isHit)   //���ʹ̶� �浹�ϸ� health ����
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
        isHit = false;
        Sprite();
        Debug.Log("SPrite�� ��");
        yield return new WaitForSeconds(invicibleTime);

        collider.enabled = true;
        isHit = true;
        Debug.Log("isHit�� ����");
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