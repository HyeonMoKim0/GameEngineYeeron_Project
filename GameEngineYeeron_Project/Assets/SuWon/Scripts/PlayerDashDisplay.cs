using UnityEngine;
using TMPro;  // TextMeshPro ���ӽ����̽�

public class PlayerDashDisplay: MonoBehaviour
{
    public PlayerDashInfermation playerDashInfo;  // PlayerDashInfermation ��ũ��Ʈ ����
    public TextMeshProUGUI dashCountText;         // TMP UI �ؽ�Ʈ ����

    void Update()
    {
        // ��� Ƚ�� �ؽ�Ʈ ������Ʈ
        if (playerDashInfo != null && dashCountText != null)
        {
            dashCountText.text = $"Dashes: {PlayerDashInfermation.dashCurrentStack}";  //�̰� static���� ���� playerDashInfo.dashCurrentStack���� �ٲ�
        }
    }
}
