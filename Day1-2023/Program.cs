using System;
using System.IO;
using Day1_BLL;

CalibrationRecoveryTool calibrationRecoveryTool = new CalibrationRecoveryTool();
string[] calibrationLines = File.ReadAllLines(@"D:\Fontys\AdventOfCode\Day1-2023\Day1-2023\PuzzleInput.txt");

int finalCalibrationCount = calibrationRecoveryTool.TryRecoveringCalibration(calibrationLines);

Console.WriteLine(finalCalibrationCount);