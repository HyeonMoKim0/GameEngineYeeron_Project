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
                Debug.Log("HunterDash 스크립트 활성화");
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
                Debug.Log("업그레이드 최대치에 도달하였습니다");
            }
        }
        else
        {
            Debug.Log("현재 플레이어에 HunterSkill 스크립트가 존재하지 않습니다");
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
                Debug.Log("업그레이드 최대치에 도달하였습니다");
            }
        }
        else
        {
            Debug.Log("현재 플레이어에 HunterSkill 스크립트가 존재하지 않습니다");
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
                Debug.Log("업그레이드 최대치에 도달하였습니다");
            }
        }
        else
        {
            Debug.Log("현재 플레이어에 HunterDash 스크립트가 존재하지 않습니다");
        }
    }

}
