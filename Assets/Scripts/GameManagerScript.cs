using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
	[SerializeField]
	GameObject obstacleIslePrefab;

	[SerializeField]
	GameObject obstacleLightHousePrefab;

	[SerializeField]
	GameObject obstacleOilSlickPrefab;

	[SerializeField]
	GameObject obstacleSchoolOfSharksPrefab;

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
		// Initialisation des listes
		warships = new List<GameObject>();
		obstacles = new List<ObstacleTemplateScript>();
		pickUps = new List<GameObject>();

		// Initialisation des variables de configuration de la partie (en dur, ouais)
		mapSize = new Vector2(20, 20);
		obstaclesDensity = 0.05f;

		// S'assurer que la somme fasse bien 1 -_- (ou proche au pire)
		obstacleIsleProbability = 0.5f;
		obstacleLightHouseProbability = 0.2f;
		obstacleOilSlickProbability = 0.2f;
		obstacleSchoolOfSharkProbability = 0.1f;

		// Création des obstacles
		for (int y = 0; y < mapSize.y; ++y)
		{
			for(int x = 0; x < mapSize.x; ++x)
			{
				float spawnObstacle = Random.value;
				if (spawnObstacle < obstaclesDensity)
				{
					float obstacleType = Random.value;
					float obstaclePositionX = Random.value * mapSize.x;
					float obstaclePositionZ = Random.value * mapSize.y;
					double obstacleSizeModifier = (Random.value - 0.5) / 10;

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

					GameObject go = Instantiate(obstaclePrefab, new Vector3(obstaclePositionX, -1, obstaclePositionZ), Random.rotation);
					go.transform.localScale = new Vector3((float)(go.transform.localScale.x * obstacleSizeModifier), (float)(go.transform.localScale.y * obstacleSizeModifier), (float)(go.transform.localScale.z * obstacleSizeModifier));
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