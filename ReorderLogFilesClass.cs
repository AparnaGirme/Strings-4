public class Solution {

    // TC => O(nlogn)
    // SC => O(1)
    public string[] ReorderLogFiles(string[] logs) {
        if(logs == null || logs.Length == 0){
            return logs;
        }

        // Array.Sort(logs, (log1, log2) => {
        //     string[] strArray1 = log1.Split(" ", 2);
        //     string[] strArray2 = log2.Split(" ", 2);

        //     bool isDigit1 = char.IsDigit(strArray1[1][0]);
        //     bool isDigit2 = char.IsDigit(strArray2[1][0]);

        //     if(!isDigit1 && !isDigit2){
        //         int compare = strArray1[1].CompareTo(strArray2[1]);
        //         if(compare == 0){
        //             return strArray1[0].CompareTo(strArray2[0]);
        //         }
        //         return compare;
        //     }
        //     else if(!isDigit1 && isDigit2){
        //         return -1;
        //     }
        //     else if(isDigit1 && !isDigit2){
        //         return 1;
        //     }
        //     else{
        //         return 0;
        //     }

        // });
        // return logs;

       return logs
            .Select((log, index) => new { log, index }) // preserve original index
            .OrderBy(entry =>
            {
                var split = entry.log.Split(' ', 2);
                bool isDigit = char.IsDigit(split[1][0]);
                return isDigit ? 1 : 0; // digit-logs after letter-logs
            })
            .ThenBy(entry =>
            {
                var split = entry.log.Split(' ', 2);
                return char.IsDigit(split[1][0]) ? null : split[1]; // sort by content
            })
            .ThenBy(entry =>
            {
                var split = entry.log.Split(' ', 2);
                return char.IsDigit(split[1][0]) ? null : split[0]; // sort by identifier
            })
            .Select(entry => entry.log)
            .ToArray();
    }
}