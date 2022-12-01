using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnPoint : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;

    private Coin _coin;

    private void Start()
    {
        _coin = Instantiate(_coinPrefab, transform.position, Quaternion.identity);
    }

    public void SpawnCoin()
    {
        _coin.gameObject.SetActive(true);
    }
}
