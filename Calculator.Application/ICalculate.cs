namespace Calculator.Application
{
   public interface ICalculate
   {
        double Calculate(string expression);
        string ToPostfixExpression(string expression);
        double Counting(string expression);
   }
}
