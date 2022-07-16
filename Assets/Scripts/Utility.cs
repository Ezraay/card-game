using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    public class Utility
    {
        private static readonly Dictionary<int, string> romanNumerals = new Dictionary<int, string>
        {
            {1, "I"}, {2, "II"}, {3, "III"}, {4, "IV"}, {5, "V"}
        };

        public static string GetRomanNumerals(int a)
        {
            romanNumerals.TryGetValue(a, out var value);
            return value;
        }

        public static Vector3 RaycastMousePoint(float height = 0f)
        {
            var plane = new Plane(Vector3.up, new Vector3(0, height, 0));
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            plane.Raycast(ray, out var enter);
            var position = ray.GetPoint(enter);
            return position;
        }
    }
}