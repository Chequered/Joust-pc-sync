using UnityEngine;
using System.Collections;


public class CustomCollision : MonoBehaviour {

	private CharacterProperties player;

	private void Start(){
		player = transform.parent.GetComponent<CharacterProperties>();
	}

	public enum Sides
	{
		Left,
		Bottom,
		Right,
		Top
	}

	public Sides side;

	private void OnTriggerEnter2D(Collider2D coll){
		if(coll.tag == "Collider"){
			switch(side)
			{
				case Sides.Bottom:
				player.OnGround = true;
				break;
			}
		}
	}

	private void OnTriggerStay2D(Collider2D coll){
		if(coll.tag == "Collider"){
			switch(side)
			{
				case Sides.Left:
				player.Bounce(1);
				break;
				case Sides.Right:
				player.Bounce(2);
				break;
				case Sides.Top:
				player.HitHead();
				break;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D coll){
		if(coll.tag == "Collider"){
			switch(side)
			{
				case Sides.Bottom:
				player.OnGround = false;
				break;
			}
		}
	}
}
