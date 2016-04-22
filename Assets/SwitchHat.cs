using UnityEngine;
using System.Collections;

public class SwitchHat: MonoBehaviour
{
	public int TexPosition;
	public Texture[] textures;
	private int index = 0;
	public Mesh[] meshes;
	public GameObject[] gameObjects;

	public MeshRenderer SMRenderer;
	public MeshFilter meshfilter;
	
	// Use this for initialization
	void Start()
	{

	//	SMRenderer = GetComponent<MeshRenderer>();
	//	meshfilter = GetComponent<MeshFilter> ();
	}
	
	public void ChangeTexInt(Player player, bool loadConfigurations, int index)
	{
		if (!loadConfigurations)
		{
			player.TextureHatsIndex = index;
		 //	player.BumpShirtIndex = index;
		}
		
		Debug.Log(SMRenderer);
		SMRenderer.materials[TexPosition].SetTexture("_MainTex", textures[player.TextureHatsIndex]);
		meshfilter.mesh = meshes [player.TextureHatsIndex];
		if (index == 8)
			SMRenderer.gameObject.transform.localPosition = new Vector3 (-0.43f, -0.08f, -0.01f);
		else if( index == 1 || index == 9 || index == 7)
			SMRenderer.gameObject.transform.localPosition = new Vector3 (-0.976f, 0.268f, -0.01f);
		else if (index == 4)
			 SMRenderer.gameObject.transform.localPosition = new Vector3 (-0.911f, 0.293f, -0.0071f);
		else SMRenderer.gameObject.transform.localPosition = new Vector3 (-0.976f, 0.088f, -0.0071f);
//		SMRenderer.materials[TexPosition].SetTexture("_BumpMap", bumps[player.BumpShirtIndex]);
		
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
			player.TextureHatsIndex = index;
		//	player.BumpShirtIndex = index;
		}
		
		Debug.Log(SMRenderer);
		SMRenderer.materials[TexPosition].SetTexture("_MainTex", textures[player.TextureHatsIndex]);
		meshfilter.mesh = meshes [player.TextureHatsIndex];

		if (index == 8)
			SMRenderer.gameObject.transform.localPosition = new Vector3 (-0.43f, -0.08f, -0.01f);
		else if( index == 1 || index == 9 || index == 7)
			SMRenderer.gameObject.transform.localPosition = new Vector3 (-0.976f, 0.268f, -0.01f);
		else if (index == 4)
			SMRenderer.gameObject.transform.localPosition = new Vector3 (-0.911f, 0.293f, -0.0071f);
		else SMRenderer.gameObject.transform.localPosition = new Vector3 (-0.976f, 0.088f, -0.0071f);

//		SMRenderer.materials[TexPosition].SetTexture("_BumpMap", bumps[player.BumpShirtIndex]);
		
	}
	
}

