using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class PlatformTextReader
{
    static PlatformTextReader tr = null;
    FileStream file = null;
    StreamReader streamReader = null;

    public PlatformTextReader()
    {
        file = File.OpenRead(Application.dataPath + "/Resources/SourceCode.rb");
        streamReader = new StreamReader(file, Encoding.UTF8, true, 128);
    }

    public static PlatformTextReader GetPlatformTextReader()
    {
        if (tr == null)
        {
            tr = new PlatformTextReader();
        }

        return tr;
    }

    public ArrayList GetLines(int lineCount)
    {
        ArrayList lines = new ArrayList();
        string line;
        int count = 0;

        while (true)
        {
            line = streamReader.ReadLine();

            if(line == null)
            {
                file.Seek(0, SeekOrigin.Begin);
            }
            else if(lineCount < count)
            {
                break;
            }

            lines.Add(line);
            count++;
        }

        return lines;
    }

    public void CloseFileStream()
    {
        streamReader.Close();
        file.Close();
    }
}
