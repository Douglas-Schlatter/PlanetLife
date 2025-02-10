using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EmojiData
{
    public string group;
    public Emoji[] emoji;
}

[System.Serializable]
public class Emoji
{
    public int[] baseUnicode;    // Códigos Unicode do emoji
    public string[] emoticons;   // Emoticons associados
    public string[] shortcodes;  // Shortcodes associados
    public bool animated;        // Se é um emoji animado
    public bool directional;     // Direcional
}
