using UnityEngine;
using System.Collections;

public class NPCController : CharacterProperties {

	public float timerStrength;
	public float rayDistance;
	public float playerViewRange;

	private float AIInputCooldown;
	private float jumpCooldown;
	private float moveCooldown;
	private int jumpsToDo;

	private MoveTarget moveTo;

	private void Start(){
		AIInputCooldown = 2.5f;
		jumpCooldown = 0.6f;
		moveCooldown = 0.4f;
		NewPos(0);
	}

	public override void Update () {
		base.Update();
		GetAIInput();
		Timers();
	}

	private void GetAIInput(){
		if(AIInputCooldown <= 0){
			jumpsToDo = Random.Range(2, 5);
			AIInputCooldown = 2.5f;
		}
		if(jumpsToDo > 0 && jumpCooldown <= 0){
			if(!ColliderAbove()){
				Jump ();
				jumpsToDo--;
				jumpCooldown = 0.6f;
			}
		}
		if(moveCooldown <= 0){
			if(moveTo != null){
				if(moveTo.Side == Sides.left && !moveTo.Reached){
					Move(1);
				}else if(moveTo.Side == Sides.right && !moveTo.Reached){
					Move(2);
				}
				moveTo.UpdateDistance(transform.position.x);
				moveCooldown = 0.4f;
				if(moveTo.Reached && !PlayerInRange()){
					NewPos(0);
				}
			}
		}
		moveTo.UpdateDistance(transform.position.x);	
		if(PlayerInRange()){

		}
	}

	private void Timers(){
		if(AIInputCooldown > 0){
			AIInputCooldown -= timerStrength;
		}
		if(jumpCooldown > 0){
			jumpCooldown -= timerStrength;
		}
		if(moveCooldown > 0){
			moveCooldown -= timerStrength;
		}
	}

	private void OnDrawGizmos(){
		if(PlayerController.singleton != null){
			Gizmos.color = Color.red;
			if(PlayerInRange()){
				Gizmos.color = Color.blue;
			}
			Gizmos.DrawLine(transform.position, PlayerController.singleton.transform.position);
		}
		if(moveTo != null){
			Gizmos.color = Color.green;
			Gizmos.DrawLine(transform.position, new Vector2(moveTo.X, transform.position.y));
			Gizmos.DrawSphere(new Vector3(moveTo.X, transform.position.y, transform.position.z), 0.2f);
		}
	}

	private bool ColliderAbove(){
		RaycastHit2D hit;
		hit = Physics2D.Raycast(transform.position, Vector2.up, rayDistance);
		if(hit.collider != null){
			if(hit.collider.tag == "Collider"){
				return true;
			}
		}
		return false;
	}

	private bool PlayerInRange(){
        if (PlayerController.singleton != null)
        {
            RaycastHit2D hit;
            hit = Physics2D.Linecast(transform.position, PlayerController.singleton.transform.position);
            if (hit.fraction <= playerViewRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
	}

	public void NewPos(int side){
		float GX = (float)Random.Range(-2, 2);
		if(moveTo != null){
			if(GX > moveTo.X){
				xForce *= 1.5f;
			}else if(GX < moveTo.X){
				xForce /= 1.5f;
			}
		}
		moveTo = new MoveTarget(GX, transform.position.x);
		if(side == 1){
			GX = (float)Random.Range(-2, 0);
			moveTo = new MoveTarget(GX, transform.position.x);
		}else if(side == 2){
			GX = (float)Random.Range(0, 2);
			moveTo = new MoveTarget(GX, transform.position.x);
		}
	}

	public void Kill(){
		Destroy(this.gameObject);
		GameController.enemiesInStage.Remove(this.gameObject);
		GameController.updateEnemyCount();
	}
}
