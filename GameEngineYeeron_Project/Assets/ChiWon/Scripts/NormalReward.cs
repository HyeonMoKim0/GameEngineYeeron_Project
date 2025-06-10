using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalReward : MonoBehaviour
{
    public GameObject playerObject;
    public HunterDash hunterDash;
    public SamuraiDash samuraiDash;

    public int[] maxUpgrade =  new int[5] { 20, 30, 8, 5, 20 };
    public int[] currentUpgrade = new int[5] { 1, 1, 1, 1, 1 };

    public void IncreaseHp()
    {
        if (currentUpgrade[0] <= maxUpgrade[0])
        {
            PlayerInfermation.health += 1;
            currentUpgrade[0]++;
        }
        else
        {
            Debug.Log("업그레이드 최대치에 도달하였습니다");
        }
        
    }

    public void IncreaseAttackPower()
    {
        if (currentUpgrade[1] <= maxUpgrade[1])
        {
            PlayerInfermation.attackPower += 4;
            currentUpgrade[1]++;
        }
        else
        {
            Debug.Log("업그레이드 최대치에 도달하였습니다");
        }

    }

    public void DecreaseDashDelay()
    {
        if (currentUpgrade[2] <= maxUpgrade[2])
        {
            PlayerDashInfermation.redashConstraintTime -= 0.5f;
            currentUpgrade[2]++;
        }
        else
        {
            Debug.Log("업그레이드 최대치에 도달하였습니다");
        }
        
    }

    public void IncreaseDashStack()
    {
        if (currentUpgrade[3] <= maxUpgrade[3])
        {
            PlayerDashInfermation.dashMaxStack += 1;
            PlayerDashInfermation.dashCurrentStack += 1;
            currentUpgrade[3]++;
        }
        else
        {
            Debug.Log("업그레이드 최대치에 도달하였습니다");
        }
    }

    public void IncreaseMoveSpeed()
    {
        if (currentUpgrade[4] <= maxUpgrade[4])
        {
            PlayerMove.moveSpeed += 2.0f;
            currentUpgrade[4]++;
        }
        else
        {
            Debug.Log("업그레이드 최대치에 도달하였습니다");
        }
    }

    public void GetHunterDash()
    {
        if (hunterDash == null && samuraiDash == null)
        {
            hunterDash = playerObject.AddComponent<HunterDash>();
        }
        else
        {
            if(!hunterDash.enabled)
            {
                hunterDash.enabled = true;
                Debug.Log("HunterDash 스크립트 활성화");
            }
        }
    }

    public void GetSamuraiDash()
    {
        if (samuraiDash == null && hunterDash == null)
        {
            samuraiDash = playerObject.AddComponent<SamuraiDash>();
        }
        else
        {
            if (!samuraiDash.enabled)
            {
                samuraiDash.enabled = true;
                Debug.Log("SamuraiDash 스크립트 활성화");
            }
        }
    }

}
