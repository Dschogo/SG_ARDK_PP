namespace Mapbox.Examples
{
    using UnityEngine;
    using Mapbox.Utils;
    using Mapbox.Unity.Map;
    using Mapbox.Unity.MeshGeneration.Factories;
    using Mapbox.Unity.Utilities;
    using System.Collections.Generic;

    public class SpawnOnMap : MonoBehaviour
    {
        [SerializeField]
        AbstractMap _map;

        [SerializeField]
        // [Geocode]
        // string[] _locationStrings;
        Vector2d[] _locations;

        float _spawnScale;

        [SerializeField]
        GameObject _markerPrefab;

        List<GameObject> _spawnedObjects;

        void Start()
        {
            string[] _locationStrings = new string[1];
            _spawnScale = Stateholder.radius / 2;

            if (Stateholder.pois[Stateholder.curr_poi].following != null)
            {
                _locationStrings[0] = Stateholder.pois[Stateholder.curr_poi].following[Stateholder.curr_schnitzel_counter].coordinates[0].ToString() + ", " + Stateholder.pois[Stateholder.curr_poi].following[Stateholder.curr_schnitzel_counter].coordinates[1].ToString();

            }
            else
            {
                _locationStrings[0] = Stateholder.pois[Stateholder.curr_poi].coordinates[0].ToString() + ", " + Stateholder.pois[Stateholder.curr_poi].coordinates[1].ToString();
            }
            _locations = new Vector2d[_locationStrings.Length];
            _spawnedObjects = new List<GameObject>();
            for (int i = 0; i < _locationStrings.Length; i++)
            {
                var locationString = _locationStrings[i];
                _locations[i] = Conversions.StringToLatLon(locationString);
                var instance = Instantiate(_markerPrefab);
                instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
                instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
                _spawnedObjects.Add(instance);
            }
        }

        private void Update()
        {
            int count = _spawnedObjects.Count;
            for (int i = 0; i < count; i++)
            {
                var spawnedObject = _spawnedObjects[i];
                var location = _locations[i];
                spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
                spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            }
        }
    }
}