using UnityEngine;

[System.Serializable]
public class LocationData
{
    public string locationName;
    public Sprite background;

    [TextArea]
    public string introText;

    public Sprite introImage;

    public Sprite companion;
}