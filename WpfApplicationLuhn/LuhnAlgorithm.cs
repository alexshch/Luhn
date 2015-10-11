using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplicationLuhn
{
    class LuhnAlgorithm
    {           

        public static bool luhnAlgorithmPerform(int[] cardNumber)
        {
            int sum = 0;
            int length = cardNumber.Length;

            revertArray(ref cardNumber);

            for (int i = 0; i < length; i++)
            {
                int luhnIndex = i + 1;
                if (0 == luhnIndex % 2)
                {
                    int tempNumber = cardNumber[i] * 2;
                    if (tempNumber > 9)
                    {
                        string strNum = tempNumber.ToString();
                        int num0 = 0;
                        int num1 = 0;
                        Int32.TryParse(strNum[0].ToString(), out num0);
                        Int32.TryParse(strNum[1].ToString(), out num1);
                        sum += (num0 + num1);
                    }
                    else
                    {
                        sum += tempNumber;
                    }
                }
                else
                {
                    sum += cardNumber[i];
                }
            }

            if (0 == sum % 10)
            {
                return true;
            }
            return false;
        }

        private static void revertArray(ref int[] array) 
        {
            int[] auxArray = (int[]) array.Clone();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = auxArray[auxArray.Length - i - 1];
            }
        }
    }
}
