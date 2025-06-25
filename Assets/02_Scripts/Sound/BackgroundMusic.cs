using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private SoundData soundData;

    private void OnEnable()
    {
        SoundManager.Instance.PlaySound(soundData);
    }
}
