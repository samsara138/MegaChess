using UnityEditor;
using UnityEngine;

namespace Board
{
    [CustomEditor(typeof(BoardModel))]
    public class BoardInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            BoardModel model = (BoardModel)target;
            if (GUILayout.Button("Initialize"))
            {
                model.InitData();
            }
        }
    }
}