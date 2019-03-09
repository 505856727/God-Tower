using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    public GameObject gridPrefab;
    public GameObject itemPrefab;
    public GameObject mainScrollView;
    public GameObject otherScrollView;
    public Transform allContent;     //所有物品栏
    public Transform otherContent;   //具体种类物品栏
    public ItemInfo ItemInfo;
    [HideInInspector]
    public int gridCount = 30;  //背包容量

    private int usedGridCount = 0;   //使用背包容量
    private List<Transform> allGridsList=new List<Transform>(); //所有物品栏物品格子

    private List<Item> allItem = new List<Item>();       //存放所有item
    private List<Item> weaponItem = new List<Item>();        //存放具体种类的item
    private List<Item> eqipmentItem = new List<Item>();

    private List<ItemUI> allItemUI = new List<ItemUI>();  //实所有物品栏ItemUI
    private List<ItemUI> weaponItemUI = new List<ItemUI>();
    private List<ItemUI> eqipmentItemUI = new List<ItemUI>();

    enum SelectedTypes
    {
        All,
        Weapon,
        Equipment
    }

    private SelectedTypes selectedType=SelectedTypes.All;
    private int selectedItem = -1;


    //test
    private List<Item> testItem = new List<Item>();

    void Awake()
    {
        InitAllGrid();
        Instance = this;
        //test

        for(int i=0;i<10;i++)
        {
            testItem.Add(new Item(i+1, "短剑", Item.ItemType.Weapon, Random.Range(1,6), "一把短剑", "Sprites/sword"));
            testItem.Add(new Item(i+1, "盔甲", Item.ItemType.Equipment, Random.Range(1, 6), "一件胸甲", "Sprites/armor"));
        }
        AddItems(testItem);
    }

    void Update()
    {
        //test
        if(Input.GetKeyDown(KeyCode.A))
        {
            int index = Random.Range(0, 2);
            Item toAddItem = testItem[index];
            AddItem(toAddItem);
        }

    }

    //添加单个物品
    public string  AddItem(Item toAddItem)
    {
        string resultStr = "得到"+toAddItem.Name;
        if(usedGridCount + 1 > gridCount)
        {
            resultStr="背包容量不足！";
        }
        else
        {
            allItem.Add(toAddItem);
            usedGridCount++;

            //每次添加物品时要更新物品栏
            SetItemUI(toAddItem);
        }
        Debug.Log(resultStr);
        return resultStr;
    }

    //添加多个物品
    public string AddItems(List<Item> toAddItemList)
    {
        string resultStr=string.Empty;
        StringBuilder stringBuilder = new StringBuilder();

        if (usedGridCount + 1 > gridCount)
        {
            resultStr= "背包容量不足！";
        }

        else
        {
            foreach (Item item in toAddItemList)
            {
                allItem.Add(item);
                stringBuilder.AppendLine(string.Format("得到{0}", item.Name));
                usedGridCount++;

                //每次添加物品时要更新物品栏
                SetItemUI(item);

                if (usedGridCount + 1 > gridCount)
                {
                    break;
                }
            }
            resultStr = stringBuilder.ToString();
        }
        Debug.Log(resultStr);
        return resultStr;
    }

    //移除物品
    public void RemoveItem(Item itemToRemove)
    {
        //test
        Debug.Log("选中："+selectedItem);

        RemoveItemUI();
        usedGridCount--;
        switch (selectedType)
        {
            case SelectedTypes.All:
                allItem.RemoveAt(selectedItem);
                foreach (Transform t in allGridsList)
                {
                    if (t.childCount > 0)
                    {
                        Destroy(t.GetChild(0).gameObject);
                        t.DetachChildren();
                    }
                }

                allItemUI.Clear();
                foreach (Item item in allItem)
                {
                    SetItemUI(item);
                }
                //test
                Debug.Log("allItem长度：" + allItem.Count);
                break;
            case SelectedTypes.Equipment:
            case SelectedTypes.Weapon:
                int i, j = 0;
                for (i = 0; i < allItem.Count; i++)
                {
                    if (allItem[i].Type == itemToRemove.Type)
                    {
                        j++;
                        if (j == selectedItem + 1)
                        {
                            break;
                        }
                    }
                }
                allItem.RemoveAt(i);
                break;
        }
        selectedItem = -1;
    }

    //更新具体种类物品list
    private void UpdateOtherItemList()
    {
        foreach(Item item in allItem)
        {
            if(item.Type==Item.ItemType.Equipment)
            {
                eqipmentItem.Add(item);
            }
            else
            {
                weaponItem.Add(item);
            }
        }
    }

    //生成所有物品栏物品格子
    private void InitAllGrid()
    {
        for (int i = 0; i < gridCount; i++)
        {
            GameObject t = Instantiate(gridPrefab) as GameObject;
            t.transform.SetParent(allContent);
            allGridsList.Add(t.transform);
        }
    }

    //添加所有物品格子
    public void AddAllGrid(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject t = Instantiate(gridPrefab) as GameObject;
            t.transform.SetParent(allContent);
            allGridsList.Add(t.transform);
        }
    }

    private Transform GetEmptyGrid()
    {
        for (int i = 0; i < allGridsList.Count; i++)
        {
            if (allGridsList[i].childCount == 0)
            {
                return allGridsList[i];
            }
        }
        return null;
    }

    //生成具体种类物品栏物品格子
    private Transform AddOtherGrid()
    {
        Transform t = Instantiate(gridPrefab).transform;
        //otherGridsList.Add(t);
        t.SetParent(otherContent);
        return t;
    }

    //生成物品UI
    private void SetItemUI(Item toAddItem)
    {
        Transform emptyGrid;
        GameObject item = Instantiate(itemPrefab);
        ItemUI itemUI = item.GetComponent<ItemUI>();

        switch (selectedType)
        {
            case SelectedTypes.All:
                emptyGrid = GetEmptyGrid();
                //test
                if (emptyGrid == null) Debug.Log("背包容量不足！");

                item.transform.SetParent(emptyGrid);
                item.transform.localPosition = Vector3.zero;
                itemUI.SetInfo(toAddItem, allItemUI.Count);
                allItemUI.Add(itemUI);
                break;
            case SelectedTypes.Equipment:
                if (toAddItem.Type == Item.ItemType.Equipment)
                {
                    emptyGrid = AddOtherGrid();
                    item.transform.SetParent(emptyGrid);
                    item.transform.localPosition = Vector3.zero;
                    itemUI.SetInfo(toAddItem, eqipmentItemUI.Count);
                    eqipmentItemUI.Add(itemUI);
                }
                break;
            case SelectedTypes.Weapon:
                {
                    if (toAddItem.Type == Item.ItemType.Weapon)
                    {
                        emptyGrid = AddOtherGrid();
                        item.transform.SetParent(emptyGrid);
                        item.transform.localPosition = Vector3.zero;
                        itemUI.SetInfo(toAddItem, weaponItemUI.Count);
                        weaponItemUI.Add(itemUI);
                    }
                }
                break;
        }
        itemUI.PointerClick += ShowItemInfo;
    }

    //移除物品UI
    private void RemoveItemUI()
    {
        if(selectedItem<0)
        {
            return;
        }
        switch(selectedType)
        {
            case SelectedTypes.All:
                allItemUI[selectedItem].transform.parent.DetachChildren();
                Destroy(allItemUI[selectedItem].gameObject);
                for(int i= selectedItem+1;i< allItemUI.Count;i++)
                {
                    allItemUI[i].SetId();
                }
                allItemUI.RemoveAt(selectedItem);
                break;
            case SelectedTypes.Equipment:
                Destroy(eqipmentItemUI[selectedItem].transform.parent.gameObject);
                for (int i = selectedItem + 1; i < eqipmentItemUI.Count; i++)
                {
                    eqipmentItemUI[i].SetId();
                }
                eqipmentItemUI.RemoveAt(selectedItem);
                break;
            case SelectedTypes.Weapon:
                Destroy(weaponItemUI[selectedItem].transform.parent.gameObject);   
                for (int i = selectedItem + 1; i < weaponItemUI.Count; i++)
                {
                    weaponItemUI[i].SetId();
                }
                weaponItemUI.RemoveAt(selectedItem);
                break;
        }
    }

    private void ShowItemInfo(Item toShowItem,int id)
    {
        ItemInfo.ShowInfo(toShowItem);
        selectedItem = id;
    }

    public void AllBtnOnClick()
    {
        mainScrollView.SetActive(true);
        otherScrollView.SetActive(false);
        if(selectedType!= SelectedTypes.All)
        {
            selectedType = SelectedTypes.All;
            foreach (Transform t in allGridsList)
            {
                if (t.childCount > 0)
                {
                    Destroy(t.GetChild(0).gameObject);
                    t.DetachChildren();
                }
            }

            allItemUI.Clear();
            foreach (Item item in allItem)
            {
                SetItemUI(item);
            }
        }
    }

    public void WeaponBtnOnClick()
    {
        weaponItem.Clear();
        UpdateOtherItemList();
        mainScrollView.SetActive(false);
        otherScrollView.SetActive(true);
        if (selectedType != SelectedTypes.Weapon)
        {
            selectedType = SelectedTypes.Weapon;

            foreach (Transform t in otherContent)
            {
                Destroy(t.gameObject);
            }

            weaponItemUI.Clear();
            foreach (Item item in weaponItem)
            {
                SetItemUI(item);
            }
        }
    }

    public void EquipmentBtnOnClick()
    {
        eqipmentItem.Clear();
        UpdateOtherItemList();
        mainScrollView.SetActive(false);
        otherScrollView.SetActive(true);
        if(selectedType != SelectedTypes.Equipment)
        {
            selectedType = SelectedTypes.Equipment;

            foreach (Transform t in otherContent)
            {
                Destroy(t.gameObject);
            }

            eqipmentItemUI.Clear();
            foreach (Item item in eqipmentItem)
            {
                SetItemUI(item);
            }
        }
    }

    public void OrganizeBtnOnClick()
    {
        for(int i=allItem.Count-2;i>=0;i--)
        {
            for(int j=0;j<=i;j++)
            {
                if(allItem[j].Type==Item.ItemType.Equipment&& allItem[j+1].Type == Item.ItemType.Weapon)
                {
                    Item temp;
                    temp = allItem[j];
                    allItem[j] = allItem[j + 1];
                    allItem[j + 1] = temp;
                }
                else if(allItem[j].Type == allItem[j + 1].Type)
                {
                    if(allItem[j].LV< allItem[j + 1].LV)
                    {
                        Item temp;
                        temp = allItem[j];
                        allItem[j] = allItem[j + 1];
                        allItem[j + 1] = temp;
                    }
                }
            }
        }

        switch(selectedType)
        {
            case SelectedTypes.All:
                foreach (Transform t in allGridsList)
                {
                    if (t.childCount > 0)
                    {
                        Destroy(t.GetChild(0).gameObject);
                        t.DetachChildren();
                    }
                }

                allItemUI.Clear();
                foreach (Item item in allItem)
                {
                    SetItemUI(item);
                }
                break;
            case SelectedTypes.Weapon:
                foreach (Transform t in otherContent)
                {
                    Destroy(t.gameObject);
                }

                weaponItemUI.Clear();
                foreach (Item item in weaponItem)
                {
                    SetItemUI(item);
                }
                break;
            case SelectedTypes.Equipment:
                foreach (Transform t in otherContent)
                {
                    Destroy(t.gameObject);
                }

                eqipmentItemUI.Clear();
                foreach (Item item in eqipmentItem)
                {
                    SetItemUI(item);
                }
                break;
        }
    }
}
