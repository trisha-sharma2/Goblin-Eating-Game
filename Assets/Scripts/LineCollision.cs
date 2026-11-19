using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LineCollision : MonoBehaviour
{
    private MainUIHandler mainUIHandler;

    private AudioSource audioSource;

    void Awake()
    {
        mainUIHandler = FindAnyObjectByType<MainUIHandler>();

        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.CompareTag("Goblin"))
        {
            audioSource.Play();
            mainUIHandler.GameOver();
        }
    }
}
