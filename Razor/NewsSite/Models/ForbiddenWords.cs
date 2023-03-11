using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Models
{
    public class ForbiddenWords
    {
        public List<string> wordList;
        public ForbiddenWords()
        {
            wordList = new List<string>
            {
                "bad",
                "mood",
                "kill",
                "death"
            };
        }
        public bool isThereAnyWord(string sentence)
        {
            for (int i = 0; i < wordList.Count; i++)
            {
                if (sentence.Contains(wordList[i]))
                {
                    return true;
                    
                }
            }
            return false;
            
        }
    }
}
