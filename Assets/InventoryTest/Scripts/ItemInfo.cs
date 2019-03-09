using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public Image desIcon;
    public Text desName;
    public Text desLv;
    public Text desType;
    public Text desDescription;

    private Item SelectedItem;

    public void ShowInfo(Item item)
    {
        SelectedItem = item;
        desIcon.gameObject.SetActive(true);
        desIcon.sprite = Resources.Load<Sprite>(item.IconPath);
        desName.text = item.Name;
        desLv.text = "LV:" + item.LV.ToString();
        desType.text = GetType(item);
        desDescription.text = item.Description;
    }

    private string GetType(Item itemPara)
    {
        string typeStr = string.Empty;
        switch(itemPara.Type)
        {
            case Item.ItemType.Equipment:
                typeStr = "装备";
                break;
            case Item.ItemType.Weapon:
                typeStr = "武器";
                break;
            default:
                typeStr = "不明物体";
                break;
        }
        return typeStr;
    }

    public void EquipBtnOnClick()
    {
        if(SelectedItem==null)
        {
            return;
        }

        //装备
    }

    public void DropBtnOnClick()
    {
        if (SelectedItem == null)
        {
            return;
        }
        desIcon.gameObject.SetActive(false);
        desName.text = string.Empty;
        desLv.text = string.Empty;
        desType.text = string.Empty;
        desDescription.text = string.Empty;
        Inventory.Instance.RemoveItem(SelectedItem);
        SelectedItem = null;
    }

}
