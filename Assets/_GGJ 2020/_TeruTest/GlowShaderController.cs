﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowShaderController : MonoBehaviour
{
    public AnimationCurve animationCurve;
	private float timer = 99;
    public float animationDuration = 1;
	public Material material;

    public void Awake()
	{
		material = GetComponent < SpriteRenderer > ().material;
	}

    public void StartAnimation()
    {
		timer = 0;
    }

    public void Update()
    {
		timer += Time.deltaTime;
		float evaluationParam = timer / animationDuration;
		material.SetFloat("_LerpValue", animationCurve.Evaluate(evaluationParam));
    }

}
