using UnityEngine;

namespace Assets.Scripts.Data
{
    /// <summary>
    /// Данные игрока, чтобы можно было ресаться на чекпоинтах
    /// </summary>
    public struct PlayerLevelData
    {
        public Vector3 LastCheckPointPosition;
        public int CollectedDiamonds;
        public int HumansEaten;

        public void ResetParamsToZero()
        {
            HumansEaten = 0;
            CollectedDiamonds = 0;
        }
    }
}
