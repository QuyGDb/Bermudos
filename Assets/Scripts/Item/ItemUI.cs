using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
{
    [HideInInspector] public int itemId;
    public Image SelectorImg;
    public Image ItemImg;
    public TextMeshProUGUI quantityText;
    public InventoryItem inventoryItem;
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

    public void ResetItemUI()
    {
        SelectorImg.gameObject.SetActive(false);
        ItemImg.gameObject.SetActive(false);
        quantityText.gameObject.SetActive(false);
        quantityText.text = "";
        ItemImg.sprite = null;
        quantityText.text = "";
        inventoryItem = default;
        isHasItem = false;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (inventoryItem != default || inventoryItem != null)
        {
            SelectorImg.gameObject.SetActive(false);
            itemUICopy = Instantiate(this.gameObject);
            RectTransform rectTransformCopy = itemUICopy.GetComponent<RectTransform>();
            rectTransformCopy.sizeDelta = rectTransform.sizeDelta;
            rectTransformCopy.pivot = rectTransform.pivot;
            rectTransformCopy.anchorMin = rectTransform.anchorMin;
            rectTransformCopy.anchorMax = rectTransform.anchorMax;
            rectTransformCopy.anchoredPosition = rectTransform.anchoredPosition;
            if (ItemUIType == ItemUIType.Inventory)
                itemUICopy.transform.SetParent(inventory.transform, false);
            else
                itemUICopy.transform.SetParent(transform.parent.parent, false);
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
        if (inventoryItem != default)
        {
            StaticEventHandler.CallItemUIEndDragChangedEvent(this);
            Destroy(itemUICopy);
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (inventoryItem != default)
        {
            StaticEventHandler.CallItemUIClickChangedEvent(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //  Debug.Log("pointer enter" + this.gameObject.name);
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
