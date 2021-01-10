using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class DayNightCircle : MonoBehaviour
{
    [SerializeField]private Gradient gradient;
    [SerializeField] private float secondsPerDay = 30;

    private Light2D _light2D;
    private float _dayTimeDuration;
    private float _dayTime;

    private void Awake()
    {
        _light2D = GetComponent<Light2D>();
        _dayTimeDuration = 1 / secondsPerDay;
    }

    private void Update()
    {
        _dayTime += Time.deltaTime * _dayTimeDuration;
        //need to normalize our dayTime our datetime constantly increases thats why we can devide to 1f
        _light2D.color = gradient.Evaluate(_dayTime % 1f);
    }

}
