//*****************************************************************************
//** 1593. Split a String Into the Max Number of Unique Substrings  leetcode **
//*****************************************************************************


#define MOD 1000000007 // Large prime number to prevent overflow
#define HASH_SIZE 32768 // Size of the boolean array for used substrings

int maxUniqueSplitHelper(char* s, int start, bool* usedSubstrings, int len, int* maxCount) {
    if(start == len) {
        return 0;
    }

    int localMax = 0;
    for(int i = start; i < len; i++) {
        // Extract the current substring from s[start] to s[i]
        char temp[17] = {0}; // s length can be at most 16
        strncpy(temp, &s[start], i - start + 1);

        // Hash the substring to check if it was used before
        long long hash = 0;
        for(int j = 0; j < strlen(temp); j++) {
            hash = (hash * 31 + (temp[j] - 'a' + 1)) % MOD; // Using modulo to prevent overflow
        }

        // If this substring was not used before
        if(!usedSubstrings[hash % 32768]) {
            usedSubstrings[hash % 32768] = true;
            int splitCount = 1 + maxUniqueSplitHelper(s, i + 1, usedSubstrings, len, maxCount);
            localMax = (splitCount > localMax) ? splitCount : localMax;
            usedSubstrings[hash % 32768] = false; // backtrack
        }
    }

    return localMax;
}

int maxUniqueSplit(char* s) {
    int len = strlen(s);
    bool usedSubstrings[32768] = {false}; // use a hash table to keep track of used substrings
    int maxCount = 0;
    return maxUniqueSplitHelper(s, 0, usedSubstrings, len, &maxCount);
}