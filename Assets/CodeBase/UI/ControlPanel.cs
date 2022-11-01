using CodeBase.Сube;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class ControlPanel : MonoBehaviour
    {
        [SerializeField] private CubeFactory _cubeFactory;

        [SerializeField] private TextMeshProUGUI _timeSpawnValue;
        [SerializeField] private Slider _timeSpawn;
        [SerializeField] private TextMeshProUGUI _speedValue;
        [SerializeField] private Slider _speed;
        [SerializeField] private TextMeshProUGUI _distanceValue;
        [SerializeField] private Slider _distance;

        private float _pastSpawnTime;

        private void Awake() => 
            UpdateTextFields();

        private void Start()
        {
            _timeSpawn.onValueChanged.AddListener(_ => TimeSpawnChanged());
            _speed.onValueChanged.AddListener(_ => SpeedChanged());
            _distance.onValueChanged.AddListener(_ => DistanceChanged());
        }

        private void Update() => 
            SpawnCube();

        private void SpawnCube()
        {
            _pastSpawnTime += Time.deltaTime;
            if (_timeSpawn.value <= _pastSpawnTime)
            {
                _pastSpawnTime = 0;
                var cube = _cubeFactory.GetNewInstance().GetComponent<CubeMovement>();
                cube.SetSpeed(_speed.value);
                cube.SetDistance(_distance.value);
            }
        }

        private void UpdateTextFields()
        {
            TimeSpawnChanged();
            SpeedChanged();
            DistanceChanged();
        }
        
        private void TimeSpawnChanged() => 
            _timeSpawnValue.text = $"Time Spawn: {_timeSpawn.value}s";

        private void SpeedChanged() => 
            _speedValue.text = $"Speed: {_speed.value}";

        private void DistanceChanged() => 
            _distanceValue.text = $"Distance: {_distance.value}";
    }
}