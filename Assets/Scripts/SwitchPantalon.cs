using UnityEngine;
using System.Collections;

public class SwitchPantalon : MonoBehaviour
{

    public int TexPosition;
    public Texture[] textures;
    public Texture[] bumps;
    private int index = 0;


    public SkinnedMeshRenderer SMRenderer;

    // Use this for initialization
    void Start()
    {
        SMRenderer = GetComponent<SkinnedMeshRenderer>();
    }

	public void ChangeTexInt(Player player, bool loadConfigurations, int index)
	{

		if (!loadConfigurations)
		{

			player.TextureShortsIndex = index;
			player.BumpShortsIndex = index;
		}
		
		
		SMRenderer.materials[TexPosition].SetTexture("_MainTex", textures[player.TextureShortsIndex]);
		SMRenderer.materials[TexPosition].SetTexture("_BumpMap", bumps[player.BumpShortsIndex]);

	}
	public void ChangeTex(Player player, bool loadConfigurations, bool isLeftIndex)
    {
        if (!loadConfigurations)
        {
			if(!isLeftIndex)
			{
				index++;
			}
			else
			{
				index--;
			}
			if (index < 0)
			{
				index = 0;
			}
			if(index >= textures.Length)
			{
				index = textures.Length - 1;
			}
            player.TextureShortsIndex = index;
            player.BumpShortsIndex = index;
        }
       

        SMRenderer.materials[TexPosition].SetTexture("_MainTex", textures[player.TextureShortsIndex]);
        SMRenderer.materials[TexPosition].SetTexture("_BumpMap", bumps[player.BumpShortsIndex]);

       
    }

}

