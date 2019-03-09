using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int ID { get; private set; }
    public string Name { get; private set; }
    public ItemType Type { get; private set; }
    public int LV { get; private set; }
    public string Description { get; private set; }
    public string IconPath { get; private set; }

    public enum ItemType
    {
        Weapon,
        Equipment
    }

    public Item(int id,string name,ItemType type,int lv,string des,string iconPath)
    {
        ID = id;
        Name = name;
        Type = type;
        LV = lv;
        Description = des;
        IconPath = iconPath;
    }
}
