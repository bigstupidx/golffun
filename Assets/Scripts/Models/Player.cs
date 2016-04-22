using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player 
{
	public int score = 0;
    
	public int stars = 0;
    private string playerID = string.Empty;

	public BaseItem[] CharactersList;
	public BaseItem[] LevelsList;
	public BaseItem[] DressesList;
	public BaseItem[] HairsList;
	public BaseItem[] ShoesList;
    public bool isReadyForTurn;
    public int[] roundScores;

    private string avatarName;

    private int textureShirtIndex;
    private int textureShoeIndex;
    private int textureShortIndex;

	private int textureHatsIndex;
	private int textureNecklaceIndex;
	private int textureGlassesIndex;

    
    private int bumpShirtIndex;
    private int bumpShoeIndex;
    private int bumpShortIndex;

    private string mesh;
    
    public int price;
    public bool isLocked;

	public int playerRound;

    public Player(string name)
	{
       
        score = Score;
        roundScores = new int[11];
        isReadyForTurn = false;
        AvatarName = name;
		playerRound = 0;



	}

    public int Score
    {
        get
        {
            if (!PlayerPrefs.HasKey(AvatarName + "_score"))
            {
                PlayerPrefs.SetInt(AvatarName + "_score", 100);
            }
            score = PlayerPrefs.GetInt(AvatarName + "_score");
            return score;
        }
        set
        {
            score = value;
            PlayerPrefs.SetInt(AvatarName + "_score", value);
        }
    }

   

    public string AvatarName
    {
        get
        {
            if (!PlayerPrefs.HasKey("avatarName"))
            {
                PlayerPrefs.SetString("avatarName", "");
            }
            avatarName = PlayerPrefs.GetString("avatarName");
            return avatarName;
        }
        set
        {
            avatarName = value;
            PlayerPrefs.SetString("avatarName", value);
        }
    }

    public string Mesh
    {
        get
        {
            if (!PlayerPrefs.HasKey(AvatarName + "mesh"))
            {
                PlayerPrefs.SetString(AvatarName + "mesh", "");
            }
            mesh = PlayerPrefs.GetString(AvatarName + "mesh");
            return mesh;
        }
        set
        {
            mesh = value;
            PlayerPrefs.SetString(AvatarName + "mesh", value);
        }
    }

    // name_avatarSettings = texture_bumps_mesh >> only indexes of meshes, bumps and textures
    public int TextureShirtIndex
    { 
         get
        {
            if (!PlayerPrefs.HasKey(AvatarName + "textureShirtIndex"))
            {
                PlayerPrefs.SetInt(AvatarName + "textureShirtIndex", 0);
            }
            textureShirtIndex = PlayerPrefs.GetInt(AvatarName + "textureShirtIndex");
            return textureShirtIndex;
        }
        set
        {
            textureShirtIndex = value;
            PlayerPrefs.SetInt(AvatarName + "textureShirtIndex", value);
        }
    }
	public int TextureHatsIndex
	{ 
		get
		{
			if (!PlayerPrefs.HasKey(AvatarName + "textureHatsIndex"))
			{
				PlayerPrefs.SetInt(AvatarName + "textureHatsIndex", 0);
			}
			textureHatsIndex = PlayerPrefs.GetInt(AvatarName + "textureHatsIndex");
			return textureHatsIndex;
		}
		set
		{
			textureHatsIndex= value;
			PlayerPrefs.SetInt(AvatarName + "textureHatsIndex", value);
		}
	}

	public int TextureGlassesIndex
	{ 
		get
		{
			if (!PlayerPrefs.HasKey(AvatarName + "textureGlassesIndex"))
			{
				PlayerPrefs.SetInt(AvatarName + "textureGlassesIndex", 0);
			}
			textureGlassesIndex = PlayerPrefs.GetInt(AvatarName + "textureGlassesIndex");
			return textureGlassesIndex;
		}
		set
		{
			textureGlassesIndex= value;
			PlayerPrefs.SetInt(AvatarName + "textureGlassesIndex", value);
		}
	}

	public int TextureNecklacesIndex
	{ 
		get
		{
			if (!PlayerPrefs.HasKey(AvatarName + "textureNecklacesIndex"))
			{
				PlayerPrefs.SetInt(AvatarName + "textureNecklacesIndex", 0);
			}
			textureNecklaceIndex = PlayerPrefs.GetInt(AvatarName + "textureNecklacesIndex");
			return textureNecklaceIndex ;
		}
		set
		{
			textureNecklaceIndex  = value;
			PlayerPrefs.SetInt(AvatarName + "textureNecklacesIndex", value);
		}
	}

    public int BumpShirtIndex
    {
        get
        {
            if (!PlayerPrefs.HasKey(AvatarName + "bumpShirtIndex"))
            {
                PlayerPrefs.SetInt(AvatarName + "bumpShirtIndex", 0);
            }
            bumpShirtIndex = PlayerPrefs.GetInt(AvatarName + "bumpShirtIndex");
            return bumpShirtIndex;
        }
        set
        {
            bumpShirtIndex = value;
            PlayerPrefs.SetInt(AvatarName + "bumpShirtIndex", value);
        }
    }



    public int TextureShoesIndex
    {
        get
        {
            if (!PlayerPrefs.HasKey(AvatarName + "textureShoesIndex"))
            {
                PlayerPrefs.SetInt(AvatarName + "textureShoesIndex", 0);
            }
            textureShoeIndex = PlayerPrefs.GetInt(AvatarName + "textureShoesIndex");
            return textureShoeIndex;
        }
        set
        {
            textureShoeIndex = value;
            PlayerPrefs.SetInt(AvatarName + "textureShoesIndex", value);
        }
    }



    public int BumpShoesIndex
    {
        get
        {
            if (!PlayerPrefs.HasKey(AvatarName + "bumpShoesIndex"))
            {
                PlayerPrefs.SetInt(AvatarName + "bumpShoesIndex", 0);
            }
            bumpShoeIndex = PlayerPrefs.GetInt(AvatarName + "bumpShoesIndex");
            return bumpShoeIndex;
        }
        set
        {
            bumpShoeIndex = value;
            PlayerPrefs.SetInt(AvatarName + "bumpShoesIndex", value);

        }
    }



    public int TextureShortsIndex
    {
        get
        {
            if (!PlayerPrefs.HasKey(AvatarName + "textureShortsIndex"))
            {
                PlayerPrefs.SetInt(AvatarName + "textureShortsIndex", 0);
            }
            textureShortIndex = PlayerPrefs.GetInt(AvatarName + "textureShortsIndex");
            return textureShortIndex;
        }
        set
        {
            textureShortIndex = value;
            PlayerPrefs.SetInt(AvatarName + "textureShortsIndex", value);

        }
    }

    public int BumpShortsIndex
    {
        get
        {
            if (!PlayerPrefs.HasKey(AvatarName + "bumpShortsIndex"))
            {
                PlayerPrefs.SetInt(AvatarName + "bumpShortsIndex", 0);
            }
            bumpShortIndex = PlayerPrefs.GetInt(AvatarName + "bumpShortsIndex");
            return bumpShortIndex;
        }
        set
        {
            bumpShortIndex = value;
            PlayerPrefs.SetInt(AvatarName + "bumpShortsIndex", value);

        }
    }

}


	



