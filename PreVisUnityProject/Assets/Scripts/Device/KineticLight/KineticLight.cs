using System.CodeDom;
using UnityEngine;

public class KineticLight : MonoBehaviour
{
    [SerializeField] private Renderer renderer;
    [SerializeField] private Light light;

    [SerializeField] private Color color;

    private void Start()
    {
    }

    public void SetColor(Color color, float dimmer)
    {
        this.color = color;
        renderer.material.SetColor("_EmissiveColor", color * 20 * dimmer);

        //renderer.material.color = color;

        light.color = color;
        light.intensity = 18 * dimmer;
    }
}
