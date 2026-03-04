using UnityEngine;
using UnityEngine.UI;
public class PlayerNoise
{
    private float _currentNoise;
    private float _minWalkNoise;
    private float _maxWalkNoise;
    private float _noiseBuildUpSpeed;
    private float _noiseDecaySpeed;
    private Slider _noiseSlider;
    public float CurrentNoise => _currentNoise;
    public PlayerNoise(float minWalkNoise,float maxWalkNoise,float buildUpSpeed,float decaySpeed,Slider noiseSlider)
    {
        _minWalkNoise = minWalkNoise;
        _maxWalkNoise = maxWalkNoise;
        _noiseBuildUpSpeed = buildUpSpeed;
        _noiseDecaySpeed = decaySpeed;
        _noiseSlider = noiseSlider;
        if (_noiseSlider != null)
        {
            _noiseSlider.maxValue = _maxWalkNoise;
            _noiseSlider.value = 0;
        }
    }
    public void UpdateNoise(bool isMoving, float deltaTime)
    {
        if (isMoving)
        {
            if (_currentNoise < _minWalkNoise)
            {
                _currentNoise = _minWalkNoise;
            }
            else
            {
                _currentNoise += _noiseBuildUpSpeed * deltaTime;
                _currentNoise = Mathf.Clamp(_currentNoise, _minWalkNoise, _maxWalkNoise);
            }
        }
        else
        {
            _currentNoise = Mathf.MoveTowards(_currentNoise, 0f, _noiseDecaySpeed * deltaTime);
        }

        UpdateSlider();
        Debug.Log(_currentNoise);
    }
    private void UpdateSlider()
    {
        if (_noiseSlider != null)
        {
            _noiseSlider.value = _currentNoise;
        }
    }
    public void ResetNoise()
    {
        _currentNoise = 0;
        UpdateSlider();
    }
}