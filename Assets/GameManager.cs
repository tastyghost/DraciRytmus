using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject mapPanel;
    public GameObject companionPanel;
    public Image companionImage;
    public Sprite currentCompanionSprite;
    public Image energyBubbleImage;
    public Sprite[] energySprites;

    private int energy = 0;
    public GameObject exercisePanel;
    public GameObject successPanel;
    public ParticleSystem confetti;
    private Transform bowl;
    private Transform[] bowlSpots;
    private bool[] bowlSpotTaken;
    private GameObject[] berriesInBowlSpots;

    public TMP_Text resultText;
    public Transform berryTray;

    public Transform[] berrySpots;
    public GameObject[] berriesInTray;

    public int correctSyllables = 1;
    private int berriesInBowl = 0;
    public Image lunaImage;
    private bool inputLocked = false;

    public Sprite lunaNormalSprite;
    public Sprite lunaThinkingSprite;
     public Image wordImage;

    private List<WordData> words = new List<WordData>();
    private WordData currentWord;


    public float feedbackDelay = 2f;

    public bool IsInputLocked()
   
{
    return inputLocked;
}


    void Awake()
    {
        bowl = GameObject.FindWithTag("Bowl").transform;

        bowlSpots = new Transform[bowl.childCount];
        bowlSpotTaken = new bool[bowl.childCount];
        berriesInBowlSpots = new GameObject[bowl.childCount];

        for (int i = 0; i < bowl.childCount; i++)
        {
            bowlSpots[i] = bowl.GetChild(i);
        }

        berrySpots = new Transform[berryTray.childCount];
        berriesInTray = new GameObject[berryTray.childCount];

        for (int i = 0; i < berryTray.childCount; i++)
        {
            berrySpots[i] = berryTray.GetChild(i);
        }
        LoadWordsFromCsv();
        LoadRandomWord();
    }

    public void RegisterBerryInTray(GameObject berry)
    {
        for (int i = 0; i < berrySpots.Length; i++)
        {
            if (berriesInTray[i] == null)
            {
                berriesInTray[i] = berry;

                berry.transform.SetParent(berrySpots[i], false);
                berry.transform.localPosition = Vector3.zero;
                berry.transform.localScale = Vector3.one;

                return;
            }
        }

        Debug.Log("Berry tray is full");
    }

    public void RemoveFromTray(GameObject berry)
    {
        for (int i = 0; i < berriesInTray.Length; i++)
        {
            if (berriesInTray[i] == berry)
            {
                berriesInTray[i] = null;
                return;
            }
        }
    }

    public bool TryPlaceInBowl(GameObject berry)
    {
        for (int i = 0; i < bowlSpots.Length; i++)
        {
            if (!bowlSpotTaken[i])
            {
                RemoveFromTray(berry);

                berry.transform.SetParent(bowlSpots[i], false);
                berry.transform.localPosition = Vector3.zero;
                berry.transform.localScale = Vector3.one * 0.7f;

                bowlSpotTaken[i] = true;
                berriesInBowlSpots[i] = berry;
                berriesInBowl++;

                return true;
            }
        }

        Debug.Log("Bowl is full");
        return false;
    }

    public void RemoveFromBowl(GameObject berry)
    {
        for (int i = 0; i < berriesInBowlSpots.Length; i++)
        {
            if (berriesInBowlSpots[i] == berry)
            {
                bowlSpotTaken[i] = false;
                berriesInBowlSpots[i] = null;
                berriesInBowl--;

                return;
            }
        }
    }

    public void ReturnBerryToTray(GameObject berry)
    {
        for (int i = berrySpots.Length - 1; i >= 0; i--)
        {
            if (berriesInTray[i] == null)
            {
                berriesInTray[i] = berry;

                berry.transform.SetParent(berrySpots[i], false);
                berry.transform.localPosition = Vector3.zero;
                berry.transform.localScale = Vector3.one;

                return;
            }
        }

        Debug.Log("Berry tray is full");
    }

