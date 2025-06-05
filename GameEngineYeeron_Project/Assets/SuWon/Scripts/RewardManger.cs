using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    public GameObject reward1;  // Reward1 ������Ʈ ����
    public GameObject reward2;  // Reward2 ������Ʈ ����
    public GameObject reward3;  // Reward3 ������Ʈ ����

    public Button reward1Button;
    public Button reward2Button;
    public Button reward3Button;

    public GameObject rewardPanel; // ���� â

    private void Start()
    {
        // ���� ��ư Ŭ�� �̺�Ʈ ���
        reward1Button.onClick.AddListener(() => OnAcceptReward(reward1));
        reward2Button.onClick.AddListener(() => OnAcceptReward(reward2));
        reward3Button.onClick.AddListener(() => OnAcceptReward(reward3));
    }

    void OnAcceptReward(GameObject reward)
    {
        Debug.Log($"{reward.name} ���� ����!");

        // ����: ���� ���� �ڵ� �ֱ�
        GiveReward(reward);

        // ���� UI ��Ȱ��ȭ
        reward.SetActive(false);

        // ���� â ��ü ��Ȱ��ȭ
        rewardPanel.SetActive(false);
    }

    void GiveReward(GameObject reward)
    {
        // ���� ���� ���� �ۼ�
        // ��: �÷��̾� ������ �߰�, ���� ���� ��

        Debug.Log($"{reward.name} ���� ���� �Ϸ�.");
    }
}