using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;
    private GameObject spawnedTrackPrefab;

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Prefabs/Network Player", transform.position, transform.rotation);
        Debug.Log(transform.position);

        // track
        Vector3 distance = new Vector3(PhotonNetwork.PlayerList.Length * 10, 0, 0);
        spawnedTrackPrefab = PhotonNetwork.Instantiate("Prefabs/Track", distance, Quaternion.identity);

    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
        PhotonNetwork.Destroy(spawnedTrackPrefab);

    }
}
