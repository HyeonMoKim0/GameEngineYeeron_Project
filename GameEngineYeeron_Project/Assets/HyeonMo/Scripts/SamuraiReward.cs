//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public enum AbilityLevel {
    level_0, level_1,
    level_2, level_3,
    level_4, level_5
}

public class SamuraiReward : MonoBehaviour {
    public GameObject[] swords = new GameObject[3]; //검을 활성화시키기 위함
    SamuraiDash samuraiDash;
    Flooring flooring;

    int index = 0; //반복문 사용용

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
            index = (int)(rotateSwordLevel); //sword가 3개인데, 0~2번째의 자식의 active를 true로하기 위함
            swords[++index].SetActive(true);
        }
        else Debug.Log("이미 만렙인데 왜 그럼? 이건 검 숫자");
    }

    public void RewardSwordRotationSpeed() {
        if (swordRotationSpeedLevel < AbilityLevel.level_5) {
            ++swordRotationSpeedLevel;
            samuraiDash.swordRotationScale += 0.2f;
        }
        else Debug.Log("이미 만렙인데 왜 그럼? 이건 회전속도조정");
    }

    public void RewardInstallFlooring() {
        if (samuraiDash.canInstallFlooring == false) { samuraiDash.canInstallFlooring = true; }
        if (installFlooringLevel < AbilityLevel.level_5) {
            flooring.destroyTime += 1f;
        }
    }
}