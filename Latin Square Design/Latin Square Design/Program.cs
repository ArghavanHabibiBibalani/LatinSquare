namespace Latin_Square_Design
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the plant specieses numner: ");
            int sampeles = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Now please enter the plant specieses numner:  (NOTE: please enter name and then enter the value) ");
            LatinSquareBuilder[,] latinSquareBuilder = new LatinSquareBuilder[sampeles , sampeles];


            for(int i = 0; i < sampeles; i++)
            {
                for(int j = 0; j < sampeles; j++)
                {
                    latinSquareBuilder[i, j] = new LatinSquareBuilder(0, 0);
                    string a = Console.ReadLine();
                    latinSquareBuilder[i, j]._name = Convert.ToInt32(a);
                    string b = Console.ReadLine();
                    latinSquareBuilder[i, j]._value = Convert.ToDouble(b);
                }
            }

            LatinSquare latinSquare = new LatinSquare(sampeles, latinSquareBuilder);
            latinSquare.Run();
        }
    }
    
}