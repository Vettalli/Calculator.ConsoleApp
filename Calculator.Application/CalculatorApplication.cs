using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Calculator.Application
{
    public class CalculatorApplication : ICalculate
    {
        readonly ISupport _support = new SupportCalc();

        public double Calculate(string expression)
        {
            var result = Counting(ToPostfixExpression(expression));

            return result;
        }

        public string ToPostfixExpression(string expression)
        {
            var finalExp = string.Empty; 
            var operStack = new Stack<char>();
                        
            if (Regex.IsMatch(expression, @"[a-zA-Z]+") || string.IsNullOrEmpty(expression))
            {
                throw new InvalidOperationException("Your expression can not be empty or contain letters");
            }
            
            var min = "(-";
            expression = expression.Insert(expression.IndexOf(min) + 1, "0");

            for (int i = 0; i < expression.Length; i++) 
            {
                if (Regex.IsMatch(expression, @"^-"))
                {
                    for (int j = 0; j < expression.Length; j++)
                    {
                        if (!char.IsDigit(expression[j + 1]) && expression[j + 1] != ',')
                        {
                            expression = expression.Insert(j + 1, ")");
                            break;
                        }
                    }

                    expression = "(0" + expression;
                }

                if (_support.IsSeparator(expression[i]))
                {
                    continue;
                }
                                 
                if (char.IsDigit(expression[i])) 
                {
                    while (!_support.IsSeparator(expression[i]) && !_support.IsOperator(expression[i]))
                    {
                        finalExp += expression[i]; 
                        i++; 

                        if (i == expression.Length)
                        {
                            break;
                        }
                    }

                    finalExp += " "; 
                    i--; 
                }
                                
                if (_support.IsOperator(expression[i])) 
                {
                    if (expression[i] == '(')
                    {
                        operStack.Push(expression[i]);
                    }
                    else if (expression[i] == ')') 
                    {
                       char s = operStack.Pop();

                        while (s != '(')
                        {
                            finalExp += s.ToString() + ' ';
                            s = operStack.Pop();
                        }
                    }
                    else 
                    {
                        if (operStack.Count > 0)
                        {
                            if (_support.GetPriority(expression[i]) <= _support.GetPriority(operStack.Peek()))
                            {
                                finalExp += operStack.Pop() + " ";

                            }
                        }
                      
                        operStack.Push(char.Parse(expression[i].ToString())); 
                    }
                }
            }
                        
            while (operStack.Count > 0)
            {
                finalExp += operStack.Pop() + " ";
            }
               
            return finalExp; 
        }

        public double Counting(string expression)
        {
            double result = 0; 
            var temp = new Stack<double>(); 
            
            for (int i = 0; i < expression.Length; i++) 
            {
                if (char.IsDigit(expression[i]))
                {
                    var numbers = string.Empty;

                    while (!_support.IsSeparator(expression[i]) && !_support.IsOperator(expression[i])) 
                    {
                        numbers += expression[i];
                        i++;
                        if (i == expression.Length)
                        {
                            break;
                        }
                    }

                    temp.Push(double.Parse(numbers)); 
                    i--;
                }
                else if (_support.IsOperator(expression[i])) 
                {
                    double a = temp.Pop();
                    double b = temp.Pop();
                    
                    result = expression[i] switch
                    {
                        '+' => b + a,
                        '-' => b - a,
                        '*' => b * a,
                        '/' => b / a,
                        '^' => double.Parse(Math
                            .Pow(double.Parse(b.ToString(CultureInfo.InvariantCulture)),
                                double.Parse(a.ToString(CultureInfo.InvariantCulture)))
                            .ToString(CultureInfo.InvariantCulture)),
                        _ => result
                    };
                    
                    temp.Push(result); 
                }
            }

            return temp.Peek(); 
        }
    }
}