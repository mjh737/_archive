using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;
using Graphite;
using System.Diagnostics;

namespace Graphite
{
    public class World
    {
        Device device;
        Scene scene;
        Cell[] cells;
        Cell[] neighbourhood;

        public int CurrentCell;
        public int width;
        public int height;
        int numQuadsPerCellSide;

        public World(Device device, Scene scene, int width, int height, int numQuadsPerCellSide)
        {
            this.device = device;
            this.width = width;
            this.height = height;
            this.scene = scene;
            this.numQuadsPerCellSide = numQuadsPerCellSide;

            cells = new Cell[width * height];
            neighbourhood = new Cell[9];

            CreateCells();
            //CreateNeighbourhood();
            scene.AddObjects(cells);
        }

        private void CreateCells()
        {
            for(int y = 0 ; y < width ; y++)
            {
                for(int x = 0 ; x < height ; x++)
                {
                    cells[(y * width) + x] = new Cell(device, x * numQuadsPerCellSide, y * numQuadsPerCellSide, numQuadsPerCellSide);
                }
            }
        }

        public void CreateNeighbourhood()
        {
            neighbourhood[0] = cells[CurrentCell - width - 1];
            neighbourhood[1] = cells[CurrentCell - width];
            neighbourhood[2] = cells[CurrentCell - width + 1];
            neighbourhood[3] = cells[CurrentCell - 1];
            neighbourhood[4] = cells[CurrentCell];
            neighbourhood[5] = cells[CurrentCell + 1];
            neighbourhood[6] = cells[CurrentCell + width - 1];
            neighbourhood[7] = cells[CurrentCell + width];
            neighbourhood[8] = cells[CurrentCell + width + 1];

            scene.AddObjects(neighbourhood);
        }

        public void CheckBounds(int x, int z)
        {
            //If we're to the left 
            if(x < cells[CurrentCell].XOffset) MoveCurrentCellLeft();
            if(x > cells[CurrentCell].XOffset + cells[CurrentCell].numVertsPerSide) MoveCurrentCellRight();
            if(z < cells[CurrentCell].ZOffset) MoveCurrentCellDown();
            if(z > cells[CurrentCell].ZOffset + cells[CurrentCell].numVertsPerSide) MoveCurrentCellUp();
        }

        private void MoveCurrentCellUp()
        {
            if(CurrentCell >= width * (height - 1)) return;
            CurrentCell += width;
            CreateNeighbourhood();
        }

        private void MoveCurrentCellDown()
        {
            if(CurrentCell < width) return;
            CurrentCell -= width;
            CreateNeighbourhood();
        }

        private void MoveCurrentCellRight()
        {
            if(CurrentCell + 1 % width == 0) return;
            CurrentCell++;
            CreateNeighbourhood();
        }

        private void MoveCurrentCellLeft()
        {
            if(CurrentCell % width == 0) return;
            CurrentCell--;
            CreateNeighbourhood();
        }

        public int GetHeight(int x, int z)
        {
            int temp1 = (int)(Math.Floor((double)x/numQuadsPerCellSide));
            int temp2 = (int)((Math.Floor((double)(z/numQuadsPerCellSide)))*width);
            int temp = temp1 + temp2;

            return cells[temp].GetHeight(x,z);
        }
    }
}
