using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInstance : MonoBehaviour {
	public Texture2D tex;
	[HideInInspector]
	public Vector2 gridPos;
	public int type; // 0: normal, 1: enter 2: exit

    public GameObject playerStartPoint;
    public GameObject playerStartPoint2;
    public GameObject playerStartPoint3;
    public GameObject playerStartPoint4;
    //public GameObject playerStartPoint5;

    public GameObject exitPoint;
    public GameObject exitPoint2;
    public GameObject exitPoint3;
    public GameObject exitPoint4;
    //exit tiles to new levels

    public GameObject boss1;
    public GameObject boss2;
    public GameObject boss3;
    public GameObject boss4;

    bool playerPlaced = false;


    [HideInInspector]
	public bool doorTop, doorBot, doorLeft, doorRight;
	[SerializeField]
	GameObject doorU, doorD, doorL, doorR, doorWall;
	[SerializeField]
	ColorToGameObject[] mappings;
	float tileSize = 16;
	Vector2 roomSizeInTiles = new Vector2(9,17);
	public void Setup(Texture2D _tex, Vector2 _gridPos, int _type, bool _doorTop, bool _doorBot, bool _doorLeft, bool _doorRight){
		tex = _tex;
		gridPos = _gridPos;
		type = _type;
		doorTop = _doorTop;
		doorBot = _doorBot;
		doorLeft = _doorLeft;
		doorRight = _doorRight;
		MakeDoors();
		GenerateRoomTiles();
	}
	void MakeDoors(){
		//top door, get position then spawn
		Vector3 spawnPos = transform.position + Vector3.up*(roomSizeInTiles.y/4 * tileSize) - Vector3.up*(tileSize/4);
		PlaceDoor(spawnPos, doorTop, doorU);
		//bottom door
		spawnPos = transform.position + Vector3.down*(roomSizeInTiles.y/4 * tileSize) - Vector3.down*(tileSize/4);
		PlaceDoor(spawnPos, doorBot, doorD);
		//right door
		spawnPos = transform.position + Vector3.right*(roomSizeInTiles.x * tileSize) - Vector3.right*(tileSize);
		PlaceDoor(spawnPos, doorRight, doorR);
		//left door
		spawnPos = transform.position + Vector3.left*(roomSizeInTiles.x * tileSize) - Vector3.left*(tileSize);
		PlaceDoor(spawnPos, doorLeft, doorL);
	}
	void PlaceDoor(Vector3 spawnPos, bool door, GameObject doorSpawn){
		// check whether its a door or wall, then spawn
		if (door){
			Instantiate(doorSpawn, spawnPos, Quaternion.identity).transform.parent = transform;
		}else{
			Instantiate(doorWall, spawnPos, Quaternion.identity).transform.parent = transform;
		}
	}
	void GenerateRoomTiles(){
		//loop through every pixel of the texture
		for(int x = 0; x < tex.width; x++){
			for (int y = 0; y < tex.height; y++){
				GenerateTile(x,y);
			}
		}
        if (playerPlaced == false && type==1  )
        {
            Vector3 spawnPos = transform.position + Vector3.down * (roomSizeInTiles.y / 4 * tileSize) - Vector3.down * (tileSize / 4);
            Vector3 playerSpawnPos = new Vector3(spawnPos.x, spawnPos.y + 16);

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainCave"))
            {
                Instantiate(playerStartPoint, playerSpawnPos, Quaternion.identity);
                playerPlaced = true;
            }

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainCave2"))
            {
                Instantiate(playerStartPoint2, playerSpawnPos, Quaternion.identity);
                playerPlaced = true;
            }

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainCave3"))
            {
                Instantiate(playerStartPoint3, playerSpawnPos, Quaternion.identity);
                playerPlaced = true;
            }

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainCave4"))
            {
                Instantiate(playerStartPoint4, playerSpawnPos, Quaternion.identity);
                playerPlaced = true;
            }
        }
        if (type == 2)
        {
            Vector3 spawnPos = transform.position + Vector3.down * (roomSizeInTiles.y / 4 * tileSize) - Vector3.down * (tileSize / 4);
            Vector3 exitSpawnPos = new Vector3(spawnPos.x, spawnPos.y + 16);
            Vector3 bossSpawnPos = new Vector3(spawnPos.x, spawnPos.y + 64);

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainCave"))
            { 
                
                Instantiate(exitPoint, exitSpawnPos, Quaternion.identity);
                Instantiate(boss1, bossSpawnPos, Quaternion.identity);
            }

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainCave2"))
            {
                
                Instantiate(exitPoint2, exitSpawnPos, Quaternion.identity);
                Instantiate(boss2, bossSpawnPos, Quaternion.identity);
            }
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainCave3"))
            {

                Instantiate(exitPoint3, exitSpawnPos, Quaternion.identity);
                Instantiate(boss3, bossSpawnPos, Quaternion.identity);
            }
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainCave4"))
            {

                Instantiate(exitPoint4, exitSpawnPos, Quaternion.identity);
                Instantiate(boss4, bossSpawnPos, Quaternion.identity);
            }
        }
    }

	void GenerateTile(int x, int y){
		Color pixelColor = tex.GetPixel(x,y);
		//skip clear spaces in texture
		if (pixelColor.a == 0){
			return;
		}
		//find the color to math the pixel
		foreach (ColorToGameObject mapping in mappings){
			if (mapping.color.Equals(pixelColor)){
				Vector3 spawnPos = positionFromTileGrid(x,y);
				Instantiate(mapping.prefab, spawnPos, Quaternion.identity).transform.parent = this.transform;
			}

		}
	}
	Vector3 positionFromTileGrid(int x, int y){
		Vector3 ret;
		//find difference between the corner of the texture and the center of this object
		Vector3 offset = new Vector3((-roomSizeInTiles.x + 1)*tileSize, (roomSizeInTiles.y/4)*tileSize - (tileSize/4), 0);
		//find scaled up position at the offset
		ret = new Vector3(tileSize * (float) x, -tileSize * (float) y, 0) + offset + transform.position;
		return ret;
	}
}
