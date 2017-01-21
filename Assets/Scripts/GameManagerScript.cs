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

    [SerializeField]
    List<Sprite> sprites;

    [SerializeField]
    List<Collider> colliders;
    [SerializeField]
    List<GameObject> isleGameObject;

    private Vector2 mapSize;
	private float obstaclesDensity;
	private float obstacleIsleProbability;
	private float obstacleLightHouseProbability;
	private float obstacleOilSlickProbability;
	private float obstacleSchoolOfSharkProbability;

	private List<GameObject> warships;

	private List<ObstacleTemplateScript> obstacles;

	private List<GameObject> pickUps;

    [SerializeField]
    EventManager eventManager;

	void Start ()
	{
        // NE PAS SUPPRIMER
        //camera.pixelRect = new Rect(Screen.width - 595, Screen.height - 655, 590, 590);
        Instantiate(eventManager);
        float targetaspect = 9.0f / 9.0f;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // if scaled height is less than current height, add letterbox
        if(scaleheight < 1.0f) {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        } else // add pillarbox
          {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }

        // Initialisation des listes
        warships = new List<GameObject>();
		obstacles = new List<ObstacleTemplateScript>();
		pickUps = new List<GameObject>();

		// Initialisation des variables de configuration de la partie (en dur, ouais)
		mapSize = new Vector2(20, 20);
		obstaclesDensity = 0.20f;

        GameObject sea = Instantiate(seaPrefab, new Vector3(mapSize.x / 2, mapSize.y / 2, 0), Quaternion.identity);

        // S'assurer que la somme fasse bien 1 -_- (ou proche au pire)
        obstacleIsleProbability = 0.8f;
		obstacleLightHouseProbability = 0.2f;
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

				    float randomRotationZ = Random.value * 360;
                    int randomAnimation = Random.Range(0, 4);

                    GameObject go;
                    //GameObject go = Instantiate(obstaclePrefab, islePosition, Quaternion.identity);
                    if(randomAnimation  == 0)
                    {
                        go = Instantiate(isleGameObject[0], islePosition, Quaternion.identity);
                    }
                    else if (randomAnimation == 1)
                    {
                        go = Instantiate(isleGameObject[1], islePosition, Quaternion.identity);
                    }
                    else if (randomAnimation == 2)
                    {
                        go = Instantiate(isleGameObject[2], islePosition, Quaternion.identity);
                    }
                    else if (randomAnimation == 3)
                    {
                        go = Instantiate(isleGameObject[3], islePosition, Quaternion.identity);
                    }
                    else
                    {
                        go = new GameObject();
                    }

                    go.transform.eulerAngles = new Vector3(go.transform.eulerAngles.x, go.transform.eulerAngles.y, randomRotationZ);
                    go.transform.localScale = new Vector3((float)(go.transform.localScale.x * obstacleSizeModifier), (float)(go.transform.localScale.y * obstacleSizeModifier), 1);
                    ObstacleTemplateScript ots = go.GetComponent<ObstacleTemplateScript>();

				    /*if (obstaclePrefab == obstacleIslePrefab)
				    {
                        go.GetComponent<ObstacleIsleScript>().ID = randomAnimation;
				        go.GetComponent<SpriteRenderer>().sprite = sprites[randomAnimation];
				        go.GetComponent<Animator>().runtimeAnimatorController =
				            (RuntimeAnimatorController) Resources.Load("Animations/Isle_" + (randomAnimation + 1) + "_animator");
				    }*/


                    // J'essaye de faire qu'on voit que la carte est sans limite
                    Vector3 islePosition2 = new Vector3(
                        go.transform.position.x > 19 ? go.transform.position.x - mapSize.x : (go.transform.position.x < 1 ? go.transform.position.x + mapSize.x : go.transform.position.x),
                        go.transform.position.y > 19 ? go.transform.position.y - mapSize.y : (go.transform.position.y < 1 ? go.transform.position.y + mapSize.y : go.transform.position.y),
                        0);

				    if (islePosition2 != islePosition)
				    {
            //            GameObject goo = Instantiate(obstaclePrefab, islePosition2, Quaternion.identity);
            //            goo.transform.eulerAngles = new Vector3(goo.transform.eulerAngles.x, goo.transform.eulerAngles.y, randomRotationZ);
            //            goo.transform.localScale = new Vector3((float)(goo.transform.localScale.x * obstacleSizeModifier), (float)(goo.transform.localScale.y * obstacleSizeModifier), 1);
            //            ObstacleTemplateScript otss = goo.GetComponent<ObstacleTemplateScript>();

				        //if (obstaclePrefab == obstacleIslePrefab)
				        //{
				        //    goo.GetComponent<SpriteRenderer>().sprite = sprites[randomAnimation];
				        //    goo.GetComponent<Animator>().runtimeAnimatorController =
				        //        (RuntimeAnimatorController) Resources.Load("Animations/Isle_" + (randomAnimation + 1) + "_animator");
				        //}

				        //obstacles.Add(otss);
                    }
				}
			}
		}

		// Création des pickups


	}

	void Update ()
	{

    }
}