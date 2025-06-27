using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Movement
{
    public class Stamina : MonoBehaviour
    {
        [Header("--- Spawn ---")] 
        
        [SerializeField] private GameObject player;
        private PlayerMovement playerMovement;

        [Header("--- Stamina ---")]
        public float maxStamina;
        public float stamina;
        public float staminaDecrease;
        public float staminaIncrease;
        public float recoveryStamina;
        
        [Header("--- UI ---")]
        public Image staminaBar;
        public CanvasGroup staminaGroup;
        public float fadeDuration;
        public float fadeOutDelay;
        public bool isFadedOut = false;
        private float fullStaminaTime;
        private float initialBarWidth;
        

        private void Start()
        {
            playerMovement = player.GetComponentInParent<PlayerMovement>();
            
            stamina = maxStamina;
            
            initialBarWidth = staminaBar.rectTransform.rect.width;
        }

        private void Update()
        {
            StaminaChange();
            StabiliseStamina();
            StaminaBar();
        }

        private void StaminaChange()
        {
            if (playerMovement.isSprinting && playerMovement.canSprint)
            {
                FadeIn();
                if (stamina > 0f)
                {
                    stamina -= staminaDecrease;
                }
                else
                {
                    playerMovement.canSprint = false;
                }
            }
            else
            {
                if (stamina < maxStamina)
                {
                    stamina += staminaIncrease;
                }
            }
            
            if (stamina >= maxStamina && !isFadedOut)
            {
                fullStaminaTime += Time.deltaTime;
                if (fullStaminaTime >= fadeOutDelay)
                {
                    FadeOut();
                }
            }
            
            if (stamina > recoveryStamina && !playerMovement.canSprint)
            {
                playerMovement.canSprint = true;
            }
        }

        private void StabiliseStamina()
        {
            stamina = Mathf.Clamp(stamina, 0f, maxStamina);
        }

        private void StaminaBar()
        {
            float staminaRatio = stamina / maxStamina;
            float newWidth = initialBarWidth * staminaRatio;
            
            staminaBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
        }

        private void FadeIn()
        {
            StartCoroutine(Fade(1f, fadeDuration));
            isFadedOut = false;
        }

        private void FadeOut()
        {
            StartCoroutine(Fade(0f, fadeDuration));
            isFadedOut = true;
        }
        
        private IEnumerator Fade(float targetAlpha, float duration)
        {
            float startAlpha = staminaGroup.alpha;
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                staminaGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
                yield return null;
            }

            staminaGroup.alpha = targetAlpha;
        }
    }
}