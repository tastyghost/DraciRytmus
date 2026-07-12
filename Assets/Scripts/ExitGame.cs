using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    private GameObject confirmationPopup;

    private void Start()
    {
        Canvas canvas = FindFirstObjectByType<Canvas>();

        if (canvas == null)
        {
            Debug.LogError("ExitGame could not find a Canvas in the scene.");
            return;
        }

        ConnectExitButton(canvas);
        CreateConfirmationPopup(canvas.transform);
    }

    public void ShowConfirmation()
    {
        if (confirmationPopup != null)
        {
            confirmationPopup.SetActive(true);
            confirmationPopup.transform.SetAsLastSibling();
        }
    }

    public void CancelQuit()
    {
        if (confirmationPopup != null)
        {
            confirmationPopup.SetActive(false);
        }
    }

    public void QuitGame()
    {
        if (confirmationPopup != null)
        {
            confirmationPopup.SetActive(false);
        }

        Debug.Log("Quit Game");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void ConnectExitButton(Canvas canvas)
    {
        Transform titlePanel = null;
        Transform[] canvasChildren = canvas.GetComponentsInChildren<Transform>(true);

        for (int i = 0; i < canvasChildren.Length; i++)
        {
            if (canvasChildren[i].name == "TitlePanel")
            {
                titlePanel = canvasChildren[i];
                break;
            }
        }

        if (titlePanel == null)
        {
            Debug.LogError("ExitGame could not find TitlePanel under the Canvas.");
            return;
        }

        Button[] buttons = titlePanel.GetComponentsInChildren<Button>(true);

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].name == "ExitButton")
            {
                buttons[i].onClick.AddListener(ShowConfirmation);
                return;
            }
        }

        Debug.LogError("ExitGame could not find ExitButton under TitlePanel.");
    }

    private void CreateConfirmationPopup(Transform canvasTransform)
    {
        confirmationPopup = CreateUiObject("ExitConfirmationPopup", canvasTransform);
        RectTransform popupRect = confirmationPopup.GetComponent<RectTransform>();
        popupRect.anchorMin = Vector2.zero;
        popupRect.anchorMax = Vector2.one;
        popupRect.offsetMin = Vector2.zero;
        popupRect.offsetMax = Vector2.zero;

        Image blocker = confirmationPopup.AddComponent<Image>();
        blocker.color = new Color(0f, 0f, 0f, 0.7f);
        blocker.raycastTarget = true;

        GameObject dialog = CreateUiObject("Dialog", confirmationPopup.transform);
        RectTransform dialogRect = dialog.GetComponent<RectTransform>();
        dialogRect.anchorMin = new Vector2(0.5f, 0.5f);
        dialogRect.anchorMax = new Vector2(0.5f, 0.5f);
        dialogRect.pivot = new Vector2(0.5f, 0.5f);
        dialogRect.anchoredPosition = Vector2.zero;
        dialogRect.sizeDelta = new Vector2(760f, 500f);

        Image dialogImage = dialog.AddComponent<Image>();
        dialogImage.color = new Color(0.96f, 0.93f, 0.84f, 1f);

        TMP_Text title = CreateLabel("Exit Game", dialog.transform, 48f);
        SetRect(title.rectTransform, new Vector2(0.08f, 0.7f), new Vector2(0.92f, 0.94f));
        title.fontStyle = FontStyles.Bold;
        title.color = new Color(0.12f, 0.18f, 0.34f, 1f);

        TMP_Text message = CreateLabel("Are you sure you want to quit the game?", dialog.transform, 34f);
        SetRect(message.rectTransform, new Vector2(0.08f, 0.36f), new Vector2(0.92f, 0.7f));
        message.color = new Color(0.12f, 0.18f, 0.34f, 1f);

        CreateDialogButton("CancelButton", "Cancel", dialog.transform,
            new Vector2(0.08f, 0.09f), new Vector2(0.46f, 0.28f), CancelQuit);

        CreateDialogButton("ConfirmExitButton", "Exit", dialog.transform,
            new Vector2(0.54f, 0.09f), new Vector2(0.92f, 0.28f), QuitGame);

        confirmationPopup.SetActive(false);
    }

    private void CreateDialogButton(
        string objectName,
        string label,
        Transform parent,
        Vector2 anchorMin,
        Vector2 anchorMax,
        UnityEngine.Events.UnityAction onClick)
    {
        GameObject buttonObject = CreateUiObject(objectName, parent);
        RectTransform rect = buttonObject.GetComponent<RectTransform>();
        SetRect(rect, anchorMin, anchorMax);

        Image image = buttonObject.AddComponent<Image>();
        image.color = new Color(0.18f, 0.24f, 0.42f, 1f);

        Button button = buttonObject.AddComponent<Button>();
        button.targetGraphic = image;
        button.onClick.AddListener(onClick);

        CreateLabel(label, buttonObject.transform, 30f);
    }

    private static GameObject CreateUiObject(string objectName, Transform parent)
    {
        GameObject uiObject = new GameObject(objectName, typeof(RectTransform));
        uiObject.layer = LayerMask.NameToLayer("UI");
        uiObject.transform.SetParent(parent, false);
        return uiObject;
    }

    private static TMP_Text CreateLabel(string text, Transform parent, float fontSize)
    {
        GameObject labelObject = CreateUiObject("Text", parent);
        RectTransform rect = labelObject.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        TextMeshProUGUI label = labelObject.AddComponent<TextMeshProUGUI>();
        label.text = text;
        label.fontSize = fontSize;
        label.alignment = TextAlignmentOptions.Center;
        label.color = Color.white;
        label.raycastTarget = false;
        return label;
    }

    private static void SetRect(RectTransform rect, Vector2 anchorMin, Vector2 anchorMax)
    {
        rect.anchorMin = anchorMin;
        rect.anchorMax = anchorMax;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }
}
