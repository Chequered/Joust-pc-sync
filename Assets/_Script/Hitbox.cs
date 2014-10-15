using UnityEngine;
using System.Collections;

public class Hitbox : MonoBehaviour {

	private GameObject unit;

	private void Start(){
		unit = this.transform.parent.gameObject;
	}

	private void OnTriggerEnter2D(Collider2D coll){
		if(coll.tag == "Enemy"){
			if(coll	.GetComponent<NPCController>().faction == Faction.enemy){
				if(coll.GetComponent<NPCController>().Height() < unit.GetComponent<PlayerController>().Height()){
					coll.GetComponent<NPCController>().Kill();
				}
			}
		}
	}
}
