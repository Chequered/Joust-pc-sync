using UnityEngine;
using System.Collections;

public class SpawnPlatform : MonoBehaviour {

	public GameObject point;

	public Vector3 GetPosition(){
        Vector3 pos = point.transform.position;
        pos.y += 0.5f;
		return pos;
	}

	public void SpawnEnemy(GameObject enem){
		GameObject enemy = GameObject.Instantiate(enem, GetPosition(), Quaternion.identity) as GameObject;
		GameController.enemiesInStage.Add(enemy);
	}
}
