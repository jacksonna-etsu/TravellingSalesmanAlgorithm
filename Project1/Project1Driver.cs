//---------------------------------------------------------------------------
// File name: Project1Driver.cs
// Project name: Project1
// ---------------------------------------------------------------------------
// Creator’s name and email: Nathaniel Jackson, jacksonna@etsu.edu
// Course-Section: 001
// Creation Date: 02/03/2019
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Diagnostics;

namespace Project1
{
    /**
     * Class Name: Project1Driver<br>
     * Class Purpose: This class is used for calculating the shortest path between 
     * an entire set of points
     *
     * <hr>
     * Date created: 02/03/2019<br>
     * @author Nathaniel Jackson
     */
    class Project1Driver
    {
        static double fastestRoute = 10000000000000000000000000000000000000000000000000000000000000000000000000000000000.0;         //fastest route found so far
        static double[] fastestPointsX;                                         //array listing the optimal route for x values
        static double[] fastestPointsY;                                         //array listing the optimal route for y values

        /**
         * Method Name: Main<br>
         * Method Purpose: This is the entry point for the program. Reads data
         * from a file and gives ouput.<br>
         *
         * <hr>
         * Date created: 02/03/2019<br>
         *
         * <hr>
         * Notes on specifications, special algorithms, and assumptions:
         *
         * <hr>
         *   @param args Array of strings used to give main method arguments
         *   @throws IOException
         */
        static void Main(string[] args)
        {
            Stopwatch sw;                       //stopwatch to time program run time
            String stringInput;                 //user input as string
            int numberOfPoints;                 //number of points in list
            int i;                              //loop variable
            string[] points;                    //storage for one set of points
            double[] x;                         //all x points
            double[] y;                         //all y points

            stringInput = Console.ReadLine();
            Int32.TryParse(stringInput, out numberOfPoints);
            x = new double[numberOfPoints];
            y = new double[numberOfPoints];

            for (i = 0; i < numberOfPoints; i++)
            {
                stringInput = Console.ReadLine();
                points = stringInput.Split(' ');
                Double.TryParse(points[0], out x[i]);
                Double.TryParse(points[1], out y[i]);
            }

            fastestPointsX = new double[numberOfPoints];
            fastestPointsY = new double[numberOfPoints];
            sw = Stopwatch.StartNew();
            CalculateFastestRoute(numberOfPoints, x, y);
            sw.Stop();
            Console.WriteLine("Shortest route: {0:0.00}", fastestRoute);
            Console.Write("Optimal route x values: ");
            for (i = 0; i < fastestPointsX.Length; i++)
            {
                Console.Write(fastestPointsX[i] + ", ");
            }
            Console.WriteLine("");
            Console.Write("Optimal route y values: ");
            for (i = 0; i < fastestPointsY.Length; i++)
            {
                Console.Write(fastestPointsY[i] + ", ");
            }
            Console.WriteLine();

            Console.WriteLine("Time used: {0} secs", sw.Elapsed.TotalMilliseconds / 1000);
            Console.ReadKey();
        }

        /**
         * Method Name: CalculateFastestRoute<br>
         * Method Purpose: This is the method that calculatest the shortest possible path
         * through an entire set of points.<br>
         *
         * <hr>
         * Date created: 02/03/2019<br>
         *
         * <hr>
         * Notes on specifications, special algorithms, and assumptions: Algorithm is recursive. If current value
         * of n is odd then it switches point n with point 1. If current value of n is even then it switches point
         * n with point i. This generates all possible permutations for the list. For each permutation the distance of the 
         * path is calculated and compared to the current shortest possible path. If the distance is shorter then it becomes
         * the new shortest distance.
         *
         * <hr>
         *   @param n Current number of points
         *   @param xPoints array containing all x values of the list of points
         *   @param yPoints array containing all y values of the list of points
         *   @throws IOException
         */
        private static void CalculateFastestRoute(int n, double[] xPoints, double[] yPoints)
        {
            int i;                                      //loop variable
            int j;                                      //loop variable
            int k;                                      //loop variable
            double xDifference;                         //distance between two x values
            double yDifference;                         //distance between two y values
            double temp;                                //temporary storage for swapping two values
            double tempDistance;                        //temporary distance between all points; used for comparison to current shortest route

            tempDistance = 0;
            if (n != 0)
            {
                for (i = 0; i < n; i++)
                {
                    CalculateFastestRoute((n - 1), xPoints, yPoints);

                    if ((n % 2) > 0)                        //if current value of n is odd
                    {
                        temp = xPoints[0];
                        xPoints[0] = xPoints[n - 1];
                        xPoints[n - 1] = temp;
                        temp = yPoints[0];
                        yPoints[0] = yPoints[n - 1];
                        yPoints[n - 1] = temp;
                        //swap point n with point 1

                        //calculate distance for current permutation
                        tempDistance = 0;
                        tempDistance += Math.Sqrt((xPoints[0] * xPoints[0]) + (yPoints[0] * yPoints[0]));
                        for (j = 0; j < xPoints.Length; j++)
                        {

                            if (j == (xPoints.Length - 1))
                            {
                                tempDistance += Math.Sqrt((xPoints[j] * xPoints[j]) + (yPoints[j] * yPoints[j]));
                            }
                            else
                            {

                                xDifference = xPoints[j] - xPoints[j + 1];
                                yDifference = yPoints[j] - yPoints[j + 1];
                                tempDistance += Math.Sqrt((xDifference * xDifference) + (yDifference * yDifference));

                            }
                        }

                        //check if distance is shorter then current shortest
                        if (tempDistance < fastestRoute)
                        {
                            fastestRoute = tempDistance;
                            for (k = 0; k < xPoints.Length; k++)
                            {
                                fastestPointsX[k] = xPoints[k];
                            }
                            for (k = 0; k < yPoints.Length; k++)
                            {
                                fastestPointsY[k] = yPoints[k];
                            }
                        }
                    }

                    else                                //if current value of n is even
                    {
                        temp = xPoints[i];
                        xPoints[i] = xPoints[n - 1];
                        xPoints[n - 1] = temp;
                        temp = yPoints[i];
                        yPoints[i] = yPoints[n - 1];
                        yPoints[n - 1] = temp;
                        //swap point n with point i

                        //calculate distance for current permutation
                        tempDistance = 0;
                        tempDistance += Math.Sqrt((xPoints[0] * xPoints[0]) + (yPoints[0] * yPoints[0]));
                        for (j = 0; j < xPoints.Length; j++)
                        {

                            if (j == (xPoints.Length - 1))
                            {
                                tempDistance += Math.Sqrt((xPoints[j] * xPoints[j]) + (yPoints[j] * yPoints[j]));
                            }
                            else
                            {

                                xDifference = xPoints[j] - xPoints[j + 1];
                                yDifference = yPoints[j] - yPoints[j + 1];
                                tempDistance += Math.Sqrt((xDifference * xDifference) + (yDifference * yDifference));

                            }
                        }
                        
                        //check if distance is shorter then current shortest
                        if (tempDistance < fastestRoute)
                        {
                            fastestRoute = tempDistance;
                            for (k = 0; k < xPoints.Length; k++)
                            {
                                fastestPointsX[k] = xPoints[k];
                            }
                            for (k = 0; k < yPoints.Length; k++)
                            {
                                fastestPointsY[k] = yPoints[k];
                            }
                        }
                    }
                }
            }
        }
    }
}
