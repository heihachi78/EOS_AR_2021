using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    private PlacementIndicator placementIndicator;
    private int currentObjectToSpawn;
    private bool isSpawned = false;
    private int clickCount;
    private GameObject obj;

    private void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>();
        currentObjectToSpawn = 0;
    }

    private void Update()
    {
        if (currentObjectToSpawn == objectsToSpawn.Length) return;
        
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (isSpawned)
            {
                clickCount++;
                if (clickCount > 2)
                {
                    clickCount = 0;
                    Destroy(obj);
                    isSpawned = false;
                    placementIndicator.turnOffVisuals = false;
                }
            }
            else
            {
                obj = Instantiate(objectsToSpawn[currentObjectToSpawn], placementIndicator.transform.position, placementIndicator.transform.rotation);
                currentObjectToSpawn += 1;
                if (currentObjectToSpawn == objectsToSpawn.Length) currentObjectToSpawn = 0;
                isSpawned = true;
                placementIndicator.turnOffVisuals = true;
                clickCount = 0;
            }   
        }
    }
}
