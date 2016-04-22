using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UISlider))]
[AddComponentMenu("Tkxel/Tween Fill")]
public class TweenFill : UITweener
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

    UISlider slider;
    //UISlider slider;

    public UISlider GetSlider
    {
        get
        {
            if (slider == null)
            {
                slider = GetComponent<UISlider>();
                if (slider == null) slider = GetComponent<UISlider>();
            }
            return slider;
        }
    }

    [System.Obsolete("Use 'value' instead")]
    public float alpha { get { return this.value; } set { this.value = value; } }

    /// <summary>
    /// Tween's current value.
    /// </summary>

    public float value { get { return GetSlider.value; } set { GetSlider.value = value; } }

    /// <summary>
    /// Tween the value.
    /// </summary>

    protected override void OnUpdate(float factor, bool isFinished) { value = Mathf.Lerp(from, to, factor); }

    /// <summary>
    /// Start the tweening operation.
    /// </summary>

    static public TweenFill Begin(GameObject go, float duration, float alpha)
    {
        TweenFill comp = UITweener.Begin<TweenFill>(go, duration);
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
