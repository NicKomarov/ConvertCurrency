
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
                Console.Write("Введіть курс для долара: ");
                decimal doll = Convert.ToDecimal(Console.ReadLine());
                Console.Write("Введіть курс для євро: ");
                decimal eu = Convert.ToDecimal(Console.ReadLine());
                Converter converter = new Converter { Dollar = doll, Euro = eu};
                incorrect_input = false;
            }
            catch(ConverterException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            catch(FormatException)
            {
                Console.WriteLine("Не коректне введення! Спробуй ще раз");
            }
        }
        while (incorrect_input);


        Console.WriteLine("Ok");
    }
}