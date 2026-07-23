using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas), typeof(CanvasScaler))]
public class ResponsiveCanvas : MonoBehaviour
{
    [SerializeField] private Vector2 referenceResolution = new Vector2(1080f, 1920f);
    [SerializeField] private RectTransform[] fullScreenBackgrounds;

    private CanvasScaler canvasScaler;
    private RectTransform canvasRect;
    private bool isUpdatingLayout;

    private void Awake()
    {
        CacheComponents();
        ConfigureCanvasScaler();
    }

    private void OnEnable()
    {
        UpdateBackgrounds();
    }

    private void OnRectTransformDimensionsChange()
    {
        if (isActiveAndEnabled)
        {
            UpdateBackgrounds();
        }
    }

    private void CacheComponents()
    {
        if (canvasScaler == null)
        {
            canvasScaler = GetComponent<CanvasScaler>();
        }

        if (canvasRect == null)
        {
            canvasRect = GetComponent<RectTransform>();
        }
    }

    private void ConfigureCanvasScaler()
    {
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = referenceResolution;
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        canvasScaler.matchWidthOrHeight = 0f;
    }

    private void UpdateBackgrounds()
    {
        if (isUpdatingLayout)
        {
            return;
        }

        isUpdatingLayout = true;
        CacheComponents();
        ConfigureCanvasScaler();

        if (fullScreenBackgrounds != null)
        {
            for (int i = 0; i < fullScreenBackgrounds.Length; i++)
            {
                ResizeBackgroundToCoverScreen(fullScreenBackgrounds[i]);
            }
        }

        isUpdatingLayout = false;
    }

    private void ResizeBackgroundToCoverScreen(RectTransform background)
    {
        if (background == null || background.parent == null)
        {
            return;
        }

        Vector3[] canvasCorners = new Vector3[4];
        canvasRect.GetWorldCorners(canvasCorners);

        RectTransform parentRect = background.parent as RectTransform;
        if (parentRect == null)
        {
            return;
        }

        Vector3 bottomLeft = parentRect.InverseTransformPoint(canvasCorners[0]);
        Vector3 topRight = parentRect.InverseTransformPoint(canvasCorners[2]);
        Vector2 targetSize = new Vector2(
            Mathf.Abs(topRight.x - bottomLeft.x),
            Mathf.Abs(topRight.y - bottomLeft.y));

        if (targetSize.x <= 0f || targetSize.y <= 0f)
        {
            return;
        }

        float imageAspect = GetImageAspect(background, targetSize);
        float targetAspect = targetSize.x / targetSize.y;

        Vector2 backgroundSize;
        if (imageAspect > targetAspect)
        {
            backgroundSize = new Vector2(targetSize.y * imageAspect, targetSize.y);
        }
        else
        {
            backgroundSize = new Vector2(targetSize.x, targetSize.x / imageAspect);
        }

        background.anchorMin = new Vector2(0.5f, 0.5f);
        background.anchorMax = new Vector2(0.5f, 0.5f);
        background.anchoredPosition = Vector2.zero;
        background.sizeDelta = backgroundSize;
    }

    private float GetImageAspect(RectTransform background, Vector2 fallbackSize)
    {
        Image image = background.GetComponent<Image>();
        if (image != null && image.sprite != null && image.sprite.rect.height > 0f)
        {
            return image.sprite.rect.width / image.sprite.rect.height;
        }

        if (fallbackSize.y > 0f)
        {
            return fallbackSize.x / fallbackSize.y;
        }

        return referenceResolution.x / referenceResolution.y;
    }
}
