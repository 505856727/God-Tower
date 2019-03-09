using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ItemUI : MonoBehaviour, IPointerClickHandler
{
    public Text nameText;
    public Text countText;
    public Image icon;
    public Text LV;

    public event Action<Item,int> PointerClick; 

    private Item item;
    private int count = 0;
    private int id;        //区分不同物体UI

    public void SetInfo(Item itemPara,int idPara)
    {
        id = idPara;
        item = itemPara;
        nameText.text = itemPara.Name;
        count++;
        countText.text = count.ToString();
        LV.text = "LV"+itemPara.LV.ToString();
        icon.sprite = Resources.Load<Sprite>(itemPara.IconPath);
    }

    public void SetId()
    {
        id--;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if(PointerClick==null)
        {
            return;
        }
        PointerClick(item,id);
    }
}
