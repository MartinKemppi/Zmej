using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zmej
{
    class FoodCreator
    {
        private int mapWidth;
        private int mapHeight;
        private char foodSym;
        private char specialFoodSym1;
        private char specialFoodSym2;
        private char specialFoodSym3;

        Random random = new Random();

        public FoodCreator
            (
                        int mapWidth, 
                        int mapHeight, 
                        char foodSym, 
                        char specialFoodSym1, 
                        char specialFoodSym2,
                        char specialFoodSym3
            )
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            this.foodSym = foodSym;
            this.specialFoodSym1 = specialFoodSym1;
            this.specialFoodSym2 = specialFoodSym2;
            this.specialFoodSym3 = specialFoodSym3;
        }

        public Point CreateFood()
        {
            int x = random.Next(2, mapWidth - 2);
            int y = random.Next(2, mapHeight - 2);

            if (random.Next(1, 21) < 3)
            {
                return new Point(x, y, specialFoodSym1);
            }
            else if (random.Next(1, 21) < 5)
            {
                return new Point(x, y, specialFoodSym2);
            }
            else if (random.Next(1, 21) < 6)
            {
                return new Point(x, y, specialFoodSym3);
            }
            else
            {
                return new Point(x, y, foodSym);               
            }            
        }
    }
}
