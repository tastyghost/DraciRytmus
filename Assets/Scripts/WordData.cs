[System.Serializable]
public class WordData
{
    public string word;
    public int syllables;
    public string topic;
    public string pictureName;
    public string audioName;
    public string syllablesAudioName;

    public WordData(
        string word,
        int syllables,
        string topic,
        string pictureName,
        string audioName,
        string syllablesAudioName)
    {
        this.word = word;
        this.syllables = syllables;
        this.topic = topic;
        this.pictureName = pictureName;
        this.audioName = audioName;
        this.syllablesAudioName = syllablesAudioName;
    }
}
