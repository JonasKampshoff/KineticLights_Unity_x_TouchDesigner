using UnityEngine;

public class KineticLightOSC : MonoBehaviour
{
    [SerializeField] private int dmxAddress;

    [SerializeField] private KineticLight kineticLight;
    [SerializeField] private GameObject kineticLightGameObject;

    [SerializeField] private float maxHeight = 6;
    [SerializeField] private float heightOffset = -1;
    [SerializeField] private float speedMultiplier = 1;


    //DMX
    private float aimedHeight = 0;
    private float speed = 1;
    private float dimmer = 1;
    private float red = 0;
    private float green = 0;
    private float blue = 0;


    private float currentHeight = 0;
    private Color color;

    void Update()
    {
        int value16bit = (ProcessOSCMessages.values[dmxAddress] << 8) | ProcessOSCMessages.values[dmxAddress + 1];
        aimedHeight = -(value16bit / 65535f) * maxHeight;
        speed = ProcessOSCMessages.values[dmxAddress + 2] / 255f;
        dimmer = ProcessOSCMessages.values[dmxAddress + 3] / 255f;
        red = ProcessOSCMessages.values[dmxAddress + 5] / 255f;
        green = ProcessOSCMessages.values[dmxAddress + 6] / 255f;
        blue = ProcessOSCMessages.values[dmxAddress + 7] / 255f;

        color.r = red; color.g = green; color.b = blue;

        kineticLight.SetColor(color,dimmer);
        currentHeight = Mathf.MoveTowards(currentHeight, aimedHeight, Time.deltaTime * speedMultiplier);
    }

    private void FixedUpdate()
    {
        kineticLightGameObject.transform.localPosition = new Vector3(0, currentHeight + heightOffset, 0);
    }
}
