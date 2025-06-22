using UnityEngine;
using SharpOSC;

public class ProcessOSCMessages : MonoBehaviour
{
    public OscReceiver oscReceiver;
   
    public static byte[] values = new byte[512];

    void Update()
    {
        for (int i = 0; i < 50 && oscReceiver.HasMessagesWaiting(); i++)
        {
            OscMessage oscMessage = oscReceiver.GetNextMessage();
            ProcessMessage(oscMessage);
        }
    }
    void ProcessMessage(OscMessage oscMessage)
    {
        if (oscMessage.Address.StartsWith('/'))
        {
            if(int.TryParse(oscMessage.Address.TrimStart('/'),out int channel))
                values[channel] = byte.Parse(oscMessage.Arguments[0].ToString());
        }
    }
}