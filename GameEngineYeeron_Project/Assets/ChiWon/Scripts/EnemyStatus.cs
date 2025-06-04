using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public int maxHP = 20;
    public int currentHP = 20;
    public int adk = 1;
    public int expValue = 10;
    public float moveSpeed = 8.0f;
    
    public GameObject objectEXP;
    public Transform player;
    
    public void Start()
    {
        currentHP = maxHP;
    }

    public void Update()
    {
        MoveToPlayer();
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    //�÷��̾ ���Ϳ� �ѹ� �浹�ϸ� �÷��̾ ���Ͱ� ���ϴ� ����� ���� �������� �ڵ����� �̵��ϴ� ���װ� ����
    void MoveToPlayer() 
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void EnemyDamaged (int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            EnemyDie();
        }
    }

    void EnemyDie()
    {
        Destroy(gameObject);
        Instantiate(objectEXP, transform.position, Quaternion.identity);
    }
}

public class FirstEnemy : EnemyStatus
{
    new public void Start()
    {
        //base.Start();
        //maxHP = 100;
        //expValue = 10;
        //adk = 1;
    }
}



