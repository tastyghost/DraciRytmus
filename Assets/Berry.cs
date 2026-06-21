using UnityEngine;
using UnityEngine.EventSystems;

public class Berry : MonoBehaviour, IPointerClickHandler
{
    private bool isInBowl = false;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.RegisterBerryInTray(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isInBowl)
        {
            gameManager.RemoveFromBowl(gameObject);
            gameManager.ReturnBerryToTray(gameObject);
            isInBowl = false;
        }
        else
        {
            bool placed = gameManager.TryPlaceInBowl(gameObject);

            if (placed)
            {
                isInBowl = true;
            }
        }
    }
}