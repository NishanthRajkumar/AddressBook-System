namespace AddressBookSystem;

/// <summary>
/// This Class handles all contacts in an address book
/// </summary>
internal class AddressBook
{
    // Dictionary for storing contacts with unique name
    private readonly Dictionary<string, Contact> addresses;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddressBook"/> class.
    /// </summary>
    public AddressBook()
    {
        addresses = new Dictionary<string, Contact>();
    }

    /// <summary>
    /// Creates a new contact.
    /// </summary>
    public void CreateContact()
    {
        AddContact(new Contact());
    }

    /// <summary>
    /// Adds the contact to the AddressBook.
    /// </summary>
    /// <param name="contact">The Contact object.</param>
    public void AddContact(Contact contact)
    {
        string name = contact.GetName();
        if (addresses.ContainsKey(name) is false && String.IsNullOrEmpty(name) is false)
        {
            addresses.Add(name, contact);
            Console.WriteLine("Contact Added Successfully");
        }
        else if (String.IsNullOrEmpty(name))
            Console.WriteLine("Invalid Contac name");
        else
            Console.WriteLine("Contact name already exists");
    }

    /// <summary>
    /// Adds multiple contacts.
    /// </summary>
    public void AddMultipleContacts()
    {
        int numberOfContacts = UserInput.GetPositiveInt("Enter no of Contacts to add: ");
        for (int i = 0; i < numberOfContacts; i++)
            CreateContact();
    }

    /// <summary>
    /// Edits a contact in AddressBook.
    /// </summary>
    public void EditContact()
    {
        Console.Write("Enter Name of contact to edit: ");
        string name = UserInput.ReadString();
        if (addresses.ContainsKey(name))
        {
            Console.WriteLine("\nCurrent info of " + name);
            addresses[name].Display();
            Console.WriteLine("\nEdit info: ");
            Contact contact = new();
            string newName = contact.GetName();
            if (addresses.ContainsKey(newName) is false || newName == name)
            {
                addresses.Remove(name);
                addresses[newName] = contact;
                Console.WriteLine("Updated!!");
            }
            else
                Console.WriteLine("Contact name already exists. Failed to update changes");
        }
        else
            Console.WriteLine("The contact does not exist");
    }

    /// <summary>
    /// Deletes a contact from AddressBook.
    /// </summary>
    public void DeleteContact()
    {
        Console.Write("Enter Name of contact to delete: ");
        string name = UserInput.ReadString();
        if (addresses.ContainsKey(name))
        {
            addresses.Remove(name);
            Console.WriteLine("Contact removed");
        }
        else
            Console.WriteLine("Contact does not exist");
    }

    /// <summary>
    /// Displays the full list of contacts in the AddressBook.
    /// </summary>
    public void Display()
    {
        Console.WriteLine("List of Contacts:");
        foreach (var name in addresses.Keys)
            addresses[name].Display();
    }
}