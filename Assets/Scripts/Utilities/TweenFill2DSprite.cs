using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UI2DSprite))]
[AddComponentMenu("Tkxel/Tween Fill")]
public class TweenFill2DSprite : UITweener
{
#if UNITY_3_5
	public float from = 1f;
	public float to = 1f;
#else
    [Range(0f, 1f)]
    public float from = 1f;
    [Range(0f, 1f)]
    public float to = 1f;
#endif

    UI2DSprite slider;



    //UI2DSprite slider;

    public UI2DSprite GetSlider
    {
        get
        {
            if (slider == null)
            {
				slider = GetComponent<UI2DSprite>();
                if (slider == null) slider = GetComponent<UI2DSprite>();
            }
            return slider;
        }
    }

    [System.Obsolete("Use 'value' instead")]
    public float alpha { get { return this.value; } set { this.value = value; } }

    /// <summary>
    /// Tween's current value.
    /// </summary>

	public float value { get { return GetSlider.fillAmount; } set { GetSlider.fillAmount = value; } }

    /// <summary>
    /// Tween the value.
    /// </summary>

    protected override void OnUpdate(float factor, bool isFinished) { value = Mathf.Lerp(from, to, factor); }

	static object UITweener {
		get;
		set;
	}

	static TweenFill comp;

    /// <summary>
    /// Start the tweening operation.
    /// </summary>

    static public TweenFill Begin(GameObject go, float duration, float alpha)
    {
		comp = TweenFill.Begin<TweenFill> (go, duration);
        comp.from = comp.value;
        comp.to = alpha;

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
