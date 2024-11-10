using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEditor;

public class Clicker : MonoBehaviour
{
    
    public TextMeshProUGUI cpsText;
    [Header("Animation")]
    public float scale = 1.2f;
    public float duration = 0.5f;
    public Ease ease = Ease.OutElastic;

    [Header("Audio")]
    public AudioClip clickSound;

    [Header("VFX")]

    public ParticleSystem clickVFX;

    [HideInInspector]public int clicks = 0;
    private AudioSource audioSource;
    private Shop shop;

    private int clickCount = 0; 
    private float elapsedTime = 0f; 
    private float clicksPerSecond = 0f; 

    private void Start() 
    {
        shop = FindObjectOfType<Shop>();
        
        clicks = PlayerPrefs.GetInt("clicks", 0);
        shop.chefCount = PlayerPrefs.GetInt("chef", 0);
        audioSource = GetComponent<AudioSource>();
        
        
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        
        
        if (elapsedTime >= 1f)
        {
            clicksPerSecond = clickCount / elapsedTime + shop.chefCount;
            clickCount = 0;
            elapsedTime = 0f;
        }

       
        if (cpsText != null)
        {
            cpsText.text = "CPS: " + clicksPerSecond.ToString("F2"); 
        }
    }

    private void OnMouseDown() 
    {
        clickVFX.Emit(1);
        clickCount++;
        clicks++;
        UiManager.instance.UpdateClicks(clicks);

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(clickSound);

        transform
            .DOScale(1, duration)
            .ChangeStartValue(scale * Vector3.one)
            .SetEase(ease);
            //.SetLoops(2, LoopType.Yoyo);3
    }
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
            Save();
    }
    private void OnApplicationQuit()
    {
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetInt("clicks", clicks);
        
        PlayerPrefs.Save();
    }
}
