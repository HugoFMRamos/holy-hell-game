using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    // Exposed mixer parameters
    public AudioMixer audioMixer;
    public string heavyMusicVolumeParameter = "heavyMusicVolume";
    public string calmMusicVolumeParameter = "calmMusicVolume";

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

    public PlayerSystem playerSystem;

    void Start()
    {
        // Initialize volume states
        targetHeavyVolume = minVolume;
        targetCalmVolume = minVolume;

        currentHeavyVolume = targetHeavyVolume;
        currentCalmVolume = targetCalmVolume;

        audioMixer.SetFloat(heavyMusicVolumeParameter, currentHeavyVolume);
        audioMixer.SetFloat(calmMusicVolumeParameter, currentCalmVolume);
    }

    void Update()
    {
        // Update target state
        bool newShouldPlayHeavy = playerSystem.heavyMusic;
        if (newShouldPlayHeavy != shouldPlayHeavy)
        {
            // Start a new transition
            shouldPlayHeavy = newShouldPlayHeavy;
            targetHeavyVolume = shouldPlayHeavy ? maxVolume : minVolume;
            targetCalmVolume = shouldPlayHeavy ? minVolume : maxVolume;
            transitionProgress = 0.0f; // Reset the transition progress
        }

        // Smoothly transition the volumes
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
}