using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	#region Singelton
	private static SoundManager _instance;

	public static SoundManager Instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<SoundManager>();
			}
			return _instance;
		}
	}
	#endregion

	public AudioSource oneShot;
	public AudioSource multipleBackgrounds;


	#region Take Five Clips
	public AudioClip afterHit;
	public AudioClip applause;
	public AudioClip bird;
	public AudioClip broken_bat;
	public AudioClip explodingbird;
	public AudioClip game_lost;
	public AudioClip golden_bat;
	public AudioClip got_heem;
	public AudioClip hole_made_1;
	public AudioClip hole_made_2_attagirl;
	public AudioClip hole_made_3;
	public AudioClip hole_made_4;
	public AudioClip hole_made_5;
	public AudioClip hole_made_6;
	public AudioClip lawnmower;
	public AudioClip lawnmower2;
	public AudioClip main_menu;
	public AudioClip pre_swing_1;
	public AudioClip pre_swing_2;
	public AudioClip pre_swing_3;
	public AudioClip pre_swing_4;
	public AudioClip pre_swing_5;
	public AudioClip pre_swing_6;
	public AudioClip pre_swing_7;
	public AudioClip pre_swing_8;
	public AudioClip pre_swing_9;
	public AudioClip sprinkler;
	public AudioClip storm;
	public AudioClip UFO;
	public AudioClip wind;
	public AudioClip arenaSound;
	public AudioClip arenaSound2;
	public AudioClip birdHitSound;


	public AudioClip teeShotApplause;
	public AudioClip teeShotPutt;


	#endregion

	public void Stop_BGM()
	{
		oneShot.Stop ();
		multipleBackgrounds.Stop ();
	}
	public void Play_afterHit()
	{
		oneShot.PlayOneShot (afterHit);
	}

	public void Play_teeShotApplause()
	{
		oneShot.PlayOneShot (teeShotApplause);
	}

	public void Play_PuttShotApplause()
	{
		oneShot.PlayOneShot (teeShotPutt);
	}
	

	public void Play_applause()
	{
		oneShot.PlayOneShot (applause);
	}

	public void Play_bird()
	{
		oneShot.PlayOneShot (bird);
	}

	public void Play_broken_bat()
	{
		if (!oneShot.isPlaying) {
			oneShot.PlayOneShot (broken_bat);
		}
	}

	public void Play_explodingbird()
	{
		oneShot.PlayOneShot (explodingbird);

	}

	public void Play_game_lost()
	{
		oneShot.PlayOneShot (game_lost);
	}

	public void Play_golden_bat()
	{
		oneShot.PlayOneShot (golden_bat);
	}

	public void Play_got_heem()
	{
		oneShot.PlayOneShot (got_heem);
	}

	public void Play_hole_made_1()
	{
		oneShot.PlayOneShot (hole_made_1);
	}

	public void Play_hole_made_2_attagirl()
	{
		oneShot.PlayOneShot (hole_made_2_attagirl);
	}

	public void Play_hole_made_3()
	{
		oneShot.PlayOneShot (hole_made_3);
	}

	public void Play_hole_made_4()
	{
		oneShot.PlayOneShot (hole_made_4);
	}

	public void Play_hole_made_5()
	{
		oneShot.PlayOneShot (hole_made_5);
	}

	public void Play_hole_made_6()
	{
		oneShot.PlayOneShot (hole_made_6);
	}

	public void Play_lawnmower()
	{
		oneShot.PlayOneShot (lawnmower);
	}

	public void Play_lawnmower2()
	{
		oneShot.PlayOneShot (lawnmower2);
	}

	public void Play_main_menu()
	{
		//oneShot.PlayOneShot (main_menu);
		oneShot.clip = main_menu;
		oneShot.Play ();


	
	}

	public void Play_pre_swing_1()
	{
		oneShot.PlayOneShot (pre_swing_1);
	}

	public void Play_pre_swing_2()
	{
		oneShot.PlayOneShot (pre_swing_2);
	}

	public void Play_pre_swing_3()
	{
		oneShot.PlayOneShot (pre_swing_3);
	}

	public void Play_pre_swing_4()
	{
		oneShot.PlayOneShot (pre_swing_4);
	}

	public void Play_pre_swing_5()
	{
		oneShot.PlayOneShot (pre_swing_5);
	}

	public void Play_pre_swing_6()
	{
		oneShot.PlayOneShot (pre_swing_6);
	}

	public void Play_pre_swing_7()
	{
		oneShot.PlayOneShot (pre_swing_7);
	}

	public void Play_pre_swing_8()
	{
		oneShot.PlayOneShot (pre_swing_8);
	}

	public void Play_pre_swing_9()
	{
		oneShot.PlayOneShot (pre_swing_9);
	}

	public void Play_sprinkler()
	{
		oneShot.PlayOneShot (sprinkler);
	}

	public void Play_storm()
	{
		oneShot.PlayOneShot (storm);
	}

	public void Play_UFO()
	{
		oneShot.PlayOneShot (UFO);
	}

	public void Play_wind()
	{
		oneShot.PlayOneShot (wind);
	}
	public void Play_birdHitSound()
	{
		oneShot.PlayOneShot (birdHitSound);
	}
	public void Play_ArenaSounds()
	{
		Play_ArenaSound2 ();

		oneShot.clip = arenaSound;
		oneShot.Play ();
		oneShot.loop=true;

	}

	public void Play_ArenaSound2()
	{
//		multipleBackgrounds.clip = arenaSound;
		multipleBackgrounds.Play ();
		multipleBackgrounds.loop=true;
	}

	public void muteSounds(){

		oneShot.volume = 0f;
		multipleBackgrounds.volume = 0f;
	
	}

	public void UnMuteSounds(){
		
		oneShot.volume = 100f;
		multipleBackgrounds.volume = 75.0f;
		
	}

}
