using System.Text.RegularExpressions;

namespace Day1_BLL;

public class CalibrationRecoveryTool
{
    private static int _firstNumber = 0;
    private static int _lastNumber = 0;

    private static readonly Dictionary<string, int> WordToNumberMap = new()
    {
        { "one", 1 }, { "two", 2 }, { "three", 3 }, { "four", 4 }, { "five", 5 }, { "six", 6 }, { "seven", 7 },
        { "eight", 8 }, { "nine", 9 }
    };

    public int TryRecoveringCalibration(string[] calibrationLines)
    {
        List<int> twoDigitNumbers = calibrationLines.Select(GetTwoDigitNumber).ToList();

        return twoDigitNumbers.Sum();
    }

    private static int GetTwoDigitNumber(string calibrationLine)
    {
        List<char> charactersCalibration = calibrationLine.ToList();
        List<char> collectedLetters = new();
        _firstNumber = 0;
        _lastNumber = 0;

        foreach (char characterCalibration in charactersCalibration)
        {
            UpdateCollectedLetters(collectedLetters, characterCalibration);
        }
        
        return int.Parse(_firstNumber.ToString() + _lastNumber.ToString());
    }

    private static void UpdateCollectedLetters(List<char> collectedLetters, char characterCalibration)
    {
        if (char.IsDigit(characterCalibration))
        {
            UpdateNumbersFromDigit(characterCalibration);
            collectedLetters.Clear();
        }
        else
        {
            if (char.IsLetter(characterCalibration))
            {
                collectedLetters.Add(characterCalibration);
            }

            UpdateNumbersFromWord(collectedLetters);
        }
    }

    private static void UpdateNumbersFromDigit(char digit)
    {
        if (_firstNumber == 0)
        {
            _firstNumber = int.Parse(digit.ToString());
        }

        _lastNumber = int.Parse(digit.ToString());
    }

    private static void UpdateNumbersFromWord(List<char> collectedLetters)
    {
        string collectedWord = string.Concat(collectedLetters);
        int maxIndex = -1;
        string foundKey = FindMaxIndexKey(collectedWord);

        if (foundKey == "")
        {
            return;
        }

        UpdateNumbersFromWordMapping(foundKey);
    }

    private static string FindMaxIndexKey(string collectedWord)
    {
        int maxIndex = -1;
        string foundKey = "";

        foreach (var kvp in WordToNumberMap)
        {
            var index = collectedWord.LastIndexOf(kvp.Key, StringComparison.OrdinalIgnoreCase);

            if (index != -1 && index > maxIndex)
            {
                maxIndex = index;
                foundKey = kvp.Key;
            }
        }

        return foundKey;
    }

    private static void UpdateNumbersFromWordMapping(string foundKey)
    {
        foreach (var kvp in WordToNumberMap.Where(kvp => foundKey.Contains(kvp.Key, StringComparison.OrdinalIgnoreCase)))
        {
            UpdateNumbersFromWordMapping(kvp.Value);
        }
    }

    private static void UpdateNumbersFromWordMapping(int wordValue)
    {
        if (_firstNumber == 0)
        {
            _firstNumber = wordValue;
        }
        else
        {
            _lastNumber = wordValue;
        }
    }
}