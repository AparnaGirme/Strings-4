/*
Given a char grid (o represents an empty cell and x represents a target object) and an API getResponse which would give you a response w.r.t. to your previous position. Write a program to find the object. You can move to any position.
enum Response {
 HOTTER,  // Moving closer to target
 COLDER,  // Moving farther from target
 SAME,    // Same distance from the target as your previous guess
 EXACT;   // Reached destination
}

// Throws an error if 'row' or 'col' is out of bounds
public Response getResponse(int row, int col) {
 // black box
}
*/

// TC => O(logm + logn)
// SC => O(1)
class HelloWorld {
    static void Main() {
        char[,] grid = {
            { 'o', 'o', 'o' },
            { 'o', 'o', 'o' },
            { 'o', 'o', 'o' },
            { 'o', 'o', 'x' },
            { 'o', 'o', 'o' }
        };

        GridNavigator navigator = new GridNavigator(grid);
        int m = grid.GetLength(0);
        int n = grid.GetLength(1);
        
        //Find the row
        int low = 0, high = m-1, row = -1, column = -1;
        while(low <= high){
            int mid = low + (high - low)/2;
            var midPosition = navigator.getResponse(mid, true);
            Console.WriteLine($"midPosition => {midPosition}");
            if(midPosition == Response.EXACT){
                row = mid;
                break;
            }
            var lowPosition = navigator.getResponse(low, true); 
            Console.WriteLine($"lowPosition => {lowPosition}");
            if(lowPosition == Response.EXACT){
                row = low;
                break;
            }
            var highPosition = navigator.getResponse(high, true);
            Console.WriteLine($"highPosition => {highPosition}");
            if(highPosition == Response.EXACT){
                row = high;
                break;
            }
            if(lowPosition == Response.HOTTER || (lowPosition == midPosition && midPosition == Response.SAME)){
                high = mid - 1;
            }
            else if(highPosition == Response.HOTTER || (highPosition == midPosition && midPosition == Response.SAME)){
                low = mid + 1;
            }
        }
        
        if(row == -1){
            Console.WriteLine("Could not find proper row");
            return;
        }
        Console.WriteLine("row "+ row);
        //Find the column
        low = 0;
        high = n-1;
        while(low <= high){
            int mid = low + (high - low)/2;
            var midPosition = navigator.getResponse(mid, false);
            Console.WriteLine($"midPosition => {midPosition}");
            if(midPosition == Response.EXACT){
                column = mid;
                break;
            }
            var lowPosition = navigator.getResponse(low, false); 
            Console.WriteLine($"lowPosition => {lowPosition}");
            if(lowPosition == Response.EXACT){
                column = low;
                break;
            }
            var highPosition = navigator.getResponse(high, false);
            Console.WriteLine($"highPosition => {highPosition}");
            if(highPosition == Response.EXACT){
                column = high;
                break;
            }
            if(lowPosition == Response.HOTTER || (lowPosition == midPosition && midPosition == Response.SAME)){
                high = mid - 1;
            }
            else if(highPosition == Response.HOTTER || (highPosition == midPosition && midPosition == Response.SAME)){
                low = mid + 1;
            }
        }
        
        Console.WriteLine($"Target found at Row: {row}, Column: {column}");
    }
}

public enum Response
{
    HOTTER,
    COLDER,
    SAME,
    EXACT
}

public class GridNavigator
{
    private readonly char[,] grid;
    private readonly int targetRow;
    private readonly int targetCol;

    private int? previousRow = null;
    private int? previousCol = null;

    public GridNavigator(char[,] grid)
    {
        this.grid = grid;

        // Find the target (x)
        for (int r = 0; r < grid.GetLength(0); r++)
        {
            for (int c = 0; c < grid.GetLength(1); c++)
            {
                if (grid[r, c] == 'x')
                {
                    targetRow = r;
                    targetCol = c;
                    return;
                }
            }
        }

        throw new InvalidOperationException("Target 'x' not found in grid.");
    }

//     public Response getResponse(int row, int col)
//     {
//         Console.WriteLine($"Checking position at {row}, {col}");
//         if (row < 0 || row >= grid.GetLength(0) || col < 0 || col >= grid.GetLength(1))
//         {
//             throw new ArgumentOutOfRangeException("Position out of bounds");
//         }

//         if (row == targetRow && col == targetCol)
//         {
//             Console.WriteLine($"Checking position at {row}, {col} => Response.EXACT");
//             return Response.EXACT;
//         }

//         double currentDistance = GetDistance(row, col);

//         if (prevRow == null || prevCol == null)
//         {
//             // First guess, just store and return SAME
//             prevRow = row;
//             prevCol = col;
//             Console.WriteLine($"Checking position at {row}, {col} => Response.SAME");
//             return Response.SAME;
//         }

//         double prevDistance = GetDistance(prevRow.Value, prevCol.Value);
//         prevRow = row;
//         prevCol = col;

//         if (currentDistance < prevDistance)
//         {
//             Console.WriteLine($"Checking position at {row}, {col} => Response.HOTTER");
//             return Response.HOTTER;
//         }
            
//         else if (currentDistance > prevDistance){
//             Console.WriteLine($"Checking position at {row}, {col} => Response.COLDER");
//             return Response.COLDER;
//         }
//         else
//         {
//             Console.WriteLine($"Checking position at {row}, {col} => Response.SAME");
//             return Response.SAME;
//         }
            
//     }
    
    public Response getResponse(int index, bool isRow)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        int currentRow = previousRow ?? 0;
        int currentCol = previousCol ?? 0;

        if (isRow)
        {
            currentRow = index;
        }
        else
        {
            currentCol = index;
        }

        if (currentRow < 0 || currentRow >= rows || currentCol < 0 || currentCol >= cols)
        {
            throw new ArgumentOutOfRangeException("Row or column is out of bounds.");
        }
        if(isRow && currentRow == targetRow){
            return Response.EXACT;
        }
        if(!isRow && currentCol == targetCol){
            return Response.EXACT;
        }
        // if (currentRow == targetRow || currentCol == targetCol)
        // {
        //     return Response.EXACT;
        // }

        if (previousRow == null && previousCol == null)
        {
            previousRow = currentRow;
            previousCol = currentCol;
            return Response.SAME;
        }

        double prevDistance = GetDistance(previousRow.Value, previousCol.Value);
        double currentDistance = GetDistance(currentRow, currentCol);

        previousRow = currentRow;
        previousCol = currentCol;

        if (currentDistance < prevDistance)
        {
            return Response.HOTTER;
        }
        else if (currentDistance > prevDistance)
        {
            return Response.COLDER;
        }
        else
        {
            return Response.SAME;
        }
    }

    private double GetDistance(int row, int col)
    {
        return Math.Sqrt((row - targetRow) * (row - targetRow) + (col - targetCol) * (col - targetCol));
    }
}
