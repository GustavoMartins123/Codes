using UnityEngine;
public static class Save
{
    public static void Save01(int book01, int book02, int book03, int book04, int book05, int book06, int book07, int book08, int book09, int book10, int book11, int book12, float mgc) 
    {
        PlayerPrefs.SetString("IdleSave", book01 + "|" + book02 + "|" + book03 + "|" + book04 + "|" + book05 + "|" + book06 + "|" + book07 + "|" + book08 + "|" + book09 + "|" + book10 + "|" + book11 + "|" + book12 + "|" + mgc);
    }

    public static string Load() 
    {
        string data = PlayerPrefs.GetString("IdleSave");
        return data;
    }
}
