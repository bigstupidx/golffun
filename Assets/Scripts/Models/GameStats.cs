using UnityEngine;
using System.Collections;



public class GameStats : MonoBehaviour {
    public int coins = 0;
    private int levelofSelectedbuilding = 0;
    public string selectedBuildingId = string.Empty;
    public int round = 0;
    public int shots = 0;
    public int playerNoIsPlaying = 0;
    public string player = string.Empty;
    public int totalGames = 0;
    

    void Start () { 
        this.coins = Coins;
        Round = this.round;
        Shots = this.shots;
        this.player = PlayerName;
        this.totalGames = TotalGames;
    }

    public void ResetGame() {
        playerNoIsPlaying = 0;
        Round = 0;
        Shots = 0;
        this.coins = Coins;
        this.totalGames = TotalGames;
    }

    public int Coins
    {
        get
        {
            if (!PlayerPrefs.HasKey("Coins"))
            {
                PlayerPrefs.SetInt("Coins", 100);
            }
			coins = PlayerPrefs.GetInt("Coins");
            return coins;
        }
        set
        {
            coins = value;
            PlayerPrefs.SetInt("Coins", value);
        }
    }

    public int Round
    {
        get
        {
            if (!PlayerPrefs.HasKey("Round"))
            {
                PlayerPrefs.SetInt("Round", 0);
            }
            round = PlayerPrefs.GetInt("Round");
            return round;
        }
        set
        {
            round = value;
            PlayerPrefs.SetInt("Round", value);
        }
    }

    public int Shots
    {
        get
        {
            if (!PlayerPrefs.HasKey("Shots"))
            {
                PlayerPrefs.SetInt("Shots", 0);
            }
            shots = PlayerPrefs.GetInt("Shots");
            return shots;
        }
        set
        {
            shots = value;
            PlayerPrefs.SetInt("Shots", value);
        }
    }


    public int TotalGames
    {
        get
        {
            if (!PlayerPrefs.HasKey("TotalGames"))
            {
                PlayerPrefs.SetInt("TotalGames", 0);
            }
            totalGames = PlayerPrefs.GetInt("TotalGames");
            return totalGames;
        }
        set
        {
            totalGames = value;
            PlayerPrefs.SetInt("TotalGames", value);
        }
    }



    public string PlayerName
    {
        get
        {
            if (!PlayerPrefs.HasKey("PlayerName"))
            {
                player = "Nauman";
                PlayerPrefs.SetString("PlayerName", player);
            }
            else {
                if (player.Length == 0)
                {
                    player = PlayerPrefs.GetString("PlayerName");
                }
            }
            return player;
        }
    }
}
