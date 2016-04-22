using UnityEngine;
using System.Collections;

public class SwitchNecklaces : MonoBehaviour {
	
	private int index = 0;
	public GameObject[] gameObjects;

	


	void Start () {

		hideAll ();
	
		gameObjects [0].SetActive (true);



	} 
	public void ChangeTexInt(Player player, bool loadConfigurations, int index)
	{
//		if (!loadConfigurations)
//		{
//			player.TextureShirtIndex = index;
//			player.BumpShirtIndex = index;
//		}
//		
//		Debug.Log(SMRenderer);
//		SMRenderer.materials[TexPosition].SetTexture("_MainTex", textures[player.TextureShirtIndex]);
//		meshfilter.mesh = meshes [index];
		hideAll ();
		gameObjects [index].SetActive (true);


	}
	
	public void hideAll(){


		for(int i=0;i<gameObjects.Length;i++)
			gameObjects [i].SetActive (false);

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
			if(index >= gameObjects.Length)
			{
				index = gameObjects.Length - 1;
			}
			player.TextureNecklacesIndex = index;
//			player.BumpShirtIndex = index;
		}

		hideAll ();
		gameObjects [player.TextureNecklacesIndex].SetActive (true);


	}
	
}

