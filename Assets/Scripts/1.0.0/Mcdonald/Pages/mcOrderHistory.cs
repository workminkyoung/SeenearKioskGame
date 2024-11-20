using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class mcOrderHistory : PageBase
{
    public GameObject orderItem;
    List<TextMeshProUGUI> _listText = new List<TextMeshProUGUI>();
    List<Button> _listBtn = new List<Button>();
    List<McOrderPrefab> _listOrder = new List<McOrderPrefab>();
    Transform _menuViwer;

    public override void Setting()
    {
        base.Setting();
        _listText.AddRange(UtilityExtensions.GetComponentsOnlyInChildren_NonRecursive<TextMeshProUGUI>(transform));
        _listBtn.AddRange(GetComponentsInChildren<Button>());
        _menuViwer = GetComponentInChildren<ContentSizeFitter>().transform;

        _listBtn[(int)eBtn.Add].onClick.AddListener(() => SwitchPage(ePage.MenuSelect));
        _listBtn[(int)eBtn.Complete].onClick.AddListener(() => NextPage());
    }

    void Init()
    {
        for (int i = 0; i < _listOrder.Count; i++)
        {
            Destroy(_listOrder[i].gameObject);
        }
        _listOrder.Clear();
    }

    public override void Show(bool state)
    {
        base.Show(state);
        if (state)
        {
            Init();

            for (int i = 0; i < McdonaldManager._selectMenu.listSetMenu.Count; i++)
            {
                CreateOrder(McdonaldManager._selectMenu.listSetMenu[i]);
            }
            for (int i = 0; i < McdonaldManager._selectMenu.listSingleMenu.Count; i++)
            {
                CreateOrder(McdonaldManager._selectMenu.listSingleMenu[i]);
            }

            _listText[(int)eText.Cost].text = McdonaldManager.Price.ToString();
        }
    }

    void CreateOrder(McMenuDetail menu)
    {
        GameObject clone = Instantiate(orderItem, _menuViwer);
        McOrderPrefab order = clone.GetComponent<McOrderPrefab>();

        order.Setting(menu, () =>
        {
            McdonaldManager.RemoveMenu(menu);
            _listOrder.Remove(order);
            Destroy(clone);
        });
    }

    void CreateOrder(McSetMenuDetail menu)
    {
        GameObject clone = Instantiate(orderItem, _menuViwer);
        McOrderPrefab order = clone.GetComponent<McOrderPrefab>();

        order.Setting(menu, () =>
        {
            McdonaldManager.RemoveMenu(menu);
            _listOrder.Remove(order);
            Destroy(clone);
        });
    }

    enum eText
    {
        Title = 0,
        Cost
    }

    enum eBtn
    {
        Add = 0,
        Complete
    }
}
