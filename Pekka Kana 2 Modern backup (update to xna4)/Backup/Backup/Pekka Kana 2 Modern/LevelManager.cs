using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TileEngine;

namespace Pekka_Kana_2_Modern
{
    public static class LevelManager
    {
        #region Deklaracje
        private static ContentManager Content;
        private static Player player;
        private static int currentLevel;
        private static Vector2 respawnLocation;
        #endregion

        #region wlasciwosci
        public static int CurrentLevel
        {
            get { return currentLevel; }
        }

        public static Vector2 RespawnLocation
        {
            get { return respawnLocation; }
            set { respawnLocation = value; }
        }
        #endregion

        #region Inicjalizacja
        public static void Initialize(ContentManager content, Player gamePlayer)
        {
            Content = content;
            player = gamePlayer;
        }
        #endregion

        #region public meth
        public static void LoadLevel(int levelNumber)
        {
            TileMap.LoadMap((System.IO.FileStream)TitleContainer.OpenStream(@"Content\Maps\MAP" +
                levelNumber.ToString().PadLeft(3, '0') + ".MAP"));
            for (int x = 0; x < TileMap.MapWidth; x++)
            {
                for (int y = 0; y < TileMap.MapHeight; y++)
                {
                    if (TileMap.CellCodeValue(x, y) == "START")
                    {
                        player.WorldLocation = new Vector2(
                            x * TileMap.TileWidth,
                            y * TileMap.TileHeight);
                    }
                }
            }
            currentLevel = levelNumber;
            respawnLocation = player.WorldLocation;

        }
        public static void ReloadLevel()
        {
            Vector2 saveRespawn = respawnLocation;
            LoadLevel(currentLevel);
            respawnLocation = saveRespawn;
            player.WorldLocation = respawnLocation;
        }

        #endregion
    }
}
