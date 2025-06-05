using UnityEditor;

[CustomEditor(typeof(PlayerMove))]
public class PlayerMoveEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector(); //�⺻ Inspector �׸���

        EditorGUILayout.Space(); //�� ����
        EditorGUILayout.LabelField("�÷��̾� �̵� �ӵ�", EditorStyles.boldLabel); //����

        PlayerMove.moveSpeed = EditorGUILayout.FloatField("�̵��ӵ�", PlayerMove.moveSpeed);

        //base.OnInspectorGUI();
    }
}
