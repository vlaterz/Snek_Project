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
    }
}
