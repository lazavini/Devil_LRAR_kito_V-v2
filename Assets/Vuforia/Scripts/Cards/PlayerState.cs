using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Vuforia.Scripts.Cards
{
    public class PlayerState
    {
        public string SeletedSkin { get; set; }
        public string SelectedEffect { get; set; }
        public string Scale { get; set; }
        public string SelectedColor { get; set; }
        public bool RandomGenerated { get; set; }
        public bool IsLoading { get; set; }
    }
}
