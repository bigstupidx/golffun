using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour
{
    public GameObject featherParticles;
	public Animator animator;
    GameObject ball;
    bool isHit;
	bool hassoundplayed=false;
    // Use this for initialization
    void Start()
    {
        ball = GameObject.Find("Ball");
		animator = GetComponent<Animator> ();
     //   Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update() {
		transform.LookAt (ball.transform);
		if(!isHit)
		transform.Translate (Vector3.forward * 30.0f  *Time.deltaTime);
 }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Ball")
        {
            isHit = true;
			animator.SetTrigger("Hit");
      //      Instantiate(featherParticles, transform.position, Quaternion.identity);
     //       Destroy(featherParticles, 1f);
            ball.GetComponent<Rigidbody>().isKinematic = true;
            ball.GetComponent<Rigidbody>().isKinematic = false;
			featherParticles.SetActive(true);
			Destroy(gameObject, 0.5f);
			if(hassoundplayed==false){
			SoundManager.Instance.Play_birdHitSound();
				hassoundplayed=true;
			}
        }
    }
}