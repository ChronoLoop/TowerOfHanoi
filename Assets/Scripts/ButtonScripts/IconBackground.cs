using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IconBackground : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image backgroundImage;
    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Color tempColor = backgroundImage.color;
        tempColor.a = 0.20f;
        backgroundImage.color = tempColor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Color tempColor = backgroundImage.color;
        tempColor.a = 0f;
        backgroundImage.color = tempColor;
    }
}