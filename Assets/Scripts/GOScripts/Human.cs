using UnityEngine;

namespace Assets.Scripts.GOScripts
{
    public class Human : MonoBehaviour
    {
        public Color HumanColor;

        public void SetColor(Color color)
        {
            var renderer = GetComponent<Renderer>();
            renderer.material.EnableKeyword("_EMISSION");
            renderer.material.SetColor("_BaseColor", color);
            HumanColor = color;
        }
    }
}
