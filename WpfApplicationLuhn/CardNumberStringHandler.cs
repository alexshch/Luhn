using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WpfApplicationLuhn
{
    class CardNumberStringHandler
    {
        public static void handleCardNumber(String inputString, StringBuilder strBuilder, int lineNumber)
        {
            String cardNumber;
            int[] cardNumberIntArray;
            
            string pattern = @"(\W|^)([0-9]{16}?)(\W|$)";
            Regex regex = new Regex(pattern);
            foreach (Match match in regex.Matches(inputString))
            {
                cardNumber = match.Groups[2].Value;
                //if ("" == cardNumber) continue;
                stringToIntArray(cardNumber, out cardNumberIntArray);
                bool res = LuhnAlgorithm.luhnAlgorithmPerform(cardNumberIntArray);
                if (true == res)
                {
                    strBuilder.AppendFormat("LINE " + String.Format("{0,-4} ",lineNumber) + inputString + " - " + cardNumber + " - OK\n");
                }
                else
                {
                    strBuilder.AppendFormat("LINE " + String.Format("{0,-4} ", lineNumber) + inputString + " - " + cardNumber + " - FAIL\n");
                }
            }
            

        }

        private static void stringToIntArray(String cardNumberString, out int[] CardNumberIntArray)
        {
            CardNumberIntArray = new int[cardNumberString.Length];
            int figure;
            for (int i = 0; i < cardNumberString.Length; i++)
            {
                Int32.TryParse(cardNumberString[i].ToString(), out figure);
                CardNumberIntArray[i] = figure;
            }
        }
    }
}
