using UnityEngine;
using UnityEngine.UI;


public class AudioController : MonoBehaviour
{
   public GameObject AudioOn;
   public GameObject AudioOff;
   
   public AudioSource audioSource;
   
  
   
   public  AudioClip audioClip;

   public Slider volumeSlider;
   
   void Update()
   {
    audioSource.volume = volumeSlider.value;

    UpdateAudioButtons();
    
   }

   public void OffAudio()
   {
      audioSource.volume = 0;
      UpdateAudioButtons();
   }

   public void OnAudio()
   {
       audioSource.volume = volumeSlider.value > 0 ? volumeSlider.value : 1f;
       UpdateAudioButtons();
   }
   void Start()
   {
       UpdateAudioButtons();
   }

   private void UpdateAudioButtons()
   {
       bool isMuted = audioSource.volume == 0;
       AudioOn.SetActive(!isMuted);
       AudioOff.SetActive(isMuted);
      
   }
   

   public void PlayAudio()
   {
       audioSource.PlayOneShot(audioClip);
   }
}
