using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Deep_Space
{
    public class Save_Load
    {
        public int LoadProgress;
        public void Saving(int levProgress)
        {
            using (StreamWriter saveFile = new StreamWriter("progress.txt"))
            {
                saveFile.Write(levProgress);
                saveFile.Close();
            }

        }
        public void Load()
        {
            using (StreamReader loadLevel = new StreamReader("progress.txt"))
            {
                string line;
                while ((line = loadLevel.ReadLine()) != null)
                {
                    LoadProgress = Convert.ToInt32(line);
                }
                loadLevel.Close();
            }


        }


    }
}
