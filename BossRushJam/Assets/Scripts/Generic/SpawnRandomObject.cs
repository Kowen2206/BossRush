using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomObject : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objects;
    [SerializeField] private List<int> _probabilities;

    GameObject GetRandomObject()
    {
        int totalProbabilities = 0;
        foreach (int probability in _probabilities)
        {
            totalProbabilities += probability;
        }

        int randomValue = Random.Range(0, totalProbabilities);

        int probabilitiesAcumulator = 0;
        for (int i = 0; i < _objects.Count; i++)
        {
            probabilitiesAcumulator += _probabilities[i];
            if (randomValue < probabilitiesAcumulator)
            {
                return _objects[i];
            }
        }
        return null;
    }

    public void SpawnObject()
    {
        GameObject randomOBJ = GetRandomObject();
        Instantiate(randomOBJ , transform.position, transform.rotation);
    }
}
