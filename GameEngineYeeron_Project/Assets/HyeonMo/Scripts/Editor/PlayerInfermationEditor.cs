using UnityEditor;

[CustomEditor(typeof(PlayerInfermation))]
public class PlayerInfermationEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector(); //�⺻ Inspector �׸���

        EditorGUILayout.Space(); //�� ����
        EditorGUILayout.LabelField("�÷��̾� �⺻ ����", EditorStyles.boldLabel); //����

        PlayerInfermation.health = EditorGUILayout.IntField("ü��(HP)", PlayerInfermation.health);

        PlayerInfermation.attackPower = EditorGUILayout.IntField("���ݷ�", PlayerInfermation.attackPower);

        //base.OnInspectorGUI();
    }
}
