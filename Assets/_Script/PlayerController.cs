using UnityEngine;
using System.Collections;

public class PlayerController : CharacterProperties {

	static public PlayerController singleton;

	private void Awake(){
		singleton = this;
	}

	public override void Update(){
		base.Update();
		GetInput();
	}

	public void GetInput(){
		if(Input.GetKeyUp(KeyCode.Space)){
			Jump ();
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			Move(1);
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			Move(2);
		}
	}
}
