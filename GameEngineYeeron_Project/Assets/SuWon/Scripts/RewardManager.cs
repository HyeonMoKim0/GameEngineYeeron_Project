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
	public NormalReward normalReward; // NormalReward 스크립트 참조

	// 보상 타입 열거형
	public enum RewardType
	{
		HealthIncrease,      // 체력 증가
		AttackIncrease,      // 공격력 증가
		DashCooldownReduce,  // 대시 쿨타임 감소
		DashStackIncrease,   // 대시 스택 증가
		SpeedIncrease,       // 이동 속도 증가
		HunterDash,          // 헌터 대시
		SamuraiDash          // 사무라이 대시
	}

	// 현재 선택된 보상들을 저장
	private RewardType[] currentRewards = new RewardType[3];

	private void Start()
	{
		// NormalReward 자동 찾기 (할당되지 않은 경우)
		if (normalReward == null)
		{
			normalReward = FindObjectOfType<NormalReward>();
		}

		// NormalReward가 없으면 에러 로그 출력
		if (normalReward == null)
		{
			Debug.LogError("NormalReward를 찾을 수 없습니다! NormalReward 컴포넌트가 씬에 있는지 확인하세요.");
			return;
		}

		// 보상 버튼 클릭 이벤트 등록
		reward1Button.onClick.AddListener(() => OnAcceptReward(0));
		reward2Button.onClick.AddListener(() => OnAcceptReward(1));
		reward3Button.onClick.AddListener(() => OnAcceptReward(2));

		// 보상 배치
		AssignRandomRewards();
	}

	void AssignRandomRewards()
	{
		// NormalReward가 없으면 기본 보상 목록 사용
		if (normalReward == null)
		{
			Debug.LogWarning("NormalReward가 할당되지 않았습니다. 기본 보상 목록을 사용합니다.");
			AssignBasicRewards();
			return;
		}

		// 업그레이드 가능한 보상만 필터링
		List<RewardType> availableRewardsList = new List<RewardType>();
		List<string> availableRewardNamesList = new List<string>();

		// 각 보상이 업그레이드 가능한지 확인
		if (normalReward.currentUpgrade[0] <= normalReward.maxUpgrade[0])
		{
			availableRewardsList.Add(RewardType.HealthIncrease);
			availableRewardNamesList.Add("체력 증가");
		}

		if (normalReward.currentUpgrade[1] <= normalReward.maxUpgrade[1])
		{
			availableRewardsList.Add(RewardType.AttackIncrease);
			availableRewardNamesList.Add("공격력 증가");
		}

		if (normalReward.currentUpgrade[2] <= normalReward.maxUpgrade[2])
		{
			availableRewardsList.Add(RewardType.DashCooldownReduce);
			availableRewardNamesList.Add("대시 쿨타임 감소");
		}

		if (normalReward.currentUpgrade[3] <= normalReward.maxUpgrade[3])
		{
			availableRewardsList.Add(RewardType.DashStackIncrease);
			availableRewardNamesList.Add("대시 스택 증가");
		}

		if (normalReward.currentUpgrade[4] <= normalReward.maxUpgrade[4])
		{
			availableRewardsList.Add(RewardType.SpeedIncrease);
			availableRewardNamesList.Add("이동 속도 증가");
		}

		// 헌터 대시와 사무라이 대시는 전직 시 획득 예정이므로 현재는 비활성화
		// availableRewardsList.Add(RewardType.HunterDash);
		// availableRewardNamesList.Add("헌터 대시");
		// availableRewardsList.Add(RewardType.SamuraiDash);
		// availableRewardNamesList.Add("사무라이 대시");

		// 리스트를 배열로 변환
		RewardType[] availableRewards = availableRewardsList.ToArray();
		string[] rewardNames = availableRewardNamesList.ToArray();

		// 사용 가능한 보상이 3개 미만이면 사용 가능한 모든 보상 사용
		if (availableRewards.Length < 3)
		{
			Debug.LogWarning($"사용 가능한 보상이 {availableRewards.Length}개입니다.");

			// 모든 기본 보상이 최대치에 도달한 경우를 대비해 최소한의 보상 제공
			if (availableRewards.Length == 0)
			{
				AssignBasicRewards();
				return;
			}
		}

		// 랜덤으로 보상 선택 (사용 가능한 보상 수에 맞춰)
		System.Random rng = new System.Random();
		int rewardsToSelect = Mathf.Min(3, availableRewards.Length);

		// 사용 가능한 인덱스 리스트 생성
		List<int> availableIndexes = new List<int>();
		for (int i = 0; i < availableRewards.Length; i++)
		{
			availableIndexes.Add(i);
		}

		// 랜덤하게 선택하고 리스트에서 제거 (중복 방지)
		for (int i = 0; i < rewardsToSelect; i++)
		{
			int randomListIndex = rng.Next(availableIndexes.Count);
			int selectedIndex = availableIndexes[randomListIndex];

			currentRewards[i] = availableRewards[selectedIndex];
			availableIndexes.RemoveAt(randomListIndex);
		}

		// UI 텍스트 업데이트
		for (int i = 0; i < 3; i++)
		{
			if (i < rewardsToSelect)
			{
				// 선택된 보상의 인덱스를 다시 찾기
				int rewardIndex = -1;
				for (int j = 0; j < availableRewards.Length; j++)
				{
					if (availableRewards[j] == currentRewards[i])
					{
						rewardIndex = j;
						break;
					}
				}

				// rewardIndex 유효성 검증 추가
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
				// 사용 가능한 보상이 부족한 경우 해당 슬롯 비활성화
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

	// 기본 보상 할당 (NormalReward가 없을 때 사용)
	void AssignBasicRewards()
	{
		RewardType[] basicRewards = {
			RewardType.HealthIncrease,
			RewardType.AttackIncrease,
			RewardType.SpeedIncrease
		};

		string[] basicRewardNames = {
			"체력 증가",
			"공격력 증가",
			"이동 속도 증가"
		};

		// 랜덤으로 3개 보상 선택 (중복 방지)
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

		// UI 텍스트 업데이트
		reward1Text.text = basicRewardNames[selectedIndexes[0]];
		reward2Text.text = basicRewardNames[selectedIndexes[1]];
		reward3Text.text = basicRewardNames[selectedIndexes[2]];

		// 모든 보상 오브젝트 활성화
		reward1.SetActive(true);
		reward2.SetActive(true);
		reward3.SetActive(true);
	}

	void OnAcceptReward(int rewardIndex)
	{
		Debug.Log($"보상 {rewardIndex + 1} 수락: {currentRewards[rewardIndex]}");

		// 실제 보상 효과 적용
		ApplyRewardEffect(currentRewards[rewardIndex]);

		// 보상 UI 비활성화
		GameObject[] rewardObjects = { reward1, reward2, reward3 };
		rewardObjects[rewardIndex].SetActive(false);

		// 보상 창 전체 비활성화
		rewardPanel.SetActive(false);
	}

	// 보상 효과 실제 적용 - NormalReward의 메서드들을 활용
	void ApplyRewardEffect(RewardType rewardType)
	{
		if (normalReward == null)
		{
			Debug.LogError("NormalReward가 할당되지 않았습니다!");
			return;
		}

		switch (rewardType)
		{
			case RewardType.HealthIncrease:
				normalReward.IncreaseHp();
				Debug.Log($"체력이 증가했습니다! 현재 체력: {PlayerInfermation.health}");
				break;

			case RewardType.AttackIncrease:
				normalReward.IncreaseAttackPower();
				Debug.Log($"공격력이 증가했습니다! 현재 공격력: {PlayerInfermation.attackPower}");
				break;

			case RewardType.DashCooldownReduce:
				normalReward.DecreaseDashDelay();
				Debug.Log($"대시 쿨타임이 감소했습니다! 현재 쿨타임: {PlayerDashInfermation.redashConstraintTime}");
				break;

			case RewardType.DashStackIncrease:
				normalReward.IncreaseDashStack();
				Debug.Log($"대시 스택이 증가했습니다! 현재 최대 스택: {PlayerDashInfermation.dashMaxStack}");
				break;

			case RewardType.SpeedIncrease:
				normalReward.IncreaseMoveSpeed();
				Debug.Log($"이동속도가 증가했습니다! 현재 이동속도: {PlayerMove.moveSpeed}");
				break;

			case RewardType.HunterDash:
				normalReward.GetHunterDash();
				Debug.Log("헌터 대시를 획득했습니다!");
				break;

			case RewardType.SamuraiDash:
				normalReward.GetSamuraiDash();
				Debug.Log("사무라이 대시를 획득했습니다!");
				break;
		}
	}

	// 보상 창 열기 (외부에서 호출)
	public void ShowRewardPanel()
	{
		AssignRandomRewards(); // 새로운 보상 생성
		rewardPanel.SetActive(true);
	}

	// 현재 업그레이드 상태 확인 (디버깅용)
	public void CheckUpgradeStatus()
	{
		if (normalReward != null)
		{
			Debug.Log("=== 현재 업그레이드 상태 ===");
			Debug.Log($"체력 업그레이드: {normalReward.currentUpgrade[0]}/{normalReward.maxUpgrade[0]}");
			Debug.Log($"공격력 업그레이드: {normalReward.currentUpgrade[1]}/{normalReward.maxUpgrade[1]}");
			Debug.Log($"대시 쿨타임 업그레이드: {normalReward.currentUpgrade[2]}/{normalReward.maxUpgrade[2]}");
			Debug.Log($"대시 스택 업그레이드: {normalReward.currentUpgrade[3]}/{normalReward.maxUpgrade[3]}");
			Debug.Log($"이동속도 업그레이드: {normalReward.currentUpgrade[4]}/{normalReward.maxUpgrade[4]}");
		}
	}
}