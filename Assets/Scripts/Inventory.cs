using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [field: SerializeField] public GameObject InventoryItemPrefab { get; private set; }

    [field: SerializeField] public int InventoryNumberOfItems{ get; private set; }

    [field: SerializeField] public RectTransform InventoryFrame { get; private set; }
    
    [field: SerializeField] public RectTransform ContentRect { get; private set; }
    
    [field: SerializeField] public Button OpenInventoryButton{ get; private set; }
    
    [field: SerializeField] public float inventoryAnimationDuration{ get; private set; }

    [field: SerializeField] public List<InventorySlot> InventorySlots { get; set; }
    
    [field: SerializeField] public InventorySlot MousePickedUpSlot { get; set; }
    
    [field: SerializeField] public InventorySlot MouseHoveredSlot { get; set; }
    
    [field: SerializeField] public Image DragNDropIconImageHolder { get; set; }

    private float _defaultAnchoredPositionX = 0;
    private float _openAnchoredPositionX = 0;


    private void OnEnable()
    {
        GamePlayEvents.OnAddCollectable += HandleOnAddCollectable;
    }

    private void OnDisable()
    {
        GamePlayEvents.OnAddCollectable -= HandleOnAddCollectable;
    }

    //rad sa eventima je observable pattern u programiranju
    public void HandleOnAddCollectable(BaseCollectable baseCollectable)
    {
        AddItemToSlot(baseCollectable);
    }

    private void Start()
    {
        _openAnchoredPositionX = -103.3026f;
        _defaultAnchoredPositionX = InventoryFrame.anchoredPosition.x;

        for (int i = 0; i < InventoryNumberOfItems; i++)
        {
            var gob = Instantiate(InventoryItemPrefab, ContentRect);

            var inventoryItem = gob.GetComponent<InventorySlot>();

            inventoryItem.SlotId = i;
            
            inventoryItem.Inventory = this;
            
            InventorySlots.Add(inventoryItem);
        }
    }

    public void AddItemToSlot(BaseCollectable baseCollectable)
    {
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            if (InventorySlots[i].Collectable != null) continue;

            InventorySlots[i].Collectable = baseCollectable;

            InventorySlots[i].IconImage.sprite = baseCollectable.ItemIconSprite;

            InventorySlots[i].IconImage.enabled = true;
            
            return;
        }
    }

    public void OnClickInventoryButton()
    {
        OpenInventoryButton.interactable = false;

        if (Mathf.Abs(InventoryFrame.anchoredPosition.x - _openAnchoredPositionX) < 1)
        {
            InventoryFrame.DOAnchorPosX(_defaultAnchoredPositionX, inventoryAnimationDuration).OnComplete(() => OpenInventoryButton.interactable = true);
            return;
        }
        InventoryFrame.DOAnchorPosX(_openAnchoredPositionX, inventoryAnimationDuration).OnComplete(() => OpenInventoryButton.interactable = true);
    }

}
