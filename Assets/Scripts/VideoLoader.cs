using UnityEngine;
using UnityEngine.Video;
using System.IO;

public class VideoLoader : MonoBehaviour
{
    [Header("References")]
    public VideoPlayer videoPlayer;
    public GameObject loadingPanel; // Assign your loading UI panel here
    public string videoFileName = "safetyVideo_compressed.mp4"; // The video inside StreamingAssets

    private bool isPrepared = false;
    private bool nextClicked = false;

    void Start()
    {
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer not assigned!");
            return;
        }

        if (loadingPanel) loadingPanel.SetActive(true);

        string videoPath;

#if UNITY_WEBGL
        // For WebGL, StreamingAssets are served from the same host as the build
        videoPath = Path.Combine(Application.streamingAssetsPath, videoFileName);
        videoPlayer.url = videoPath; 
#else
        // For Editor / PC builds
        videoPath = Path.Combine(Application.streamingAssetsPath, videoFileName);
        videoPlayer.url = "file:///" + videoPath;
#endif

        Debug.Log("Video URL: " + videoPlayer.url);

        // Subscribe to events
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.started += OnVideoStarted;
        videoPlayer.errorReceived += OnVideoError;

        // Start preparing the video
        videoPlayer.Prepare();
    }

    private void OnVideoPrepared(VideoPlayer vp)
    {
        Debug.Log("Video prepared. Ready to play.");
        isPrepared = true;

        // Auto play when ready if onclickplay pressed already
        if(nextClicked)
            vp.Play();
    }
    public void OnClickPlay()
    {
        nextClicked = true;
        if (isPrepared)
        {
            videoPlayer.Play();
        }
        else
        {
            Debug.LogWarning("Video is not yet prepared.");
        }
    }
    private void OnVideoStarted(VideoPlayer vp)
    {
        Debug.Log("Video started playing.");
        if (loadingPanel) loadingPanel.SetActive(false);
    }

    private void OnVideoError(VideoPlayer vp, string message)
    {
        Debug.LogError("Video error: " + message);
        if (loadingPanel) loadingPanel.SetActive(false);
    }
}
