using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerClickHandler
{
    [field: SerializeField] public BaseCollectable Collectable;

    [field: SerializeField] public int SlotID { get; set; }
    [field: SerializeField] public Image IconImage { get; set; }

    [field: SerializeField] private Image BackgroundImage { get; set; }

    private Color _bgDefaultColor;

    private Color _bgHoveredColor;


    [field: SerializeField] private Image FrameImage { get; set; }

    [field: SerializeField] public Inventory Inventory { get; set; }



    private void Start()
    {
        IconImage.enabled = false;
        
        _bgDefaultColor = BackgroundImage.color;
        _bgHoveredColor = new Color(_bgDefaultColor.r, _bgDefaultColor.g, _bgDefaultColor.b, 1);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        BackgroundImage.color = _bgHoveredColor;
        Inventory.MouseHoveredSlot = this;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        BackgroundImage.color = _bgDefaultColor;
        Inventory.MouseHoveredSlot = null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Inventory.MousePickedUpSlot = this;

        IconImage.enabled = false;

        Inventory.DragNDropIconImageHolder.sprite = IconImage.sprite;
        Inventory.DragNDropIconImageHolder.gameObject.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Inventory.DragNDropIconImageHolder.transform.position = Input.mousePosition;

    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if(EventSystem.current.IsPointerOverGameObject() == false)
        {
            GameplayEvents.RaiseOnRemoveItemFromInventory(Collectable);

            Collectable = null;
            IconImage.sprite = null;
            IconImage.enabled = false;

            Inventory.DragNDropIconImageHolder.gameObject.SetActive(false);
            Inventory.DragNDropIconImageHolder.sprite = null;
            Inventory.MousePickedUpSlot = null;

            return;
        }

        if(Inventory.MouseHoveredSlot != null )
        {
            var spriteToExchange = IconImage.sprite;
            var collecatbleToExchange = Collectable;

            IconImage.sprite = Inventory.MouseHoveredSlot.IconImage.sprite;
            
            Collectable = Inventory.MouseHoveredSlot.Collectable;
            
            Inventory.MouseHoveredSlot.IconImage.sprite = spriteToExchange;
            
            Inventory.MouseHoveredSlot.Collectable = collecatbleToExchange;
            
                
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

    private void CheckCollectable(BaseCollectable collectable)
    {
        if (collectable == null)
        {
            
        }

        collectable.enabled = true;

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
