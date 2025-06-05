using UnityEditor;

[CustomEditor(typeof(PlayerMove))]
public class PlayerMoveEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector(); //기본 Inspector 그리기

        EditorGUILayout.Space(); //줄 간격
        EditorGUILayout.LabelField("플레이어 이동 속도", EditorStyles.boldLabel); //제목

        PlayerMove.moveSpeed = EditorGUILayout.FloatField("이동속도", PlayerMove.moveSpeed);

        //base.OnInspectorGUI();
    }
}
