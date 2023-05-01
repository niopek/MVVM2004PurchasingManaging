using System;
using System.Collections.Generic;

namespace MVVM2004PurchasingManaging.Utils;

public static class Util
{
    public static List<int> FindIndeksFromText(string text)
    {
        List<int> list = new();

        string tempString = "";

        foreach (char c in text)
        {
            if (Char.IsDigit(c))
            {
                tempString += c;

            }
            else
            {
                if(tempString.Length > 0)
                {
                    list.Add(int.Parse(tempString));
                }
                tempString = "";
            }

            
        }
        if(tempString.Length > 0)
        {
            list.Add(int.Parse(tempString));
        }
        return list;
    }

    public static bool IsTextBoxEmpty(string text)
    {
        if (text == null || text == "")
            return true;

        return false;
    }
}
