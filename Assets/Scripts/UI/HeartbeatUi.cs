using UnityEngine;
using UnityEngine.UI;
public class HeartBeatUI : MonoBehaviour
{
    public Sprite[] heartSprites;
    public float frameRate = 0.4f;
    private Image _heartImage;
    private int _currentFrame = 0;
    private float _timer = 0f;
    void Start()
    {
        _heartImage = GetComponent<Image>();
    }
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= frameRate)
        {
            _timer = 0f;
            _currentFrame = (_currentFrame + 1) % heartSprites.Length;
            _heartImage.sprite = heartSprites[_currentFrame];
        }
    }
}