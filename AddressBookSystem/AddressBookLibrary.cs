namespace AddressBookSystem;

/// <summary>
/// This class keeps collection of Address books
/// </summary>
internal class AddressBookLibrary
{
    private readonly Dictionary<string, AddressBook> library;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddressBookLibrary"/> class.
    /// </summary>
    public AddressBookLibrary()
    {
        library = new Dictionary<string, AddressBook>();
    }

    /// <summary>
    /// Adds an address book to the library.
    /// </summary>
    public void AddAddressBook()
    {
        string name = UserInput.GetName("Enter Name of AddressBook: ");
        if (library.ContainsKey(name) is false && String.IsNullOrEmpty(name) is false)
        {
            library.Add(name, new AddressBook());
            Console.WriteLine("AddressBook Added Successfully");
        }
        else if (String.IsNullOrEmpty(name))
            Console.WriteLine("Invalid name");
        else
            Console.WriteLine("AddressBook with that name already exists");
    }

    /// <summary>
    /// Opens an existing AddressBook with menu option to interact with it
    /// </summary>
    public void OpenAddressBook()
    {
        string name = UserInput.GetName("Enter Name of AddressBook: ");
        if (library.ContainsKey(name))
            AddressBookMenu.List(name, library[name]);
        else
            Console.WriteLine("Addressbook with that name does not exist");
    }

    /// <summary>
    /// Display all Address Books in the library
    /// </summary>
    public void Display()
    {
        Console.WriteLine("List of Address Books:");
        foreach (var name in library.Keys)
            Console.WriteLine(name);
    }

    /// <summary>
    /// Filter results based on location
    /// </summary>
    public void LocationFilter()
    {
        int option = 0;
        Console.WriteLine("Search the full library of AddressBooks:");
        Console.WriteLine("1. Search by state");
        Console.WriteLine("2. Search by city");
        Console.Write("Option: ");
        do
        {
            try
            {
                option = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Input must be Integer only");
            }
        } while (option != 1 && option != 2);
        switch (option)
        {
            case 1:
                Console.Write("Enter state: ");
                string state = Console.ReadLine();
                Console.WriteLine($"List of contacts in {state}");
                StateFilter(state);
                break;
            case 2:
                Console.WriteLine("Enter City: ");
                string city = Console.ReadLine();
                Console.WriteLine($"List of contacts in {city}");
                CityFilter(city);
                break;
            default:
                Console.WriteLine("Error!!!");
                break;
        }
    }

    /// <summary>
    /// Filter results by city
    /// </summary>
    public void CityFilter(string city)
    {
        Dictionary<string, AddressBook>.Enumerator enumerator = library.GetEnumerator();
        while (enumerator.MoveNext())
        {
            enumerator.Current.Value.CityFilter(city);
        }
    }

    /// <summary>
    /// Filter results by state
    /// </summary>
    public void StateFilter(string State)
    {
        Dictionary<string, AddressBook>.Enumerator enumerator = library.GetEnumerator();
        while (enumerator.MoveNext())
        {
            enumerator.Current.Value.StateFilter(State);
        }
    }
}