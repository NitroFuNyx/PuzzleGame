using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InventoryScrollButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region Events Declaration
    public event Action OnScrollButtonPressed;
    public event Action OnScrollButtonReleased;
    #endregion Events Declaration

    public void OnPointerDown(PointerEventData eventData)
    {
        OnScrollButtonPressed?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnScrollButtonReleased?.Invoke();
    }
}
