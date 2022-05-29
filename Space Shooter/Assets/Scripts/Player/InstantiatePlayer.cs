
using UnityEngine;

public class InstantiatePlayer : MonoBehaviour
{
    public GameObject shipSpawnPosition;

    public GameObject[] PlayerPrefabs;
    int currentShip;

    private void Awake()
    {
        currentShip = PlayerPrefs.GetInt("currentShipNumber", 1);

        (Instantiate(PlayerPrefabs[currentShip - 1], shipSpawnPosition.transform.position, shipSpawnPosition.transform.rotation) as GameObject).transform.parent = shipSpawnPosition.transform;

    }

}
