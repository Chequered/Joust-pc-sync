using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	public static List<GameObject> enemiesInStage = new List<GameObject>();
	public GameObject[] spawnPlatforms;
	public GameObject[] enemies;

	private GameObject player;
	private Vector2 posLeft;
	private Vector2 posRight;
	private Vector2 newPlayerPos;
	private static bool spawnOver;
	private static int spawned;
	private float spawnTimer;

	private void Start(){
		player = GameObject.FindGameObjectWithTag("Player");
		posLeft = new Vector2(0, player.transform.position.y);
		posRight = new Vector2(16, player.transform.position.y);
		spawnTimer = 8f;
		updateEnemyCount();
	}

	private void Update(){
		if(player.transform.position.x < posLeft.x){
			TeleportPlayer(posRight.x, player);
		}else if(player.transform.position.x > posRight.x){
			TeleportPlayer(posLeft.x, player);
		}
		foreach(GameObject enem in enemies){
			if(enem != null){
				if(enem.transform.position.x < posLeft.x){
					TeleportPlayer(posRight.x, enem);
					enem.GetComponent<NPCController>().NewPos(1);
				}else if(enem.transform.position.x > posRight.x){
					TeleportPlayer(posLeft.x, enem);
					enem.GetComponent<NPCController>().NewPos(2);
				}
			}
		}
		if(spawnOver && spawned < 3){
			spawnTimer -= 0.2f;
			if(spawnTimer <= 0){
				int r = (int) Random.Range (0, spawnPlatforms.Length);
				spawnPlatforms[r].GetComponent<SpawnPlatform>().SpawnEnemy(enemies[(int) Random.Range (0, enemies.Length - 1)]);
				spawnTimer = 8f;
				spawned++;
			}
		}
	}

	public static void updateEnemyCount(){
		if(enemiesInStage.Count <= 0){
			spawnOver = true;
			spawned = 0;
		}
	}

	private void TeleportPlayer(float _x, GameObject _player){
		newPlayerPos.x = _x;
		newPlayerPos.y = _player.transform.position.y;
		_player.transform.position = newPlayerPos;
	}

}