public void CheckAnswer()
{
    if (inputLocked)
    {
        return;
    }

    if (berriesInBowl == correctSyllables)
    {
        ShowSuccessScreen();
    }
    else
    {
        StartCoroutine(WrongAnswerRoutine());
    }
}
    private void ResetBowl()
{
    GameObject[] berriesToReturn = new GameObject[berriesInBowlSpots.Length];

    for (int i = 0; i < berriesInBowlSpots.Length; i++)
    {
        berriesToReturn[i] = berriesInBowlSpots[i];
    }

    for (int i = 0; i < berriesToReturn.Length; i++)
    {
        if (berriesToReturn[i] != null)
        {
            RemoveFromBowl(berriesToReturn[i]);
            ReturnBerryToTray(berriesToReturn[i]);

            Berry berryScript = berriesToReturn[i].GetComponent<Berry>();
            if (berryScript != null)
            {
                berryScript.SetInBowl(false);
            }
        }
    }
}
private IEnumerator WrongAnswerRoutine()
{
    inputLocked = true;

    resultText.text = "Zkusíme to ještě jednou.";

    if (lunaImage != null && lunaThinkingSprite != null)
    {
        lunaImage.sprite = lunaThinkingSprite;
    }

    yield return new WaitForSeconds(feedbackDelay);

    ResetBowl();

    if (lunaImage != null && lunaNormalSprite != null)
    {
        lunaImage.sprite = lunaNormalSprite;
    }

    resultText.text = "";

    inputLocked = false;
}
private void LoadWordsFromCsv()
{
    TextAsset csvFile = Resources.Load<TextAsset>("words");

    if (csvFile == null)
    {
        Debug.LogError("CSV file not found. Make sure it is in Assets/Resources/words.csv");
        return;
    }

    string[] lines = csvFile.text.Split('\n');

    for (int i = 1; i < lines.Length; i++)
    {
        string line = lines[i].Trim();

        if (string.IsNullOrEmpty(line))
        {
            continue;
        }

        string[] values = line.Split(';');

        string word = values[0];
        int syllables = int.Parse(values[1]);
        string topic = values[2];
        string pictureName = values[3];

        words.Add(new WordData(word, syllables, topic, pictureName));
    }

    Debug.Log("Loaded words: " + words.Count);
}

private void LoadRandomWord()
{
    if (words.Count == 0)
    {
        Debug.LogError("No words loaded.");
        return;
    }

    int index = Random.Range(0, words.Count);
    currentWord = words[index];

    correctSyllables = currentWord.syllables;

    string imageName = currentWord.pictureName.Replace(".png", "");
    Sprite sprite = Resources.Load<Sprite>("WordCards/" + imageName);

    if (sprite == null)
    {
        Debug.LogError("Image not found: " + imageName);
        return;
    }

    wordImage.sprite = sprite;

    Debug.Log("Current word: " + currentWord.word + ", syllables: " + currentWord.syllables);
}

private IEnumerator CorrectAnswerRoutine()
{
    inputLocked = true;

    resultText.text = "Správně! Luna má radost!";

    yield return new WaitForSeconds(feedbackDelay);

    ResetBowl();

    LoadRandomWord();

    resultText.text = "";

    inputLocked = false;
}

private void ShowSuccessScreen()
{
    inputLocked = true;

    exercisePanel.SetActive(false);
    successPanel.SetActive(true);

    if (confetti != null)
{
    confetti.Clear();
    confetti.Play();
}
energy++;

if (energy > 5)
{
    energy = 5;
}

UpdateEnergyBubble();
}

public void NextWord()
{
    if (energy >= 5)
{
    ShowMapPanel();
    return;
}

    ResetBowl();
    LoadRandomWord();

    successPanel.SetActive(false);
    exercisePanel.SetActive(true);

    resultText.text = "";
    inputLocked = false;
}

private void UpdateEnergyBubble()
{
    if (energyBubbleImage != null && energySprites != null && energySprites.Length > energy)
    {
        energyBubbleImage.sprite = energySprites[energy];
    }
}

private void ShowCompanionPanel()
{
    successPanel.SetActive(false);
    companionPanel.SetActive(true);

    if (companionImage != null && currentCompanionSprite != null)
    {
        companionImage.sprite = currentCompanionSprite;
    }
}

public void ContinueAfterCompanion()
{
    energy = 0;
    UpdateEnergyBubble();

    companionPanel.SetActive(false);
    exercisePanel.SetActive(true);

    ResetBowl();
    LoadRandomWord();

    resultText.text = "";
    inputLocked = false;
}

private void ShowMapPanel()
{
    successPanel.SetActive(false);
    mapPanel.SetActive(true);

    inputLocked = true;
}

public void ContinueFromMap()
{
    mapPanel.SetActive(false);
    exercisePanel.SetActive(true);

    energy = 0;
    UpdateEnergyBubble();

    ResetBowl();
    LoadRandomWord();

    inputLocked = false;
}

}