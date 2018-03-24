using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    float screenHeight;

    float minX, maxX;
    float minYInterval, maxYInterval;
    float lastPlatformY;

    int currentBlock;
    int numPlatformsPerBlock;

    Vector3 spawnPos;


    // platform prefabs
    public Dictionary<GameObject, float> platformsProbs = new Dictionary<GameObject, float>();
    
    public GameObject normalPlatformPrefab;
    public GameObject horizontalMovingPlatformPrefab;
    public GameObject vanishPlatformPrefab;


    // Use this for initialization
    void Start () {
        screenHeight = ScreenUtils.ScreenTop - ScreenUtils.ScreenBottom;

        // hardcode for now
        float PlatformWidth = 1.14f;
        float PlatformHeight = 0.3f;
        float jumpHeight = 1.5f;

        minX = ScreenUtils.ScreenLeft + PlatformWidth / 2;
        maxX = ScreenUtils.ScreenRight - PlatformWidth / 2;
        minYInterval = PlatformHeight;
        maxYInterval = jumpHeight - 0.1f;
        lastPlatformY = ScreenUtils.ScreenBottom + minYInterval / 2;

        currentBlock = 0;
        numPlatformsPerBlock = 25;


        // initialize the prefabs
        platformsProbs.Add(normalPlatformPrefab, 0.95f);
        platformsProbs.Add(horizontalMovingPlatformPrefab, 0.05f);
        platformsProbs.Add(vanishPlatformPrefab, 0);




        // generate the initial level
        float minY =  - 0.5f * screenHeight;
        float maxY = 0.5f * screenHeight;

        // make sure the first platform is right below the doodler
        spawnPos = new Vector3(0, lastPlatformY);
        Instantiate(normalPlatformPrefab, spawnPos, Quaternion.identity);
        while (spawnPos.y < (maxY - minYInterval / 2))
        {
            spawnPos.x = Random.Range(minX, maxX);
            spawnPos.y += Random.Range(minYInterval, maxYInterval);

            GeneratePlatform(spawnPos);
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (Camera.main.transform.position.y > currentBlock * screenHeight)
        {
            currentBlock++;
            UpdatePlatformProbs();
            float minY = (currentBlock - 0.5f) * screenHeight;
            float maxY = (currentBlock + 0.5f) * screenHeight;

            while (spawnPos.y < (maxY - minYInterval / 2))
            {
                spawnPos.x = Random.Range(minX, maxX);
                spawnPos.y += Random.Range(minYInterval, maxYInterval);
                GeneratePlatform(spawnPos);
            }
        }
	}

    private void GeneratePlatform(Vector3 pos)
    {
        float p = Random.Range(0f, 1f);
        float accuProb = 0;
        float probNormal = platformsProbs[normalPlatformPrefab];
        float probHori = platformsProbs[horizontalMovingPlatformPrefab];
        float probVani = platformsProbs[vanishPlatformPrefab];

        accuProb = probNormal;
        if (p <= probNormal)
        {
            Instantiate(normalPlatformPrefab, pos, Quaternion.identity);
        }
        else if (p <= (accuProb += probHori))
        {  
            Instantiate(horizontalMovingPlatformPrefab, pos, Quaternion.identity);
        }
        else
        {
            Debug.Log(p + "   " + accuProb);
            Instantiate(vanishPlatformPrefab, pos, Quaternion.identity);
        }
    }

    private void UpdatePlatformProbs()
    {
        if (currentBlock > 10)
        {
            platformsProbs[vanishPlatformPrefab] = 0.1f + 0.001f * currentBlock;
        }
        platformsProbs[normalPlatformPrefab] = (1 - platformsProbs[vanishPlatformPrefab]) * (0.9f - currentBlock * 0.003f);
        platformsProbs[horizontalMovingPlatformPrefab] = 1 - platformsProbs[vanishPlatformPrefab] - platformsProbs[normalPlatformPrefab];


//        Debug.Log(platformsProbs[normalPlatformPrefab] + " " + platformsProbs[horizontalMovingPlatformPrefab] + " " + platformsProbs[vanishPlatformPrefab]);
    }
}
