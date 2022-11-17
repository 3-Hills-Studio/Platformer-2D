using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerClickHandler
{
    
    [field: SerializeField] public int SlotId{ get; set; }
    
    [field: SerializeField] public BaseCollectable Collectable { get; set; }

    [field: SerializeField] public Image BackgroundImage { get; set; }
    
    [field: SerializeField] public Image FrameImage { get; set; }
    
    [field: SerializeField] public Image IconImage { get; set; }
    
    [field: SerializeField] public Inventory Inventory { get; set; }
    

    private Color _bgDefaultColor;
    
    private Color _bgHoveredColor;
    
    private void Start()
    {
        IconImage.enabled = false;
        FrameImage.enabled = false;
        
        _bgDefaultColor = BackgroundImage.color;

        _bgHoveredColor = new Color(_bgDefaultColor.r, _bgDefaultColor.g, _bgDefaultColor.b, 1);
    }

/*
    private void Update()
    {
        if (Input.touchCount <= 0) return;
        
        BackgroundImage.color = _bgHoveredColor;
        Inventory.MouseHoveredSlot = this;
        
    }
*/
    public void OnPointerEnter(PointerEventData eventData)
    {
        BackgroundImage.color = _bgHoveredColor;
        Inventory.MouseHoveredSlot = this;
        
        Debug.Log($"item slot hovered id: {SlotId}");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        BackgroundImage.color = _bgDefaultColor;
        Inventory.MouseHoveredSlot = null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
#if UNITY_ANDROID
        Inventory.MouseHoveredSlot = this;
#endif
        Inventory.MousePickedUpSlot = this;

        IconImage.enabled = false;

        Inventory.DragNDropIconImageHolder.sprite = IconImage.sprite;
        Inventory.DragNDropIconImageHolder.gameObject.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
#if UNITY_STANDALONE_WIN
        Inventory.DragNDropIconImageHolder.transform.position = Input.mousePosition;
#elif UNITY_ANDROID
        
        Debug.Log("android on drag event");
        
        if (Input.touchCount <= 0) return;
        
        Inventory.MouseHoveredSlot = this;

        Vector2 touchPosition = Input.GetTouch(0).position;
        
        Inventory.DragNDropIconImageHolder.transform.position = touchPosition;
#endif
/*
        if (Inventory.MouseHoveredSlot != null)
        {
            Debug.Log($"Mouse hovered slot: {Inventory.MouseHoveredSlot.SlotId}");
        }
        */
    }

    public void OnEndDrag(PointerEventData eventData)
    {
#if UNITY_ANDROID
        Inventory.MouseHoveredSlot = null;
#endif
        if (EventSystem.current.IsPointerOverGameObject() == false)
        {
            GamePlayEvents.RaiseOnRemoveItemFromInventory(Collectable);
            
            Collectable = null;
            IconImage.sprite = null;
            IconImage.enabled = false;
            
            Inventory.DragNDropIconImageHolder.sprite = null;
            Inventory.DragNDropIconImageHolder.gameObject.SetActive(false);
            Inventory.MousePickedUpSlot = null;
            
            return;
        }
        
        if (Inventory.MouseHoveredSlot != null)
        {
            var spriteToExchange = IconImage.sprite;
            var collectableToExchange = Collectable;
            
            IconImage.sprite = Inventory.MouseHoveredSlot.IconImage.sprite;
            
            Collectable = Inventory.MouseHoveredSlot.Collectable;
            
            Inventory.MouseHoveredSlot.IconImage.sprite = spriteToExchange;

            Inventory.MouseHoveredSlot.Collectable = collectableToExchange;
        }

        CheckIconImage(IconImage);
        CheckIconImage(Inventory.MouseHoveredSlot.IconImage);
        
        Inventory.DragNDropIconImageHolder.gameObject.SetActive(false);
        
        Inventory.MousePickedUpSlot = null;
    }

    private void CheckIconImage(Image image)
    {
        if (image.sprite == null)
        {
            image.enabled = false;
            return;
        }
        image.enabled = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Collectable.Use();
        
        Collectable = null;
        
        IconImage.sprite = null;
        
        IconImage.enabled = false;
        
        Inventory.MousePickedUpSlot = null;
    }
}
