using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileEngine
{
    [Serializable]
    public class MapSquare
    {
        #region Declarations
        public int[] LayerTiles = new int[3];
        public string CodeValue = "";
        public bool Passable = true;
        #endregion

        #region Constructor
        public MapSquare(
            int background,
            int interactive,
            int foreground,
            string code,
            bool passable)
        {
            LayerTiles[0] = background;
            LayerTiles[1] = interactive;
            LayerTiles[2] = foreground;
            CodeValue = code;
            Passable = passable;
        }
        #endregion

        #region Public Methods
        public void TogglePassable()
        {
            Passable = !Passable;
        }

        public void TimeToggle() { }
        #endregion


    }

}
