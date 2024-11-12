using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audmanager : MonoBehaviour
{
    public AudioSource PacManEatSound;

    public void PlayPacManEatSound()
    {
        PacManEatSound.Play();
    }
}
