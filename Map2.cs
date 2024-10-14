using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Media;
using static System.Net.Mime.MediaTypeNames;
using System.CodeDom.Compiler;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MostroDiBiscottiMonoGame
{
    public class Map2
    {
        string sLevel;
        int nLevelWidth = 140;
        int nLevelHeight = 64;
        int pinQuantity = 0;
        public Map2(Size size, ref Caramelo caramelo, ref VElement Verlets, ref SoftBody monstruo, int Level)
        {
            // / para monstruo
            // 3 4 5 burbujas
            // 6 estrella
            // 8 megaestrella
            //1 para pin
            //* para caramelo

            sLevel = Levels.list[Global.currentLevel];

            int nTileWidth =  size.Width / nLevelWidth;
            int nTileHeight = size.Height / nLevelHeight;

            Size bmp = new Size(2 * size.Width, size.Height);

            for (int y = 0; y < nLevelHeight; y++)
            {
                for (int x = 0; x < nLevelWidth; x++)
                {
                    int index = y * nLevelWidth + x;
                    if (sLevel[index] == '*')
                    {
                        // Calcula la posición del caramelo basada en el índice del tile
                        Vector2 posicion = new Vector2(x * nTileWidth, y * nTileHeight);
                        caramelo = new Caramelo(x * nTileWidth, y * nTileHeight, 100000, size.Width, size.Height);
                        //caramelo.punto.old.X = -0.01f;
                        Verlets.Points.Add(caramelo.punto);
                    }
                    if (sLevel[index] == '/')
                    {
                        //SetTile(x + (int)fOffsetX, y + (int)fOffsetY, '.');
                        monstruo = new SoftBody(new VPoint(x * nTileWidth, y * nTileHeight, -2000, bmp.Width, bmp.Height));
                        Verlets.Points.AddRange(monstruo.Points);
                        Verlets.bodies.Add(monstruo);
                    }
                }
            }
            for (int y = 0; y < nLevelHeight; y++)
            {
                for (int x = 0; x < nLevelWidth; x++)
                {
                    int index = y * nLevelWidth + x;
                    switch (sLevel[index])
                    {
                        case '.':
                            break;

                        case '7':
                            //SetTile(x + (int)fOffsetX, y + (int)fOffsetY, '.');
                            Pin pinWall = new Pin(x * nTileWidth, y * nTileHeight, pinQuantity, bmp.Width, bmp.Height);
                            Verlets.addPoint(pinWall.punto);
                            pinQuantity++;
                            break;

                        case '1':
                            //SetTile(x + (int)fOffsetX, y + (int)fOffsetY, '.');
                            Pin pin = new Pin(x * nTileWidth, y * nTileHeight, pinQuantity, bmp.Width, bmp.Height);
                            Rope cuerda = new Rope(ref pin, ref caramelo);
                            Verlets.addPoint(pin.punto);
                            Verlets.Cuerdas.Add(cuerda);
                            Verlets.Points.AddRange(cuerda.Points);
                            pinQuantity++;
                            break;
                        case '3':
                            //SetTile(x + (int)fOffsetX, y + (int)fOffsetY, '.');
                            Bubble burbuja = new Bubble(x * nTileWidth, y * nTileHeight, pinQuantity, bmp.Width, bmp.Height, 1);
                            Verlets.addPoint(burbuja.punto);
                            pinQuantity++;
                            break;
                        case '4':
                            //SetTile(x + (int)fOffsetX, y + (int)fOffsetY, '.');
                            Bubble burbujaV = new Bubble(x * nTileWidth, y * nTileHeight, pinQuantity, bmp.Width, bmp.Height, 0);
                            Verlets.addPoint(burbujaV.punto);
                            pinQuantity++;
                            break;
                        case '5':
                            //SetTile(x + (int)fOffsetX, y + (int)fOffsetY, '.');
                            Bubble burbujaR = new Bubble(x * nTileWidth, y * nTileHeight, pinQuantity, bmp.Width, bmp.Height, 2);
                            Verlets.addPoint(burbujaR.punto);
                            pinQuantity++;
                            break;
                        case '6':
                            //SetTile(x + (int)fOffsetX, y + (int)fOffsetY, '.');
                            Estrella estrella = new Estrella(x * nTileWidth, y * nTileHeight, pinQuantity, bmp.Width, bmp.Height);
                            Verlets.addPoint(estrella.punto);
                            pinQuantity++;
                            break;
                        case '8':
                            //SetTile(x + (int)fOffsetX, y + (int)fOffsetY, '.');
                            MegaEstrella megaEstrella = new MegaEstrella(x * nTileWidth, y * nTileHeight, pinQuantity, bmp.Width, bmp.Height);
                            Verlets.addPoint(megaEstrella.punto);
                            pinQuantity++;
                            break;

                    }
                }
            }


        }
        public void SetTile(float x, float y, char c)//changes the tile
        {
            if (x >= 0 && x < nLevelWidth && y >= 0 && y < nLevelHeight)
            {
                int index = (int)y * nLevelWidth + (int)x;
                sLevel = sLevel.Remove(index, 1).Insert(index, c.ToString());
                //score += 100;
            }
        }

        public char GetTile(float x, float y)
        {
            if (x >= 0 && x < nLevelWidth && y >= 0 && y < nLevelHeight)
                return sLevel[(int)y * nLevelWidth + (int)x];
            else
                return ' ';
        }

    }
}
