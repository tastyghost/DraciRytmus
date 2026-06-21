using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Transform bowl;
    private Transform[] bowlSpots;
    private bool[] taken;

    void Start()
    {
        bowl = GameObject.FindWithTag("Bowl").transform;

        bowlSpots = new Transform[bowl.childCount];

        for (int i = 0; i < bowl.childCount; i++)
        {
            bowlSpots[i] = bowl.GetChild(i);
        }

        taken = new bool[bowlSpots.Length];
    }

    public bool TryPlaceInBowl(GameObject berry)
    {
        for (int i = 0; i < bowlSpots.Length; i++)
        {
            if (!taken[i])
            {
                berry.transform.SetParent(bowlSpots[i], false);
                berry.transform.localPosition = Vector3.zero;
                berry.transform.localScale = berry.transform.localScale * 0.3f;
                taken[i] = true;
                return true;
            }
        }

        Debug.Log("Bowl is full");
        return false;
    }

    public void checkAnswer()
    {
        
    }

}