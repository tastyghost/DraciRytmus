using UnityEngine;

[System.Serializable]
public class MapPathData
{
    public string pathName;

    public int fromLocationIndex;
    public int toLocationIndex;

    public RectTransform[] points;
}