namespace Latin_Square_Design
{
    internal class LatinSquare
    {
        //////INITIALIZING THE VARIABLES
        public static int _samplesInRow; // SPECISES OF OUR PLANET(INTIGER NUMBER)
        public LatinSquareBuilder[, ] _amountOfPlanets; // NAME AND VALUE OF THE PLANETS WILLBE SAVE OMN THIS MATRIX

        public double[] _x_i; //SUM OF THE VALUE OF ROWS
        public double[] _x_j; //SUM OF THE VALUE OF COLS
        public double _x_ij; //SUM OF ALL VALUES

        public LatinSquareBuilder[] _totalTreatment; //SUM OF TREATMENT SEPARATED BY NAME
        public LatinSquareBuilder[] _averageTreatment; //AVERARGE TREATMENT
        public LatinSquare(int samplesInRow, LatinSquareBuilder[, ] amountOfPlanets)
        {
            _samplesInRow = samplesInRow;

            _x_i = new double[_samplesInRow];
            _x_j = new double[_samplesInRow];

            _totalTreatment = new LatinSquareBuilder[_samplesInRow];
            _averageTreatment = new LatinSquareBuilder[_samplesInRow];

            _amountOfPlanets = (LatinSquareBuilder[,]) amountOfPlanets.Clone();

            for (int i = 0; i < _x_i.Length; i++)
            {
                for(int j = 0; j < _x_j.Length; j++)
                {
                    _x_i[i] += _amountOfPlanets[i, j]._value;
                    _x_j[j] += _amountOfPlanets[i, j]._value;
                }
            }

            for(int i = 0; i < _x_i.Length; i++)
            {
                _x_ij += _x_i[i];
            }

            for(int i = 0; i < _totalTreatment.Length; i++)
            {
                _totalTreatment[i] = new LatinSquareBuilder(i + 1, 0);

                for(int j = 0; j < _samplesInRow; j++)
                {
                    for(int k = 0; k < _samplesInRow; k++)
                    {
                        if (i + 1 == _amountOfPlanets[j, k]._name)
                        {
                            _totalTreatment[i]._value += _amountOfPlanets[j, k]._value;
                        }
                    }
                }
            }

            for (int i = 0; i < _averageTreatment.Length; i++)
            {
                _averageTreatment[i] = new LatinSquareBuilder(i + 1, 0);
                _averageTreatment[i]._value = (_totalTreatment[i]._value / _samplesInRow);
            }
        }

        private double CF(double x_ij, LatinSquareBuilder[,] amountOfPlanets)
        {
            double cf = Math.Pow(x_ij, 2) /  amountOfPlanets.Length;

            return cf;
        }

        private double TSS(LatinSquareBuilder[,] amountOfPlanets, double x_ij, int samplesInRow)
        {
            double total = 0;

            for (int i = 0; i < samplesInRow; i++)
            {
                for (int j = 0; j < samplesInRow; j++)
                {
                    total += amountOfPlanets[i, j]._value * amountOfPlanets[i, j]._value;
                }
            }

            double tss = total - CF(x_ij, amountOfPlanets);

            return Math.Round(tss, 2);
        }

        private double RSS(double[] x_i, int samplesInRow, double x_ij, LatinSquareBuilder[,] amountOfPlanets)
        {
            double totalI = 0;

            for (int i = 0; i < x_i.Length; i++)
            {
                double pow = Math.Pow(x_i[i], 2);
                totalI += pow;
            }

            double rss = (totalI / samplesInRow) - CF(x_ij, amountOfPlanets);

            return Math.Round(rss, 2);
        }

        private double CSS(double[] x_j, int samplesInRow, double x_ij, LatinSquareBuilder[,] amountOfPlanets)
        {
            double totalJ = 0;

            for (int i = 0; i < x_j.Length; i++)
            {
                totalJ += Math.Pow(x_j[i], 2);
            }

            double css = (totalJ / samplesInRow) - CF(x_ij, amountOfPlanets);

            return Math.Round(css, 2);
        }

