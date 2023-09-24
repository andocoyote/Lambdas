namespace lambdas
{
    public class Person
    {
        public int Age { get; set; }
        public string Name { get; set; }
    }

    internal class Program
    {
        // 1. Anonymous methods can be defined using the delegate keyword
        // This function delegate takes a Person and returns a Boolean
        public delegate bool FilterDelegate(Person person);

        // Delegates are function pointers but you declare the return type and parameter types differently
        // This function pointer takes a Person and returns a Boolean
        public static Func<Person, bool> FilterFunction { get; set; } = null;

        static void Main(string[] args)
        {
            Person p1 = new() { Name = "Ando", Age = 47 };
            Person p2 = new() { Name = "Danika", Age = 48 };
            Person p3 = new() { Name = "Margie", Age = 12 };
            Person p4 = new() { Name = "Bart", Age = 13 };

            List<Person> people = new List<Person>() { p1, p2, p3, p4 };

            // 2. Explicitly define a delegate variable 'filter' using an anonymous method rather than an already-defined method
            // This function needs to take a Person and return a Boolean
            FilterDelegate filter = delegate (Person p)
            {
                return p.Age > 10 && p.Age < 20;
            };
            DisplayPeopleViaDelegate("Dogs", people, filter);
            Console.WriteLine();

            // 3. Lambdas are anonymous functions.
            // Read "=>" as "goes into" or "where" or "such that"
            // Use ( param1, param2, ... ) => ... for multiple parameters
            // Use { <statements> } to define multiple statements inside of a lambda
            DisplayPeopleViaDelegate("Lambda via delegate Dogs", people, p =>
            {
                bool greaterThan10 = p.Age > 10;
                bool lessThan20 = p.Age < 20;

                return greaterThan10 && lessThan20;
            });
            Console.WriteLine();

            // 4. Delegates vs. lambdas vs. function pointers
            // Define the function using a function pointer
            FilterFunction = (p => p.Age > 10 && p.Age < 20);
            DisplayPeopleViaFunctionPointer("Dogs", people, FilterFunction);
            Console.WriteLine();

            // Alternatively, just pass in a lambda rather than a function pointer
            DisplayPeopleViaFunctionPointer("Lambda via function pointer Dogs", people, p =>
            {
                bool greaterThan10 = p.Age > 10;
                bool lessThan20 = p.Age < 20;

                return greaterThan10 && lessThan20;
            });
            Console.WriteLine();

            // So, delegates and lambdas are really just function pointers!
            // Else, there would be a compiler error or warning below.
            // What do you gain by using delegates over function pointers?  Nothing.
            // What do you gain by using lambdas?  Less coding overhead- you simply inline the lambda function
            DisplayPeopleViaDelegate("Another lambda via delegate Dogs", people, ApplyAgeRange);
            Console.WriteLine();

            DisplayPeopleViaFunctionPointer("Another lambda via function pointer Dogs", people, ApplyAgeRange);
            Console.WriteLine();
        }

        static void DisplayPeopleViaDelegate(string title, List<Person> people, FilterDelegate filter)
        {
            Console.WriteLine($"DisplayPeopleViaDelegate {title}:");

            List<Person> dogs = people.Where(p => filter(p)).ToList();

            foreach (Person p in dogs)
            {
                Console.WriteLine($"{p.Name}, {p.Age} years old");
            }
            
        }

        static void DisplayPeopleViaFunctionPointer(string title, List<Person> people, Func<Person, bool> filter)
        {
            Console.WriteLine($"DisplayPeopleViaFunctionPointer {title}:");

            List<Person> dogs = people.Where(p => filter(p)).ToList();

            foreach (Person p in dogs)
            {
                Console.WriteLine($"{p.Name}, {p.Age} years old");
            }
        }

        static bool ApplyAgeRange(Person p)
        {
            bool greaterThan10 = p.Age > 10;
            bool lessThan20 = p.Age < 20;

            return greaterThan10 && lessThan20;
        }
    }
}