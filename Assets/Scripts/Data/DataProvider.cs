using UnityEngine;
using System.Collections;

public static class DataProvider
{
    public static ArrayList playerList;
    public static ArrayList playerDataList;
    public static ArrayList GetPlayerData()
    {
        //Save Data To Preferences for default objects to unlock initially

        if (playerDataList == null)
        {
            playerDataList = new ArrayList();
        }
        else
        {
            playerDataList.Clear();
        }
        playerDataList.Add(new PlayerData("Preem_Character_Tpose", 100, false));
        playerDataList.Add(new PlayerData("BenyBond_Character_Tpose", 200, true));
        playerDataList.Add(new PlayerData("Savy_Character_Tpose", 300, true));
        playerDataList.Add(new PlayerData("Taz_Character_Tpose", 400, true));
        return playerDataList;
    }
    public static ArrayList GetPlayerShirtsData(string name){
        if (playerList == null)
        {
            playerList = new ArrayList();
        }
        else {
            playerList.Clear();
        }
        if (name == "Preem_Character_Tpose")
        {
            playerList.Add(new PlayerItems(0, 10, false));
            playerList.Add(new PlayerItems(1, 20, true));
            playerList.Add(new PlayerItems(2, 30, true));
            playerList.Add(new PlayerItems(3, 40, true));
            playerList.Add(new PlayerItems(4, 50, true));
            playerList.Add(new PlayerItems(5, 60, true));
            playerList.Add(new PlayerItems(6, 80, true));
            playerList.Add(new PlayerItems(7, 90, true));
        }

        if (name == "BenyBond_Character_Tpose")
        {
            playerList.Add(new PlayerItems(0, 10, false));
            playerList.Add(new PlayerItems(1, 50, true));
            playerList.Add(new PlayerItems(2, 90, true));
            playerList.Add(new PlayerItems(3, 120, true));
            playerList.Add(new PlayerItems(4, 150, true));
            playerList.Add(new PlayerItems(5, 160, true));
            playerList.Add(new PlayerItems(6, 160, true));
            playerList.Add(new PlayerItems(7, 160, true));
            playerList.Add(new PlayerItems(8, 160, true));
            playerList.Add(new PlayerItems(9, 160, true));
        }

        if (name == "Savy_Character_Tpose")
        {
            playerList.Add(new PlayerItems(0, 20, false));
            playerList.Add(new PlayerItems(1, 40, true));
            playerList.Add(new PlayerItems(2, 60, true));
            playerList.Add(new PlayerItems(3, 80, true));
            playerList.Add(new PlayerItems(4, 100, true));
            playerList.Add(new PlayerItems(5, 120, true));
            playerList.Add(new PlayerItems(6, 160, true));
            playerList.Add(new PlayerItems(7, 160, true));
            playerList.Add(new PlayerItems(8, 160, true));
        }
        //
        if (name == "Taz_Character_Tpose")
        {
            playerList.Add(new PlayerItems(0, 10, false));
            playerList.Add(new PlayerItems(1, 25, true));
            playerList.Add(new PlayerItems(2, 65, true));
            playerList.Add(new PlayerItems(3, 85, true));
            playerList.Add(new PlayerItems(4, 101, true));
            playerList.Add(new PlayerItems(5, 121, true));
            playerList.Add(new PlayerItems(6, 160, true));
            playerList.Add(new PlayerItems(7, 160, true));
            playerList.Add(new PlayerItems(8, 160, true));
            playerList.Add(new PlayerItems(9, 160, true));
        }
        return playerList;
    }

}
public class PlayerItems {
    public int index;
    public int price;
    public bool isLocked;
    public PlayerItems(int indx, int prc, bool locked)
    {
        this.index = indx;
        this.price = prc;
        this.isLocked = locked;
    }
}
public class PlayerData
{
    public string id;
    public int price;
    public bool isLocked;
    public PlayerData(string id, int prc, bool locked)
    {
        this.id = id;
        this.price = prc;
        this.isLocked = locked;
    }
}

