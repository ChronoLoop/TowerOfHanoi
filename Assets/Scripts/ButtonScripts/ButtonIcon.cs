using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonIcon : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image buttonImage;
    public void OnPointerClick(PointerEventData eventData)
    {
        Color tempColor = buttonImage.color;
        tempColor.a = 1f;
        buttonImage.color = tempColor;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Color tempColor = buttonImage.color;
        tempColor.a = 0.7f;
        buttonImage.color = tempColor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Color tempColor = buttonImage.color;
        tempColor.a = 150f / 255f;
        buttonImage.color = tempColor;
    }
}