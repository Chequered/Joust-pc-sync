using UnityEngine;
using System.Collections;

public class SpawnPlatform : MonoBehaviour {

	public GameObject point;

	public Vector3 GetPosition(){
		return point.transform.position;
	}

	public void SpawnEnemy(GameObject enem){
		GameObject enemy = GameObject.Instantiate(enem, GetPosition(), Quaternion.identity) as GameObject;
		GameController.enemiesInStage.Add(enemy);
		Debug.Log ("enemy spawned");
	}
}
