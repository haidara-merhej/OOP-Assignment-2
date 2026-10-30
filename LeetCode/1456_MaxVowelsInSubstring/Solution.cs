
int max = MaxVowels("abciiidef", 3);

Console.WriteLine(max);

int MaxVowels(string s, int k) {
    HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };
    int maxVowels = 0;
    int currentCount = 0;

    for(int i = 0; i < k; i++)
    {
        if(vowels.Contains(s[i]))
            currentCount++;
    }

    maxVowels = currentCount;

    for(int i = k; i < s.Length; i++)
    {
        if(vowels.Contains(s[i]))
            currentCount++;

        if(vowels.Contains(s[i - k]))
            currentCount--;

        maxVowels = Math.Max(maxVowels, currentCount);
    }

    return maxVowels;
}
