using CsvHelper;
using Newtonsoft.Json;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;

namespace AddressBookSystem;

/// <summary>
/// This Class handles all contacts in an address book
/// </summary>
public class AddressBook
{
    const string PATH = @"C:\Users\Nishanth\Desktop\codingclub\RFP\Assignments\AddressBookSystem\AddressBookSystem\AddressBook Library\";
    public const string TEXT_FILES_PATH = PATH + @"Text Files\";
    public const string CSV_FILES_PATH = PATH + @"CSV Files\";
    public const string JSON_FILES_PATH = PATH + @"JSON Files\";
    public Dictionary<string, Contact> ContactList { get; set; }
    public readonly string name;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddressBook"/> class.
    /// </summary>
    public AddressBook(string name)
    {
        ContactList = new Dictionary<string, Contact>();
        this.name = name;
    }

    /// <summary>
    /// Creates a new contact
    /// </summary>
    public void CreateContact()
    {
        AddContact(new Contact());
    }

    /// <summary>
    /// Adds the contact to the AddressBook
    /// </summary>
    /// <param name="contact">The Contact object.</param>
    public void AddContact(Contact contact)
    {
        contact.GetContactInfo();
        string name = contact.FullName;
        if (name == null)
        {
            Console.WriteLine("Invalid Contact name");
            return;
        }
        if (ContactList.Any(e => e.Value.Equals(contact)) is false)
        {
            ContactList.Add(name, contact);
            Console.WriteLine("Contact Added Successfully");
        }
        else
            Console.WriteLine("Contact name already exists");
    }

