using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterReward : MonoBehaviour
{
    public GameObject playerObject;
    public HunterSkill hunterSkill;
    public HunterDash hunterDash;

    public int[] maxReward = new int[3] { 5, 3, 3 };
    public int[] currentReward = new int[3] { 2, 1, 1 };

    public void AutoBullet()
    {
        if(hunterSkill == null)
        {
            hunterSkill = playerObject.AddComponent<HunterSkill>();
        }
        else
        {
            if (!hunterSkill.enabled)
            {
                hunterSkill.enabled = true;
                Debug.Log("HunterDash ��ũ��Ʈ Ȱ��ȭ");
            }
        }
    }

    public void AddAutoBullet()
    {
        if (hunterSkill != null)
        {
            if (currentReward[0] <= maxReward[0])
            {
                hunterSkill.IncreaseAutoBulletCountReward();
                currentReward[0]++;
            }
            else
            {
                Debug.Log("���׷��̵� �ִ�ġ�� �����Ͽ����ϴ�");
            }
        }
        else
        {
            Debug.Log("���� �÷��̾ HunterSkill ��ũ��Ʈ�� �������� �ʽ��ϴ�");
        }

    }

    public void DecreaseAutoBulletDelay()
    {
        if (hunterSkill != null)
        {
            if (currentReward[1] <= maxReward[1])
            {
                hunterSkill.DecreaseAutoShootCooldownReward();
                currentReward[1]++;
            }
            else
            {
                Debug.Log("���׷��̵� �ִ�ġ�� �����Ͽ����ϴ�");
            }
        }
        else
        {
            Debug.Log("���� �÷��̾ HunterSkill ��ũ��Ʈ�� �������� �ʽ��ϴ�");
        }

    }

    public void IncreaseDashbullet()
    {
        if (hunterDash != null)
        {
            if (currentReward[2] <= maxReward[2])
            {
                hunterDash.UpgradeDashBulletsReward();
                currentReward[2]++;
            }
            else
            {
                Debug.Log("���׷��̵� �ִ�ġ�� �����Ͽ����ϴ�");
            }
        }
        else
        {
            Debug.Log("���� �÷��̾ HunterDash ��ũ��Ʈ�� �������� �ʽ��ϴ�");
        }
    }

}
