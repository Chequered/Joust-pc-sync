using UnityEngine;
using System.Collections;

public enum Faction
{
	player,
	enemy,
	neutral
}

public abstract class CharacterProperties : MonoBehaviour {

	public GameObject collisionBottom;
	public GameObject hitbox;
	public bool onGround;
	public bool alive;
	public Faction faction;
	public float jumpForceOnJumping;
	public float moveForceWhenWalking;
	public float maxForceWhenWalking;
	public float bounceForce;
	public float bounceCooldown;
	public float gravity;
	public float jumpForce;
	public float yForce;
	public float xForce;
	public Vector2 pos;
	public float gravityMultiplier;
	public bool isJumping;
	public float jumpDuration;
	public bool justBounced;
	
	public virtual void Update(){
		if(jumpDuration > 0){
			jumpDuration -= 0.05f;
		}else{
			jumpDuration = 0;
		}
		if(!onGround){
			yForce -= GetGravity();
		}else{
			gravityMultiplier = 0;
		}
		if(isJumping){
			if(gravityMultiplier < 2.5f){
				gravityMultiplier += 0.001f;
			}
			jumpForce -= GetGravity() / 2;
			yForce += jumpForce;
			if(jumpForce <= 0){
				isJumping = false;
			}
		}
		ApplyForce();
		ResetForces();
		if(bounceCooldown > 0){
			bounceCooldown -= 0.12f;
		}
		if(justBounced && bounceCooldown <= 0){
			justBounced = false;
		}
	}
	
	public void ApplyForce(){
		pos = transform.position;
		pos.y += yForce;
		pos.x += xForce;
		transform.position = pos;
	}
	
	public void Move(int dir){
		if(dir == 1){
			xForce -= moveForceWhenWalking;
		}else if(dir == 2){
			xForce += moveForceWhenWalking;
		}
		if(!justBounced){
			if(xForce > maxForceWhenWalking){
				xForce = maxForceWhenWalking;
			}
			else if(xForce < -maxForceWhenWalking){
				xForce = -maxForceWhenWalking;
			}
		}
	}
	
	public void Jump(){
		jumpForce = jumpForceOnJumping;
		jumpDuration = 1;
		isJumping = true;
	}
	
	public void ResetForces(){
		yForce = 0;
	}
	
	float gravityDeduction;
	public float GetGravity(){
		gravityDeduction = (gravity * jumpDuration) / 3;
		return gravity + gravity * gravityMultiplier - gravityDeduction;
	}
	
	public void Bounce(int side){
		if(bounceCooldown <= 0){
			if(side == 1){
				xForce *= xForce;
			}else if(side == 2){
				xForce *= -xForce;
			}else if(side == 3){
				yForce *= -yForce;
			}
			if(side != 3 && !justBounced){
				xForce *= bounceForce;
			}
			yForce *= bounceForce;
			justBounced = true;
			bounceCooldown = 10;
			ApplyForce();
		}
	}
	
	public void HitHead(){
		yForce = - 0.1f;
		ApplyForce();
	}

	
	public float Height(){
		return this.transform.position.y;
	}

	public bool OnGround {
		get {
			return onGround;
		}
		set {
			onGround = value;
		}
	}
}
