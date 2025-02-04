﻿using UnityEngine;
using System.Collections;

public class FlowerAnimator : MonoBehaviour {

	private Node node;
    private AIFlower aIFlower;

	public SpriteRenderer spriteRenderer;
    public Light glowingLight;
	
	public Color currentColor = new Color(1, 1, 1, 0);
    public Color transitionColor = new Color(1, 1, 1, 0);
	public Color nextColor = Color.white;
	
	public float colorChangeDuration = 2.0f;
	public float colorChangeStartTime;
	
	public bool changingColor;
    public bool isGlowingFlower = false;

	// Use this for initialization
	void Start () {
		node = gameObject.GetComponent<Node>();
        aIFlower = gameObject.GetComponent<AIFlower>();

		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.color = currentColor;

        ChangeColorRandom();

        if (isGlowingFlower)
        {
            glowingLight = gameObject.transform.GetChild(0).GetComponent<Light>();
        }

	}
	
	// Update is called once per frame
	void Update () {
		if (changingColor)
		{
			float colorChangeTime = Time.time - colorChangeStartTime;
			transitionColor = Color.Lerp(currentColor, nextColor, colorChangeTime / colorChangeDuration);

            spriteRenderer.color = transitionColor;
			
			if(colorChangeTime > colorChangeDuration)
			{
				changingColor = false;
				currentColor = nextColor;
			}
		}
		
		if (node.captured)
		{
			gameObject.transform.Rotate (0, 0 , Time.deltaTime * 60);
		}
	}

	public void ChangeColor(Color newColor)
	{
		nextColor = newColor;
		currentColor = transitionColor;
		changingColor = true;
		colorChangeStartTime = Time.time;
	}
	
	public void ChangeColorRandom()
	{
        if (isGlowingFlower)
        {
            ChangeColor(
            new Color(
            Random.Range(0.0f, .6f),
            .92F,
            .92F)
            );
        }
        else
        {
            ChangeColor(
                new Color(
                Random.Range(0.0f, 1.0f),
                Random.Range(0.0f, 1.0f),
                Random.Range(0.0f, 1.0f))
                );
        }
	}

	public void OnCapture()
	{
        if (isGlowingFlower)
        {
            ChangeColor(Color.white);
        }
        else
        {
            ChangeColor(Color.red);
        }
	}

	public void OnNeutral()
	{
		ChangeColorRandom();
	}

    // Manually sets the alpha of the sprite. Useful for fading in and out without effect color transitions.
    public void SetAlpha(float alpha)
    {
        spriteRenderer.color = new Color(transitionColor.r, transitionColor.g, transitionColor.b, transitionColor.a * alpha);

        if (isGlowingFlower)
        {
            glowingLight.intensity = Mathf.Lerp(6.0F, 0, .7F); 
        }
    }
}
