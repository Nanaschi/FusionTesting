using UnityEngine;

namespace FPS.Scripts.Extensions
{
    public static class Extensions
    {
        public static Vector3 GetRandomSpawnPosition(this Vector3 vector3, float _rangeToSpawnXZ,float _yHeight = 0)
        {
            return new Vector3(Random.Range
                (-_rangeToSpawnXZ, _rangeToSpawnXZ), _yHeight, Random.Range(-_rangeToSpawnXZ, _rangeToSpawnXZ));
        }
    }
}