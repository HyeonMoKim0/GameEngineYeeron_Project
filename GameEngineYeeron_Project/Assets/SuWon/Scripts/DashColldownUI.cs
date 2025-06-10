using UnityEngine;
using UnityEngine.UI;

public class DashCooldownUI : MonoBehaviour
{
	public Image cooldownImage; // ��Ÿ���� ǥ���� �̹��� (���� UI)
	public float dashCooldown = 5f; // �뽬 ��Ÿ��
	private float currentCooldown = 0f; // ���� �뽬 ��Ÿ��

	private bool isDashReady = true; // �뽬 �غ� ����

	void Start()
	{
		currentCooldown = dashCooldown; // �ʱ� ��Ÿ�� �� ����
	}

	void Update()
	{
		if (!isDashReady) // �뽬�� �غ���� ���� ���
		{
			currentCooldown -= Time.deltaTime; // ��Ÿ�� ����
			cooldownImage.fillAmount = currentCooldown / dashCooldown; // ���� UI�� ��Ÿ�� ���� ����

			if (currentCooldown <= 0f)
			{
				isDashReady = true; // �뽬 �غ� �Ϸ�
				currentCooldown = dashCooldown; // ��Ÿ�� ����
			}
		}
	}

	// �뽬�� ����� �� ȣ��
	public void UseDash()
	{
		if (isDashReady) // �뽬�� �غ�Ǿ��� ���� �뽬 ���
		{
			isDashReady = false; // �뽬 ��� �� �غ� ���°� �ƴ�
		}
	}
}
