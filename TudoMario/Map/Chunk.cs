﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.Collections.Immutable;
using TudoMario.Rendering;
using Windows.UI.Core;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TudoMario.Map
{
    public enum ColliderOption
    {
        GenerateCollider
    }
    public sealed partial class Chunk
    {
        public Tile[,] Tiles = new Tile[16, 16];
        /// <summary>
        /// X,Y coords of chunk in the registered map.
        /// </summary>
        public Vector2 ChunkPosition { get; set; } = new Vector2();
        public Chunk() { }

        /// <summary>
        /// Sets the tile at the given coordinates in this chunk to the given tile type.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetTileAt(int x, int y, Tile tile)
        {

            //0 is the top since we go from top left so it has to be mirrored
            y = 15 - y;

            Tiles[x, y] = tile;
            tile.TilePosition = new Vector2(x, y);
            tile.ChunkParent = this;

            //ChunkCanvas.Children.Add(tile);
            //Canvas.SetLeft(tile, (x * 32));
            //Canvas.SetTop(tile, (y * 32));
        }
        /// <summary>
        /// Sets the tile at the given coordinates in this chunk to the given tile type and registers a collider for it.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tileType"></param>
        /// <param name="imagePath"></param>
        public void SetTileAt(int x, int y, Tile tile, bool solid, MovementModifier modifier = null)
        {

            //0 is the top since we go from top left so it has to be mirrored
            y = 15 - y;

            GenerateColliderForTile(x, y, modifier, solid);
            Tiles[x, y] = tile;
            tile.TilePosition = new Vector2(x, y);
            tile.ChunkParent = this;

            //ChunkCanvas.Children.Add(tile);
            //Canvas.SetLeft(tile, (x * 32));
            //.SetTop(tile, (y * 32));
        }
        /// <summary>
        /// Creates a new tile at the given coordinates and the given textures.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="texture"></param>
        public void SetTileAt(int x, int y, BitmapImage texture)
        {
            //0 is the top since we go from top left so it has to be mirrored

            y = 15 - y;

            Tile tile = new Tile();
            tile.Texture = texture;

            Tiles[x, y] = tile;
            tile.TilePosition = new Vector2(x, y);
            tile.ChunkParent = this;
        }
        /// <summary>
        /// Creates a new tile at the given coordinates and the given textures and creates a collider for it.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="texture"></param>
        /// <param name="opt"></param>
        public void SetTileAt(int x, int y, BitmapImage texture, bool solid, MovementModifier modifier = null)
        {



            //0 is the top since we go from top left so it has to be mirrored
            y = 15 - y;

            Tile tile = new Tile();
            GenerateColliderForTile(x, y, modifier, solid);
            Tiles[x, y] = tile;
            tile.TilePosition = new Vector2(x, y);
            tile.ChunkParent = this;
            tile.Texture = texture;
        }

        [Obsolete]
        /// <summary>
        /// NOT IMPLEMENTED NEW REPRESENTATION Fills this chunk with the given tiletype
        /// </summary>
        /// <param name="tileType"></param>
        /// <param name="imagePath"></param>
        public void FillChunkWith(BitmapImage texture)
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    i = 15 - i;

                    Tile _tile = (Tile)Activator.CreateInstance(typeof(Tile));
                    _tile.Texture = texture;

                    Tiles[j, i] = _tile;
                }
            }
        }

        private void GenerateColliderForTile(int x, int y, MovementModifier modifier, bool solid)
        {
            Vector2 tileCenter = GetLogicalCenterOfTile(x, y);
            ColliderWithModifier tileCollider = new ColliderWithModifier(modifier, solid)
            {
                Position = tileCenter,
                Size = new Vector2(32, 32)
            };
        }
        private Vector2 GetLogicalCenterOfTile(int x, int y)
        {
            Vector2 chunkTopLeft = GetTopLeftPositionOfChunk();

            float tileTopLeftX = chunkTopLeft.X + x * 32;
            float tileTopLeftY = chunkTopLeft.Y - y * 32;

            Vector2 tileTopLeft = new Vector2(tileTopLeftX, tileTopLeftY);

            return GetTileCenterFromTopLeft(tileTopLeft);

        }
        private Vector2 GetTopLeftPositionOfChunk()
        {
            float x = ChunkPosition.X * 512f;
            float y = ChunkPosition.Y * 512f;
            return new Vector2(x, y);
        }

        private Vector2 GetTileCenterFromTopLeft(Vector2 tileTopLeft)
        {
            return new Vector2(tileTopLeft.X + 16, tileTopLeft.Y - 16);
        }
    }
}
