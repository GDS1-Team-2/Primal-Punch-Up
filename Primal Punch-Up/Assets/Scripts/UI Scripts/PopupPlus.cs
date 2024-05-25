using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupPlus : MonoBehaviour
{
    // Start is called before the first frame update
    public float firstduration;
    public Vector2 startPos;
    public Vector2 endPos;
    public Color startColour;
    public Color endColour;

    void Start()
    {
        firstduration = 1;
        startPos = gameObject.GetComponent<RectTransform>().anchoredPosition;
        endPos = new Vector2(startPos.x, startPos.y+50f);
        startColour = gameObject.GetComponent<Text>().color;
        endColour = new Color(startColour.r, startColour.g, startColour.b, 0f);
        StartCoroutine(Animation(firstduration, startPos, endPos, startColour, endColour));
    }

    // Update is called once per frame
    private IEnumerator Animation(float duration, Vector3 start, Vector3 end, Color startC, Color endC)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(startPos, endPos, (elapsedTime / duration));
            gameObject.GetComponent<Text>().color = Color.Lerp(startColour, endColour, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
