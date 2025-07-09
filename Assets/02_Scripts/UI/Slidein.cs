using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class Slidein : MonoBehaviour
{
    [SerializeField] private GameObject[] start;
    private Animator animator;
    [SerializeField] private AudioMixer audioMixer;

    private void Start()
    {
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        float systemVolume = PlayerPrefs.GetFloat("SystemVolume", 1f);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20f);
        audioMixer.SetFloat("SystemVolume", Mathf.Log10(systemVolume) * 20f);
        StartCoroutine(StartSlide());
    }

    private IEnumerator StartSlide()
    {

        yield return new WaitForSeconds(0.25f);
        animator = start[0].GetComponent<Animator>();
        animator.Play("slidestart1");
        yield return new WaitForSeconds(0.25f);
        animator = start[1].GetComponent<Animator>();
        animator.Play("slidestart2");
        yield return new WaitForSeconds(0.25f);
        animator = start[2].GetComponent<Animator>();
        animator.Play("slidestart3");
        yield return new WaitForSeconds(0.25f);
        animator = start[3].GetComponent<Animator>();
        animator.Play("slidestart4");
    }
}
