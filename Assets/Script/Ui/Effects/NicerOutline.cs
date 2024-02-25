using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Ui.Effects
{
    public class NicerOutline : BaseMeshEffect
    {
        [SerializeField] private Color mEffectColor = new(0f, 0f, 0f, 0.5f);

        [SerializeField] private Vector2 mEffectDistance = new(1f, -1f);

        [SerializeField] private bool mUseGraphicAlpha = true;

        public Color EffectColor
        {
            get => mEffectColor;
            set
            {
                mEffectColor = value;
                if (graphic != null)
                {
                    graphic.SetVerticesDirty();
                }
            }
        }

        private Vector2 EffectDistance
        {
            get => mEffectDistance;
            set
            {
                if (value.x > 600f)
                {
                    value.x = 600f;
                }

                if (value.x < -600f)
                {
                    value.x = -600f;
                }

                if (value.y > 600f)
                {
                    value.y = 600f;
                }

                if (value.y < -600f)
                {
                    value.y = -600f;
                }

                if (mEffectDistance == value)
                {
                    return;
                }

                mEffectDistance = value;
                if (graphic != null)
                {
                    graphic.SetVerticesDirty();
                }
            }
        }

        public bool UseGraphicAlpha
        {
            get => mUseGraphicAlpha;
            set
            {
                mUseGraphicAlpha = value;
                if (graphic != null)
                {
                    graphic.SetVerticesDirty();
                }
            }
        }


        private void ApplyShadow(List<UIVertex> vert, Color32 color, int start, int end, float x, float y)
        {
            var num = vert.Count * 2;
            if (vert.Capacity < num)
            {
                vert.Capacity = num;
            }

            for (var i = start; i < end; i++)
            {
                var uIVertex = vert[i];
                vert.Add(uIVertex);

                var position = uIVertex.position;

                position.x += x;
                position.y += y;
                uIVertex.position = position;
                var color2 = color;
                if (mUseGraphicAlpha)
                {
                    color2.a = (byte)(color2.a * vert[i].color.a / 255);
                }

                uIVertex.color = color2;

                vert[i] = uIVertex;
            }
        }

        public override void ModifyMesh(VertexHelper vh)
        {
            if (!IsActive())
            {
                return;
            }

            var foundation = GetComponent<Text>();

            var bestFitAdjustment = 1f;

            if (foundation && foundation.resizeTextForBestFit)
            {
                bestFitAdjustment = (float)foundation.cachedTextGenerator.fontSizeUsedForBestFit /
                                    (foundation.resizeTextMaxSize - 1); //max size seems to be exclusive
            }

            var distanceX = EffectDistance.x * bestFitAdjustment;
            var distanceY = EffectDistance.y * bestFitAdjustment;

            var vert = new List<UIVertex>();
            vh.GetUIVertexStream(vert);

            var start = 0;
            var count = vert.Count;
            ApplyShadow(vert, EffectColor, start, vert.Count, distanceX, distanceY);
            start = count;
            count = vert.Count;
            ApplyShadow(vert, EffectColor, start, vert.Count, distanceX, -distanceY);
            start = count;
            count = vert.Count;
            ApplyShadow(vert, EffectColor, start, vert.Count, -distanceX, distanceY);
            start = count;
            count = vert.Count;
            ApplyShadow(vert, EffectColor, start, vert.Count, -distanceX, -distanceY);

            start = count;
            count = vert.Count;

            ApplyShadow(vert, EffectColor, start, vert.Count, distanceX, 0);
            start = count;
            count = vert.Count;

            ApplyShadow(vert, EffectColor, start, vert.Count, -distanceX, 0);

            start = count;
            count = vert.Count;
            ApplyShadow(vert, EffectColor, start, vert.Count, 0, distanceY);
            start = count;

            ApplyShadow(vert, EffectColor, start, vert.Count, 0, -distanceY);

            vh.Clear();
            vh.AddUIVertexTriangleStream(vert);
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            EffectDistance = mEffectDistance;
            base.OnValidate();
        }
#endif
    }
}