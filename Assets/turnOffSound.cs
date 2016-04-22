using UnityEngine;
using System.Collections;

public class turnOffSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		SoundManager.Instance.Stop_BGM ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
