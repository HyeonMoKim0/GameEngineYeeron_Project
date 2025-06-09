using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    public GameObject reward1;  // Reward1 오브젝트 연결
    public GameObject reward2;  // Reward2 오브젝트 연결
    public GameObject reward3;  // Reward3 오브젝트 연결

    public Button reward1Button;
    public Button reward2Button;
    public Button reward3Button;

    public GameObject rewardPanel; // 보상 창

    private void Start()
    {
        // 보상 버튼 클릭 이벤트 등록
        reward1Button.onClick.AddListener(() => OnAcceptReward(reward1));
        reward2Button.onClick.AddListener(() => OnAcceptReward(reward2));
        reward3Button.onClick.AddListener(() => OnAcceptReward(reward3));
    }

    void OnAcceptReward(GameObject reward)
    {
        Debug.Log($"{reward.name} 보상 수락!");

        // 예시: 보상 지급 코드 넣기
        GiveReward(reward);

        // 보상 UI 비활성화
        reward.SetActive(false);

        // 보상 창 전체 비활성화
        rewardPanel.SetActive(false);
    }

    void GiveReward(GameObject reward)
    {
        // 보상 지급 로직 작성
        // 예: 플레이어 아이템 추가, 점수 증가 등

        Debug.Log($"{reward.name} 보상 지급 완료.");
    }
}