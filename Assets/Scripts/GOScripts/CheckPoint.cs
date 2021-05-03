using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.GOScripts
{
    public class CheckPoint : MonoBehaviour
    {
        public Color Color;
        private Renderer _meshRenderer;

        //На старте задаем случайный "правильный" цвет чекпоинту и всем дочерним группам людей, которые помечены
        void Start()
        {
            var allColors = GameController.GetInstance.GameColors;

            Color = allColors[Random.Range(0, allColors.GetUpperBound(0))];

            _meshRenderer = GetComponentInChildren<Renderer>();
            _meshRenderer.material.EnableKeyword("_EMISSION");
            _meshRenderer.material.SetColor("_BaseColor", Color);

            var selectedColors = allColors.Where(a => a != Color).ToArray();
            var someRandomColor = selectedColors[Random.Range(0, selectedColors.GetUpperBound(0))];
            var humanGroups = GetComponentsInChildren<HumanGroup>();
            foreach (var humanGroup in humanGroups)
                humanGroup.SetHumansColor(humanGroup.IsNeededColor ? Color : someRandomColor);
        }
    }
}
