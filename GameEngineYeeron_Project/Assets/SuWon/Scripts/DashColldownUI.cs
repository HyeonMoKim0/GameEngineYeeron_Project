using UnityEngine;
using UnityEngine.UI;

public class DashCooldownUI : MonoBehaviour
{
	public Image cooldownImage; // 쿨타임을 표시할 이미지 (원형 UI)
	public float dashCooldown = 5f; // 대쉬 쿨타임
	private float currentCooldown = 0f; // 현재 대쉬 쿨타임

	private bool isDashReady = true; // 대쉬 준비 여부

	void Start()
	{
		currentCooldown = dashCooldown; // 초기 쿨타임 값 설정
	}

	void Update()
	{
		if (!isDashReady) // 대쉬가 준비되지 않은 경우
		{
			currentCooldown -= Time.deltaTime; // 쿨타임 감소
			cooldownImage.fillAmount = currentCooldown / dashCooldown; // 원형 UI에 쿨타임 비율 적용

			if (currentCooldown <= 0f)
			{
				isDashReady = true; // 대쉬 준비 완료
				currentCooldown = dashCooldown; // 쿨타임 리셋
			}
		}
	}

	// 대쉬를 사용할 때 호출
	public void UseDash()
	{
		if (isDashReady) // 대쉬가 준비되었을 때만 대쉬 사용
		{
			isDashReady = false; // 대쉬 사용 후 준비 상태가 아님
		}
	}
}
