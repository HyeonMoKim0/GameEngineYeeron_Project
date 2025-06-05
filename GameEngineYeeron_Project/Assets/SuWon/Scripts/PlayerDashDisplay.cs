using UnityEngine;
using TMPro; // TextMeshPro ����� ���� �߰�

public class PlayerDashDisplay : MonoBehaviour
{
    public PlayerDash playerDash;  // PlayerDash ��ũ��Ʈ ����
    public TextMeshProUGUI dashCountText;  // ��� Ƚ�� ǥ�� UI �ؽ�Ʈ ����

    void Update()
    {
        // ��� Ƚ���� UI�� �ǽð����� �ݿ�
        if (playerDash != null && dashCountText != null)
        {
            dashCountText.text = "Dash: " + playerDash.dashCurrentStack.ToString();
        }
    }
}