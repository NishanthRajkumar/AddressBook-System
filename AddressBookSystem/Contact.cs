namespace AddressBookSystem;

/// <summary>
/// This class handles each individual contact
/// </summary>
internal class Contact
{
    // Declared attributes required for a person's contact
    private string firstName = "";
    private string lastName = "";
    private string email = "";
    private string phone = "";
    private string city = "";
    private string state = "";
    private string zip = "";
    private string address = "";

    /// <summary>
    /// Initializes a new instance of the <see cref="Contact"/> class.
    /// <list>Gets Input from user to intialize the values</list>
    /// <list>First Name cannot be empty</list>
    /// </summary>
    public Contact()
    {
        GetContactInfo();
    }

    /// <summary>
    /// Gets the contact information from user.
    /// </summary>
    private void GetContactInfo()
    {
        firstName = UserInput.GetName("First name: ");
        Console.Write("Last Name: ");
        lastName = UserInput.ReadString();
        Console.Write("Email: ");
        email = UserInput.ReadString();
        phone = UserInput.GetNumber("Phone: ");
        Console.Write("City: ");
        city = UserInput.ReadString();
        Console.Write("State: ");
        state = UserInput.ReadString();
        zip = UserInput.GetZip("Zip: ");
        Console.Write("Address: ");
        address = UserInput.ReadString();
    }

    /// <summary>
    /// Displays all contents of the contact
    /// </summary>
    public void Display()
    {
        Console.WriteLine("\nName: " + firstName + " " + lastName);
        Console.WriteLine("Email: " + email);
        Console.WriteLine("Phone: " + phone);
        Console.WriteLine("City: " + city);
        Console.WriteLine("State: " + state);
        Console.WriteLine("Zip: " + zip);
        Console.WriteLine("Address: " + address);
    }

    /// <summary>
    /// Gets the full name of the contact.
    /// </summary>
    /// <returns>The Full Name</returns>
    public string GetName()
    {
        if (String.IsNullOrEmpty(lastName) is true)
            return firstName;
        return firstName + " " + lastName;
    }

    public override bool Equals(object obj)
    {
        if (obj is not Contact)
            return false;
        else if (GetName() == ((Contact)obj).GetName())
            return true;
        return false;
    }
}