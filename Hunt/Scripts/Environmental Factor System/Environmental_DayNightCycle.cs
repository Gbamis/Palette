using UnityEngine;
using Zenject;

namespace HT
{
    public class Environmental_DayNightCycle : MonoBehaviour
    {
        [Inject] private GameplayEvent gameplayEvent;

        private int _hour;
        private int _minute;
        private int day = 1;

        public UI_DayTime_View ui_dayTime;
        private DayTimeModel dayTimeModel;


        [Header("Time Params")]
        [SerializeField, Range(0, 24)] private float _timeOfDay;
        [SerializeField] private float _orbitSpeed;
        [SerializeField] private float _orbitSpeedMax;
        [SerializeField] private float _axisOffset;
        [SerializeField] private Gradient _nightLight;
        [SerializeField] private AnimationCurve _sunCurve;

        [Header("Sun")]
        [SerializeField] private Light _sun;
        [SerializeField] private Material SkyboxMaterial;
        [SerializeField] private AnimationCurve skyCurve;



        private void Start() => gameplayEvent.OnGetHourNormalized += () => _timeOfDay / 24;

        private void OnValidate() => ProgressTime();

        private void Update()
        {
            _orbitSpeed = (_timeOfDay > 7 && _timeOfDay < 15) ? 0.01f : _orbitSpeedMax;
            _timeOfDay += Time.deltaTime * _orbitSpeed;
            ProgressTime();
        }



        private void ProgressTime()
        {
            float currentTime = _timeOfDay / 24;
            float sunRotation = Mathf.Lerp(-90, 270, currentTime);

            _sun.transform.rotation = Quaternion.Euler(sunRotation, _axisOffset, 0);

            _hour = Mathf.FloorToInt(_timeOfDay);
            _minute = Mathf.FloorToInt(_timeOfDay / (24f / 1440f) % 60);

            RenderSettings.ambientLight = _nightLight.Evaluate(currentTime);
            _sun.intensity = _sunCurve.Evaluate(currentTime) + 8.5f;

            _timeOfDay %= 24;
            if (_hour > 23.9) { day++; }

            dayTimeModel.SetModel(day, _hour, _minute, _timeOfDay);
            ui_dayTime.SetData(dayTimeModel);


            if (SkyboxMaterial != null)
            {
                float code = skyCurve.Evaluate(currentTime) * 181;
                code /= 181;
                Color color = new(code, code, code);
                RenderSettings.skybox.SetColor("_Tint", color);
            }

        }
    }
}