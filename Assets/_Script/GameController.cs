using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	public static List<GameObject> enemiesInStage = new List<GameObject>();
    public static GameController instant;

	public GameObject[] spawnPlatforms;
	public GameObject[] enemies;
    public GameObject livesText;
    public GameObject playerPrefab;

    public bool playerNeedsSpawn;

	private GameObject player;
	private Vector2 posLeft;
	private Vector2 posRight;
	private Vector2 newPlayerPos;
	private static bool spawnOver;
	private static int spawned;
	private float spawnTimer;
    private uint lives = 3;

	private void Start(){
		player = GameObject.FindGameObjectWithTag("Player");
		posLeft = new Vector2(0, player.transform.position.y);
		posRight = new Vector2(16, player.transform.position.y);
        instant = this;
		spawnTimer = 8f;
		updateEnemyCount();
	}

	private void Update(){
        if (player != null)
        {
            if (player.transform.position.x < posLeft.x)
            {
                TeleportPlayer(posRight.x, player);
            }
            else if (player.transform.position.x > posRight.x)
            {
                TeleportPlayer(posLeft.x, player);
            }
        }
		foreach(GameObject enem in enemiesInStage){
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
        if(playerNeedsSpawn)
        {
            GameObject playr = Instantiate(playerPrefab, new Vector3(3.7f, -3.5f, 0), Quaternion.identity) as GameObject;
            player = playr;
            playerNeedsSpawn = false;
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

    public void TakeALife()
    {
        lives -= 1;
        if(lives <= 0)
        {
            Application.LoadLevel(0);
        }
        playerNeedsSpawn = true;
        livesText.GetComponent<TextMesh>().text = "Lives: " + lives;
    }

}
