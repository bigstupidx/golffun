using UnityEngine;
using System.Collections;

public class SwitchFranela : MonoBehaviour
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
			player.TextureShirtIndex = index;
			player.BumpShirtIndex = index;
		}
		
		Debug.Log(SMRenderer);
		SMRenderer.materials[TexPosition].SetTexture("_MainTex", textures[player.TextureShirtIndex]);
		SMRenderer.materials[TexPosition].SetTexture("_BumpMap", bumps[player.BumpShirtIndex]);
		
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
            player.TextureShirtIndex = index;
            player.BumpShirtIndex = index;
        }

        Debug.Log(SMRenderer);
        SMRenderer.materials[TexPosition].SetTexture("_MainTex", textures[player.TextureShirtIndex]);
        SMRenderer.materials[TexPosition].SetTexture("_BumpMap", bumps[player.BumpShirtIndex]);

    }

}

