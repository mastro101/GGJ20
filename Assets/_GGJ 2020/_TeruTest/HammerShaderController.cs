using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerShaderController : GlowShaderController
{
	public Color[] stepsColors = new Color[4];

    void ChangeStep(int step)
	{
		material.SetColor("_Color", stepsColors[Mathf.Clamp(step, 0, 3)]);
	}

}
