using Board;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Util
{
    [CreateAssetMenu(fileName = "BoardParser", menuName = "Mega Chess/Board Parser")]
    public class TextBoardParser : ScriptableObject
    {
        [SerializeField] private TextAsset TextFile;

        [SerializeField] private string OutputPath = "Assets/Data/BoardSettings";


        public void ParseData()
        {
            ScriptableObject.CreateInstance(typeof(BoardModel));
            string[] lines = TextFile.text.Split('\n');
            foreach(string line in lines)
            {
                foreach(string word in line.Split(' '))
                {

                }
            }
        }

        private void SaveAsset(BoardModel obj, string name = "test.asset")
        {
            string path = Path.Combine(OutputPath, name);
            AssetDatabase.CreateAsset(obj, path);
        }
    }
}
