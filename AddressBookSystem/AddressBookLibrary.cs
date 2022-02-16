namespace AddressBookSystem;

// This class used for keeping collection of AddressBook objects
internal class AddressBookLibrary
{
    private Dictionary<string, AddressBook> library;

    // Default Constructor
    public AddressBookLibrary()
    {
        library = new Dictionary<string, AddressBook>();
    }

    // Adds new AddressBook to library
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
}