using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class McdonaldProperties : MonoBehaviour
{
    public static McMenu _McMenu;
    public static string _McBrand = "mcdonald";
    public static List<string> _McListTitle = new List<string>();

    /// <summary>
    /// dont use this on "Awake"
    /// </summary>
    public static McMenuDetail FindMenu(eMcTitleList title, string name)
    {
        McItem item = _McMenu.items[(int)title];
        for (int i = 0; i < item.name.Length; i++)
        {
            if (item.name[i] == name)
            {
                McMenuDetail itemDetail = new McMenuDetail();
                itemDetail.title = item.title;
                itemDetail.name = item.name[i];
                itemDetail.price = item.price[i];
                itemDetail.index = i;
                itemDetail.image = DataManager.Instance.LoadItemSprite_spriteSheet(
                    _McBrand, itemDetail.title, itemDetail.index);

                return itemDetail;
            }
        }

        return null;
    }
}

[Serializable]
public class McItem
{
    public string title;
    public string[] name;
    public int[] price;
}

[Serializable]
public class McMenu
{
    public McItem[] items;
}

public class McMenuDetail
{
    public string title;
    public string name;
    public int price;
    public int index;
    public Sprite image;
}

public class McSetMenuDetail
{
    public McMenuDetail burger;
    public McMenuDetail side;
    public McMenuDetail drink;
}

public class McSelectMenu
{
    public List<McMenuDetail> listSingleMenu;
    public List<McSetMenuDetail> listSetMenu;
}

public enum eMcTitleList
{
    Burger = 0,
    HappySnack,
    Side,
    Coffee,
    Dessert,
    Drinks,
    HappyMeal,
    
    Set,
    LargeSet,

    SetSide,
    SetDrink
}

enum eMcItemElement
{
    title = 0,
    name,
    pric
}

public enum ePage
{
    Home = 0,
    HowToEat,
    MenuSelect,
    SetOrSingle,
    Option,
    ShoppingBasketConfirm,
    AddPopup,
    //LightBox,
    ShoppingMenuPush,
    OrderHistory,
    HowToPay,
    PayCard,
    PayMobileGift,
    OrderComplete,
    OrderComplete2//바뀔수있음
}