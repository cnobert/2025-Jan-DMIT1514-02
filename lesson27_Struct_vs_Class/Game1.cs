using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson27_Struct_vs_Class;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }
    protected class TwoNumbersClass
    {
        int a, b;
        public TwoNumbersClass(int n01, int n02)
        {
            a = n01;
            b = n02;
        }

        public int B { get => b; set => b = value; }
        public int A { get => a; set => a = value; }
    }
    
    //structs are "value" types
    //and example of a struct is a Vector2
    protected struct TwoNumbersStruct 
    {
        public int a, b;
    }
    protected void AtoB(TwoNumbersClass theNumbers)
    {
        theNumbers.A = theNumbers.B;
    }
    protected void AtoBStructs(TwoNumbersStruct theNumbers)
    {
        theNumbers.a = theNumbers.b;
    }
    struct PointStruct
    {
        public double X;
        public double Y;
        public double Z;

        public PointStruct(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double CalculateDistance()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }
    }

    // Define a class with the same fields as the struct
    class PointClass
    {
        public double X;
        public double Y;
        public double Z;

        public PointClass(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double CalculateDistance()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }
    }
    protected override void Initialize()
    {
        //int, float, double, char are all "value" types
        // int a = 10;
        // int b = 20;

        // float c = 30;

        // TwoNumbersStruct numStruct;
        // numStruct.a = 100;
        // numStruct.b = 200;

        // TwoNumbersClass theNumbers;//at this point, it's equal to "null"
        // theNumbers = new TwoNumbersClass(500, 1000);
        // AtoB(theNumbers);
        // Console.WriteLine($"theNumbers.a = {theNumbers.A}");
        // Console.WriteLine($"theNumbers.b = {theNumbers.B}");

        // AtoBStructs(numStruct);
        // Console.WriteLine($"numStruct.a = {numStruct.a}");
        // Console.WriteLine($"numStruct.b = {numStruct.b}");

        const int arraySize = 10_000_000; // 10 million objects
        Console.WriteLine($"Creating and processing {arraySize:N0} objects...\n");

        // Test structs
        Stopwatch structStopwatch = new Stopwatch();
        structStopwatch.Start();
        
        // Create array of structs
        PointStruct[] structArray = new PointStruct[arraySize];
        

        // Initialize structs
        for (int i = 0; i < arraySize; i++)
        {
            structArray[i] = new PointStruct(i * 0.1, i * 0.2, i * 0.3);
        }
        
        // Process structs (calculate distance for each)
        double structSum = 0;
        for (int i = 0; i < arraySize; i++)
        {
            structSum += structArray[i].CalculateDistance();
        }
        
        structStopwatch.Stop();

        // Test classes
        Stopwatch classStopwatch = new Stopwatch();
        classStopwatch.Start();
        
        // Create array of classes
        PointClass[] classArray = new PointClass[arraySize];
        
        // Initialize classes
        for (int i = 0; i < arraySize; i++)
        {
            classArray[i] = new PointClass(i * 0.1, i * 0.2, i * 0.3);
        }
        
        // Process classes (calculate distance for each)
        double classSum = 0;
        for (int i = 0; i < arraySize; i++)
        {
            classSum += classArray[i].CalculateDistance();
        }
        
        classStopwatch.Stop();
        // Display results
        Console.WriteLine("Results:");
        Console.WriteLine($"Struct Time: {structStopwatch.ElapsedMilliseconds:N0} ms");
        Console.WriteLine($"Class Time:  {classStopwatch.ElapsedMilliseconds:N0} ms");
        Console.WriteLine($"Difference:  {classStopwatch.ElapsedMilliseconds - structStopwatch.ElapsedMilliseconds:N0} ms");
        
        if (structStopwatch.ElapsedMilliseconds < classStopwatch.ElapsedMilliseconds)
        {
            double speedup = (double)classStopwatch.ElapsedMilliseconds / structStopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Structs were {speedup:F2}x faster than classes");
        }
        else
        {
            double speedup = (double)structStopwatch.ElapsedMilliseconds / classStopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Classes were {speedup:F2}x faster than structs");
        }
        
        // Verify calculations produced the same result
        Console.WriteLine($"\nCalculation sums (should be equal):");
        Console.WriteLine($"Struct sum: {structSum:F2}");
        Console.WriteLine($"Class sum:  {classSum:F2}");
    }



}
