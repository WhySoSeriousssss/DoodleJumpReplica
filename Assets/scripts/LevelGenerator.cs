using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    float screenLeft, screenRight, screenTop, screenBottom;
    float screenHeight;

    float minYInterval, maxYInterval;
    float lastPlatformY;

    int currentBlock;
    int numPlatformsPerBlock;

    Vector3 spawnPos;

    // platform prefabs
    public GameObject normalPlatformPrefab;
    public GameObject horizontalMovingPlatformPrefab;
    public GameObject vanishPlatformPrefab;


    // Use this for initialization
    void Start () {
        screenLeft = ScreenUtils.ScreenLeft;
        screenRight = ScreenUtils.ScreenRight;
        screenTop = ScreenUtils.ScreenTop;
        screenBottom = ScreenUtils.ScreenBottom;

        screenHeight = screenTop - screenBottom;

        // hardcode for now
        minYInterval = 0.2f;
        maxYInterval = 1.5f;
        lastPlatformY = screenBottom + minYInterval / 2;

        currentBlock = 0;
        numPlatformsPerBlock = 25;

        // generate the initial level
        float minY =  - 0.5f * screenHeight;
        float maxY = 0.5f * screenHeight;

        // make sure the first platform is right below the doodler
        spawnPos = new Vector3(0, lastPlatformY);
        Instantiate(normalPlatformPrefab, spawnPos, Quaternion.identity);
        while (spawnPos.y < (maxY - minYInterval / 2))
        {
            spawnPos.x = Random.Range(screenLeft, screenRight);
            spawnPos.y += Random.Range(minYInterval, maxYInterval);
            Instantiate(normalPlatformPrefab, spawnPos, Quaternion.identity);
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (Camera.main.transform.position.y > currentBlock * screenHeight)
        {
            currentBlock++;
            float minY = (currentBlock - 0.5f) * screenHeight;
            float maxY = (currentBlock + 0.5f) * screenHeight;

            while (spawnPos.y < (maxY - minYInterval / 2))
            {
                spawnPos.x = Random.Range(screenLeft, screenRight);
                spawnPos.y += Random.Range(minYInterval, maxYInterval);
                Instantiate(normalPlatformPrefab, spawnPos, Quaternion.identity);

            }
        }
	}
}
