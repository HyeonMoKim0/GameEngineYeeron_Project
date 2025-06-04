using UnityEditor;

[CustomEditor(typeof(PlayerInfermation))]
public class PlayerInfermationEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector(); //기본 Inspector 그리기

        EditorGUILayout.Space(); //줄 간격
        EditorGUILayout.LabelField("플레이어 기본 정보", EditorStyles.boldLabel); //제목

        PlayerInfermation.health = EditorGUILayout.IntField("체력(HP)", PlayerInfermation.health);

        PlayerInfermation.attackPower = EditorGUILayout.IntField("공격력", PlayerInfermation.attackPower);

        //base.OnInspectorGUI();
    }
}