        private double VSS(LatinSquareBuilder[] TotalTreatment, int samplesInRow, double x_ij, LatinSquareBuilder[,] amountOfPlanets)
        {
            double sigmaOfTheSumOfTreatments = 0;

            for (int i = 0; i < TotalTreatment.Length; i++)
            {
                sigmaOfTheSumOfTreatments += (Math.Pow(TotalTreatment[i]._value, 2));   
            }

            double vss = (sigmaOfTheSumOfTreatments / samplesInRow) - CF(x_ij, amountOfPlanets);

            return Math.Round(vss, 2);
        }

        private double ESS(LatinSquareBuilder[] TotalTreatment, LatinSquareBuilder[,] amountOfPlanets, double x_ij, int samplesInRow, double[] x_i, double[] x_j)
        {
            double ess = TSS(amountOfPlanets, x_ij, samplesInRow) - RSS(x_i, samplesInRow, x_ij, amountOfPlanets) - CSS(x_j, samplesInRow, x_ij, amountOfPlanets) - VSS(TotalTreatment, samplesInRow, x_ij, amountOfPlanets);
            
            return Math.Round(ess, 2);
        }

        private double[,] MakeChart(LatinSquareBuilder[] TotalTreatment, LatinSquareBuilder[,] amountOfPlanets, double x_ij, int samplesInRow, double[] x_i, double[] x_j)
        {
            double[,] chart = new double[5, 5];

            chart[0, 0] = chart[1, 0] = chart[2, 0] = samplesInRow - 1;

            chart[3, 0] = (samplesInRow - 1) * (samplesInRow - 2);
            chart[4, 0] = Math.Pow(samplesInRow, 2) - 1;

            chart[0, 1] = RSS(x_i, samplesInRow, x_ij, amountOfPlanets);
            chart[1, 1] = CSS(x_j, samplesInRow, x_ij, amountOfPlanets);
            chart[2, 1] = VSS(TotalTreatment, samplesInRow, x_ij, amountOfPlanets);
            chart[3, 1] = ESS(TotalTreatment, amountOfPlanets, x_ij, samplesInRow, x_i, x_j);
            chart[4, 1] = chart[0, 1] + chart[1, 1] + chart[2, 1] + chart[3, 1];

            chart[0, 2] = Math.Round(chart[0, 1] / chart[0, 0], 2);
            chart[1, 2] = Math.Round(chart[1, 1] / chart[1, 0], 2);
            chart[2, 2] = Math.Round(chart[2, 1] / chart[2, 0], 2);
            chart[3, 2] = Math.Round(chart[3, 1] / chart[3, 0], 2);

            chart[0, 3] = Math.Round(chart[0, 2] / chart[3, 2], 2);
            chart[1, 3] = Math.Round(chart[1, 2] / chart[3, 2], 2);
            chart[2, 3] = Math.Round(chart[2, 2] / chart[3, 2], 2);

            return chart;

        }

        public void Run()
        {
            double[,] chart = MakeChart(_totalTreatment, _amountOfPlanets, _x_ij, _samplesInRow, _x_i, _x_j);

            var table = new TablePrinter("Source", "DF", "SS", "MS", "F");
            table.AddRow("Row", chart[0, 0], chart[0, 1], chart[0, 2], chart[0, 3]);
            table.AddRow("Column", chart[1, 0], chart[1, 1], chart[1, 2], chart[1, 3]);
            table.AddRow("Treatment", chart[2, 0], chart[2, 1], chart[2, 2], chart[2, 3]);
            table.AddRow("Error", chart[3, 0], chart[3, 1], chart[3, 2], chart[3, 3]);
            table.AddRow("Total", chart[4, 0], chart[4, 1], chart[4, 2], chart[4, 3]);

            table.Print();
        }
    }
}

