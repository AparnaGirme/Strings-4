public class Solution {
    // TC => O(n)
    // SC => O(1)
    public int MyAtoi(string s) {
        s = s.Trim();
        if(string.IsNullOrEmpty(s)){
            return 0;
        }
        char sign = '+';
        if(s[0] == '-'){
            sign = '-';
        }
        if(s[0] != '+' && s[0] != '-' && !char.IsDigit(s[0])){
            return 0;
        }

        int num = 0;
        int limit = Int32.MaxValue / 10;
        for(int i = 0; i< s.Length; i++){
            if(char.IsDigit(s[i])){
                if(sign == '+'){
                    if((num > limit) || (num == limit && s[i] >= '8'))
                    {
                        return Int32.MaxValue;
                    }
                }
                else
                {
                    if((num > limit) || num == limit && (s[i] >= '8' || s[i] >= '9'))
                    {
                        return Int32.MinValue;
                    }
                }
                num = num * 10 + (s[i] - '0');
            }
            else if(i != 0){
                break;
            }
            
        }
        if(sign == '-'){
            return -num;
        }
        return num;
    }
}