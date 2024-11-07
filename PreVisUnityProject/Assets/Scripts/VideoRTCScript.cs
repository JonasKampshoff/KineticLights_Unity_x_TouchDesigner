using Unity.WebRTC;
using UnityEngine;

public class VideoRTCScript : MonoBehaviour
{

    [SerializeField] RenderTexture render;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        var receiveStream = new MediaStream();
        receiveStream.OnAddTrack = e =>
        {
            if (e.Track is VideoStreamTrack track)
            {
                // You can access received texture using `track.Texture` property.
            }
            else if (e.Track is AudioStreamTrack track2)
            {
                // This track is for audio.
            }
        };

        var peerConnection = new RTCPeerConnection();
        peerConnection.OnTrack = (RTCTrackEvent e) => {
            if (e.Track.Kind == TrackKind.Video)
            {
                // Add track to MediaStream for receiver.
                // This process triggers `OnAddTrack` event of `MediaStream`.
                receiveStream.AddTrack(e.Track);
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
