using UnityEngine;
using System.Collections;

public class Colorizer : MonoBehaviour 
{
	const bool VERBOSE = false;
	
	// This is how much affected objects should be dimmed. This is optional,
	// but adds to the "subtracted" effect.
	
	const float FADE_ALPHA = 0.4f;
	
	// Grab the object's original color to be used for subtractive mix as well as
	// when you need to reset the object's color back to it's original values.
	
	Color original_;
	
	void Start () 
	{
		original_ = this.renderer.material.color;
	}
	
	public void ResetColor()
	{
		this.renderer.material.color = new Color(original_.r,original_.g,original_.b,original_.a);
		
		if (VERBOSE)
			Debug.Log("Reset "+this+": " + original_);
	}
	
	public void LightColor(float red, float green, float blue, bool fadeToo)
	{
		// Here's the real "magic," which isn't very impressive.. but achieves a "subtractive" color
		// from the colors passed in as parameters.
		
		float red_mix = red * original_.r;
		float green_mix = green * original_.g;
		float blue_mix = blue * original_.b;
		
		this.renderer.material.color = new Color(red_mix,green_mix,blue_mix, fadeToo ? FADE_ALPHA : original_.a);
		
		if (VERBOSE)
		{
			PrintColor ("Original",original_);
			PrintColor ("Light",new Color(red,green,blue));
			PrintColor ("Mixed",this.renderer.material.color);
		}
	}
	
	// Debugging (VERBOSE) stuff
	private void PrintColor(string label, Color color)
	{
		Debug.Log(this+"> "+label+": r="+color.r+", g="+color.g+", b="+color.b+", a="+color.a);
	}
}
