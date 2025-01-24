using UnityEngine;

public class FootstepSystem : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip wood;
    public AudioClip grass;
    public AudioClip rock;

    RaycastHit hit;
    public Transform rayStart;
    public float range;
    public LayerMask layerMask;
    public Animator animator;

    private void Update() {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
            animator.SetBool("Walking", true);
            if(Input.GetKey(KeyCode.LeftShift)) animator.speed = 1.71f;
            else animator.speed = 1.0f;
        } else {
            animator.SetBool("Walking", false);
        }
    }

    public void Footstep() {
        if(Physics.Raycast(rayStart.position, rayStart.transform.up * -1, out hit, range, layerMask)) {
            if(hit.collider.CompareTag("Wood")) {
                PlayFootstepSound(wood);
            }
            if(hit.collider.CompareTag("Grass")) {
                PlayFootstepSound(grass);
            }
            if(hit.collider.CompareTag("Rock")) {
                PlayFootstepSound(rock);
            }
        }
    }

    void PlayFootstepSound(AudioClip audio) {
        audioSource.pitch = Random.Range(0.8f, 1f);
        audioSource.PlayOneShot(audio);
    }
}
