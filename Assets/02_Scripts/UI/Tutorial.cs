using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private RectTransform slidePanel;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private SoundData soundData;
    [SerializeField] private int total = 3;
    private float slideSpeed = 0.5f;
    private float slideWidth = 15f;

    private int currentIndex = 0;
    private void Start()
    {
        UpdateButtonInteractable();
    }
    private void UpdateButtonInteractable()
    {
        leftButton.interactable = currentIndex > 0;
        rightButton.interactable = currentIndex < total - 1;
    }

    public void MoveLeft()
    {
        if (currentIndex == 0) return;
        currentIndex--;
        SoundManager.Instance.PlaySound(soundData);
        SlideToCurrent();
    }

    public void MoveRight()
    {
        if (currentIndex >= total - 1) return;
        currentIndex++;
        SoundManager.Instance.PlaySound(soundData);
        SlideToCurrent();
    }

    private void SlideToCurrent()
    {
        float targetX = -currentIndex * slideWidth;
        slidePanel.DOAnchorPosX(targetX, slideSpeed).SetEase(Ease.InOutQuad);
        UpdateButtonInteractable();
    }
}
