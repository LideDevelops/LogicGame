using System.Collections.Generic;
using System.Linq;
using CuriosOtter.LogicGame.Gates;
using TMPro;
using UnityEngine;

namespace Utility
{
    public static class ConnectionPointSpawner
    {
        public static void SpawnConnectionPoints(Transform transform, SpriteRenderer renderer, IEnumerable<string> pointName, bool onLeftSide)
        {
            var connectionDotPrefab = Resources.Load<GameObject>("Prefabs/ConnectionDot");
            for (int i = 0; i < pointName.Count(); i++)
            {
                var connectionDot = GameObject.Instantiate(connectionDotPrefab, transform);
                var rectTransform = connectionDot.GetComponent<RectTransform>();
                var topOfGate = transform.up * (renderer.size.y / 2);
                var yPos = CalculateYPosBaseOnIndex(topOfGate, i, pointName, renderer);
                var leftOfGate = (onLeftSide ? -1 : 1) * (transform.right * (renderer.size.x / 2));
                rectTransform.position = transform.position + new Vector3(0, yPos) + leftOfGate;
                connectionDot.GetComponent<TextMeshPro>().text = pointName.ElementAt(i);
            }
        }

        private static float CalculateYPosBaseOnIndex(Vector3 topOfGate, int i, IEnumerable<string> pointName, SpriteRenderer renderer)
        {
            return Mathf.Lerp(topOfGate.y, topOfGate.y - renderer.size.y, (float)(i + 1) / (pointName.Count() + 1));
        }
    }
}