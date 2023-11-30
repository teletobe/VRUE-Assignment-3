using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Test : MonoBehaviourPun, IPunObservable
{
    [SerializeField]
    public int x;
    [SerializeField]
    public int y;
    [SerializeField]
    public int z;
    [SerializeField]
    public int value;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(x);
            stream.SendNext(y);
            stream.SendNext(z);
        }
        else
        {
            x = (int)stream.ReceiveNext();
            y = (int)stream.ReceiveNext();
            z = (int)stream.ReceiveNext();
        }
    }
}
