using UnityEngine;

namespace Assets.Scripts.GOScripts
{
    public class HumanGroup : MonoBehaviour
    {
        public bool IsNeededColor = false; // true: В начале раунда перекрасится в цвет чекпоинта, false: покрасится в какой-то другой

        public void SetHumansColor(Color color)
        {
            foreach (Transform child in transform)
                child.GetComponent<Human>().SetColor(color);
        }
    }
}
