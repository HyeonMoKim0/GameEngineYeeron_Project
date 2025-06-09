//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public enum AbilityLevel {
    level_0, level_1,
    level_2, level_3,
    level_4, level_5
}

public class SamuraiReward : MonoBehaviour {
    public GameObject[] swords = new GameObject[3]; //���� Ȱ��ȭ��Ű�� ����
    SamuraiDash samuraiDash;
    Flooring flooring;

    int index = 0; //�ݺ��� ����

    [HideInInspector] public AbilityLevel enhanceAfterImageLevel = AbilityLevel.level_0;
    [HideInInspector] public AbilityLevel swordRotationSpeedLevel = AbilityLevel.level_0;
    [HideInInspector] public AbilityLevel rotateSwordLevel = AbilityLevel.level_0;
    [HideInInspector] public AbilityLevel installFlooringLevel = AbilityLevel.level_0;

    void Start() {
        if (samuraiDash == null) { GetComponent<SamuraiDash>(); }
        if (flooring == null) { GetComponent<Flooring>(); }
    }

    public void RewardRotateSword() {
        if (samuraiDash.canRotateSword == false) { samuraiDash.canRotateSword = true; }
        if (rotateSwordLevel < AbilityLevel.level_3) {
            index = (int)(rotateSwordLevel); //sword�� 3���ε�, 0~2��°�� �ڽ��� active�� true���ϱ� ����
            swords[++index].SetActive(true);
        }
        else Debug.Log("�̹� �����ε� �� �׷�? �̰� �� ����");
    }

    public void RewardSwordRotationSpeed() {
        if (swordRotationSpeedLevel < AbilityLevel.level_5) {
            ++swordRotationSpeedLevel;
            samuraiDash.swordRotationScale += 0.2f;
        }
        else Debug.Log("�̹� �����ε� �� �׷�? �̰� ȸ���ӵ�����");
    }

    public void RewardInstallFlooring() {
        if (samuraiDash.canInstallFlooring == false) { samuraiDash.canInstallFlooring = true; }
        if (installFlooringLevel < AbilityLevel.level_5) {
            flooring.destroyTime += 1f;
        }
    }
}