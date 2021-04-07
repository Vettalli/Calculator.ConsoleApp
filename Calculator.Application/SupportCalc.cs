namespace Calculator.Application
{
    public class SupportCalc : ISupport
    {
        public bool IsSeparator(char sep)
        {
            return " =".IndexOf(sep) != -1;
        }

        public bool IsOperator(char oper)
        {
            return "+-/*^()".IndexOf(oper) != -1;
        }

        public byte GetPriority(char symbol)
        {
            return symbol switch
            {
                '(' => 0,
                ')' => 1,
                '+' => 2,
                '-' => 2,
                '*' => 4,
                '/' => 4,
                '^' => 5,
                _ => 6
            };
        }
    }
}
