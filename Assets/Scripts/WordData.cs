[System.Serializable]
public class WordData
{
    public string word;
    public int syllables;
    public string topic;
    public string pictureName;

    public WordData(string word, int syllables, string topic, string pictureName)
    {
        this.word = word;
        this.syllables = syllables;
        this.topic = topic;
        this.pictureName = pictureName;
    }
}