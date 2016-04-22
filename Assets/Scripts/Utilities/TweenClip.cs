using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIPanel))]
[AddComponentMenu("Tkxel/Tween Clip")]
public class TweenClip : UITweener
{
	#if UNITY_3_5
	public float from = 1f;
	public float to = 1f;
	#else
	//[Range(0f, 1f)]
	public float from = 1f;
	//[Range(0f, 1f)]
	public float to = 1f;
	#endif
	
	UIPanel slider;
	//UISlider slider;
	
	public UIPanel GetSlider
	{
		get
		{
			if (slider == null)
			{
				slider = GetComponent<UIPanel>();
				if (slider == null) slider = GetComponent<UIPanel>();
			}
			return slider;
		}
	}
	
	[System.Obsolete("Use 'value' instead")]
	public float offset { get { return this.value; } set { this.value = value; } }
	
	/// <summary>
	/// Tween's current value.
	/// </summary>
	
	public float value { get { return GetSlider.clipOffset.x; } set { 

			Vector2 temp = new Vector2(value, 0);
			GetSlider.clipOffset = temp; } 
	}
	
	/// <summary>
	/// Tween the value.
	/// </summary>
	
	protected override void OnUpdate(float factor, bool isFinished) { value = Mathf.Lerp(from, to, factor); }
	
	/// <summary>
	/// Start the tweening operation.
	/// </summary>
	
	static public TweenClip Begin(GameObject go, float duration, float clipOnThisValue)
	{
		TweenClip comp = UITweener.Begin<TweenClip>(go, duration);
		comp.from = comp.value;
		comp.to = clipOnThisValue;
		
		if (duration <= 0f)
		{
			comp.Sample(1f, true);
			comp.enabled = false;
		}
		return comp;
	}
	
	public override void SetStartToCurrentValue() { from = value; }
	public override void SetEndToCurrentValue() { to = value; }
}
