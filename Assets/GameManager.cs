using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Transform bowl;
    private Transform[] bowlSpots;
    private bool[] bowlSpotTaken;
    private GameObject[] berriesInBowlSpots;

    public Transform berryTray;

    public Transform[] berrySpots;
    public GameObject[] berriesInTray;

    public int correctSyllables = 1;
    private int berriesInBowl = 0;

    void Start()
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
                berry.transform.localScale = Vector3.one * 0.3f;

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
        if (berriesInBowl == correctSyllables)
        {
            Debug.Log("Správně!");
        }
        else
        {
            Debug.Log("Zkusíme to ještě jednou.");
        }
    }
}