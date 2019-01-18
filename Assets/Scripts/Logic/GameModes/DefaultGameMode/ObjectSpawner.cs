using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
	#region MEMBERS

    [SerializeField]
    private GameObject[] objectsCollection;
    [SerializeField]
    private MultiplierFloat spawnFrequency;

    [Space(5)]
    [SerializeField]
    private Transform spawnContainer;
    [SerializeField]
    private Transform spawnPoint;

	#endregion

	#region PROPERTIES

    public GameObject[] ObjectsCollection {
    	get {return objectsCollection;}
    	private set {objectsCollection = value;}
    }
    public MultiplierFloat SpawnFrequency {
    	get {return spawnFrequency;}
    }
    public Transform SpawnContainer {
    	get {return spawnContainer;}
    }
    private Transform SpawnPoint {
        get {return spawnPoint;}
    }

    public bool IsEnabled {get; set;}

    private float NextSpawnTime {get; set;}

	#endregion

	#region FUNCTIONS

    public void SetActive (bool state)
    {
        IsEnabled = state;
    }

    public void SetObjectsCollection (GameObject[] collection)
    {
        ObjectsCollection = collection;
    }

    protected void Update ()
    {
        if (IsEnabled == false)
        {
            return;
        }
        
        HandleSpawning();
    }

    private void HandleSpawning ()
    {
        if (Time.time < NextSpawnTime)
        {
            return;
        }

        NextSpawnTime = Time.time + 1f / SpawnFrequency.Value;

        GameObject randomItem = ObjectsCollection[Random.Range(0, ObjectsCollection.Length)];
        GameObject spawnedItem = Instantiate(randomItem, SpawnContainer);

        spawnedItem.transform.position = SpawnPoint.position;
    }

	#endregion

	#region CLASS_ENUMS

	#endregion
}
