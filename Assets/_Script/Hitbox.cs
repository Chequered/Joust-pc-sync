using UnityEngine;
using System.Collections;

public class Hitbox : MonoBehaviour {

    public GameObject scoreText;

    private int score = 0;

    private GameObject unit;

	private void Start(){
		unit = this.transform.parent.gameObject;
	}

	private void OnTriggerEnter2D(Collider2D coll){
		if(coll.tag == "Enemy"){
			if(coll	.GetComponent<NPCController>().faction == Faction.enemy){
				if(coll.GetComponent<NPCController>().Height() < unit.GetComponent<PlayerController>().Height()){
					coll.GetComponent<NPCController>().Kill();
                    audio.Play();
                    score += 25;
                    scoreText.GetComponent<TextMesh>().text = "Score: " + score;
                }
                else
                {
                    audio.Play();
                    GameController.instant.TakeALife();
                    Destroy(GameObject.FindGameObjectWithTag("Player"));
                }
			}
		}
	}
}
