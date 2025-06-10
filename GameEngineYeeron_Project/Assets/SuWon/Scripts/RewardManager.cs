using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class RewardManager : MonoBehaviour
{
	[Header("UI References")]
	public GameObject reward1;
	public GameObject reward2;
	public GameObject reward3;
	public Button reward1Button;
	public Button reward2Button;
	public Button reward3Button;
	public GameObject rewardPanel;
	public TMP_Text reward1Text;
	public TMP_Text reward2Text;
	public TMP_Text reward3Text;

	[Header("Reward System Reference")]
	public NormalReward normalReward; // NormalReward ��ũ��Ʈ ����

	// ���� Ÿ�� ������
	public enum RewardType
	{
		HealthIncrease,      // ü�� ����
		AttackIncrease,      // ���ݷ� ����
		DashCooldownReduce,  // ��� ��Ÿ�� ����
		DashStackIncrease,   // ��� ���� ����
		SpeedIncrease,       // �̵� �ӵ� ����
		HunterDash,          // ���� ���
		SamuraiDash          // �繫���� ���
	}

	// ���� ���õ� ������� ����
	private RewardType[] currentRewards = new RewardType[3];

	private void Start()
	{
		// NormalReward �ڵ� ã�� (�Ҵ���� ���� ���)
		if (normalReward == null)
		{
			normalReward = FindObjectOfType<NormalReward>();
		}

		// NormalReward�� ������ ���� �α� ���
		if (normalReward == null)
		{
			Debug.LogError("NormalReward�� ã�� �� �����ϴ�! NormalReward ������Ʈ�� ���� �ִ��� Ȯ���ϼ���.");
			return;
		}

		// ���� ��ư Ŭ�� �̺�Ʈ ���
		reward1Button.onClick.AddListener(() => OnAcceptReward(0));
		reward2Button.onClick.AddListener(() => OnAcceptReward(1));
		reward3Button.onClick.AddListener(() => OnAcceptReward(2));

		// ���� ��ġ
		AssignRandomRewards();
	}

	void AssignRandomRewards()
	{
		// NormalReward�� ������ �⺻ ���� ��� ���
		if (normalReward == null)
		{
			Debug.LogWarning("NormalReward�� �Ҵ���� �ʾҽ��ϴ�. �⺻ ���� ����� ����մϴ�.");
			AssignBasicRewards();
			return;
		}

		// ���׷��̵� ������ ���� ���͸�
		List<RewardType> availableRewardsList = new List<RewardType>();
		List<string> availableRewardNamesList = new List<string>();

		// �� ������ ���׷��̵� �������� Ȯ��
		if (normalReward.currentUpgrade[0] <= normalReward.maxUpgrade[0])
		{
			availableRewardsList.Add(RewardType.HealthIncrease);
			availableRewardNamesList.Add("ü�� ����");
		}

		if (normalReward.currentUpgrade[1] <= normalReward.maxUpgrade[1])
		{
			availableRewardsList.Add(RewardType.AttackIncrease);
			availableRewardNamesList.Add("���ݷ� ����");
		}

		if (normalReward.currentUpgrade[2] <= normalReward.maxUpgrade[2])
		{
			availableRewardsList.Add(RewardType.DashCooldownReduce);
			availableRewardNamesList.Add("��� ��Ÿ�� ����");
		}

		if (normalReward.currentUpgrade[3] <= normalReward.maxUpgrade[3])
		{
			availableRewardsList.Add(RewardType.DashStackIncrease);
			availableRewardNamesList.Add("��� ���� ����");
		}

		if (normalReward.currentUpgrade[4] <= normalReward.maxUpgrade[4])
		{
			availableRewardsList.Add(RewardType.SpeedIncrease);
			availableRewardNamesList.Add("�̵� �ӵ� ����");
		}

		// ���� ��ÿ� �繫���� ��ô� ���� �� ȹ�� �����̹Ƿ� ����� ��Ȱ��ȭ
		// availableRewardsList.Add(RewardType.HunterDash);
		// availableRewardNamesList.Add("���� ���");
		// availableRewardsList.Add(RewardType.SamuraiDash);
		// availableRewardNamesList.Add("�繫���� ���");

		// ����Ʈ�� �迭�� ��ȯ
		RewardType[] availableRewards = availableRewardsList.ToArray();
		string[] rewardNames = availableRewardNamesList.ToArray();

		// ��� ������ ������ 3�� �̸��̸� ��� ������ ��� ���� ���
		if (availableRewards.Length < 3)
		{
			Debug.LogWarning($"��� ������ ������ {availableRewards.Length}���Դϴ�.");

			// ��� �⺻ ������ �ִ�ġ�� ������ ��츦 ����� �ּ����� ���� ����
			if (availableRewards.Length == 0)
			{
				AssignBasicRewards();
				return;
			}
		}

		// �������� ���� ���� (��� ������ ���� ���� ����)
		System.Random rng = new System.Random();
		int rewardsToSelect = Mathf.Min(3, availableRewards.Length);

		// ��� ������ �ε��� ����Ʈ ����
		List<int> availableIndexes = new List<int>();
		for (int i = 0; i < availableRewards.Length; i++)
		{
			availableIndexes.Add(i);
		}

		// �����ϰ� �����ϰ� ����Ʈ���� ���� (�ߺ� ����)
		for (int i = 0; i < rewardsToSelect; i++)
		{
			int randomListIndex = rng.Next(availableIndexes.Count);
			int selectedIndex = availableIndexes[randomListIndex];

			currentRewards[i] = availableRewards[selectedIndex];
			availableIndexes.RemoveAt(randomListIndex);
		}

		// UI �ؽ�Ʈ ������Ʈ
		for (int i = 0; i < 3; i++)
		{
			if (i < rewardsToSelect)
			{
				// ���õ� ������ �ε����� �ٽ� ã��
				int rewardIndex = -1;
				for (int j = 0; j < availableRewards.Length; j++)
				{
					if (availableRewards[j] == currentRewards[i])
					{
						rewardIndex = j;
						break;
					}
				}

				// rewardIndex ��ȿ�� ���� �߰�
				if (rewardIndex >= 0 && rewardIndex < rewardNames.Length)
				{
					switch (i)
					{
						case 0:
							reward1Text.text = rewardNames[rewardIndex];
							reward1.SetActive(true);
							break;
						case 1:
							reward2Text.text = rewardNames[rewardIndex];
							reward2.SetActive(true);
							break;
						case 2:
							reward3Text.text = rewardNames[rewardIndex];
							reward3.SetActive(true);
							break;
					}
				}
			}
			else
			{
				// ��� ������ ������ ������ ��� �ش� ���� ��Ȱ��ȭ
				switch (i)
				{
					case 0:
						reward1.SetActive(false);
						break;
					case 1:
						reward2.SetActive(false);
						break;
					case 2:
						reward3.SetActive(false);
						break;
				}
			}
		}
	}

	// �⺻ ���� �Ҵ� (NormalReward�� ���� �� ���)
	void AssignBasicRewards()
	{
		RewardType[] basicRewards = {
			RewardType.HealthIncrease,
			RewardType.AttackIncrease,
			RewardType.SpeedIncrease
		};

		string[] basicRewardNames = {
			"ü�� ����",
			"���ݷ� ����",
			"�̵� �ӵ� ����"
		};

		// �������� 3�� ���� ���� (�ߺ� ����)
		System.Random rng = new System.Random();
		int[] selectedIndexes = new int[3];

		for (int i = 0; i < 3; i++)
		{
			int randomIndex;
			do
			{
				randomIndex = rng.Next(basicRewards.Length);
			}
			while (System.Array.Exists(selectedIndexes, element => element == randomIndex));

			selectedIndexes[i] = randomIndex;
			currentRewards[i] = basicRewards[randomIndex];
		}

		// UI �ؽ�Ʈ ������Ʈ
		reward1Text.text = basicRewardNames[selectedIndexes[0]];
		reward2Text.text = basicRewardNames[selectedIndexes[1]];
		reward3Text.text = basicRewardNames[selectedIndexes[2]];

		// ��� ���� ������Ʈ Ȱ��ȭ
		reward1.SetActive(true);
		reward2.SetActive(true);
		reward3.SetActive(true);
	}

	void OnAcceptReward(int rewardIndex)
	{
		Debug.Log($"���� {rewardIndex + 1} ����: {currentRewards[rewardIndex]}");

		// ���� ���� ȿ�� ����
		ApplyRewardEffect(currentRewards[rewardIndex]);

		// ���� UI ��Ȱ��ȭ
		GameObject[] rewardObjects = { reward1, reward2, reward3 };
		rewardObjects[rewardIndex].SetActive(false);

		// ���� â ��ü ��Ȱ��ȭ
		rewardPanel.SetActive(false);
	}

	// ���� ȿ�� ���� ���� - NormalReward�� �޼������ Ȱ��
	void ApplyRewardEffect(RewardType rewardType)
	{
		if (normalReward == null)
		{
			Debug.LogError("NormalReward�� �Ҵ���� �ʾҽ��ϴ�!");
			return;
		}

		switch (rewardType)
		{
			case RewardType.HealthIncrease:
				normalReward.IncreaseHp();
				Debug.Log($"ü���� �����߽��ϴ�! ���� ü��: {PlayerInfermation.health}");
				break;

			case RewardType.AttackIncrease:
				normalReward.IncreaseAttackPower();
				Debug.Log($"���ݷ��� �����߽��ϴ�! ���� ���ݷ�: {PlayerInfermation.attackPower}");
				break;

			case RewardType.DashCooldownReduce:
				normalReward.DecreaseDashDelay();
				Debug.Log($"��� ��Ÿ���� �����߽��ϴ�! ���� ��Ÿ��: {PlayerDashInfermation.redashConstraintTime}");
				break;

			case RewardType.DashStackIncrease:
				normalReward.IncreaseDashStack();
				Debug.Log($"��� ������ �����߽��ϴ�! ���� �ִ� ����: {PlayerDashInfermation.dashMaxStack}");
				break;

			case RewardType.SpeedIncrease:
				normalReward.IncreaseMoveSpeed();
				Debug.Log($"�̵��ӵ��� �����߽��ϴ�! ���� �̵��ӵ�: {PlayerMove.moveSpeed}");
				break;

			case RewardType.HunterDash:
				normalReward.GetHunterDash();
				Debug.Log("���� ��ø� ȹ���߽��ϴ�!");
				break;

			case RewardType.SamuraiDash:
				normalReward.GetSamuraiDash();
				Debug.Log("�繫���� ��ø� ȹ���߽��ϴ�!");
				break;
		}
	}

	// ���� â ���� (�ܺο��� ȣ��)
	public void ShowRewardPanel()
	{
		AssignRandomRewards(); // ���ο� ���� ����
		rewardPanel.SetActive(true);
	}

	// ���� ���׷��̵� ���� Ȯ�� (������)
	public void CheckUpgradeStatus()
	{
		if (normalReward != null)
		{
			Debug.Log("=== ���� ���׷��̵� ���� ===");
			Debug.Log($"ü�� ���׷��̵�: {normalReward.currentUpgrade[0]}/{normalReward.maxUpgrade[0]}");
			Debug.Log($"���ݷ� ���׷��̵�: {normalReward.currentUpgrade[1]}/{normalReward.maxUpgrade[1]}");
			Debug.Log($"��� ��Ÿ�� ���׷��̵�: {normalReward.currentUpgrade[2]}/{normalReward.maxUpgrade[2]}");
			Debug.Log($"��� ���� ���׷��̵�: {normalReward.currentUpgrade[3]}/{normalReward.maxUpgrade[3]}");
			Debug.Log($"�̵��ӵ� ���׷��̵�: {normalReward.currentUpgrade[4]}/{normalReward.maxUpgrade[4]}");
		}
	}
}