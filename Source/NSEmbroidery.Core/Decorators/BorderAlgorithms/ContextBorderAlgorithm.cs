using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NSEmbroidery.Core.Decorators.BorderAlgorithms
{
    class ContextBorderAlgorithm
    {
        private IBorderAlgorithm Algorithm;


        public ContextBorderAlgorithm(GridType type)
        {
            SetAlgorithm(type);
        }

        public ContextBorderAlgorithm(IBorderAlgorithm algorithm)
        {
            Algorithm = algorithm;
        }


        public void SetAlgorithm(IBorderAlgorithm alg)
        {
            Algorithm = alg;
        }


        public void ExecuteAlgorithm(Canvas canvas, int x, int y, int width, int height, Color color, Aligns align)
        {
            Algorithm.SetBorder(canvas, x, y, width, height, color, align);
        }

        public void SetAlgorithm(GridType type)
        {
            switch (type)
            {
                case GridType.SolidLine:
                    this.SetAlgorithm(new LineBorder());
                    break;
            }
        }

    }
}