    /// <summary>
    /// Adds multiple contacts
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
        if (ContactList.ContainsKey(name))
        {
            Console.WriteLine("\nCurrent info of " + name);
            ContactList[name].Display();
            Console.WriteLine("\nEdit info: ");
            Contact contact = new();
            contact.GetContactInfo();
            string newName = contact.FullName;
            if (ContactList.ContainsKey(newName) is false || newName == name)
            {
                ContactList.Remove(name);
                ContactList[newName] = contact;
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
        if (ContactList.ContainsKey(name))
        {
            ContactList.Remove(name);
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
        foreach (var name in ContactList.Keys)
            ContactList[name].Display();
    }

    /// <summary>
    /// Look up the contact details of the specified name
    /// </summary>
    public void LookUp(string fullName)
    {
        if (ContactList.ContainsKey(fullName))
            ContactList[fullName].Display();
        else
            Console.WriteLine("Contact does not exist");
    }

    /// <summary>
    /// Display filtered results based on location
    /// </summary>
    public void DisplayFilteredList()
    {
        List<Contact> filteredList = LocationFilter();
        foreach (Contact contact in filteredList)
            contact.Display();
    }

    /// <summary>
    /// filter by Location
    /// </summary>
    /// <returns>A list of contacts</returns>
    public List<Contact> LocationFilter()
    {
        int option = 0;
        MatchLocation Match;
        List<Contact> filteredList = new List<Contact>();
        Console.WriteLine("Filter Contact list in full library of AddressBooks:");
        Console.WriteLine("1. Filter by state");
        Console.WriteLine("2. Filter by city");
        Console.Write("Option: ");
        while (option != 1 && option != 2)
            while (int.TryParse(Console.ReadLine(), out option) is false)
                Console.WriteLine("Input must be Integer only");
        if (option == 1)
        {
            Match = new MatchLocation((contact, state) => { return contact.State == state; });
            Console.Write("Enter state: ");
        }
        else
        {
            Match = new MatchLocation((contact, city) => { return contact.City == city; });
            Console.WriteLine("Enter City: ");
        }
        string location = Console.ReadLine();
        FilterList(location, filteredList, Match);
        return filteredList;
    }

    /// <summary>
    /// Filters the list.
    /// </summary>
    /// <param name="location">The location.</param>
    /// <param name="filterList">The filter list.</param>
    /// <param name="Match">The match delegate</param>
    public void FilterList(string location, List<Contact> filterList, MatchLocation Match)
    {
        foreach (Contact contact in ContactList.Values)
            if (Match(contact, location))
                filterList.Add(contact);
    }

    /// <summary>
    /// Displays the count of contacts by location.
    /// </summary>
    public void DisplayCountByLocation()
    {
        Console.WriteLine("Count of contacts at the library level:");
        var cityWiseCount = GetLocationCount(contact => { return contact.City; }, DelegatesList.CityMatch);
        var stateWiseCount = GetLocationCount(contact => { return contact.State; }, DelegatesList.StateMatch);
        Console.WriteLine("\nCity wise count: ");
        foreach (var city in cityWiseCount)
            Console.WriteLine($"City: {city.Key}, No of contacts: {city.Value}");
        Console.WriteLine("\nState wise count: ");
        foreach (var state in stateWiseCount)
            Console.WriteLine($"State: {state.Key}, No of contacts: {state.Value}");
    }

    /// <summary>
    /// Gets the location count.
    /// </summary>
    /// <param name="Selector">The selector.</param>
    /// <param name="Match">The match.</param>
    /// <returns>returns location wise count as dictionary</returns>
    public Dictionary<string, int> GetLocationCount(GetLocation Selector, MatchLocation Match)
    {
        Dictionary<string, int> counts = new();
        var locationList = ContactList.Values.Select(x => Selector(x)).Distinct().ToList();
        foreach (var location in locationList)
            counts.Add(location, ContactList.Values.Count(contact => Match(contact, location)));
        return counts;
    }

    /// <summary>
    /// Menu option for sorting the contact list
    /// </summary>
    public void SortOptions()
    {
        int option = 0;
        Console.WriteLine("Sorting Options:");
        Console.WriteLine("1. Sort by Name");
        Console.WriteLine("2. Sort by City");
        Console.WriteLine("3. Sort by State");
        Console.WriteLine("4. Sort by Zip");
        Console.Write("Option: ");
        if (int.TryParse(Console.ReadLine(), out option))
        {
            switch (option)
            {
                case 1:
                    Console.WriteLine("Sorting by Name\n");
                    Sort(x => x.Value.FullName);
                    break;
                case 2:
                    Console.WriteLine("Sorting by City\n");
                    Sort(x => x.Value.City);
                    break;
                case 3:
                    Console.WriteLine("Sorting by State\n");
                    Sort(x => x.Value.State);
                    break;
                case 4:
                    Console.WriteLine("Sorting by Zip\n");
                    Sort(x => x.Value.Zip);
                    break;
                default:
                    Console.WriteLine("Option must be in (1-4)!!!");
                    break;
            }
        }
        else
            Console.WriteLine("Invalid option!");
    }

    /// <summary>
    /// Sorts the contact list using the specified sort condition.
    /// </summary>
    /// <param name="sortCondition">The sort condition.</param>
    private void Sort(Func<KeyValuePair<string, Contact>, string> sortCondition)
    {
        Console.WriteLine("Sorted List:");
        var sorted = ContactList.OrderBy(sortCondition);
        foreach (var contact in sorted)
            Console.WriteLine("\n" + contact.Value);
    }

    /// <summary>
    /// Saves to file.
    /// </summary>
    public void SaveToFile()
    {
        try
        {
            SaveAsJSON();
        }
        catch (Exception)
        {
            Console.WriteLine("Failed to save the addressbook as json file");
        }
        try
        {
            SaveAsTxt();
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to save the addressbook as text file");
            Console.WriteLine("Error msg: " + e.Message);
        }
        try
        {
            SaveAsCSV();
        }
        catch (Exception)
        {
            Console.WriteLine("Failed to save the addressbook as csv file");
        }
    }

    /// <summary>
    /// Saves as text file.
    /// </summary>
    public void SaveAsTxt()
    {
        FileStream fileStream = new FileStream(TEXT_FILES_PATH + name + @".txt", FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fileStream, ContactList); // convert object to binary & writes in a text file  
        fileStream.Close();
        fileStream.Dispose();
    }

    /// <summary>
    /// Saves as CSV file.
    /// </summary>
    public void SaveAsCSV()
    {
        using (var writer = new StreamWriter(CSV_FILES_PATH + name + @".csv"))
        using (var csvExport = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csvExport.WriteRecords(ContactList);
        }
    }

    /// <summary>
    /// Saves as json file.
    /// </summary>
    public void SaveAsJSON()
    {
        string jsonText = JsonConvert.SerializeObject(ContactList, Formatting.Indented);
        File.WriteAllText(JSON_FILES_PATH + name + @".json", jsonText);
    }

    /// <summary>
    /// Reads from json file.
    /// </summary>
    public void ReadFromFile()
    {
        try
        {
            string fileContent = File.ReadAllText(JSON_FILES_PATH + name + @".json");
            ContactList = JsonConvert.DeserializeObject<Dictionary<string, Contact>>(fileContent);
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine($"Failed to Read file! {name}.json File could not be found");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Read from File failed!");
            Console.WriteLine("Exception Error Message: ");
            Console.WriteLine(ex.Message);
        }
    }
}