using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
{
    [HideInInspector] public int itemId;
    public Image SelectorImg;
    public Image ItemImg;
    public TextMeshProUGUI quantityText;
    [HideInInspector] public InventoryItem inventoryItem;
    [HideInInspector] public ItemUIType ItemUIType;
    public Inventory inventory;
    GameObject itemUICopy;
    [HideInInspector] public bool isHasItem;
    private RectTransform rectTransform;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        inventory = transform.parent?.transform.parent?.transform.parent?.transform.parent?.transform.parent?.GetComponent<Inventory>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (inventoryItem.item != null)
        {
            itemUICopy = Instantiate(this.gameObject);
            RectTransform rectTransformCopy = itemUICopy.GetComponent<RectTransform>();
            rectTransformCopy.sizeDelta = rectTransform.sizeDelta;
            rectTransformCopy.pivot = rectTransform.pivot;
            rectTransformCopy.anchorMin = rectTransform.anchorMin;
            rectTransformCopy.anchorMax = rectTransform.anchorMax;
            rectTransformCopy.anchoredPosition = rectTransform.anchoredPosition;

            itemUICopy.transform.SetParent(inventory.transform, false);
            itemUICopy.transform.position = HelperUtilities.GetMousePositionInUI(rectTransform);

        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (itemUICopy != null)
        {
            itemUICopy.transform.position = HelperUtilities.GetMousePositionInUI(rectTransform);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        StaticEventHandler.CallItemUIChangedEvent(this);
        Destroy(itemUICopy);
        Debug.Log("end drag" + this.gameObject.name);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Debug.Log(this.gameObject.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StaticEventHandler.CallItemUIHoverChangedEvent(this);
    }


    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(SelectorImg), SelectorImg);
        HelperUtilities.ValidateCheckNullValue(this, nameof(ItemImg), ItemImg);
        HelperUtilities.ValidateCheckNullValue(this, nameof(quantityText), quantityText);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
#endif
    #endregion
}
