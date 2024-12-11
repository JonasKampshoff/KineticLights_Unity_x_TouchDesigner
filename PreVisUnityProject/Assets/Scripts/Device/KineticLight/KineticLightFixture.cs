using IA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class KineticLightFixture : DMXFixture
{
    public override int getUniverse { get { return universe; } }
    public override int getDmxAddress { get { return dmxAddress; } }
    public override Dictionary<string, int> getChannelFunctions { get { return channelFunctions; } }
    private Dictionary<string, int> channelFunctions = new Dictionary<string, int> { { ChannelName.LIFT, 0 }, { ChannelName.LIFT_FINE, 1 }, { ChannelName.SPEED, 2 }, { ChannelName.DIMMER, 3 }, { ChannelName.STROBE, 4 }, { ChannelName.RED, 5 }, { ChannelName.GREEN, 6 }, { ChannelName.BLUE, 7 }, { ChannelName.COLOR_MACRO, 8 }, { ChannelName.CONTROL, 9 }};

    [SerializeField] private KineticLight kineticLight;
    [SerializeField] private GameObject kineticLightGameObject;


    [SerializeField] private float maxHeight = 6;
    [SerializeField] private float heightOffset = -1;


    [Serializable]
    private struct Values
    {
        public float height;
        public Color color;
        public float dimmer;
    }

    [SerializeField] Values currentValues = new Values();


    void Update()
    {
        GetWireData();
    }

    private void FixedUpdate()
    {
        kineticLightGameObject.transform.localPosition = new Vector3 (0, - maxHeight * currentValues.height + heightOffset, 0);
    }


    void GetWireData()
    {
        if (artNetData.dmxDataMap != null)
        {
            currentValues.color.r = artNetData.dmxDataMap[universe - 1][dmxAddress - 1 + (int)channelFunctions[ChannelName.RED]] / 256f;
            currentValues.color.g = artNetData.dmxDataMap[universe - 1][dmxAddress - 1 + (int)channelFunctions[ChannelName.GREEN]] / 256f;
            currentValues.color.b = artNetData.dmxDataMap[universe - 1][dmxAddress - 1 + (int)channelFunctions[ChannelName.BLUE]] / 256f;
            currentValues.dimmer = artNetData.dmxDataMap[universe - 1][dmxAddress - 1 + (int)channelFunctions[ChannelName.DIMMER]] / 256f;

            currentValues.height = artNetData.dmxDataMap[universe - 1][dmxAddress - 1 + (int)channelFunctions[ChannelName.LIFT_FINE]] / 65536f + artNetData.dmxDataMap[universe - 1][dmxAddress - 1 + (int)channelFunctions[ChannelName.LIFT]] / 256f;

            kineticLight.SetColor(currentValues.color, currentValues.dimmer);
        }
    }
}

