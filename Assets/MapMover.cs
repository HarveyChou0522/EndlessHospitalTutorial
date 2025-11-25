using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMover : MonoBehaviour
{
    public float moveSpeed = 5f;
    //public MapSpawner spawner;
    public bool stopMove = false;

    // Update is called once per frame
    void Update()
    {
        if(stopMove) return;

        if(this.transform.position.x <= -4.99f)
        {
            Destroy(gameObject);
        }
        else
        {
            moveSpeed = MapSpawner.instance.mapMoveSpeed;
            this.transform.Translate(-1 * moveSpeed * Time.deltaTime, 0, 0);
        }
    }

    private void OnDestroy()
    {
        MapSpawner.instance.SpawnMap();
        MapSpawner.instance.DespawnMap(this.gameObject);
    }
}
