﻿using Interfaces;

namespace Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly string _key = "";
    private readonly List<char> _alphabetEn = new() { 'a', 'b', 'c', 'd', 'e', 'f', 'g',
        'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's',
        't', 'u', 'v', 'w', 'x', 'y', 'z'};
    private readonly List<char> _alphabetUa = new() { 'а', 'б', 'в', 'г', 'ґ', 'д', 'е',
        'є', 'ж', 'з', 'и', 'і', 'ї', 'й', 'к', 'л', 'м', 'н', 'о',
        'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ю', 'я'};
    private readonly List<char> _numbers = new() {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', };
    private readonly List<char> _symbols = new() { ' ', '.', ',', '@' };
    private List<char> AlphabetUpperEn => _alphabetEn.Select(x => char.ToUpper(x)).ToList();
    private List<char> AlphabetUpperUa => _alphabetUa.Select(x => char.ToUpper(x)).ToList();
        
    private List<char> _substitionTable = new();
    private List<char> _alphabet = new();

    public TokenService() 
    {
        _alphabet.AddRange(_alphabetEn);
        _alphabet.AddRange(AlphabetUpperEn);
        _alphabet.AddRange(_alphabetUa);
        _alphabet.AddRange(AlphabetUpperUa);
        _alphabet.AddRange(_numbers);
        _alphabet.AddRange(_symbols);

        List<int> substitionBox = Enumerable.Range(0, _alphabet.Count).Select(x => x).OrderBy(x => x).ToList();
        Shuffle(substitionBox);
        substitionBox.ForEach((value) => {
            _substitionTable.Add(_alphabet[value]);
        });
    }

    private IList<T> Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = new Random().Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

        return list;
    }

    public string Encrypt(string data) 
    {
        var cipheredData = "";
        foreach (var c in data) 
        {
            var cipheredCharIndex = _alphabet.FindIndex(x => x == c);
            var cipheredChar = _substitionTable[cipheredCharIndex];
            cipheredData += cipheredChar;
        }

        return cipheredData;
    }

    public string Decrypt(string cipheredData)
    {
        var text = "";
        foreach (var c in cipheredData.Replace("Loli ", ""))
        {
            var decryptedCharIndex = _substitionTable.FindIndex(x => x == c);
            var decryptedChar = _alphabet[decryptedCharIndex];
            text += decryptedChar;
        }

        return text;
    }

    public string GenerateToken(string data)
    {
        var text = Encrypt(data);
        var prefix = "Loli ";
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(text);

        return prefix + Convert.ToBase64String(plainTextBytes);
    }
}