using UnityEditor;
using UnityEngine;

namespace Board
{
    [CustomEditor(typeof(BoardModel))]
    public class BoardModelInspector : Editor
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

namespace Util
{
    [CustomEditor(typeof(TextBoardParser))]
    public class TextBoardInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            TextBoardParser model = (TextBoardParser)target;
            if (GUILayout.Button("Parse"))
            {
                model.ParseData();
            }
        }
    }
}