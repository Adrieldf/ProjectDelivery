using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{
    [SerializeField] private List<GameObject> mapSectionsToSpawn;
    [SerializeField] private List<MapSection> activeSections;
    [SerializeField] private Vector3 firstSpawnPosition;

    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MapSection");
        for (int i = 0; i < objs.Length; i++)
            DestroyImmediate(objs[i]);


        SpawnNextRandomSection(firstSpawnPosition);
        SpawnNextRandomSection();

    }


    void Update()
    {

    }


    public void SpawnNextRandomSection(Vector3? spawnPoint = null)
    {
        int rnd = Random.Range(0, mapSectionsToSpawn.Count);
        GameObject selectedSection = mapSectionsToSpawn[rnd];


        Vector3 spawnPos = spawnPoint ?? (activeSections[activeSections.Count - 1].NextSectionSpawnPosition.position);
        spawnPos.y += selectedSection.transform.localScale.y / 2;
        GameObject spawned = Instantiate(selectedSection, spawnPos, Quaternion.identity);

        MapSection mapSection = spawned.GetComponent<MapSection>();
        mapSection.spawnNextSectionEvent += SpawnNextRandomSection;
        activeSections.Add(mapSection);

        if (activeSections.Count > 4)
            DestroyOlderSection();
    }

    public void DestroyOlderSection()
    {
        Destroy(activeSections[0].gameObject);
        activeSections.RemoveAt(0);
    }
}
