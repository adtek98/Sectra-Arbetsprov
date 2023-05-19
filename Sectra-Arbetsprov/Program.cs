using Sectra_Arbetsprov;

List<Register> registers = new List<Register>();
StreamReader streamReader = (args.Length == 1) ? new StreamReader(args[0]) : null; // Creating an instance of StreamReader to read text from a file, but only if there is one command-line argument. Otherwise makes it null.
string[] operators = { "add", "multiply", "subtract" };

if (streamReader != null) Console.WriteLine("reading from file..\n");
else
{
    Console.WriteLine("<register> <operation> <value>");
    Console.WriteLine("print <register>");
    Console.WriteLine("quit\n");
}

while (true)
{

    string line = streamReader != null ? streamReader.ReadLine() : Console.ReadLine(); // If streamReader is not null, read input from file. Otherwise read input from console.

    if (line == null) break; // If there is no line in the file, breaks the loop and exits the program

    if (streamReader != null) Console.WriteLine(line);  // If reading from a file, write out the line.

    string[] input = line.ToLower().Split(" "); // splits the input to an array.
    float inputValue = 0;

    if (input[0] == "quit" && input.Length == 1)
    {
        Console.WriteLine("goodbye!");
        break;
    }

    if (input.Length == 3 && operators.Contains(input[1]))
    {
        if (float.TryParse(input[2], out float value)) inputValue = value; // Checks if the third input is a number or a string. If it is a number, put it as inputValue.
        else
        {
            var r = registers.Find(r => r.Name == input[2]); // If the third input is a string, check if it exists in list.

            if (r != null) 
            {
                inputValue = r.Value; // if it exists, put Value as inputValue.
            }
            else
            {
                Console.WriteLine("secondary register does not exist.");
                continue; // Skips rest of the loop and starts over
            }
        }

        Register register = registers.Find(r => r.Name == input[0]);

        if (register == null)  // If the register is new, add it to the list.
        {
            register = new Register() { Name = input[0] };
            registers.Add(register);
        }

        switch (input[1]) // Switch case for handling operators.
        {
            case "add":
                register.Value += inputValue;
                break;
            case "subtract":
                register.Value -= inputValue;
                break;
            case "multiply":
                register.Value *= inputValue;
                break;
        }

    }
    else if (input[0] == "print" && input.Length == 2)
    {
        Register register = registers.Find(r => r.Name == input[1]);
        Console.WriteLine(register != null ? register.Value.ToString() : "register does not exist, please try again."); // Prints out the targeted register value if it exist.
    }
    else
    {
        Console.WriteLine("invalid command, please try again.");
    }

}

streamReader?.Close();