using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MapMovement : MonoBehaviour
{
    public RectTransform lunaIcon;
    public RectTransform pathDotsParent;
    public GameObject pathDotPrefab;

    public float moveDurationPerStep = 0.45f;
    public float jumpHeight = 35f;
    public float dotScale = 1f;

    private bool isMoving = false;

    public bool IsMoving()
    {
        return isMoving;
    }

    public void MoveLunaAlongPath(RectTransform[] pathPoints, System.Action onComplete)
    {
        if (isMoving)
        {
            return;
        }

        StartCoroutine(MoveRoutine(pathPoints, onComplete));
    }

    private IEnumerator MoveRoutine(RectTransform[] pathPoints, System.Action onComplete)
    {
        if (pathPoints == null || pathPoints.Length == 0)
        {
            onComplete?.Invoke();
            yield break;
        }

        isMoving = true;

        for (int i = 0; i < pathPoints.Length; i++)
        {
            Vector2 startPos = lunaIcon.anchoredPosition;
            Vector2 endPos = pathPoints[i].anchoredPosition;

            float elapsed = 0f;

            while (elapsed < moveDurationPerStep)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / moveDurationPerStep;

                Vector2 flatPosition = Vector2.Lerp(startPos, endPos, t);

                float jumpOffset = Mathf.Sin(t * Mathf.PI) * jumpHeight;

                lunaIcon.anchoredPosition = new Vector2(
                    flatPosition.x,
                    flatPosition.y + jumpOffset
                );

                yield return null;
            }

            lunaIcon.anchoredPosition = endPos;

            CreatePathDot(endPos);
        }

        isMoving = false;

        onComplete?.Invoke();
    }

    private void CreatePathDot(Vector2 position)
    {
        if (pathDotPrefab == null || pathDotsParent == null)
        {
            return;
        }

        GameObject dot = Instantiate(pathDotPrefab, pathDotsParent);
        RectTransform dotRect = dot.GetComponent<RectTransform>();

        dotRect.anchoredPosition = position;
        dotRect.localScale = Vector3.one * dotScale;
    }
}