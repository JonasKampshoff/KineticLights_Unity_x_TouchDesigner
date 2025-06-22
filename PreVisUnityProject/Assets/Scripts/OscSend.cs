using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpOSC;

public class OscSend : MonoBehaviour
{
    UDPSender sender;
    void Start()
    {
        sender = new UDPSender("127.0.0.1", 55554);
    }
    void Update()
    {
        Debug.Log("Send");
        OscMessage messagex = new ("/p1/head:tx", -transform.position.z / 5f);
        OscMessage messagey = new ("/p1/head:ty", transform.position.y / 5f - 1);
        OscMessage messagez = new ("/p1/head:tz", (-transform.position.x + 5) / 10f);
        sender.Send(messagex);
        sender.Send(messagey);
        sender.Send(messagez);

    }
}