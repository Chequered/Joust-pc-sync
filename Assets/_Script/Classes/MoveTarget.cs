using UnityEngine;
using System.Collections;

public enum Sides
{
	left,
	right
}

public class MoveTarget
{
	protected float x;
	protected float px;
	protected Sides side;
	protected float distance;
	protected bool reached;

	public MoveTarget(float _x, float _px)
	{
		if(_x > 0)
		{
			side = Sides.right;
		}else{
			side = Sides.left;
		}
		x = _px + _x;
		px = _px;
	}

	public void UpdateDistance(float _px){
		if(side == Sides.left){
			distance = _px - x;
		}else if(side == Sides.right){
			distance = x - _px;
		}
		if(distance <= 0){
			reached = true;
		}
	}

	public float X {
		get {
			return x;
		}
	}

	public Sides Side {
		get {
			return side;
		}
	}

	public float Distance {
		get {
			return distance;
		}
	}

	public bool Reached {
		get {
			return reached;
		}
	}
}

