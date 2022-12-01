using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    private CoinSpawnPoint[] _coinSpawnPoints;

    private void Start()
    {
        _coinSpawnPoints = GetComponentsInChildren<CoinSpawnPoint>();

        if (_coinSpawnPoints.Length > 0)
        {
            StartCoroutine(SpawnCoins());
        }
    }

    private IEnumerator SpawnCoins()
    {
        WaitForSeconds waitToNextSpawn = new WaitForSeconds(3);
        int spawnPointsCount = _coinSpawnPoints.Length;
        int currentSpawnPointIndex = 0;

        while(true)
        {
            _coinSpawnPoints[currentSpawnPointIndex].SpawnCoin();
            currentSpawnPointIndex++;

            if (currentSpawnPointIndex >= spawnPointsCount)
            {
                currentSpawnPointIndex = 0;
            }

            yield return waitToNextSpawn;
        }
    }
}
