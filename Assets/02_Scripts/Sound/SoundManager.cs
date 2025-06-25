using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Single
    public static SoundManager Instance = null;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public SoundPlayer soundPlayerPrefab;

    public void PlaySound(SoundData soundData)
    {
        SoundPlayer soundPlayer = Instantiate(soundPlayerPrefab, transform);
        soundPlayer.PlaySound(soundData);
    }
}
