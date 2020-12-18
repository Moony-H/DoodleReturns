
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public enum Item // 아이템의 타입.
{
    ink,
    hammer,
    cutter,
    tape
}

public class ItemSystem : MonoBehaviour
{
    public static ItemSystem instance;

    [System.Serializable]
    public class HaveItem
    {
        public string name; // 무슨 값을 설정해도 itemtype의 string값으로 초기화함.
        public Item itemType;
        public int count;
        public Sprite itemImg;
        public bool canbuy = false;

        [Header("상점 옵션"), Range(0, 20)]
        public int ink_price;
    }

    public List<HaveItem> itemList;

    public GameObject itemUILayout;
    public GameObject itemUIObj;
    public List<GameObject> itemUIObjList;

    public void Awake()
    {
        instance = this;
        for (int i = 0; i < itemList.Count; i++)
        {
            itemList[i].name = itemList[i].itemType.ToString();
        }
        ItemUIObj_Reflash(true);
    }

    private void Update()
    {
        int sceneidx = SceneManager.GetActiveScene().buildIndex;
        itemUILayout.SetActive(4 <= sceneidx && sceneidx <= 6);
    }

    public void CanUseItem(List<Item> _list)
    {
        for (int i = 0; i < itemList.Count; i++)
            itemList[i].canbuy = itemList[i].itemType == Item.ink;

        for(int i = 0; i < _list.Count; i++)
        {
            ItemFind(_list[i]).canbuy = true;
        }
    }

    public HaveItem ItemFind(Item _type) // 아이템을 리스트에서 찾아서 리턴
    {
        HaveItem result = null;
        for(int i = 0; i < itemList.Count; i++)
        {
            if(itemList[i].itemType == _type)
            {
                result = itemList[i];
            }
        }
        return result;
    }

    public void ItemReset(int _ct = 0) // 아이템 개수를 모두 초기화 합니다.
    {
        for(int i = 0; i < itemList.Count; i++)
        {
            itemList[i].count = _ct;
        }
        ItemUIObj_Reflash();
    }

    public void ItemGet(Item _type, int _ct = 1) // 아이템을 얻습니다.
    {
        ItemFind(_type).count += _ct;
        ItemUIObj_Reflash();
    }

    public bool ItemUse(Item _type, int _ct = 1) // 아이템을 사용합니다. (기본값 : 1개 사용)
    {
        var target = ItemFind(_type);
        if (target.count < _ct) return false;
        ItemFind(_type).count -= _ct;
        ItemUIObj_Reflash();
        return true;
    }

    public void ItemMake(string _wantItemName)
    {
        ItemMake((Item)System.Enum.Parse(typeof(Item), _wantItemName), 1);
    }

    public bool ItemMake(Item _wantItem, int _ct = 1) // 아이템을 제작합니다.
    {
        bool result = ItemUse(Item.ink, ItemFind(_wantItem).ink_price * _ct);
        if (result)
        {
            ItemGet(_wantItem, _ct);
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlayMakeSound();
        }
        return result;
    }


    /********** 아이템 UI ***********/
    public void ItemUIObj_Show(bool _show = true) // 아이템 UI를 보여줍니다.
    {
        itemUILayout.SetActive(_show);
    }

    //string uiobjname = "";
    public void ItemUIObj_Reflash(bool _firstActive = false) // 아이템 UI를 새로고침합니다. (아이템 사용 시 바로 적용될 수 있도록)
    {
        for(int i = 0; i < itemList.Count; i++)
        {
            var target = itemList[i];
            GameObject obj = null;

            for (int j = 0; j < itemUIObjList.Count; j++)
            {
                if (itemUIObjList[j].name == target.name)
                {
                    obj = itemUIObjList[j];
                    if(_firstActive)
                        obj.SetActive(target.count > 0);
                }
            }

            if (target.canbuy && obj == null) 
                // 아이템의 개수가 1개 이상인데 그에 해당하는 객체가 없을 경우 새로 생성.
            {
                obj = Instantiate(itemUIObj, Vector3.zero, Quaternion.identity);
                obj.name = target.name;
                obj.transform.SetParent(itemUILayout.transform);
                Button btn = obj.transform.Find("ItemImage").GetComponent<Button>();
                if(obj.name != Item.ink.ToString())
                {
                    string uiobjname = obj.name;
                    btn.onClick.AddListener(delegate () { ItemMake(uiobjname); });
                }
                itemUIObjList.Add(obj);
            }

            if (obj == null) continue;

            obj.transform.Find("ItemText").GetComponent<Text>().text = "×" + target.count;
            obj.transform.Find("ItemImage").GetComponent<Image>().sprite = target.itemImg;

            //if(target.count == 0)
            //    obj.
        }

    }

}
