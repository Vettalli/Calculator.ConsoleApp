using System.IO;

namespace Calculator.Application
{
    public class CalcFromFile : IFile
    {
        public void CalculateExpressions(string path)
        {
            ICalculate test = new CalculatorApplication();

            var expressions = File.ReadAllLines(path);
            var toAppend = false;

            for (var i = 0; i < expressions.Length; i++)
            {
                if (i >= 1)
                {
                    toAppend = true;
                }

                try
                {
                    var sum = test.Calculate(expressions[i]);

                    using var sw = new StreamWriter(path, toAppend, System.Text.Encoding.Default);
                    sw.WriteLine($"{expressions[i]} = {sum}");
                }
                catch
                {
                    using var sw = new StreamWriter(path, toAppend, System.Text.Encoding.Default);
                    sw.WriteLine($"{expressions[i]} = Wrong expression!");
                }
            }
        }
    }
}
