using UnityEngine;

namespace RhinoLoader
{
    public static class ColorExtention
    {
        public static Color ToUnity(this System.Drawing.Color color)
        {
            return new Color(color.R / 255f, color.G / 255f, color.B / 255f);
        }
    }
}