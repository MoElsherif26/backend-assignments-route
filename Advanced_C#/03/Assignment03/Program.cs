namespace Assignment03
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Q1 - Reverse Queue Using Stack
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);

            Console.WriteLine("Original Queue: " + string.Join(", ", queue));
            ReverseQueue(queue);
            Console.WriteLine("Reversed Queue: " + string.Join(", ", queue));
            #endregion

            Console.WriteLine(new string('-', 40));

            #region Q2 - Balanced Parentheses Check
            string input1 = "[()]{}";
            string input2 = "[(])";

            Console.WriteLine($"Input: {input1} => {(IsBalanced(input1) ? "Balanced" : "Not Balanced")}");
            Console.WriteLine($"Input: {input2} => {(IsBalanced(input2) ? "Balanced" : "Not Balanced")}");
            #endregion

            Console.ReadLine();
        }

        // Q1: Reverse Queue Using Stack
        static void ReverseQueue(Queue<int> queue)
        {
            Stack<int> stack = new Stack<int>();

            while (queue.Count > 0)
            {
                stack.Push(queue.Dequeue());
            }

            while (stack.Count > 0)
            {
                queue.Enqueue(stack.Pop());
            }
        }

        // Q2: Balanced Parentheses Check
        static bool IsBalanced(string str)
        {
            Stack<char> stack = new Stack<char>();
            Dictionary<char, char> pairs = new Dictionary<char, char>
            {
                { ')', '(' },
                { ']', '[' },
                { '}', '{' }
            };

            foreach (char ch in str)
            {
                if (pairs.ContainsValue(ch))
                {
                    stack.Push(ch);
                }
                else if (pairs.ContainsKey(ch))
                {
                    if (stack.Count == 0 || stack.Pop() != pairs[ch])
                        return false;
                }
            }

            return stack.Count == 0;
        }
    }
}
