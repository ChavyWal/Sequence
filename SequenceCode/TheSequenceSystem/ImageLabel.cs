using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheSequenceSystem.Sequence;

namespace TheSequenceSystem
{
    internal class ImageLabel
    {
        Sequence.GameStatusEnum GameStatus;

        public System.Drawing.Color Backcolor { get; set; }

        private void SetBackcolor()
        {
            switch (GameStatus)
            {
                case GameStatusEnum.Memorize:
                    Backcolor = Color.Turquoise;
                    break;
                default:
                    Backcolor = Color.Black;
                    break;
            }
        }
    }
}
