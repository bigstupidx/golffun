using UnityEngine;
using System.Collections;

public class SwitchGlasses : MonoBehaviour {
	
	public int TexPosition;
	public Texture[] textures;
	private int index = 0;
	public Mesh[] meshes;
	public GameObject[] gameObjects;
	
	public MeshRenderer SMRenderer;
	public MeshFilter meshfilter;
	

	public void ChangeTexInt(Player player, bool loadConfigurations, int index)
	{
		if (!loadConfigurations)
		{
			player.TextureGlassesIndex = index;
		//	player.BumpShirtIndex = index;
		}
		
		Debug.Log(SMRenderer);
		SMRenderer.materials[TexPosition].SetTexture("_MainTex", textures[player.TextureGlassesIndex]);
		meshfilter.mesh = meshes [player.TextureGlassesIndex];
		//		SMRenderer.materials[TexPosition].SetTexture("_BumpMap", bumps[player.BumpShirtIndex]);
		gameObjects [player.TextureGlassesIndex].SetActive (true);
		
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
			player.TextureGlassesIndex= index;
			//player.BumpShirtIndex = index;
		}
		
		Debug.Log(SMRenderer);
		SMRenderer.materials[TexPosition].SetTexture("_MainTex", textures[player.TextureGlassesIndex]);
		meshfilter.mesh = meshes [player.TextureGlassesIndex];
		
		//		SMRenderer.materials[TexPosition].SetTexture("_BumpMap", bumps[player.BumpShirtIndex]);
//		gameObjects [index].SetActive (true);
		
	}
	
}

