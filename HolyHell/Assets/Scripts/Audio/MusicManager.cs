using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    // Exposed mixer parameters
    public AudioMixer audioMixer;
    public string heavyMusicVolumeParameter = "heavyMusicVolume";
    public string calmMusicVolumeParameter = "calmMusicVolume";
    public string filterMenuParameter = "menuFilter";

    // Volume control
    public float maxVolume = -8f;
    public float minVolume = -50f;

    // Music state
    public bool shouldPlayHeavy = false;

    // Transition settings
    public float transitionDuration = 4.0f; // Duration of transition in seconds
    private float transitionProgress = 0.0f; // Progress of the transition (0 to 1)

    private float currentHeavyVolume;
    private float currentCalmVolume;

    private float targetHeavyVolume;
    private float targetCalmVolume;

    private float maxFilterValue = 22000, minFilterValue = 500;

    public PlayerSystem playerSystem;
    public CanvasController canvasController;
    private GameObject playerObject;

    void Start()
    {
        // Initialize volume states
        targetHeavyVolume = minVolume;
        targetCalmVolume = minVolume;

        currentHeavyVolume = targetHeavyVolume;
        currentCalmVolume = targetCalmVolume;

        audioMixer.SetFloat(heavyMusicVolumeParameter, currentHeavyVolume);
        audioMixer.SetFloat(calmMusicVolumeParameter, currentCalmVolume);
        audioMixer.SetFloat(filterMenuParameter, maxFilterValue);

        GameManager.OnPlayerInstantiated += HandlePlayerInstantiated;

        // Check if the player is already cached
        if (GameManager.CachedPlayerInstance != null)
        {
            HandlePlayerInstantiated(GameManager.CachedPlayerInstance);
        }
    }

    private float delayTimer = 0.0f; 

    void Update()
    {

        if(canvasController.isMenuActive){
            audioMixer.SetFloat(filterMenuParameter, minFilterValue);
        } else audioMixer.SetFloat(filterMenuParameter, maxFilterValue);


        delayTimer += Time.deltaTime;

        if (delayTimer < 5.5f)
        return; 

        Debug.Log("START MUSIC");
        if (playerSystem.heavyMusic != shouldPlayHeavy)
        {
            shouldPlayHeavy = playerSystem.heavyMusic;
            targetHeavyVolume = shouldPlayHeavy ? maxVolume : minVolume;
            targetCalmVolume = shouldPlayHeavy ? minVolume : maxVolume;
            transitionProgress = 0.0f; 
        }


        if (transitionProgress < 1.0f)
        {
            transitionProgress += Time.deltaTime / transitionDuration;
            transitionProgress = Mathf.Clamp01(transitionProgress);

            currentHeavyVolume = Mathf.Lerp(currentHeavyVolume, targetHeavyVolume, transitionProgress);
            currentCalmVolume = Mathf.Lerp(currentCalmVolume, targetCalmVolume, transitionProgress);

            audioMixer.SetFloat(heavyMusicVolumeParameter, currentHeavyVolume);
            audioMixer.SetFloat(calmMusicVolumeParameter, currentCalmVolume);
        }
    }

    private void HandlePlayerInstantiated(GameObject playerInstance)
    {
        playerObject = playerInstance;
        playerSystem = playerObject.transform.GetChild(0).GetComponent<PlayerSystem>();
        canvasController = playerObject.transform.GetChild(1).GetComponent<CanvasController>();
    }
}