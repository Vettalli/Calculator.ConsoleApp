namespace Calculator.Application
{
    interface ISupport
    {
        bool IsSeparator(char sep);
        bool IsOperator(char oper);
        public byte GetPriority(char symbol);
    }
}
