using System.Collections.Generic;

using UnityEngine;

using ZL.Unity.Collections;

namespace ZL.Unity
{
    public sealed class Discolorator
    {
        private Color color = Color.red;

        public Color Color => color;

        private float deltaH;

        private float deltaS;

        private float deltaV;

        public Discolorator(ColorPalette color) : this(color.ToColor()) { }

        public Discolorator(Color color)
        {
            this.color = color;

            Reset();
        }

        public void Reset()
        {
            moveHSVRoutile = MoveHSVRoutile(color);
        }

        public Color MoveHSV(float deltaH, float deltaS, float deltaV)
        {
            this.deltaH = deltaH;

            this.deltaS = deltaS;

            this.deltaV = deltaV;

            moveHSVRoutile.MoveNext();

            return moveHSVRoutile.Current;
        }

        private IEnumerator<Color> moveHSVRoutile;

        private IEnumerator<Color> MoveHSVRoutile(Color color)
        {
            Color.RGBToHSV(color, out float H, out float S, out float v);

            Linear linearH = new(H, 1f, LinearMode.Repeat);

            Linear linearS = new(S, 1f, LinearMode.PingPong);

            Linear linearV = new(v, 1f, LinearMode.PingPong);

            while (true)
            {
                yield return Color.HSVToRGB(H, S, v);

                H = linearH.Interval(deltaH, LinearMode.Repeat);

                S = linearS.Interval(deltaS, LinearMode.PingPong);

                v = linearV.Interval(deltaV, LinearMode.Repeat);
            }
        }
    }
}