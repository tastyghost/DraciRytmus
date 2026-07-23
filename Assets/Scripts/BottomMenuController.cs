using UnityEngine;

public class BottomMenuController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    public void OpenMap()
    {
        if (gameManager != null)
        {
            gameManager.OpenMapFromBottomBar();
        }
    }

    public void OpenCollection()
    {
        if (gameManager != null)
        {
            gameManager.OpenCollectionFromBottomBar();
        }
    }

    public void OpenSettings()
    {
        if (gameManager != null)
        {
            gameManager.OpenSettingsFromBottomBar();
        }
    }

    public void OpenMenu()
    {
        if (gameManager != null)
        {
            gameManager.OpenMenuFromBottomBar();
        }
    }
}
