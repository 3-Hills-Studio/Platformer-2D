using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [field: SerializeField] private BaseCollectable Collectable{ get; set; }

    [field: SerializeField] private Image BackgroundImage { get; set; }
    
    [field: SerializeField] private Image FrameImage { get; set; }
    
    [field: SerializeField] private Image IconImage { get; set; }

    private Color _bgDefaultColor;
    
    private Color _bgHoveredColor;
    
    private void Start()
    {
        IconImage.enabled = false;
        FrameImage.enabled = false;
        
        _bgDefaultColor = BackgroundImage.color;

        _bgHoveredColor = new Color(_bgDefaultColor.r, _bgDefaultColor.g, _bgDefaultColor.b, 1);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        BackgroundImage.color = _bgHoveredColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        BackgroundImage.color = _bgDefaultColor;
    }
}
