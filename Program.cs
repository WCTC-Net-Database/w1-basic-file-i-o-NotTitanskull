namespace W1_Basic_File_IO;

class Program
{
    // I wasn't able to get the relative path to work, so I'm using an absolute path.
    private const string CharacterData = "../../../input.csv";
    
    static void Main()
    {
        while (true)
        {
            PrintMenu();
            string? userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    DisplayCharacters();
                    break;
                case "2":
                    AddCharacter();
                    break;
                case "3":
                    LevelUpCharacter();
                    break;
                case "4":
                    Console.WriteLine("\nExiting application...");
                    return;
                default:
                    Console.WriteLine("\nInvalid option. Please choose 1–4.");
                    break;
            }
            Console.WriteLine("\nPress Enter to return to the menu...");
            Console.ReadLine();
        }
    }

    static void PrintMenu()
    {
        Console.Clear();
        Console.WriteLine("1.) Display Characters");
        Console.WriteLine("2.) Add Character");
        Console.WriteLine("3.) Level Up Character");
        Console.WriteLine("4.) Exit");
        Console.Write("> ");
    }
    
    static void DisplayCharacters()
    {
        Console.WriteLine("--------------");
        
        foreach (string record in File.ReadLines(CharacterData))
        {
            string[] fields = record.Split(',', 5);
            
            string name = fields[0];
            string charClass = fields[1];
            int level = int.Parse(fields[2]);
            int hp = int.Parse(fields[3]);
            
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Class: {charClass}");
            Console.WriteLine($"Level: {level}");
            Console.WriteLine($"HP: {hp}");
            
            if (fields.Length == 5)
            {
                string[] equipment = fields[4].Split('|');
                Console.WriteLine($"Equipment: {string.Join(", ", equipment)}");
            }
            else
            {
                Console.WriteLine("Equipment: None");
            }
            Console.WriteLine("--------------");
        }
    }
    
    static void AddCharacter()
    {
        Console.Write("Enter your character's name: ");
        string name = Console.ReadLine();
        
        Console.Write("Enter your character's class: ");
        string characterClass = Console.ReadLine();
        
        Console.Write("Enter your character's level: ");
        int level = int.Parse(Console.ReadLine());
        
        Console.Write("Enter your character's HP: ");
        int hp = int.Parse(Console.ReadLine());
        
        Console.Write("Enter your character's equipment (separate items with a '|'): ");
        string[] equipment = Console.ReadLine().Split('|', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        string newRecord = $"{name},{characterClass},{level},{hp},{string.Join("|", equipment)}";
        
        using (var writer = new StreamWriter(CharacterData, append: true))
        {
            writer.WriteLine(newRecord);
        }
        
        Console.WriteLine("\nCharacter added.");
    }
    
    static void LevelUpCharacter()
    {
        string[] records = File.ReadAllLines(CharacterData);
        
        Console.WriteLine("\n--------------");
        for (int i = 0; i < records.Length; i++)
        {
            string[] fields = records[i].Split(',', 5);
            string name = fields[0];
            int level = int.Parse(fields[2]);
            Console.WriteLine($"{i + 1}. {name}, Level {level}");
        }
        Console.WriteLine("--------------");
        
        Console.Write($"\nChoose a character (1-{records.Length}): ");
        int index = int.Parse(Console.ReadLine()!) - 1;
        
        string[] charField = records[index].Split(',', 5);
        charField[2] = (int.Parse(charField[2]) + 1).ToString();

        records[index] = string.Join(",", charField);

        File.WriteAllLines(CharacterData, records);
        Console.WriteLine($"\n{charField[0]} leveled up to {charField[2]}!");
    }
}