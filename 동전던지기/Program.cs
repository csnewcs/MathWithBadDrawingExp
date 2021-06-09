using System;

namespace 동전던지기
{
    class Program
    {
        static void Main(string[] args)
        {
            Program pro = new Program();
            int[] first = pro.throwCoin(30, 10000);
            Console.WriteLine($"{first[0]}({(double)(first[0])/100}%), {first[1]}({(double)(first[1])/100}%), {first[2]}({(double)(first[2])/100}%)");
        }
        public int[] throwCoin(int coins, int loop)
        {
            int[] result = new int[3] {0, 0, 0};
            Random rd = new Random();
            int extreme = coins - coins / 4;
            for(int i = 0; i < loop; i++)
            {
                int top = 0;
                int back = 0;
                for(int j = 0; j < coins; j++)
                {
                    int coin = rd.Next(0, 2); //0: 뒷면, 1: 앞면
                    if(coin == 0) top++;
                    else back++;
                }
                if(top >= extreme) result[0]++;
                else if(back >= extreme) result[2]++;
                else result[1]++;
            }
            return result;
        }
    }
}
