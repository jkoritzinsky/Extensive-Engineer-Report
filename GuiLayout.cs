using System;
using UnityEngine;

namespace JKorTech.Extensive_Engineer_Report
{
    /// <summary>
    /// Class wrapper that (ab)uses using statements to ensure matched <see cref="GUILayout"/> method calls. 
    /// </summary>
    class GuiLayout : IDisposable
    {
        public enum Method
        {
            Horizontal,
            Vertical,
            ScrollView,
            Area
        }

        private Method method;
        
        public GuiLayout(Method method, ref Vector2 scrollPos)
        {
            this.method = method;
            if (method == Method.ScrollView)
                scrollPos = GUILayout.BeginScrollView(scrollPos);
            else
                throw new ArgumentException(nameof(method));
        }

        public GuiLayout(Method method, Rect areaRect)
        {
            this.method = method;
            if (method == Method.ScrollView)
                GUILayout.BeginArea(areaRect);
            else
                throw new ArgumentException(nameof(method));
        }

        public GuiLayout(Method method)
        {
            this.method = method;
            switch (method)
            {
                case Method.Horizontal:
                    GUILayout.BeginHorizontal();
                    break;
                case Method.Vertical:
                    GUILayout.BeginVertical();
                    break;
                default:
                    throw new ArgumentException(nameof(method));
            }
        }

        public void Dispose()
        {
            switch (method)
            {
                case Method.Horizontal:
                    GUILayout.EndHorizontal();
                    break;
                case Method.Vertical:
                    GUILayout.EndVertical();
                    break;
                case Method.ScrollView:
                    GUILayout.EndScrollView();
                    break;
                case Method.Area:
                    GUILayout.EndArea();
                    break;
                default:
                    break;
            }
        }
    }
}
