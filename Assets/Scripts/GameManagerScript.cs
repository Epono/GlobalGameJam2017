using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    Camera camera;

    [SerializeField]
	GameObject obstacleIslePrefab;

	[SerializeField]
	GameObject obstacleLightHousePrefab;

	[SerializeField]
	GameObject obstacleOilSlickPrefab;

	[SerializeField]
	GameObject obstacleSchoolOfSharksPrefab;

    [SerializeField]
    GameObject seaPrefab;

	private Vector2 mapSize;
	private float obstaclesDensity;
	private float obstacleIsleProbability;
	private float obstacleLightHouseProbability;
	private float obstacleOilSlickProbability;
	private float obstacleSchoolOfSharkProbability;

	private List<GameObject> warships;

	private List<ObstacleTemplateScript> obstacles;

	private List<GameObject> pickUps;

	void Start ()
	{
        // NE PAS SUPPRIMER
        //camera.pixelRect = new Rect(Screen.width - 595, Screen.height - 655, 590, 590);

        // Initialisation des listes
        warships = new List<GameObject>();
		obstacles = new List<ObstacleTemplateScript>();
		pickUps = new List<GameObject>();

		// Initialisation des variables de configuration de la partie (en dur, ouais)
		mapSize = new Vector2(20, 20);
		obstaclesDensity = 0.15f;

        GameObject sea = Instantiate(seaPrefab, new Vector3(mapSize.x / 2, mapSize.y / 2, 0), Quaternion.identity);

        // S'assurer que la somme fasse bien 1 -_- (ou proche au pire)
        obstacleIsleProbability = 0.5f;
		obstacleLightHouseProbability = 0.5f;
		obstacleOilSlickProbability = 0.0f;
		obstacleSchoolOfSharkProbability = 0.0f;

		// Création des obstacles
		for (int y = 0; y < mapSize.y; ++y)
		{
			for(int x = 0; x < mapSize.x; ++x)
			{
				float spawnObstacle = Random.value;
				if (spawnObstacle < obstaclesDensity)
				{
					float obstacleType = Random.value;
					double obstacleSizeModifier = 1 + ((Random.value - 0.5) / 1);

					GameObject obstaclePrefab;
					if(obstacleType < obstacleIsleProbability) {
						// Isle
						obstaclePrefab = obstacleIslePrefab;
					} else if(obstacleType < obstacleIsleProbability + obstacleLightHouseProbability) {
						// LightHouse
						obstaclePrefab = obstacleLightHousePrefab;
					} else if(obstacleType < obstacleIsleProbability + obstacleLightHouseProbability + obstacleOilSlickProbability) {
						// Oil Slick
						obstaclePrefab = obstacleOilSlickPrefab;
					} else {
						// School of Sharks
						obstaclePrefab = obstacleSchoolOfSharksPrefab;
					}

                    Vector3 islePosition;

                    // Vérifier si y'a pas autre chose pas loin
                    //while(true) {
                    islePosition = new Vector3(Random.value * mapSize.x, Random.value * mapSize.y, 0);
                    //    bool ok = true;
                    //    foreach(ObstacleTemplateScript obstacle in obstacles) {
                    //        //if(obstacle == null) {
                    //        //    ok = false;
                    //        //    break;
                    //        //}
                    //        if(Vector3.Distance(islePosition, obstacle.gameObject.transform.position) < 2) {
                    //            ok = false;
                    //            break;
                    //        }
                    //    }
                    //    if(ok) {
                    //        break;
                    //    }
                    //}

                    GameObject go = Instantiate(obstaclePrefab, islePosition, Quaternion.identity);
                    go.transform.eulerAngles = new Vector3(go.transform.eulerAngles.x, go.transform.eulerAngles.y, Random.value * 360);
					go.transform.localScale = new Vector3((float)(go.transform.localScale.x * obstacleSizeModifier), (float)(go.transform.localScale.y * obstacleSizeModifier), 1);
					ObstacleTemplateScript ots = go.GetComponent<ObstacleTemplateScript>();

					obstacles.Add(ots);
				}
			}
		}

		// Création des pickups


	}

	void Update ()
	{

    }
}