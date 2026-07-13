using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class FinalLocationDebugButton
{
#if UNITY_EDITOR || DEVELOPMENT_BUILD
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void CreateButton()
    {
        GameManager gameManager = Object.FindFirstObjectByType<GameManager>();
        Canvas canvas = Object.FindFirstObjectByType<Canvas>();

        if (gameManager == null || canvas == null)
        {
            return;
        }

        Transform titlePanel = FindChildByName(canvas.transform, "TitlePanel");

        if (titlePanel == null || FindChildByName(titlePanel, "DebugFinalLocationButton") != null)
        {
            return;
        }

        GameObject buttonObject = new GameObject("DebugFinalLocationButton", typeof(RectTransform));
        buttonObject.layer = LayerMask.NameToLayer("UI");
        buttonObject.transform.SetParent(titlePanel, false);

        RectTransform rect = buttonObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(1f, 0f);
        rect.anchorMax = new Vector2(1f, 0f);
        rect.pivot = new Vector2(1f, 0f);
        rect.anchoredPosition = new Vector2(-30f, 30f);
        rect.sizeDelta = new Vector2(360f, 90f);

        Image image = buttonObject.AddComponent<Image>();
        image.color = new Color(0.75f, 0.22f, 0.22f, 0.95f);

        Button button = buttonObject.AddComponent<Button>();
        button.targetGraphic = image;
        button.onClick.AddListener(gameManager.DebugStartFinalLocation);

        GameObject labelObject = new GameObject("Text", typeof(RectTransform));
        labelObject.layer = LayerMask.NameToLayer("UI");
        labelObject.transform.SetParent(buttonObject.transform, false);

        RectTransform labelRect = labelObject.GetComponent<RectTransform>();
        labelRect.anchorMin = Vector2.zero;
        labelRect.anchorMax = Vector2.one;
        labelRect.offsetMin = Vector2.zero;
        labelRect.offsetMax = Vector2.zero;

        TextMeshProUGUI label = labelObject.AddComponent<TextMeshProUGUI>();
        label.text = "DEV: Finální lokace";
        label.fontSize = 28f;
        label.fontStyle = FontStyles.Bold;
        label.alignment = TextAlignmentOptions.Center;
        label.color = Color.white;
        label.raycastTarget = false;
    }

    private static Transform FindChildByName(Transform parent, string childName)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>(true);

        for (int i = 0; i < children.Length; i++)
        {
            if (children[i].name == childName)
            {
                return children[i];
            }
        }

        return null;
    }
#endif
}
