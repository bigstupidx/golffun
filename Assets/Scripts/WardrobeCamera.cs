using UnityEngine;
using System.Collections;

public class WardrobeCamera : MonoBehaviour {


    public Transform franelaPos;
    public Transform pantalonPos;
    public Transform zapatoPos;
	public Transform hatsPos;

    public Transform cameraPos;

    public void MoveFranela(){
        StopAllCoroutines();
        StartCoroutine(MoveToFranela());
    }

    public void MovePantalon(){
        StopAllCoroutines();
        StartCoroutine(MoveToPantalon());
    }

	public void MoveHats(){
		StopAllCoroutines();
		StartCoroutine(MoveToHats());
	}

	IEnumerator MoveToHats()
	{
		while (Vector3.Distance(cameraPos.position, hatsPos.position) > 0.1f)
		{
			cameraPos.position =  Vector3.Lerp(cameraPos.position, hatsPos.position, Time.deltaTime);
			yield return null;
		}
	}

    public void MoveZapato()
    {
        StopAllCoroutines();
        StartCoroutine(MoveToZapato());
    }

    IEnumerator MoveToFranela()
    {
        while (Vector3.Distance(cameraPos.position, franelaPos.position) > 0.1f)
        {
            cameraPos.position =  Vector3.Lerp(cameraPos.position, franelaPos.position, Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator MoveToPantalon()
    {

        while (Vector3.Distance(cameraPos.position, pantalonPos.position) > 0.1f)
        {
            cameraPos.position = Vector3.Lerp(cameraPos.position, pantalonPos.position, Time.deltaTime);
            yield return null;
        }

    }

    IEnumerator MoveToZapato()
    {

        while (Vector3.Distance(cameraPos.position, zapatoPos.position) > 0.1f)
        {
            cameraPos.position = Vector3.Lerp(cameraPos.position, zapatoPos.position, Time.deltaTime);
            yield return null;
        }

    }



}
