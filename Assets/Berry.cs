using UnityEngine;
using UnityEngine.EventSystems;

public class Berry : MonoBehaviour, IPointerClickHandler
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gameManager.TryPlaceInBowl(gameObject);
    }
}