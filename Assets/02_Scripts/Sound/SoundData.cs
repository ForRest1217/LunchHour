using UnityEngine;

[CreateAssetMenu(fileName = "SoundData")]
public class SoundData : ScriptableObject
{
    public SoundType soundType;
    public AudioClip audioClip;
}

public enum SoundType
{
    SFX,
    BGM,
    System,
}
