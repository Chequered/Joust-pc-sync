using UnityEngine;
using System.Collections;

public enum CharAnimation
{
	Walk,
	Slide,
	Jump
}

public class SpriteManager : MonoBehaviour
{

	public Sprite[] walkingFrames;
	public Sprite[] jumpingFrames;
	public Sprite slideSprite;
	public float frameTime;

	public int direction;
	private uint currentFrame;

	private SpriteRenderer SR;
	private Quaternion rightRot;
	private Quaternion leftRot;

	private void Start()
	{
		if(GetComponent<SpriteRenderer>() != null)
		{
			SR = GetComponent<SpriteRenderer>();
			currentFrame = 0;
			frameTime = 3;
			direction = 1;
			SR.sprite = walkingFrames[currentFrame];

			rightRot = new Quaternion(0, 0, 0, 1);
			leftRot = new Quaternion(0, 180, 0, 1);
		}else{
			Debug.LogError("This character has no sprite renderer!");
		}
	}

	public void RotateChar(int dir)
	{
		direction = dir;
		if(dir == 1)
		{
			transform.rotation = leftRot;
		}else if(dir == 2)
		{
			transform.rotation = rightRot;
		}
	}

	public void Animate(CharAnimation anim)
	{
		frameTime -= 0.5f;
		if(frameTime <= 0)
		{
			switch(anim)
			{
			case CharAnimation.Walk:
				currentFrame++;
				if(currentFrame > walkingFrames.Length - 1)
				{
					currentFrame = 0;
				}
				SR.sprite = walkingFrames[currentFrame];
				frameTime = 3;
				break;
			case CharAnimation.Slide:
				SR.sprite = slideSprite;
				break;
			case CharAnimation.Jump:
				if(currentFrame < jumpingFrames.Length)
				{
					currentFrame++;
				}
				if(currentFrame > jumpingFrames.Length - 1)
				{
					currentFrame = 0;
				}
				SR.sprite = jumpingFrames[currentFrame];
				frameTime = 8;
				break;
			}
		}
	}

	public void Animate(CharAnimation anim, float acc)
	{
		frameTime -= 0.5f * 20 * acc;
		if(frameTime <= 0)
		{
			switch(anim)
			{
			case CharAnimation.Walk:
				currentFrame++;
				if(currentFrame > walkingFrames.Length - 1)
				{
					currentFrame = 0;
				}
				SR.sprite = walkingFrames[currentFrame];
				frameTime = 3;
				break;
			case CharAnimation.Slide:
				SR.sprite = slideSprite;
				break;
			case CharAnimation.Jump:
				if(currentFrame < jumpingFrames.Length)
				{
					currentFrame++;
				}
				if(currentFrame > jumpingFrames.Length - 1)
				{
					currentFrame = 0;
				}
				SR.sprite = jumpingFrames[currentFrame];
				frameTime = 8;
				break;
			}
		}
	}
}

