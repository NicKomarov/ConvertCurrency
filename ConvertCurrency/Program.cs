
using System.Text;
using static System.Formats.Asn1.AsnWriter;

class Converter
{
    public decimal dollar, euro;
    public decimal Dollar
    {
        get { return dollar; }
        set
        {
            if (value <= 0)
                throw new ConverterException("Курс долару не може бути не додатнім");
            else
                dollar = value;
        }
    }
    public decimal Euro
    {
        get { return euro; }
        set
        {
            if (value <= 0)
                throw new ConverterException("Курс євро не може бути не додатнім");
            else
                euro = value;
        }
    }
}

class ConverterException : ArgumentException
{
    public ConverterException(string message)
        : base(message)
    { }
}
class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        bool incorrect_input = true;
        do
        {
            try
            {
                Console.Write("Введіть курс для долара(decimal): ");
                decimal doll = Convert.ToDecimal(Console.ReadLine());
                Console.Write("Введіть курс для євро(decimal): ");
                decimal eu = Convert.ToDecimal(Console.ReadLine());

                Converter converter = new Converter { Dollar = doll, Euro = eu};

                Console.Write("Введіть валюту з якої переводити(g - гривня, d - долар, e - євро): ");
                string from = Console.ReadLine();
                Console.Write("Введіть валюту в яку переводити(g - гривня, d - долар, e - євро): ");
                string to = Console.ReadLine();
                if (from != "g" && from != "e" && from != "d" || to != "g" && to != "e" && to != "d")
                {
                    throw new FormatException();
                }
                Console.Write("Введіть суму(decimal): ");
                decimal sum = Convert.ToDecimal(Console.ReadLine());
                if (sum < 0)    throw new ArgumentException("Сума не може бути від'ємною");

                decimal result;
                if (from == to) result = sum;
                else if (from == "g")
                {
                    if (to == "e") result = sum / converter.euro;
                    else result = sum / converter.dollar;
                }
                else if (from == "e")
                {
                    result = sum * converter.euro;
                }
                else result = sum * converter.dollar;
                Console.WriteLine(result);

                incorrect_input = false;
            }
            catch(ConverterException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Не коректне введення! Спробуй ще раз");
            }
        }
        while (incorrect_input);

        Console.ReadKey();
    }
}