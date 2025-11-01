using UnityEngine;


public class CertificateManager : MonoBehaviour
{
    public static CertificateManager Instance;
    public GameObject certificatePanel;


    void Awake() { if (Instance == null) Instance = this; else Destroy(gameObject); }


    public void ShowCertificate()
    {
        if (certificatePanel) certificatePanel.SetActive(true);
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySound(SoundManager.Instance.guide_Success);
    }
}