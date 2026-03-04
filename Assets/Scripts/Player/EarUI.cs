using UnityEngine;
using UnityEngine.UI;
public class EarUI : MonoBehaviour
{
    public Sprite[] earSprites;
    public float frameRate = 0.4f;
    private Image _earImage;
    private int _currentFrame = 0;
    private float _timer = 0f;
    void Start()
    {
        _earImage = GetComponent<Image>();
    }
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= frameRate)
        {
            _timer = 0f;
            _currentFrame = (_currentFrame + 1) % earSprites.Length;
            _earImage.sprite = earSprites[_currentFrame];
        }
    }
}