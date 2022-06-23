using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using File = System.IO.File;

namespace ByteTyper
{
    public class GetText : MonoBehaviour
    {
        #region Fields
        
        private Dictionary<int, List<string>> parsedDictionary = new Dictionary<int, List<string>>();
        
        #endregion

        #region Properties


        #endregion

        #region Unity Methods

        void Awake()
        {
            
        }

        void Start()
        {

        }

        #endregion

        #region Public Methods

        public Dictionary<int, List<string>> ReadData()
        {
            string readFromFilePath = Application.streamingAssetsPath + "/Recall_Chat/" + "Chat" + ".txt";
            
            List<string> fileLines = File.ReadAllLines(readFromFilePath).ToList();

            var index = 0;
            foreach (var line in fileLines)
            {
                ParseLine(index, line);
                index++;
            }

            return parsedDictionary;
        }
       
        

        #endregion

        #region Private Methods
        
        private void ParseLine(int index ,string line)
        {
            string[] strValues = line.Split(' ');
            List<string> lineValue = new List<string>();

            var key = "";
            var newString = "";

            bool isFirst = true;
            
            foreach(string str in strValues)
            {
                if (isFirst)
                {
                    key = str;
                    isFirst = false;
                }
                else
                {
                    newString += str + " ";
                }
            }
            
            lineValue.Add(key);
            lineValue.Add(newString);
            
            parsedDictionary.Add(index, lineValue);
            
        }

        #endregion
    }
}